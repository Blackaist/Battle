using System;
using UnityEngine;

namespace Project
{
	[Serializable]
	public struct SettingsData
	{
		public int startingNumber;
		public string defaultLocale;

		public int StartingNumber { get => startingNumber; set => startingNumber = value; }
		public readonly string Locale => defaultLocale;
	}

	public sealed class GameSettings : SingletonScene<GameSettings>
	{
		private const string _settingsName = "Settings.json";
		private const string _saveName = "Save.dat";


		private SettingsData _settings = default;
		public SettingsData Settings => _settings;


		private Localization _localization = null;
		public Localization Localization => _localization;


		private JSONLoader<SettingsData> _settingsLoader = null;
		private JSONLoader<SettingsData> _saveLoader = null;


		public event Action SettingsUpdated = null;
		public event Action LocalizationUpdated = null;


		private void Start()
		{
			var streamingAssetsPath = Application.streamingAssetsPath;
			_settingsLoader = new(streamingAssetsPath, _settingsName);
			_localization = new(streamingAssetsPath);

			_saveLoader = new(Application.persistentDataPath, _saveName);

			if (_saveLoader.IsExist())
			{
				_saveLoader.TryLoad(out var newSettings);
				UpdateSettings(newSettings);
			}
			else
			{
				LoadSettings();
			}
		}

		public void LoadSettings()
		{
			var isSuccess = _settingsLoader.TryLoad(out var newSettings);

			if (isSuccess)
			{
				UpdateSettings(newSettings);
			}
		}

		private void UpdateSettings(SettingsData newSettings)
		{
			if (!_settings.Equals(newSettings))
			{
				//if (_settings.defaultLocale != newSettings.defaultLocale)
				{
					_localization.SetLocale(newSettings.Locale);
					UpdateLocalization();
				}

				_settings = newSettings;

				SettingsUpdated?.Invoke();
			}
		}

		private void UpdateLocalization()
		{
			_localization.Load();

			LocalizationUpdated?.Invoke();
		}

		public void IncrementStartingValue()
		{
			_settings.StartingNumber++;

			_saveLoader.TrySave(_settings);
		}
	}
}
