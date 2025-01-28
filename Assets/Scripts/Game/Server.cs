using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
	public partial class Server
	{
		private AssetLoader _loader = null;
		private List<EntityObject> _downloadedEntities = new();
		private List<AbilityData> _downloadedAbilities = new();
		private List<EffectData> _downloadedEffects = new();
		private ClientServerAdapter _adapter = null;

		public bool IsCompleted { get; private set; } = false;
		public IReadOnlyCollection<EntityObject> Entities => _downloadedEntities;
		public IReadOnlyCollection<AbilityData> Abilities => _downloadedAbilities;
		public IReadOnlyCollection<EffectData> Effects => _downloadedEffects;

		public Server(ClientServerAdapter adapter)
		{
			_adapter = adapter;
			_loader = new AssetLoader();
		}

		~Server() 
		{
			_loader.UnloadAll();
		}

		//only in main thread
		public void LoadAssets()
		{
			IsCompleted = false;

			_loader.UnloadAll();
			_downloadedEntities.Clear();
			_downloadedAbilities.Clear();
			_downloadedEffects.Clear();

			_loader.Start(_downloadedEntities, "AssetBundles/entity");
			_loader.Start(_downloadedEffects, "AssetBundles/effect");
			_loader.Start(_downloadedAbilities, "AssetBundles/ability");

			IsCompleted = true;
		}

		public void TrySelectEntity(EntityObject entity)
		{
			if (_downloadedEntities.Any(x => x.Equals(entity)))
			{
				var randomElement = (new System.Random()).Next(0, Entities.Count);

				_adapter.OnServerSetEntity(entity, Entities.ElementAt(randomElement));
			}
		}

		public void OnSelectAbility(string ability)
		{
			_adapter.OnServerAbilitySelect(ability);
		}

		public void OnNextTurn(bool isLeftTurn)
		{
			isLeftTurn = !isLeftTurn;
			_adapter.OnServerNextTurn(isLeftTurn);
		}
	}

}
