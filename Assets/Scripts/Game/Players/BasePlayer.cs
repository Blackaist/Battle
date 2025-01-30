using Project.Ability;
using Project.Effect;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
	public abstract class BasePlayer : MonoBehaviour, IPlayer
	{
		[SerializeField] private SpriteRenderer _playerModel = null;

		protected List<IAbility> _abilities = new();
		protected List<IEffect> _effects = new();

		protected EntityObject _entityData = null;

		public event Action<IAbility> AbilityExecuted = null;
		public event Action<IEffect> EffectAdded = null;

		public event Action HPUpdated = null;
		public event Action ShieldUpdated = null;

		public event Action TurnStarted = null;
		public event Action TurnEnded = null;

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

					HPUpdated?.Invoke();
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
					ShieldUpdated?.Invoke();
				}
			}
		}

		public EntityStats GetStats => _entityData.Stats;

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

			TurnStarted?.Invoke();
		}

		public virtual void EndTurn()
		{
			foreach (var effect in _effects)
			{
				effect.OnEndTurn();
			}

			RemoveAllExpiredEffects();

			TurnEnded?.Invoke();
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
			CurrentHP = _entityData.Stats.HP;
			CurrentShield = 0;

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
				EffectAdded?.Invoke(effect);
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

		protected void OnAbilityExecuted(IAbility param)
		{
			AbilityExecuted?.Invoke(param);
		}
	}
}
