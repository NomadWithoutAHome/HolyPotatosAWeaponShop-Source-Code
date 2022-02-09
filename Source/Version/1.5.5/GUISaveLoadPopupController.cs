using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISaveLoadPopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private JsonFileController jsonFileController;

	private UILabel title_label;

	private UIButton button_back;

	private UIButton button_yes;

	private UIGrid slotGrid;

	private Dictionary<string, GameObject> saveLoadSlotList;

	private int selectedIndex;

	private bool saveFile;

	private GameObject panel_scenario;

	private GameObject panel_scenarioIcon;

	private UILabel selectScenLabel;

	private UIPopupList scenarioButton;

	private List<GameScenario> gameScenList;

	private bool start;

	private string scenarioRefID;

	private string suffix;

	private Dictionary<string, string> dirList;

	private string loadSuccess;

	private bool isAutoSave;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		jsonFileController = GameObject.Find("JsonFileController").GetComponent<JsonFileController>();
		title_label = commonScreenObject.findChild(base.gameObject, "Title_bg/Title_label").GetComponent<UILabel>();
		button_back = commonScreenObject.findChild(base.gameObject, "Button_back").GetComponent<UIButton>();
		button_yes = commonScreenObject.findChild(base.gameObject, "Button_yes").GetComponent<UIButton>();
		slotGrid = commonScreenObject.findChild(base.gameObject, "SlotGrid").GetComponent<UIGrid>();
		saveLoadSlotList = new Dictionary<string, GameObject>();
		selectedIndex = -1;
		saveFile = false;
		panel_scenario = commonScreenObject.findChild(base.gameObject, "Panel_Scenario").gameObject;
		panel_scenarioIcon = commonScreenObject.findChild(base.gameObject, "Panel_ScenarioIcon").gameObject;
		selectScenLabel = commonScreenObject.findChild(panel_scenario, "SelectScenLabel").GetComponent<UILabel>();
		scenarioButton = commonScreenObject.findChild(panel_scenario, "ScenarioButton").GetComponent<UIPopupList>();
		gameScenList = new List<GameScenario>();
		start = false;
		scenarioRefID = string.Empty;
		suffix = string.Empty;
		dirList = jsonFileController.loadSaveFileDir(game);
		loadSuccess = string.Empty;
		isAutoSave = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Button_back":
			viewController.closeSaveLoadPopup();
			if (GameObject.Find("Panel_StartScreen") != null)
			{
				GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().enableButtons();
			}
			break;
		case "Button_yes":
			if (saveFile)
			{
				string text = dirList["save" + selectedIndex + "Load"];
				if (text == string.Empty)
				{
					saveLoad();
					break;
				}
				GameData gameData = game.getGameData();
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("saveLoadMenu11"), PopupType.PopupTypeSaveLoad, null, colorTag: false, null, map: false, string.Empty);
			}
			else
			{
				saveLoad();
			}
			break;
		default:
			selectSlot(gameObjectName);
			break;
		}
	}

	public void setReference(bool save, string aScenarioRefID, bool fromStart = true)
	{
		GameData gameData = game.getGameData();
		saveFile = save;
		scenarioRefID = aScenarioRefID;
		start = fromStart;
		if (!fromStart)
		{
			selectScenLabel.text = gameData.getTextByRefId("saveLoadMenu13");
			GetComponent<UIPanel>().depth = 19;
			panel_scenario.GetComponent<UIPanel>().depth = 20;
			panel_scenarioIcon.GetComponent<UIPanel>().depth = 21;
			gameScenList = gameData.getGameScenarioList();
			List<string> list = new List<string>();
			foreach (GameScenario gameScen in gameScenList)
			{
				list.Add(gameScen.getGameScenarioName());
			}
			scenarioButton.items = list;
			if (save)
			{
				selectScenLabel.text = gameData.getTextByRefId("saveLoadMenu12");
				scenarioButton.GetComponentInChildren<UILabel>().color = Color.grey;
				scenarioButton.GetComponent<BoxCollider>().enabled = false;
				commonScreenObject.findChild(panel_scenarioIcon, "FilterArrow").GetComponent<UISprite>().enabled = false;
			}
		}
		else
		{
			selectScenLabel.text = gameData.getTextByRefId("saveLoadMenu12");
			scenarioButton.GetComponentInChildren<UILabel>().color = Color.grey;
			scenarioButton.GetComponent<BoxCollider>().enabled = false;
			commonScreenObject.findChild(panel_scenarioIcon, "FilterArrow").GetComponent<UISprite>().enabled = false;
		}
		scenarioButton.value = gameData.getGameScenarioByRefId(aScenarioRefID).getGameScenarioName();
	}

	public void setScenario()
	{
		foreach (GameScenario gameScen in gameScenList)
		{
			if (scenarioButton.value == gameScen.getGameScenarioName())
			{
				scenarioRefID = gameScen.getGameScenarioRefId();
			}
		}
		selectedIndex = -1;
		setYesButton();
		spawnSaveLoadObj();
	}

	private void spawnSaveLoadObj()
	{
		GameData gameData = game.getGameData();
		foreach (GameObject value in saveLoadSlotList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		saveLoadSlotList.Clear();
		if (scenarioRefID != "10001")
		{
			suffix = scenarioRefID;
		}
		else
		{
			suffix = string.Empty;
		}
		if (saveFile)
		{
			title_label.text = gameData.getTextByRefId("saveLoadMenu01");
			button_yes.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("saveLoadMenu05");
			GameObject gameObject = commonScreenObject.createPrefab(slotGrid.gameObject, "autoSaveLoadSlot", "Prefab/SaveLoad/saveLoadSlot", Vector3.zero, Vector3.one, Vector3.zero);
			Object.DestroyImmediate(gameObject.GetComponent<BoxCollider>());
			commonScreenObject.findChild(gameObject, "Info/saveLoadTitle").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu03").ToUpper(CultureInfo.InvariantCulture);
			commonScreenObject.findChild(gameObject, "Info/saveLoadTitle").localPosition = new Vector3(-205f, -2f, 0f);
			string text = dirList["autosave" + suffix + "Load"];
			if (text == string.Empty)
			{
				gameObject.GetComponent<UISprite>().spriteName = "bg_Slotunused";
				commonScreenObject.findChild(gameObject, "EmptyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu08").ToUpper(CultureInfo.InvariantCulture);
				commonScreenObject.findChild(gameObject, "Info").gameObject.SetActive(value: false);
			}
			else
			{
				setSlotInfo(gameObject, text);
			}
			saveLoadSlotList.Add(gameObject.name, gameObject);
			for (int i = 1; i <= 5; i++)
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(slotGrid.gameObject, "slot_" + i + suffix, "Prefab/SaveLoad/saveLoadSlot", Vector3.zero, Vector3.one, Vector3.zero);
				string text2 = dirList["save" + i + suffix + "Load"];
				commonScreenObject.findChild(gameObject2, "Info/saveLoadTitle").GetComponent<UILabel>().text = i.ToString();
				commonScreenObject.findChild(gameObject2, "Info/saveLoadTitle").GetComponent<UILabel>().fontSize = 16;
				if (text2 == string.Empty)
				{
					gameObject2.GetComponent<UISprite>().spriteName = "bg_Slotunused";
					commonScreenObject.findChild(gameObject2, "EmptyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu08").ToUpper(CultureInfo.InvariantCulture);
					commonScreenObject.findChild(gameObject2, "Info").gameObject.SetActive(value: false);
				}
				else
				{
					setSlotInfo(gameObject2, text2);
				}
				saveLoadSlotList.Add(gameObject2.name, gameObject2);
			}
		}
		else
		{
			title_label.text = gameData.getTextByRefId("saveLoadMenu02");
			button_yes.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("saveLoadMenu06");
			GameObject gameObject3 = commonScreenObject.createPrefab(slotGrid.gameObject, "autoSaveLoadSlot", "Prefab/SaveLoad/saveLoadSlot", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject3, "Info/saveLoadTitle").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu03").ToUpper(CultureInfo.InvariantCulture);
			commonScreenObject.findChild(gameObject3, "Info/saveLoadTitle").localPosition = new Vector3(-205f, -2f, 0f);
			string text3 = dirList["autosave" + suffix + "Load"];
			if (text3 == string.Empty)
			{
				gameObject3.GetComponent<UISprite>().spriteName = "bg_Slotunused";
				Object.DestroyImmediate(gameObject3.GetComponent<BoxCollider>());
				commonScreenObject.findChild(gameObject3, "EmptyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu08").ToUpper(CultureInfo.InvariantCulture);
				commonScreenObject.findChild(gameObject3, "Info").gameObject.SetActive(value: false);
			}
			else
			{
				setSlotInfo(gameObject3, text3);
			}
			CommonAPI.debug("autosaveobj name: " + gameObject3.name);
			saveLoadSlotList.Add(gameObject3.name, gameObject3);
			for (int j = 1; j <= 5; j++)
			{
				GameObject gameObject4 = commonScreenObject.createPrefab(slotGrid.gameObject, "slot_" + j + suffix, "Prefab/SaveLoad/saveLoadSlot", Vector3.zero, Vector3.one, Vector3.zero);
				string text4 = dirList["save" + j + suffix + "Load"];
				commonScreenObject.findChild(gameObject4, "Info/saveLoadTitle").GetComponent<UILabel>().text = j.ToString();
				commonScreenObject.findChild(gameObject4, "Info/saveLoadTitle").GetComponent<UILabel>().fontSize = 16;
				if (text4 == string.Empty)
				{
					gameObject4.GetComponent<UISprite>().spriteName = "bg_Slotunused";
					Object.DestroyImmediate(gameObject4.GetComponent<BoxCollider>());
					commonScreenObject.findChild(gameObject4, "EmptyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu08").ToUpper(CultureInfo.InvariantCulture);
					commonScreenObject.findChild(gameObject4, "Info").gameObject.SetActive(value: false);
				}
				else
				{
					setSlotInfo(gameObject4, text4);
				}
				saveLoadSlotList.Add(gameObject4.name, gameObject4);
			}
		}
		button_back.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("saveLoadMenu07");
		slotGrid.Reposition();
		setYesButton();
	}

	private void selectSlot(string gameObjectName)
	{
		if (gameObjectName == "autoSaveLoadSlot")
		{
			if (selectedIndex != 0)
			{
				if (selectedIndex != -1)
				{
					saveLoadSlotList["slot_" + selectedIndex].GetComponent<UISprite>().spriteName = "bg_Slotnormal";
				}
				saveLoadSlotList[gameObjectName].GetComponent<UISprite>().spriteName = "bg_Slotselected";
				selectedIndex = 0;
			}
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			int num = CommonAPI.parseInt(array[1]);
			if (selectedIndex != num)
			{
				if (selectedIndex != -1)
				{
					if (selectedIndex == 0)
					{
						saveLoadSlotList["autoSaveLoadSlot"].GetComponent<UISprite>().spriteName = "bg_Slotnormal";
					}
					else
					{
						string text = dirList["save" + selectedIndex + "Load"];
						if (text == string.Empty)
						{
							saveLoadSlotList["slot_" + selectedIndex].GetComponent<UISprite>().spriteName = "bg_Slotunused";
						}
						else
						{
							saveLoadSlotList["slot_" + selectedIndex].GetComponent<UISprite>().spriteName = "bg_Slotnormal";
						}
					}
				}
				saveLoadSlotList[gameObjectName].GetComponent<UISprite>().spriteName = "bg_Slotselected";
				selectedIndex = num;
			}
		}
		setYesButton();
	}

	private void setYesButton()
	{
		if (selectedIndex == -1)
		{
			button_yes.isEnabled = false;
		}
		else
		{
			button_yes.isEnabled = true;
		}
	}

	private void setSlotInfo(GameObject slotObj, string infoString)
	{
		GameData gameData = game.getGameData();
		slotObj.GetComponent<UISprite>().spriteName = "bg_Slotnormal";
		GameObject aObject = commonScreenObject.findChild(slotObj, "Info").gameObject;
		string[] array = infoString.Split('#');
		if (array.Length > 1)
		{
			commonScreenObject.findChild(aObject, "ShopNameLabel").GetComponent<UILabel>().text = array[0];
			commonScreenObject.findChild(aObject, "PlayerNameLabel").GetComponent<UILabel>().text = array[1];
			commonScreenObject.findChild(aObject, "GoldFrame/GoldLabel").GetComponent<UILabel>().text = array[2];
			commonScreenObject.findChild(aObject, "FameFrame/FameLabel").GetComponent<UILabel>().text = array[3];
			long halfHours = long.Parse(array[4]);
			List<int> list = CommonAPI.convertHalfHoursToIntList(halfHours);
			char[] array2 = list[4].ToString().ToCharArray();
			string text;
			string text2;
			if (array2.Length > 1)
			{
				text = array2[0].ToString();
				text2 = array2[1].ToString();
			}
			else
			{
				text = "0";
				text2 = array2[0].ToString();
			}
			string text3;
			string text4;
			if (list[5] == 1)
			{
				text3 = "3";
				text4 = "0";
			}
			else
			{
				text3 = "0";
				text4 = "0";
			}
			UILabel[] componentsInChildren = commonScreenObject.findChild(aObject, "ClockSaveSlot").GetComponentsInChildren<UILabel>();
			UILabel[] array3 = componentsInChildren;
			foreach (UILabel uILabel in array3)
			{
				switch (uILabel.gameObject.name.Split('_')[1])
				{
				case "0":
					uILabel.text = text;
					break;
				case "1":
					uILabel.text = text2;
					break;
				case "2":
					uILabel.text = ":";
					break;
				case "3":
					uILabel.text = text3;
					break;
				case "4":
					uILabel.text = text4;
					break;
				}
			}
			Season seasonByMonth = CommonAPI.getSeasonByMonth(list[1]);
			commonScreenObject.findChild(aObject, "Season_icon").GetComponent<UISprite>().spriteName = CommonAPI.getSeasonIconName(seasonByMonth);
			UILabel component = commonScreenObject.findChild(aObject, "Date/Day_label").GetComponent<UILabel>();
			UILabel component2 = commonScreenObject.findChild(aObject, "Date/Month_label").GetComponent<UILabel>();
			UILabel component3 = commonScreenObject.findChild(aObject, "Date/Year_label").GetComponent<UILabel>();
			Vector3 localPosition = new Vector3(-15f, -14f, 0f);
			Vector3 localPosition2 = new Vector3(-15f, 14f, 0f);
			Vector3 localPosition3 = new Vector3(-15f, 0f, 0f);
			LanguageType lANGUAGE = Constants.LANGUAGE;
			if (lANGUAGE == LanguageType.kLanguageTypeJap)
			{
				localPosition = new Vector3(-15f, 14f, 0f);
				localPosition2 = new Vector3(-15f, 1f, 0f);
				localPosition3 = new Vector3(-15f, -13f, 0f);
			}
			component.transform.localPosition = localPosition3;
			component2.transform.localPosition = localPosition2;
			component3.transform.localPosition = localPosition;
			component.text = CommonAPI.formatDateDay(list[3] + list[2] * 7 + 1);
			component2.text = gameData.getTextByRefIdWithDynText("monthLong", "[month]", (list[1] + 1).ToString());
			component3.text = gameData.getTextByRefIdWithDynText("yearLong", "[year]", (list[0] + 1).ToString());
			Weather weatherByRefId = gameData.getWeatherByRefId(array[5]);
			commonScreenObject.findChild(aObject, "WeatherIcon").GetComponent<UISprite>().spriteName = "icon_" + weatherByRefId.getImage();
			commonScreenObject.findChild(aObject, "WeatherLabel").GetComponent<UILabel>().text = weatherByRefId.getWeatherName();
		}
		else
		{
			slotObj.GetComponent<UISprite>().spriteName = "bg_Slotunused";
			if (!saveFile)
			{
				Object.DestroyImmediate(slotObj.GetComponent<BoxCollider>());
			}
			commonScreenObject.findChild(slotObj, "EmptyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("saveLoadMenu08").ToUpper(CultureInfo.InvariantCulture);
			commonScreenObject.findChild(slotObj, "Info").gameObject.SetActive(value: false);
		}
	}

	public void saveLoad()
	{
		if (saveFile)
		{
			string text = commonScreenObject.getController("ShopMenuController").GetComponent<ShopMenuController>().saveGame("save" + selectedIndex, isPlayerSave: true);
			viewController.closeSaveLoadPopup();
			string[] array = text.Split('_');
			if (array[0] == "SUCCESS")
			{
				if (array.Length > 1)
				{
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, game.getGameData().getTextByRefId("saveLoadMenu09") + " " + array[1], PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
				}
				else
				{
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, game.getGameData().getTextByRefId("saveLoadMenu09"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
				}
				return;
			}
			string displayText = game.getGameData().getTextByRefId("errorCommon13") + " " + text + "\n\n" + game.getGameData().getTextByRefId("errorCommon14");
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, displayText, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
		else
		{
			if (GameObject.Find("Panel_PayDay") != null)
			{
				viewController.closePayDayPopup(resume: false);
			}
			if (GameObject.Find("Panel_GameOver") != null)
			{
				viewController.closeGameOver();
			}
			StartCoroutine("loadEverything");
		}
	}

	private IEnumerator loadEverything()
	{
		GameObject.Find("StationController").GetComponent<StationController>().resetStationSmithList();
		GUIDecorationController decorationController = GameObject.Find("GUIDecorationController").GetComponent<GUIDecorationController>();
		viewController.clearItemGetQueue();
		viewController.clearObjectiveCompleteQueue();
		viewController.clearWeatherParticles();
		if (selectedIndex == 0)
		{
			loadSuccess = commonScreenObject.getController("ShopMenuController").GetComponent<ShopMenuController>().loadGame("autosave" + suffix);
			isAutoSave = true;
		}
		else
		{
			loadSuccess = commonScreenObject.getController("ShopMenuController").GetComponent<ShopMenuController>().loadGame("save" + selectedIndex);
			isAutoSave = false;
		}
		yield return null;
	}

	public void loadAllUI()
	{
		string[] array = loadSuccess.Split('_');
		if (array[0] == "SUCCESS")
		{
			GameObject.Find("Panel_TopLeftMenu").GetComponent<GUITopMenuNewController>().refreshPlayerStats(isLoad: true);
			BottomMenuController component = GameObject.Find("Panel_BottomMenu").GetComponent<BottomMenuController>();
			component.loadButtons();
			component.setTutorialState(string.Empty);
			viewController.showPanelsAtStart();
			viewController.showShop();
			GameObject.Find("GUIGridController").GetComponent<GUIGridController>().createWorld();
			GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>().setReference();
			GUICharacterAnimationController component2 = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
			component2.resetCharacters();
			component2.spawnCharacters(startTimerAfter: true, isAutoSave);
			commonScreenObject.getController("ShopMenuController").GetComponent<ShopMenuController>().generateWeather();
			GameObject.Find("LoadingMask").GetComponent<LoadingScript>().startLoadingFromBlack(string.Empty);
			viewController.closeSettingsMenu(hide: true, resume: false);
			viewController.closeStartScreen();
			viewController.closeSaveLoadPopup();
			viewController.showTransparentMask();
		}
		else
		{
			string text = string.Empty;
			if (game.getGameData() != null)
			{
				text = game.getGameData().getTextByRefId("errorCommon12");
			}
			if (text == string.Empty || text == "errorCommon12")
			{
				text = "This save file has been corrupted.";
			}
			text = text + " " + loadSuccess;
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, text, PopupType.PopupTypeLoadFail, null, colorTag: false, null, map: false, string.Empty);
		}
	}
}
