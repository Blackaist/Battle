using System.IO;
using System;
using UnityEngine;

public class JSONLoader<T>
{
	private string _dataDirPath = "";
	private string _dataFileName = "";

	public JSONLoader(string dataDirPath, string dataFileName)
	{
		_dataDirPath = dataDirPath;
		_dataFileName = dataFileName;
	}

	public bool TryLoad(out T data)
	{
		var jsonData = LoadJson();
		if (jsonData != null && jsonData != "")
		{
			data = JsonUtility.FromJson<T>(jsonData);
			return true;
		}

		data = default;

		return false;
	}

	public string LoadJson()
	{
		string fullPath = Path.Combine(_dataDirPath, _dataFileName);

		if (File.Exists(fullPath))
		{
			try
			{
				var jsonData = "";
				using (FileStream stream = new FileStream(fullPath, FileMode.Open))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						jsonData = reader.ReadToEnd();
					}
				}
				return jsonData;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
				return "";
			}
		}

		return "";
	}

	public bool TrySave(T data)
	{
		var fullPath = Path.Combine(_dataDirPath, _dataFileName);

		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
			var jsonData = JsonUtility.ToJson(data, true);

			using (FileStream stream = new FileStream(fullPath, FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter(stream))
				{
					writer.Write(jsonData);
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogError(e);
			return false;
		}

		return true;
	}

	public bool IsExist()
	{
		string fullPath = Path.Combine(_dataDirPath, _dataFileName);

		return File.Exists(fullPath);
	}
}
