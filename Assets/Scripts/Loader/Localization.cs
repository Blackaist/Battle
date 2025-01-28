using System;
using System.Collections.Generic;

namespace Project
{
	[Serializable]
	public struct KeyValueItem
	{
		public string Key;
		public string Value;

		public KeyValueItem(string key, string value)
		{
			Key = key;
			Value = value;
		}
	}

	[Serializable]
	public struct LocalizedItems
	{
		public List<KeyValueItem> locales;
	}

	public sealed class Localization 
	{
		private const string _settingsName = "Locale_{0}.json";

		private JSONLoader<LocalizedItems> _loader = null;

		private LocalizedItems items = new();

		private string _path = "";
		private string _locale = "";

		public Localization(string path)
		{
			_path = path;
		}

		public void SetLocale(string locale) => _locale = locale;

		public void Load()
		{
			var settingsLocaleName = string.Format(_settingsName, _locale);

			_loader = new(_path, settingsLocaleName);

			var jsonData = _loader.TryLoad(out items);
		}

		public string Get(string key)
		{
			foreach (var item in items.locales)
			{
				if (item.Key == key)
				{
					return item.Value;
				}
			}

			return key;
		}
	}

}
