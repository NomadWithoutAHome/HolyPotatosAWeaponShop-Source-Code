using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonFileController : MonoBehaviour
{
	public bool checkContent(string aPath)
	{
		bool result = false;
		aPath = Application.persistentDataPath + "/" + aPath;
		CommonAPI.debug("checkContent " + aPath);
		try
		{
			FileInfo fileInfo = new FileInfo(aPath);
			if (fileInfo != null)
			{
				if (fileInfo.Exists)
				{
					using (FileStream fileStream = new FileStream(aPath, FileMode.Open, FileAccess.Read))
					{
						if (fileStream.Length > 0)
						{
							result = true;
						}
						fileStream.Close();
						return result;
					}
				}
				return result;
			}
			return result;
		}
		catch (IOException ex)
		{
			CommonAPI.debug("check content: " + ex.ToString());
			return result;
		}
	}

	public void checkMyDocDirectory()
	{
		if (!Directory.Exists(CommonAPI.getSystemFolderPath()))
		{
			Directory.CreateDirectory(CommonAPI.getSystemFolderPath());
		}
	}

	public bool checkMyDocContent(string aPath)
	{
		bool result = false;
		checkMyDocDirectory();
		CommonAPI.debug("check My Document content: " + CommonAPI.getSystemFolderPath() + aPath);
		if (File.Exists(CommonAPI.getSystemFolderPath() + aPath))
		{
			aPath = CommonAPI.getSystemFolderPath() + aPath;
			try
			{
				FileInfo fileInfo = new FileInfo(aPath);
				if (fileInfo != null)
				{
					if (fileInfo.Exists)
					{
						using (FileStream fileStream = new FileStream(aPath, FileMode.Open, FileAccess.Read))
						{
							if (fileStream.Length > 0)
							{
								result = true;
							}
							fileStream.Close();
							return result;
						}
					}
					return result;
				}
				return result;
			}
			catch (IOException ex)
			{
				CommonAPI.debug("check My Document content: " + ex.ToString());
				return result;
			}
		}
		return result;
	}

	public string readContent(string aPath)
	{
		string text = string.Empty;
		string empty = string.Empty;
		empty = ((!checkMyDocContent(aPath)) ? (Application.persistentDataPath + "/" + aPath) : (CommonAPI.getSystemFolderPath() + aPath));
		try
		{
			using FileStream fileStream = new FileStream(empty, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			text = binaryReader.ReadString();
			fileStream.Close();
		}
		catch (IOException ex)
		{
			CommonAPI.debug("read content: " + ex.ToString());
		}
		if (!checkMyDocContent(aPath))
		{
			CommonAPI.debug("copying same file to mydoc");
			saveContent(aPath, text);
		}
		return text;
	}

	public void saveContent(string aPath, string text)
	{
		checkMyDocDirectory();
		aPath = CommonAPI.getSystemFolderPath() + aPath;
		try
		{
			using FileStream fileStream = new FileStream(aPath, FileMode.Create, FileAccess.Write);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(text);
			fileStream.Close();
		}
		catch (IOException ex)
		{
			CommonAPI.debug("save content: " + ex.ToString());
		}
	}

	public void saveSaveFileDir(Dictionary<string, string> textList)
	{
		checkMyDocDirectory();
		string path = CommonAPI.getSystemFolderPath() + "WSDir.txt";
		string text = string.Empty;
		foreach (KeyValuePair<string, string> text3 in textList)
		{
			string text2 = text;
			text = text2 + text3.Key + "=" + text3.Value + ";";
		}
		text = SC.FromString(text);
		try
		{
			using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(text);
			fileStream.Close();
		}
		catch (IOException ex)
		{
			CommonAPI.debug("save Dir content: " + ex.ToString());
		}
	}

	public Dictionary<string, string> loadSaveFileDir(Game game)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string text = string.Empty;
		checkMyDocDirectory();
		string path = CommonAPI.getSystemFolderPath() + "WSDir.txt";
		if (File.Exists(path))
		{
			try
			{
				using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				text = binaryReader.ReadString();
				fileStream.Close();
			}
			catch (IOException ex)
			{
				CommonAPI.debug("read Dir Content: " + ex.ToString());
			}
			text = SC.ToString(text);
			if (text != string.Empty)
			{
				string[] array = text.TrimEnd(';').Split(';');
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					string[] array3 = text2.Split('=');
					dictionary.Add(array3[0], array3[1]);
				}
			}
			else
			{
				dictionary = loadFromPlayerPref(game);
			}
		}
		else
		{
			dictionary = loadFromPlayerPref(game);
		}
		return dictionary;
	}

	public Dictionary<string, string> loadFromPlayerPref(Game game)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("autosaveLoad", SC.ToString(PlayerPrefs.GetString("autosaveLoad", string.Empty)));
		for (int i = 1; i <= 5; i++)
		{
			dictionary.Add("save" + i + "Load", SC.ToString(PlayerPrefs.GetString("save" + i + "Load", string.Empty)));
		}
		List<GameScenario> otherGameScenarioNoCheck = game.getGameData().getOtherGameScenarioNoCheck();
		string empty = string.Empty;
		foreach (GameScenario item in otherGameScenarioNoCheck)
		{
			empty = item.getGameScenarioRefId();
			dictionary.Add("autosave" + empty + "Load", SC.ToString(PlayerPrefs.GetString("autosave" + empty + "Load", string.Empty)));
			for (int j = 1; j <= 5; j++)
			{
				dictionary.Add("save" + j + empty + "Load", SC.ToString(PlayerPrefs.GetString("save" + j + empty + "Load", string.Empty)));
			}
		}
		saveSaveFileDir(dictionary);
		return dictionary;
	}

	public string readRefContent(string aPath)
	{
		string result = string.Empty;
		aPath = Application.dataPath + "/" + aPath;
		CommonAPI.debug("readRefContent " + aPath);
		CommonAPI.debug("readContent dataPath: " + Application.dataPath);
		try
		{
			using FileStream fileStream = new FileStream(aPath, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			result = binaryReader.ReadString();
			fileStream.Close();
			return result;
		}
		catch (IOException ex)
		{
			CommonAPI.debug("read content: " + ex.ToString());
			return result;
		}
	}

	public void saveRefContent(string aPath, string text)
	{
		aPath = Application.dataPath + "/" + aPath;
		CommonAPI.debug("saveRefContent " + aPath);
		try
		{
			using FileStream fileStream = new FileStream(aPath, FileMode.Create, FileAccess.Write);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(text);
			fileStream.Close();
		}
		catch (IOException ex)
		{
			CommonAPI.debug("save content error: " + ex.ToString());
		}
	}

	public void saveContentPlayerPrefs(string key, string text)
	{
		CommonAPI.debug(key + " (" + text.Length + ") save to pp: " + text);
		PlayerPrefs.SetString(key, text);
		PlayerPrefs.Save();
	}

	public void removeContentFromPlayerPrefs(string key)
	{
		CommonAPI.debug(key + " remove from pp");
		PlayerPrefs.DeleteKey(key);
		PlayerPrefs.Save();
	}

	public string readContentPlayerPrefs(string key)
	{
		return PlayerPrefs.GetString(key, string.Empty);
	}
}
