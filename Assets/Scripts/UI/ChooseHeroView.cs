using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Project.UI
{
	public class ChooseHeroView : MonoBehaviour
	{
		[SerializeField] private GameObject _cardViewPrefab = null;
		[SerializeField] private Transform _cardTransform = null;
		[SerializeField] private Button _updateContent = null;

		[Header("Check for serialization")]
		[SerializeField] private Text _counterText = null;
		[SerializeField] private Button _counterButton = null;

		private List<CardView> _cards = new();

		public void Awake()
		{
			if (ClientManager.Instance)
			{
				Build();
				UpdateText();
			}
		}

		public void OnEnable()
		{
			_updateContent.onClick.AddListener(OnUpdateContentPressed);
			_counterButton.onClick.AddListener(OnCounterButtonClicked);

			if (ClientManager.Instance)
			{
				ClientManager.Instance.OnLoaded += OnAssetsUpdated;
			}
		}

		public void OnDisable()
		{
			_updateContent.onClick.RemoveListener(OnUpdateContentPressed);
			_counterButton.onClick.RemoveListener(OnCounterButtonClicked);

			if (ClientManager.Instance)
			{
				ClientManager.Instance.OnLoaded -= OnAssetsUpdated;
			}

		}

		public void OnUpdateContentPressed()
		{
			ClientManager.Instance.UpdateAssets();

			GameSettings.Instance.LoadSettings();
			UpdateText();
		}

		private void OnCounterButtonClicked()
		{
			GameSettings.Instance.IncrementStartingValue();
			UpdateText();
		}

		private void OnAssetsUpdated()
		{
			ClearCardView();
			Build();
		}

		private void UpdateText()
		{
			_counterText.text = GameSettings.Instance.Settings.StartingNumber.ToString();
		}

		public void Build()
		{
			foreach (var entity in ClientManager.Instance.Entities)
			{
				AddCard(entity);
			}
		}

		public void AddCard(EntityObject entity)
		{
			if (_cards != null)
			{
				if (_cards.All(x => x.CurrentEntity.Name != entity.Name))
				{
					var cardView = Instantiate(_cardViewPrefab, _cardTransform).GetComponent<CardView>();
					cardView.Setup(entity);

					cardView.OnClicked += OnCardViewClicked;

					_cards.Add(cardView);
				}
			}
		}

		private void OnCardViewClicked(EntityObject entity)
		{
			ClientManager.Instance.SelectEntity(entity);

			Dispose();
		}

		private void Dispose()
		{
			ClearCardView();
			gameObject.SetActive(false);
		}

		private void ClearCardView()
		{
			foreach (var cardView in _cards)
			{
				cardView.OnClicked -= OnCardViewClicked;
				Destroy(cardView.gameObject);
			}

			_cards.Clear();
		}
	}

}
