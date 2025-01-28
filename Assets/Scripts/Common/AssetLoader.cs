using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Project
{
	public class AssetLoader
	{
		private List<AssetBundle> _bundles = new();

		//public Action OnCompleted = null;

		public void Start<T>(List<T> entities, string path) where T : ScriptableObject
		{
			var assetPath = Path.Combine(Application.dataPath, path);

			var newBundle = AssetBundle.LoadFromFile(assetPath);
			_bundles.Add(newBundle);

			var handle = newBundle.LoadAllAssets<T>();

			foreach (var asset in handle)
			{
				entities.Add(asset);
			}

			//OnCompleted?.Invoke();
		}

		public void UnloadAll()
		{
			foreach(var bundle in _bundles)
			{
				bundle.Unload(true);
			}

			_bundles.Clear();
		}
	}
}