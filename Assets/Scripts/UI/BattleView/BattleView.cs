using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Project.Ability;

namespace Project
{
    public class BattleView : MonoBehaviour
    {
        [SerializeField] private GameObject _abilityButtonPrefab = null;
        [SerializeField] private Transform _abilityParent = null;
        [SerializeField] private GameObject _gameOver = null;
        [SerializeField] private Button _restartButton = null;

        private Dictionary<string, AbilityButton> _buttons = new();

        public Action<string> OnAbilityPressed = null;
        public Action OnRestartPressed = null;

		public void Awake()
		{
            _restartButton.onClick.AddListener(OnRestartClicked);
		}

		private void OnDestroy()
		{
			_restartButton.onClick.RemoveListener(OnRestartClicked);
			_buttons.Clear();
		}

        public void OnRestartClicked() => OnRestartPressed?.Invoke();

		public void Init(IReadOnlyCollection<IAbility> datas)
        {
			DestroyButtons();

			foreach (var data in datas)
            {
                var button = Instantiate(_abilityButtonPrefab, _abilityParent).GetComponent<AbilityButton>();
                button.Init(data.AbilityData);
                button.SetCooldown(data.Cooldown);

                button.OnAbilityClicked += OnAbilityClicked;

				_buttons.Add(data.AbilityData.Name, button);
			}

			GameStart();
		}

		public void GameStart()
        {
			_gameOver.SetActive(false);
		}

        public void GameOver()
        {
            _gameOver.SetActive(true);
            UpdateStateButtons(false);
		}

        public void UpdateCooldown(string abilityName, int cooldownCounter = 0)
        {
            if (_buttons != null)
            {
                var isSuccess = _buttons.TryGetValue(abilityName, out var button);

                if (isSuccess)
                {
                    button.SetCooldown(cooldownCounter);
                }
            }
        }

        public void UpdateStateButtons(bool enable)
        {
            foreach (var button in _buttons)
            {
                button.Value.SetEnable(enable);
            }
        }

		private void OnAbilityClicked(string abilityName)
        {
            OnAbilityPressed?.Invoke(abilityName);
		}

        private void DestroyButtons()
        {
			if (_buttons.Count > 0)
			{
				foreach (var button in _buttons.Values)
				{
                    button.OnAbilityClicked -= OnAbilityClicked;

					Destroy(button.gameObject);
				}

				_buttons.Clear();
			}
		}

    }
}