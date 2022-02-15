using UnityEngine;

public class GUIStartScreenController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GUIScenarioSelectController scenarioController;

	private GameObject chatDrawer;

	private GameObject loadDogPop;

	private GameObject startDogPop;

	private bool chatOpened;

	private UILabel startButtonLabel;

	private UILabel loadButtonLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		if (GameObject.Find("Panel_ScenarioSelect") != null)
		{
			scenarioController = GameObject.Find("Panel_ScenarioSelect").GetComponent<GUIScenarioSelectController>();
		}
		chatDrawer = GameObject.Find("ChatDrawer");
		loadDogPop = GameObject.Find("StartScreenLoadButton/DogPop");
		startDogPop = GameObject.Find("StartScreenStartButton/DogPop");
		startButtonLabel = GameObject.Find("StartScreenStartButtonLabel").GetComponent<UILabel>();
		loadButtonLabel = GameObject.Find("StartScreenLoadButtonLabel").GetComponent<UILabel>();
		chatOpened = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "StartScreenStartButton":
			game.getPlayer().setGameScenario("10001");
			viewController.closeStartScreen();
			viewController.showPanelsAtStart();
			viewController.showShop();
			break;
		case "StartScreenLoadButton":
			viewController.showSaveLoadPopup(save: false, "10001");
			disableButtons();
			break;
		case "ChatButton":
			setChatDrawer();
			break;
		case "SettingsButton":
			viewController.showSettingsMenu(start: true);
			disableButtons();
			break;
		case "LanguageButton":
			viewController.showLanguageMenu();
			disableButtons();
			break;
		case "FacebookButton":
			Application.OpenURL("https://www.facebook.com/holypotatogame");
			break;
		case "TwitterButton":
			Application.OpenURL("https://twitter.com/holypotatogame");
			break;
		case "InternetButton":
			CommonAPI.openUrl("https://www.day-lightstudios.com/?utm_source=hpaws-game&utm_medium=ingame-link&utm_campaign=ingame-link");
			break;
		case "WikiButton":
			Application.OpenURL("http://holy-potatoes-weapon-shop.wikia.com/");
			break;
		case "DiscordButton":
			CommonAPI.openUrl("https://discord.me/holypotatoes");
			break;
		case "MercButton":
		case "MercLogo":
			CommonAPI.openUrl("https://store.holypotatoesgame.com/?utm_source=hpaws-game&utm_medium=in-game-link");
			break;
		case "FacebookHPWISButton":
			CommonAPI.openUrl("https://www.facebook.com/holypotatogame/");
			break;
		case "TwitterHPWISButton":
			CommonAPI.openUrl("https://twitter.com/holypotatogame");
			break;
		case "InternetHPWISButton":
		case "HPWTHLogo":
			CommonAPI.openUrl("https://www.gog.com/game/holy_potatoes_a_spy_story");
			break;
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		startButtonLabel.text = gameData.getTextByRefId("startScreen01");
		loadButtonLabel.text = gameData.getTextByRefId("startScreen02");
		GameObject.Find("DiscordLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("discordText").ToUpper();
	}

	public void processHover(bool isOver, string gameObjectName)
	{
	}

	public void enableButtons()
	{
		GameObject.Find("ChatButton").GetComponent<UIButton>().isEnabled = true;
		GameObject.Find("StartScreenStartButton").GetComponent<UIButton>().isEnabled = true;
		GameObject.Find("StartScreenLoadButton").GetComponent<UIButton>().isEnabled = true;
		GameObject.Find("SettingsButton").GetComponent<UIButton>().isEnabled = true;
		if (GameObject.Find("LanguageButton") != null)
		{
			GameObject.Find("LanguageButton").GetComponent<BoxCollider>().enabled = true;
		}
		GameObject.Find("HPWTHLogo").GetComponent<UIButton>().isEnabled = true;
		GameObject.Find("MercButton").GetComponent<UIButton>().isEnabled = true;
		GameObject.Find("MercLogo").GetComponent<UIButton>().isEnabled = true;
		GameObject.Find("DiscordButton").GetComponent<UIButton>().isEnabled = true;
		if (scenarioController != null)
		{
			scenarioController.enableButton();
		}
	}

	public void disableButtons()
	{
		GameObject.Find("ChatButton").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("StartScreenStartButton").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("StartScreenLoadButton").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("SettingsButton").GetComponent<UIButton>().isEnabled = false;
		if (GameObject.Find("LanguageButton") != null)
		{
			GameObject.Find("LanguageButton").GetComponent<BoxCollider>().enabled = false;
		}
		GameObject gameObject = GameObject.Find("ChatDrawer");
		gameObject.GetComponent<TweenPosition>().enabled = false;
		gameObject.transform.localPosition = new Vector3(0f, -175f, 0f);
		chatOpened = false;
		GameObject.Find("HPWTHLogo").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("MercButton").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("MercLogo").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("DiscordButton").GetComponent<UIButton>().isEnabled = false;
		if (scenarioController != null)
		{
			scenarioController.disableButton();
		}
	}

	private void setChatDrawer()
	{
		if (chatOpened)
		{
			commonScreenObject.tweenPosition(chatDrawer.GetComponent<TweenPosition>(), new Vector3(0f, 175f, 0f), new Vector3(0f, -175f, 0f), 0.4f, null, string.Empty);
			chatOpened = false;
		}
		else
		{
			commonScreenObject.tweenPosition(chatDrawer.GetComponent<TweenPosition>(), new Vector3(0f, -175f, 0f), new Vector3(0f, 175f, 0f), 0.4f, null, string.Empty);
			chatOpened = true;
		}
	}
}
