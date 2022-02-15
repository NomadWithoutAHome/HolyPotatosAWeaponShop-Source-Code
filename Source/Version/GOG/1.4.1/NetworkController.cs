using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
	private Game game;

	private ViewController viewController;

	private DynamicDataController dynamicDataController;

	private string BASEURL_GET_REF_DATA = "http://54.251.97.227/WeaponStory/Development/Scripts/GetJson.php";

	private string URL_GET_REF_DATA = "http://54.251.97.227/WeaponStory/Development/Scripts/GetJson.php";

	private string LANGUAGE;

	private bool isDownloading;

	private float elapsedTime;

	private string coroutineName;

	private WWW www;

	private WWW wwwManaRegen;

	private WWW wwwWebviewLinks;

	private WWW wwwNotification;

	private WWW wwwDailyLogin;

	private Dictionary<LanguageType, string> langList;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		coroutineName = string.Empty;
		if (Constants.LANGUAGE == LanguageType.kLanguageTypeJap)
		{
			URL_GET_REF_DATA += "?language=JAPANESE";
		}
		langList = new Dictionary<LanguageType, string>();
		langList.Add(LanguageType.kLanguageTypeEnglish, string.Empty);
		langList.Add(LanguageType.kLanguageTypeJap, "?language=JAPANESE");
		langList.Add(LanguageType.kLanguageTypeChinese, "?language=CHINESE");
		langList.Add(LanguageType.kLanguageTypeFrench, "?language=FRENCH");
		langList.Add(LanguageType.kLanguageTypeRussia, "?language=RUSSIAN");
		langList.Add(LanguageType.kLanguageTypeGermany, "?language=GERMAN");
		langList.Add(LanguageType.kLanguageTypeSpanish, "?language=SPANISH");
	}

	private void Start()
	{
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		dynamicDataController = GameObject.Find("DynamicDataController").GetComponent<DynamicDataController>();
		elapsedTime = 0f;
	}

	public void getRefData()
	{
		StartCoroutine(networkGetRefDataAllLang());
	}

	private IEnumerator networkGetRefData()
	{
		www = new WWW(URL_GET_REF_DATA);
		isDownloading = true;
		GUILoadingController loadingController = GameObject.Find("GUILoadingController").GetComponent<GUILoadingController>();
		loadingController.startProgess();
		loadingController.updateMaxProgress(0.5f);
		while (!www.isDone)
		{
			yield return null;
		}
		loadingController.updateMaxProgress(0.75f);
		isDownloading = false;
		elapsedTime = 0f;
		CommonAPI.debug("networkGetRefData: " + www.text);
		if (www.error != null)
		{
			CommonAPI.debug("www.error: " + www.error);
			yield break;
		}
		PlayerPrefs.SetString("lastRefUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		ServerResponse aResponse = JsonMapper.ToObject<ServerResponse>(www.text);
		GameObject.Find("RefDataController").GetComponent<RefDataController>().processRefData(aResponse);
		loadingController.updateMaxProgress(1f);
	}

	private IEnumerator networkGetRefDataAllLang()
	{
		isDownloading = true;
		GUILoadingController loadingController = GameObject.Find("GUILoadingController").GetComponent<GUILoadingController>();
		loadingController.startProgess();
		loadingController.updateMaxProgress(0.5f);
		foreach (KeyValuePair<LanguageType, string> aPair in langList)
		{
			www = new WWW(URL_GET_REF_DATA + aPair.Value);
			while (!www.isDone)
			{
				yield return null;
			}
			loadingController.updateMaxProgress(0.75f);
			isDownloading = false;
			elapsedTime = 0f;
			if (www.error != null)
			{
				CommonAPI.debug("www.error: " + www.error);
				continue;
			}
			PlayerPrefs.SetString("lastRefUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			CommonAPI.debug(string.Concat("language: ", aPair.Key, " www.text: ", www.text));
			yield return StartCoroutine(loadRefData(www.text, aPair.Key));
		}
		GameObject.Find("RefDataController").GetComponent<RefDataController>().getRefDataFromFile();
		loadingController.updateMaxProgress(1f);
	}

	private IEnumerator loadRefData(string aText, LanguageType aType)
	{
		ServerResponse aResponse = JsonMapper.ToObject<ServerResponse>(www.text);
		GameObject.Find("RefDataController").GetComponent<RefDataController>().processRefData(aResponse, aType);
		yield return new WaitForSeconds(5f);
	}
}
