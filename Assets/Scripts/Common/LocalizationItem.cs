using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	[RequireComponent(typeof(Text))]
	public class LocalizationItem : MonoBehaviour
	{
		[SerializeField] private string _key = "";

		private Text _textComponent = null;

		private void Awake()
		{
			_textComponent = GetComponent<Text>();

			UpdateText();
		}

		private void OnEnable()
		{
			if (GameSettings.Instance != null)
			{
				GameSettings.Instance.OnLocalizationUpdated += UpdateText;
			}
		}

		private void OnDisable()
		{
			if (gameObject != null && GameSettings.Instance != null)
			{
				GameSettings.Instance.OnLocalizationUpdated -= UpdateText;
			}
		}

		private void UpdateText()
		{
			_textComponent.text = GameSettings.Instance.Localization.Get(_key);
		}
	}

}
