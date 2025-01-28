using Project.Ability;
using Project.Effect;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

namespace Project.Player
{
	public abstract class BasePlayer : MonoBehaviour, IPlayer
	{
		[SerializeField] private SpriteRenderer _playerModel = null;
		[SerializeField] private Slider _playerBarSlider = null;
		[SerializeField] private Text _hpText = null;
		[SerializeField] private Text _shieldText = null;

		protected List<IAbility> _abilities = new();
		protected List<IEffect> _effects = new();

		protected EntityObject _entityData = null;

		private int _currentHP = 0;
		public int CurrentHP
		{
			get => _currentHP;
			private set
			{
				if (_currentHP != value)
				{
					if (value <= 0)
					{
						_currentHP = 0;
					}
					else
					{
						_currentHP = Mathf.Min(value, _entityData.Stats.HP);
					}

					RedrawHPBar();
				}
			}
		}

		private int _currentShield = 0;
		public int CurrentShield
		{
			get => _currentShield;
			private set
			{
				if (_currentShield != value)
				{
					_currentShield = Mathf.Max(value, 0);
					RedrawShieldBar();
				}
			}
		}

		public EntityStats GetStats => _entityData.Stats;

		public Action<IAbility> OnAbilityExecuted { get; set; }
		public Action<IEffect> OnEffectAdded { get; set; }
		public Action OnTurnStarted { get; set; }
		public Action OnTurnEnded { get; set; }

		public abstract void ExecuteAbility(string abilityName);

		public virtual void StartTurn()
		{
			foreach (var ability in _abilities)
			{
				ability.DecreaseCooldown();
			}

			foreach (var effect in _effects)
			{
				effect.OnStartTurn();
			}

			RemoveAllExpiredEffects();

			OnTurnStarted?.Invoke();
		}

		public virtual void EndTurn()
		{
			foreach (var effect in _effects)
			{
				effect.OnEndTurn();
			}

			RemoveAllExpiredEffects();

			OnTurnEnded?.Invoke();
		}

		public void Init(List<IAbility> abilities, EntityObject entity)
		{
			_abilities = abilities;
			_entityData = entity;

			_playerModel.sprite = entity.Model;
			_playerModel.color = entity.Color;

			Restart();
		}

		public void UpdateHP(int count, bool ignoreShield = false)
		{
			if (!ignoreShield && _currentShield > 0 && count < 0)
			{
				UpdateShield(count);
			}
			else
			{
				CurrentHP += count;
			}
		}

		public void UpdateShield(int count)
		{
			if (_currentShield + count < 0)
			{
				var diff = _currentShield + count;
				CurrentShield = 0;

				UpdateHP(diff);
			}
			else
			{
				CurrentShield += count;
			}
		}

		public void Restart()
		{
			_currentHP = _entityData.Stats.HP;

			RedrawHPBar();
			RedrawShieldBar();

			foreach (var ability in _abilities)
			{
				ability.Reset();
			}

			RemoveAllEffects();
		}

		public List<IEffect> GetEffects() => _effects;

		public void AddEffect(IEffect effect)
		{
			var prevEffect = _effects.Find(x => x.EffectData.Name == effect.EffectData.Name);
			if (prevEffect != null)
			{
				prevEffect.RestartDurability();
			}
			else
			{
				_effects.Add(effect);
				OnEffectAdded?.Invoke(effect);
			}
		}

		public void RemoveEffect(IEffect effect)
		{
			_effects.Remove(effect);
		}

		private void RemoveAllExpiredEffects()
		{
			_effects.RemoveAll(x => x.Durability == 0);
		}

		private void RemoveAllEffects()
		{
			_effects.ForEach(x => x.DestroyEffect());
			_effects.Clear();
		}

		private void RedrawHPBar()
		{
			var maxHP = _entityData.Stats.HP;
			_playerBarSlider.value = (float)CurrentHP / maxHP;

			_hpText.text = string.Format("{0}/{1}", _currentHP, maxHP);
		}

		private void RedrawShieldBar()
		{
			if (_currentShield > 0)
			{
				_shieldText.text =  string.Format("Shield: {0}", _currentShield.ToString());
			}
			else
			{
				_shieldText.text = "";
			}
		}
	}
}
