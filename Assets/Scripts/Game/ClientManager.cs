using Project.Ability;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class ClientManager : MonoBehaviour
	{
		public static ClientManager Instance { get; private set; } = null;

		private ClientServerAdapter _adapter = null;
		private Coroutine _coroutine = null;


		public Action Loaded = null;
		public Action<EntityObject, EntityObject> SetEntity = null;
		public Action<string> AbilitySelected = null;
		public Action<bool> NextTurn = null;

		public IReadOnlyCollection<EntityObject> Entities => _adapter.Entities();
		public IReadOnlyCollection<AbilityData> Abilities => _adapter.Abilities();
		public IReadOnlyCollection<EffectData> Effects => _adapter.Effects();


		private void Awake()
		{
			if (Instance == null)
			{
				_adapter = new ClientServerAdapter(this);

				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(this);
			}
		}

		public void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		private void Start()
		{
			UpdateAssets();
		}

		public void UpdateAssets()
		{
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}

			_coroutine = StartCoroutine(LoadingAssets());
		}

		private IEnumerator LoadingAssets()
		{
			_adapter.LoadAssets();

			while (!_adapter.IsServerLoaded)
			{
				yield return null;
			}

			Loaded?.Invoke();
		}

		public void SelectEntity(EntityObject entity)
		{
			_adapter.OnClientSelectedEntity(entity);
		}

		public void OnSetEntity(EntityObject leftEntity, EntityObject rightEntity) => SetEntity?.Invoke(leftEntity, rightEntity);

		public void OnClientAbilitySelected(string ability) => _adapter.OnServerAbilitySelect(ability);
		public void OnServerAbilitySelected(string ability) => AbilitySelected?.Invoke(ability);

		public void OnClientNextTurn(bool isLeftTurn) => _adapter.OnClientNextTurn(isLeftTurn);

		public void OnServerNextTurn(bool isLeftTurn) => NextTurn?.Invoke(isLeftTurn);
	}
}
