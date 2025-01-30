using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
	public class CardView : MonoBehaviour
	{
		[SerializeField] private Image _heroImage = null;
		[SerializeField] private Text _heroName = null;
		[SerializeField] private UIStats _uiStats = null;

		[SerializeField] private Button _button = null;

		public EntityObject CurrentEntity { get; private set; }

		public Action<EntityObject> OnClicked = null;

		private void OnEnable()
		{
			_button.onClick.AddListener(OnButtonClicked);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnButtonClicked);
		}

		public void Setup(EntityObject entity)
		{
			_heroImage.color = entity.Color;
			_heroImage.sprite = entity.Model;

			_heroName.text = entity.Name;

			_uiStats.Setup(entity.Stats);

			CurrentEntity = entity;
		}

		private void OnButtonClicked()
		{
			OnClicked?.Invoke(CurrentEntity);
		}
	}

}
