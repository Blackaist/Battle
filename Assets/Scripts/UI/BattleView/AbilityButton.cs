using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class AbilityButton : MonoBehaviour
	{
		[SerializeField] private Image _image = null;
		[SerializeField] private Button _button = null;
		[SerializeField] private Text _name = null;
		[SerializeField] private Text _cooldown = null;

		public Action<string> OnAbilityClicked = null;

		private string _abilityName = "";

		private int _cooldownTimer = 0;

		public void Awake()
		{
			_button.onClick.AddListener(OnButtonClicked);
		}

		public void OnDestroy()
		{
			_button.onClick.RemoveListener(OnButtonClicked);
		}

		public void Init(AbilityData data)
		{
			_image.color = data.Color;
			_name.text = _abilityName = data.Name;
		}

		public void SetCooldown(int cooldownCounter)
		{
			_cooldownTimer = cooldownCounter;
			var onCooldown = cooldownCounter > 0;

			_cooldown.gameObject.SetActive(onCooldown);
			_cooldown.text = cooldownCounter.ToString();
		}

		public void SetEnable(bool enable)
		{
			if ((_cooldownTimer == 0 && enable) || !enable)
			{
				_button.interactable = enable;
			}
		}

		private void OnButtonClicked()
		{
			OnAbilityClicked?.Invoke(_abilityName);
		}
	}
}
