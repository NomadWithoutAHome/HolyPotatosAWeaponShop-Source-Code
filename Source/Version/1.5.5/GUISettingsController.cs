using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISettingsController : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private ViewController viewController;

	private Game game;

	private GameData gameData;

	private BoxCollider closeButton;

	private UIButton saveButton;

	private UIButton loadButton;

	private UIButton shortcutButton;

	private UILabel enterCodeLabel;

	private UILabel returnLabel;

	private UILabel quitLabel;

	private UILabel copyrightLabel;

	private UILabel rightsReservedLabel;

	private UISprite soundButton;

	private UILabel soundLabel;

	private UISprite[] soundColliders;

	private UISprite musicButton;

	private UILabel musicLabel;

	private UISprite[] musicColliders;

	private GameObject speedMeter;

	private UILabel speedLabel;

	private bool fromStart;

	private Color32 onColor;

	private float soundUnit;

	private List<Resolution> filteredRes;

	private UIPopupList resolutionFilter;

	private UILabel resolutionLabel;

	private UISprite fullScreenToggle;

	private UILabel fullScreenLabel;

	private int prevWidth;

	private int prevHeight;

	private int currentWidth;

	private int currentHeight;

	private string currScenRefID;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<BoxCollider>();
		if (GameObject.Find("SaveButton") != null)
		{
			saveButton = commonScreenObject.findChild(base.gameObject, "SaveButton").GetComponent<UIButton>();
		}
		if (GameObject.Find("LoadButton") != null)
		{
			loadButton = commonScreenObject.findChild(base.gameObject, "LoadButton").GetComponent<UIButton>();
		}
		shortcutButton = commonScreenObject.findChild(base.gameObject, "KeyShortcutButton").GetComponent<UIButton>();
		if (GameObject.Find("EnterCodeLabel") != null)
		{
			enterCodeLabel = commonScreenObject.findChild(base.gameObject, "EnterCodeButton/EnterCodeLabel").GetComponent<UILabel>();
		}
		if (commonScreenObject.findChild(base.gameObject, "ReturnButton") != null)
		{
			returnLabel = commonScreenObject.findChild(base.gameObject, "ReturnButton/ReturnLabel").GetComponent<UILabel>();
		}
		quitLabel = commonScreenObject.findChild(base.gameObject, "QuitButton/QuitLabel").GetComponent<UILabel>();
		copyrightLabel = GameObject.Find("CopyrightLabel").GetComponent<UILabel>();
		rightsReservedLabel = GameObject.Find("RightsReservedLabel").GetComponent<UILabel>();
		soundButton = commonScreenObject.findChild(base.gameObject, "SoundToggle/SoundToggleButton").GetComponent<UISprite>();
		soundLabel = commonScreenObject.findChild(base.gameObject, "SoundToggle/Sound_label").GetComponent<UILabel>();
		soundColliders = commonScreenObject.findChild(base.gameObject, "SoundToggle/SoundColliders").GetComponentsInChildren<UISprite>();
		musicButton = commonScreenObject.findChild(base.gameObject, "MusicToggle/MusicToggleButton").GetComponent<UISprite>();
		musicLabel = commonScreenObject.findChild(base.gameObject, "MusicToggle/Music_label").GetComponent<UILabel>();
		musicColliders = commonScreenObject.findChild(base.gameObject, "MusicToggle/MusicColliders").GetComponentsInChildren<UISprite>();
		if (GameObject.Find("SpeedMeter") != null)
		{
			speedMeter = GameObject.Find("SpeedMeter");
		}
		if (GameObject.Find("SpeedMeter_label") != null)
		{
			speedLabel = GameObject.Find("SpeedMeter_label").GetComponent<UILabel>();
		}
		onColor = new Color32(25, 188, 156, byte.MaxValue);
		soundUnit = 0.2f;
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int < 1)
		{
			turnMusic(isOn: false, buttonPressed: false);
		}
		else if (@int >= 1)
		{
			turnMusic(isOn: true, buttonPressed: false);
		}
		int int2 = PlayerPrefs.GetInt("soundOnOff", 1);
		if (int2 < 1)
		{
			turnSound(isOn: false);
		}
		else if (int2 >= 1)
		{
			turnSound(isOn: true);
		}
		filteredRes = new List<Resolution>();
		Resolution[] resolutions = Screen.resolutions;
		Resolution[] array = resolutions;
		for (int i = 0; i < array.Length; i++)
		{
			Resolution item = array[i];
			if (item.width >= 1024)
			{
				filteredRes.Add(item);
			}
		}
		resolutionFilter = commonScreenObject.findChild(base.gameObject, "Panel_ResolutionFilter/FilterButton").GetComponent<UIPopupList>();
		resolutionLabel = commonScreenObject.findChild(base.gameObject, "Panel_ResolutionFilter/ResolutionLabel").GetComponent<UILabel>();
		fullScreenToggle = commonScreenObject.findChild(base.gameObject, "FullScreenButton").GetComponent<UISprite>();
		fullScreenLabel = commonScreenObject.findChild(base.gameObject, "FullScreenButton").GetComponentInChildren<UILabel>();
		currScenRefID = string.Empty;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "QuitButton":
			disableAllButtons();
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("settings19"), PopupType.PopupTypeQuit, null, colorTag: false, null, map: false, string.Empty);
			return;
		case "SoundToggleButton":
			toggleSound();
			return;
		case "MusicToggleButton":
			toggleMusic();
			return;
		case "SpeedMeter_left":
			GameObject.Find("ShopViewController").GetComponent<ShopViewController>().debugSpeedMeter(-0.05f);
			updateSpeedMeter();
			return;
		case "SpeedMeter_right":
			GameObject.Find("ShopViewController").GetComponent<ShopViewController>().debugSpeedMeter(0.05f);
			updateSpeedMeter();
			return;
		case "Close_button":
			if (fromStart)
			{
				GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().enableButtons();
				viewController.closeSettingsMenu(hide: false, resume: false);
			}
			else
			{
				viewController.closeSettingsMenu();
			}
			return;
		case "SaveButton":
			viewController.showSaveLoadPopup(save: true, currScenRefID, fromStart: false);
			return;
		case "LoadButton":
			viewController.showSaveLoadPopup(save: false, currScenRefID, fromStart: false);
			return;
		case "KeyShortcutButton":
			viewController.showKeyShortcutPopup(fromStart);
			viewController.closeSettingsMenu(hide: false, resume: false);
			return;
		case "EnterCodeButton":
			viewController.closeSettingsMenu(hide: true, resume: false);
			viewController.showCodePopup();
			return;
		case "FullScreenButton":
			toggleFullScreen();
			return;
		case "ReturnButton":
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("settings20"), PopupType.PopupTypeReturnHome, null, colorTag: false, null, map: false, string.Empty);
			return;
		}
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "MusicCollider":
			setMusicVol(CommonAPI.parseInt(array[1]));
			break;
		case "SoundCollider":
			setSoundVol(CommonAPI.parseInt(array[1]));
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
		fromStart = start;
		GameData gameData = game.getGameData();
		if (returnLabel != null)
		{
			returnLabel.text = gameData.getTextByRefId("settings18").ToUpper(CultureInfo.InvariantCulture);
		}
		quitLabel.text = gameData.getTextByRefId("menuGeneral12");
		copyrightLabel.text = gameData.getTextByRefId("settings02");
		rightsReservedLabel.text = string.Empty;
		soundLabel.text = gameData.getTextByRefId("settings05").ToUpper(CultureInfo.InvariantCulture);
		musicLabel.text = gameData.getTextByRefId("settings04").ToUpper(CultureInfo.InvariantCulture);
		if (!start)
		{
			saveButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("settings08").ToUpper(CultureInfo.InvariantCulture);
			loadButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("settings09").ToUpper(CultureInfo.InvariantCulture);
			if (enterCodeLabel != null)
			{
				enterCodeLabel.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("codePopup01").ToUpper(CultureInfo.InvariantCulture);
			}
			Player player = game.getPlayer();
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			if (gameData.checkFeatureIsUnlocked(gameLockSet, "SAVELOAD", completedTutorialIndex))
			{
				saveButton.isEnabled = true;
				loadButton.isEnabled = true;
			}
			else
			{
				saveButton.isEnabled = false;
				loadButton.isEnabled = false;
			}
		}
		else
		{
			GetComponent<UIPanel>().depth = 105;
		}
		shortcutButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("settings11").ToUpper(CultureInfo.InvariantCulture);
		List<string> list = new List<string>();
		int num = -1;
		int num2 = 0;
		currentWidth = PlayerPrefs.GetInt("screenWidth", 1920);
		currentHeight = PlayerPrefs.GetInt("screenHeight", 1080);
		prevWidth = currentWidth;
		prevHeight = currentHeight;
		foreach (Resolution filteredRe in filteredRes)
		{
			if (filteredRe.width == currentWidth && filteredRe.height == currentHeight)
			{
				num = num2;
			}
			list.Add(filteredRe.width + "X" + filteredRe.height);
			num2++;
		}
		CommonAPI.debug("currentWidth: " + currentWidth + " currentheight: " + currentHeight);
		if (num != -1)
		{
			resolutionFilter.value = currentWidth + "X" + currentHeight;
			resolutionFilter.GetComponentInChildren<UILabel>().text = list[num];
		}
		else
		{
			Screen.SetResolution(currentWidth, currentHeight, Screen.fullScreen);
			resolutionFilter.value = string.Empty;
			resolutionFilter.GetComponentInChildren<UILabel>().text = string.Empty;
		}
		resolutionFilter.items = list;
		resolutionLabel.text = gameData.getTextByRefId("settings13").ToUpper(CultureInfo.InvariantCulture);
		fullScreenLabel.text = gameData.getTextByRefId("settings14").ToUpper(CultureInfo.InvariantCulture);
		if (Screen.fullScreen)
		{
			PlayerPrefs.SetInt("fullScreen", 1);
			fullScreenToggle.spriteName = "checkbox_a";
		}
		else
		{
			PlayerPrefs.SetInt("fullScreen", 0);
			fullScreenToggle.spriteName = "checkbox_b";
		}
		currScenRefID = game.getPlayer().getGameScenario();
		updateSpeedMeter();
	}

	public void updateSpeedMeter()
	{
		if (speedMeter != null && speedMeter.activeSelf)
		{
			speedMeter.SetActive(value: false);
		}
	}

	public void toggleSound()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int < 1)
		{
			turnSound(isOn: true);
		}
		else if (@int >= 1)
		{
			turnSound(isOn: false);
		}
	}

	public void toggleMusic()
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int < 1)
		{
			turnMusic(isOn: true);
		}
		else if (@int >= 1)
		{
			turnMusic(isOn: false);
		}
	}

	private void setSoundVol(int barVolume)
	{
		float num = (float)barVolume * soundUnit;
		PlayerPrefs.SetFloat("soundVol", num);
		audioController.setSfxVolume(num);
		UISprite[] array = soundColliders;
		foreach (UISprite uISprite in array)
		{
			if (CommonAPI.parseInt(uISprite.gameObject.name.Split('_')[1]) <= barVolume)
			{
				uISprite.spriteName = "greenBar";
			}
			else
			{
				uISprite.spriteName = "none";
			}
		}
		if (soundButton.spriteName != "bton")
		{
			PlayerPrefs.SetInt("soundOnOff", 1);
			soundButton.spriteName = "bton";
		}
	}

	public void turnSound(bool isOn)
	{
		GameData gameData = game.getGameData();
		if (isOn)
		{
			PlayerPrefs.SetInt("soundOnOff", 1);
			soundButton.spriteName = "bton";
			float @float = PlayerPrefs.GetFloat("soundVol", 1f);
			int num = Mathf.RoundToInt(@float / soundUnit);
			UISprite[] array = soundColliders;
			foreach (UISprite uISprite in array)
			{
				if (CommonAPI.parseInt(uISprite.gameObject.name.Split('_')[1]) <= num)
				{
					uISprite.spriteName = "greenBar";
				}
				else
				{
					uISprite.spriteName = "none";
				}
			}
			if (!fromStart)
			{
				Weather weather = game.getPlayer().getWeather();
				audioController.playWeatherAudio(weather.getWeatherRefId());
			}
		}
		else
		{
			PlayerPrefs.SetInt("soundOnOff", -1);
			soundButton.spriteName = "btoff";
			UISprite[] array2 = soundColliders;
			foreach (UISprite uISprite2 in array2)
			{
				uISprite2.spriteName = "none";
			}
			audioController.stopAllLoopSE();
		}
	}

	private void setMusicVol(int barVolume)
	{
		float num = (float)barVolume * soundUnit;
		PlayerPrefs.SetFloat("musicVol", num);
		audioController.setBgmVolume(num);
		UISprite[] array = musicColliders;
		foreach (UISprite uISprite in array)
		{
			if (CommonAPI.parseInt(uISprite.gameObject.name.Split('_')[1]) <= barVolume)
			{
				uISprite.spriteName = "greenBar";
			}
			else
			{
				uISprite.spriteName = "none";
			}
		}
		if (musicButton.spriteName != "bton")
		{
			PlayerPrefs.SetInt("musicOnOff", 1);
			musicButton.spriteName = "bton";
			audioController.playBGMAudio(string.Empty);
		}
	}

	public void turnMusic(bool isOn, bool buttonPressed = true)
	{
		GameData gameData = game.getGameData();
		if (isOn)
		{
			PlayerPrefs.SetInt("musicOnOff", 1);
			musicButton.spriteName = "bton";
			float @float = PlayerPrefs.GetFloat("musicVol", 1f);
			int num = Mathf.RoundToInt(@float / soundUnit);
			UISprite[] array = musicColliders;
			foreach (UISprite uISprite in array)
			{
				if (CommonAPI.parseInt(uISprite.gameObject.name.Split('_')[1]) <= num)
				{
					uISprite.spriteName = "greenBar";
				}
				else
				{
					uISprite.spriteName = "none";
				}
			}
			if (buttonPressed)
			{
				string empty = string.Empty;
				empty = ((!fromStart) ? CommonAPI.getSeasonBGM(CommonAPI.getSeasonByMonth(game.getPlayer().getSeasonIndex())) : "startmenu");
				audioController.playBGMAudio(empty);
			}
		}
		else
		{
			PlayerPrefs.SetInt("musicOnOff", -1);
			musicButton.spriteName = "btoff";
			UISprite[] array2 = musicColliders;
			foreach (UISprite uISprite2 in array2)
			{
				uISprite2.spriteName = "none";
			}
			if (buttonPressed)
			{
				audioController.stopBGMAudio();
			}
		}
	}

	public void setNewResolution()
	{
		int num = -1;
		if (resolutionFilter.value != string.Empty)
		{
			string[] array = resolutionFilter.value.Split('X');
			for (int i = 0; i < filteredRes.Count; i++)
			{
				if (filteredRes[i].width == CommonAPI.parseInt(array[0]) && filteredRes[i].height == CommonAPI.parseInt(array[1]))
				{
					num = i;
				}
			}
		}
		CommonAPI.debug("selectedResIndex: " + num);
		if (filteredRes.Count > 0 && num != -1 && (currentWidth != filteredRes[num].width || currentHeight != filteredRes[num].height))
		{
			CommonAPI.debug("width: " + filteredRes[num].width + " height: " + filteredRes[num].height + " fullscreen: " + Screen.fullScreen);
			currentWidth = filteredRes[num].width;
			currentHeight = filteredRes[num].height;
			PlayerPrefs.SetInt("screenWidth", currentWidth);
			PlayerPrefs.SetInt("screenHeight", currentHeight);
			Screen.SetResolution(currentWidth, currentHeight, Screen.fullScreen);
			disableAllButtons();
			viewController.showSettingsConfirm(fromStart);
		}
	}

	public void keepNewResolution()
	{
		prevWidth = currentWidth;
		prevHeight = currentHeight;
	}

	public void revertResolution()
	{
		currentWidth = prevWidth;
		currentHeight = prevHeight;
		PlayerPrefs.SetInt("screenWidth", currentWidth);
		PlayerPrefs.SetInt("screenHeight", currentHeight);
		Screen.SetResolution(currentWidth, currentHeight, Screen.fullScreen);
		resolutionFilter.value = currentWidth + "X" + currentHeight;
		resolutionFilter.GetComponentInChildren<UILabel>().text = currentWidth + "X" + currentHeight;
	}

	private void toggleFullScreen()
	{
		if (Screen.fullScreen)
		{
			PlayerPrefs.SetInt("fullScreen", 0);
			Screen.SetResolution(currentWidth, currentHeight, fullscreen: false);
			fullScreenToggle.spriteName = "checkbox_b";
		}
		else
		{
			PlayerPrefs.SetInt("fullScreen", 1);
			Screen.SetResolution(currentWidth, currentHeight, fullscreen: true);
			fullScreenToggle.spriteName = "checkbox_a";
		}
	}

	public void enableAllButtons()
	{
		BoxCollider[] componentsInChildren = base.gameObject.GetComponentsInChildren<BoxCollider>();
		BoxCollider[] array = componentsInChildren;
		foreach (BoxCollider boxCollider in array)
		{
			boxCollider.enabled = true;
		}
	}

	public void disableAllButtons()
	{
		BoxCollider[] componentsInChildren = base.gameObject.GetComponentsInChildren<BoxCollider>();
		BoxCollider[] array = componentsInChildren;
		foreach (BoxCollider boxCollider in array)
		{
			boxCollider.enabled = false;
		}
	}

	public void quit()
	{
		Application.Quit();
	}

	public void returnToHome()
	{
		Application.LoadLevel("ALLNGUIMENU");
	}
}
