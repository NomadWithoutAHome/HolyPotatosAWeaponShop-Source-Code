using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUILanguageController : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private ViewController viewController;

	private Game game;

	private GameData gameData;

	private UILabel titleLabel;

	private BoxCollider closeButton;

	private UIButton confirmButton;

	private List<string> languageList;

	private GameObject panel_language;

	private UISprite languageFlag;

	private UIPopupList languageFilter;

	private UILabel languageLabel;

	private LanguageType selectedLanguage;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		titleLabel = commonScreenObject.findChild(base.gameObject, "Title_label").GetComponent<UILabel>();
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<BoxCollider>();
		confirmButton = commonScreenObject.findChild(base.gameObject, "ConfirmButton").GetComponent<UIButton>();
		panel_language = commonScreenObject.findChild(base.gameObject, "Panel_Language").gameObject;
		languageFlag = commonScreenObject.findChild(base.gameObject, "Panel_LanguageArrow/LanguageFlag").GetComponent<UISprite>();
		languageLabel = commonScreenObject.findChild(panel_language, "LanguageLabel").GetComponent<UILabel>();
		languageFilter = commonScreenObject.findChild(panel_language, "FilterButton").GetComponent<UIPopupList>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "ConfirmButton":
			GameObject.Find("Close_button").GetComponent<UIButton>().enabled = false;
			confirmButton.enabled = false;
			if (Constants.LANGUAGE != selectedLanguage)
			{
				CommonAPI.debug("new language: " + selectedLanguage);
				PlayerPrefs.SetString("LANGUAGE", selectedLanguage.ToString());
				Constants.LANGUAGE = selectedLanguage;
				GameObject.Find("Panel_LanguageButton").GetComponent<GUILanguageButtonController>().setReference();
				GameObject.Find("RefDataController").GetComponent<RefDataController>().getRefDataFromFile();
				FontScript[] array = Object.FindObjectsOfType(typeof(FontScript)) as FontScript[];
				FontScript[] array2 = array;
				foreach (FontScript fontScript in array2)
				{
					fontScript.replaceFonts();
				}
				FontSceneScript[] array3 = Object.FindObjectsOfType(typeof(FontSceneScript)) as FontSceneScript[];
				FontSceneScript[] array4 = array3;
				foreach (FontSceneScript fontSceneScript in array4)
				{
					fontSceneScript.replaceFonts();
				}
				if (GameObject.Find("Panel_ScenarioSelect") != null)
				{
					GameObject.Find("Panel_ScenarioSelect").GetComponent<GUIScenarioSelectController>().setReference(bool.Parse(PlayerPrefs.GetString("FirstTime", "TRUE")));
				}
				GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().setReference();
			}
			viewController.closeLanguageMenu();
			GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().enableButtons();
			break;
		case "Close_button":
			viewController.closeLanguageMenu();
			GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().enableButtons();
			break;
		}
	}

	private void Update()
	{
		handleInput();
	}

	private void handleInput()
	{
		if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007"))) && !GameObject.Find("blackmask_popup").GetComponent<BoxCollider>().enabled && GameObject.Find("Panel_SaveLoadPopup") == null && closeButton.enabled)
		{
			processClick("Close_button");
		}
	}

	public void setReference(bool start = false)
	{
		GameData gameData = game.getGameData();
		titleLabel.GetComponent<UILabel>().text = gameData.getTextByRefId("settings16");
		confirmButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("settings17");
		if (panel_language != null)
		{
			languageList = new List<string>();
			languageList.Add("ENGLISH");
			languageList.Add("FRENCH");
			languageList.Add("GERMAN");
			languageList.Add("SPANISH");
			languageList.Add("CHINESE");
			languageList.Add("JAPANESE");
			languageList.Add("RUSSIAN");
			string text = CommonAPI.convertLanguageTypeToLanguageString(Constants.LANGUAGE);
			languageLabel.text = gameData.getTextByRefId("settings15");
			languageFlag.spriteName = "flag_" + text.ToLower(CultureInfo.InvariantCulture);
			languageFilter.value = text;
			languageFilter.GetComponentInChildren<UILabel>().text = text;
			languageFilter.items = languageList;
		}
		selectedLanguage = Constants.LANGUAGE;
	}

	public void setNewLanguage()
	{
		switch (languageFilter.value)
		{
		case "ENGLISH":
			selectedLanguage = LanguageType.kLanguageTypeEnglish;
			break;
		case "CHINESE":
			selectedLanguage = LanguageType.kLanguageTypeChinese;
			break;
		case "FRENCH":
			selectedLanguage = LanguageType.kLanguageTypeFrench;
			break;
		case "JAPANESE":
			selectedLanguage = LanguageType.kLanguageTypeJap;
			break;
		case "ITALIAN":
			selectedLanguage = LanguageType.kLanguageTypeItalian;
			break;
		case "SPANISH":
			selectedLanguage = LanguageType.kLanguageTypeSpanish;
			break;
		case "GERMAN":
			selectedLanguage = LanguageType.kLanguageTypeGermany;
			break;
		case "RUSSIAN":
			selectedLanguage = LanguageType.kLanguageTypeRussia;
			break;
		}
		languageFlag.spriteName = "flag_" + languageFilter.value.ToLower(CultureInfo.InvariantCulture);
	}
}
