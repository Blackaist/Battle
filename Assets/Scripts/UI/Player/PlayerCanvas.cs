using Project.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(BasePlayer))]
    public class PlayerCanvas : MonoBehaviour
    {
		[SerializeField] private Slider _playerBarSlider = null;
		[SerializeField] private Text _hpText = null;
		[SerializeField] private Text _shieldText = null;

		private IPlayer _player = null;

		private void Awake()
		{
			_player = GetComponent<IPlayer>();
		}

		private void OnEnable()
		{
			_player.HPUpdated += RedrawHPBar;
			_player.ShieldUpdated += RedrawShieldBar;

			RedrawHPBar();
			RedrawShieldBar();
		}

		private void OnDisable()
		{
			_player.HPUpdated -= RedrawHPBar;
			_player.ShieldUpdated -= RedrawShieldBar;
		}

		private void RedrawHPBar()
		{
			var maxHP = _player.GetStats.HP;
			var currentHP = _player.CurrentHP;

			_playerBarSlider.value = (float)currentHP / maxHP;

			_hpText.text = string.Format("{0}/{1}", currentHP, maxHP);
		}

		private void RedrawShieldBar()
		{
			if (_player.CurrentShield > 0)
			{
				_shieldText.text = string.Format("Shield: {0}", _player.CurrentShield.ToString());
			}
			else
			{
				_shieldText.text = "";
			}
		}
	}
}