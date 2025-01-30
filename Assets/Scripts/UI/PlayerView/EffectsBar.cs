using Project.Effect;
using Project.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.UI
{
	[RequireComponent(typeof(BasePlayer))]
	public class EffectsBar : MonoBehaviour
	{
		[SerializeField] private GameObject _effectPrefab = null;
		[SerializeField] private Transform _effectsLayout = null;

		private BasePlayer _player = null;

		private Dictionary<string, EffectViewItem> _items = new();

		private void Awake()
		{
			_player = GetComponent<BasePlayer>();
		}

		private void OnEnable()
		{
			ClearAll();

			_player.TurnStarted += UpdateEffects;
			_player.TurnEnded += UpdateEffects;
			_player.EffectAdded += AddEffect;
		}

		private void OnDisable()
		{
			_player.TurnStarted -= UpdateEffects;
			_player.TurnEnded -= UpdateEffects;
			_player.EffectAdded -= AddEffect;
		}

		private void UpdateEffects()
		{
			var currentEffects = _player.GetEffects();

			for (var i = _items.Count - 1; i >= 0; i--)
			{
				(var key, var value) = _items.ElementAt(i);

				var currentEffect = currentEffects.FirstOrDefault(x => x.EffectData.Name == key) ?? null;
				if (currentEffect != null)
				{
					value.SetDurability(currentEffect.Durability);
				}
				else
				{
					Destroy(value.gameObject);
					_items.Remove(key);
				}
			}
		}

		private void AddEffect(IEffect effect)
		{
			var name = effect.EffectData.Name;
			var item = CreateItem(effect.EffectData.Color, effect.Durability).SetName(name);
			_items.Add(name, item);
		}

		private EffectViewItem CreateItem(Color color, int durability)
		{
			return Instantiate(_effectPrefab, _effectsLayout).GetComponent<EffectViewItem>()
				.SetColor(color)
				.SetDurability(durability);
		}

		private void ClearAll()
		{
			foreach(var item in _items.Values)
			{
				Destroy(item.gameObject);
			}

			_items.Clear();
		}
	}
}
