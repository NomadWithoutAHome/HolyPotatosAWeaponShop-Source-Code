using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUITopMenuNewController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel currentSmithCountLabel;

	private UILabel maxSmithCountLabel;

	private UILabel moneyLabel;

	private UILabel shopLevelLabel;

	private UILabel playerName;

	private UILabel shopName;

	private UILabel ticketLabel;

	private UILabel fameValue;

	private UISprite feedDogBg;

	private UIButton feedDogButton;

	private UISprite feedDogSpriteDog;

	private UISprite feedDogSpriteBowl;

	private UISprite feedDogSpriteFood;

	private UIButton randomScenarioButton;

	private TweenScale randomScenarioScale;

	private UISprite randomScenarioTimer;

	private long scenarioStartTime;

	private int scenarioDuration;

	private GameObject moneyAdd;

	private int prevMoney;

	private GameObject fameAdd;

	private int prevFame;

	private GameObject calendar;

	private UILabel[] clockTimeList;

	private string currWeatherRefID;

	private UISprite weatherIcon;

	private UILabel weatherLabel;

	private UISprite seasonIcon;

	private UILabel dateDay;

	private UILabel dateMonth;

	private UILabel dateYear;

	private TweenScale saveIconTween;

	private Vector3 fameFromPos;

	private Vector3 fameToPos;

	private Vector3 moneyFromPos;

	private Vector3 moneyToPos;

	private bool isFirstUpdate;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		currentSmithCountLabel = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/CurrentSmithCountLabel").GetComponent<UILabel>();
		maxSmithCountLabel = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/MaxSmithCountLabel").GetComponent<UILabel>();
		moneyLabel = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/MoneyLabel").GetComponent<UILabel>();
		shopLevelLabel = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/ShopLevel_label").GetComponent<UILabel>();
		playerName = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/PlayerName").GetComponent<UILabel>();
		shopName = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/ShopName").GetComponent<UILabel>();
		ticketLabel = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/Ticket_label").GetComponent<UILabel>();
		fameValue = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/Fame_value").GetComponent<UILabel>();
		feedDogBg = commonScreenObject.findChild(base.gameObject, "FeedDog_bg").GetComponent<UISprite>();
		feedDogButton = commonScreenObject.findChild(feedDogBg.gameObject, "FeedDog_button").GetComponent<UIButton>();
		feedDogSpriteDog = commonScreenObject.findChild(feedDogBg.gameObject, "FeedDog_dog").GetComponent<UISprite>();
		feedDogSpriteBowl = commonScreenObject.findChild(feedDogBg.gameObject, "FeedDog_bowl").GetComponent<UISprite>();
		feedDogSpriteFood = commonScreenObject.findChild(feedDogSpriteBowl.gameObject, "FeedDog_food").GetComponent<UISprite>();
		randomScenarioButton = commonScreenObject.findChild(base.gameObject, "RandomScenario_button").GetComponent<UIButton>();
		randomScenarioScale = randomScenarioButton.GetComponent<TweenScale>();
		randomScenarioTimer = commonScreenObject.findChild(randomScenarioButton.gameObject, "RandomScenario_timer").GetComponent<UISprite>();
		scenarioStartTime = 0L;
		scenarioDuration = 0;
		moneyAdd = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/MoneyLabel/Money_add").gameObject;
		prevMoney = 0;
		fameAdd = commonScreenObject.findChild(base.gameObject, "ShopStatsFrame/Fame_value/Fame_add").gameObject;
		prevFame = 0;
		calendar = commonScreenObject.findChild(GameObject.Find("Panel_topRight"), "Calendar").gameObject;
		clockTimeList = commonScreenObject.findChild(calendar, "ClockSlot").GetComponentsInChildren<UILabel>();
		currWeatherRefID = string.Empty;
		seasonIcon = commonScreenObject.findChild(calendar, "Season_icon").GetComponent<UISprite>();
		weatherIcon = commonScreenObject.findChild(calendar, "Weather_icon").GetComponent<UISprite>();
		weatherLabel = commonScreenObject.findChild(calendar, "Weather_label").GetComponentInChildren<UILabel>();
		dateDay = commonScreenObject.findChild(calendar, "Date/Day_label").GetComponentInChildren<UILabel>();
		dateMonth = commonScreenObject.findChild(calendar, "Date/Month_label").GetComponentInChildren<UILabel>();
		dateYear = commonScreenObject.findChild(calendar, "Date/Year_label").GetComponentInChildren<UILabel>();
		saveIconTween = commonScreenObject.findChild(calendar, "Save_icon").GetComponent<TweenScale>();
		fameFromPos = new Vector3(30f, 0f, 0f);
		fameToPos = new Vector3(40f, 0f, 0f);
		moneyFromPos = new Vector3(40f, 0f, 0f);
		moneyToPos = new Vector3(55f, 0f, 0f);
		isFirstUpdate = true;
	}

	public void processClick(string gameObjectName)
	{
		setVariables();
		switch (gameObjectName)
		{
		case "button_record":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showShopRecordPopup();
			break;
		case "FeedDog_button":
			audioController.playButtonAudio();
			game.getPlayer().feedDog();
			updateDogBowl();
			dogJump();
			break;
		case "RandomScenario_button":
			audioController.playButtonAudio();
			showRandomScenario();
			break;
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (commonScreenObject.findChild(base.gameObject, "button_record").GetComponent<UIButton>().isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("200009")) && !viewController.getHasPopup())
		{
			processClick("button_record");
		}
	}

	public void refreshPlayerStats(bool isLoad = false)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (isFirstUpdate || isLoad)
		{
			setTopMenuTutorialState();
			isFirstUpdate = false;
		}
		currentSmithCountLabel.text = player.getSmithList().Count.ToString();
		maxSmithCountLabel.text = player.getShopMaxSmith().ToString();
		int playerGold = player.getPlayerGold();
		if (!isLoad && playerGold > prevMoney)
		{
			moneyAdd.GetComponent<UILabel>().text = "[56AE59]+" + CommonAPI.formatNumber(playerGold - prevMoney) + "[-]";
			commonScreenObject.tweenPosition(moneyAdd.GetComponent<TweenPosition>(), moneyFromPos, moneyToPos, 0.5f, null, string.Empty);
			commonScreenObject.tweenAlpha(moneyAdd.GetComponent<TweenAlpha>(), 0f, 1f, 1f, null, string.Empty);
		}
		else if (!isLoad && playerGold < prevMoney)
		{
			moneyAdd.GetComponent<UILabel>().text = "[E54242]-" + CommonAPI.formatNumber(prevMoney - playerGold) + "[-]";
			commonScreenObject.tweenPosition(moneyAdd.GetComponent<TweenPosition>(), moneyFromPos, moneyToPos, 0.5f, null, string.Empty);
			commonScreenObject.tweenAlpha(moneyAdd.GetComponent<TweenAlpha>(), 0f, 1f, 1f, null, string.Empty);
		}
		moneyLabel.text = CommonAPI.formatNumber(playerGold);
		prevMoney = playerGold;
		shopLevelLabel.text = player.getShopLevelInt().ToString();
		playerName.text = player.getPlayerName();
		shopName.text = player.getShopName();
		int fame = player.getFame();
		if (!isLoad && fame > prevFame)
		{
			fameAdd.GetComponent<UILabel>().text = "[56AE59]+" + CommonAPI.formatNumber(fame - prevFame) + "[-]";
			commonScreenObject.tweenPosition(fameAdd.GetComponent<TweenPosition>(), fameFromPos, fameToPos, 0.5f, null, string.Empty);
			commonScreenObject.tweenAlpha(fameAdd.GetComponent<TweenAlpha>(), 0f, 1f, 1f, null, string.Empty);
		}
		else if (!isLoad && fame < prevFame)
		{
			fameAdd.GetComponent<UILabel>().text = "[E54242]-" + CommonAPI.formatNumber(prevFame - fame) + "[-]";
			commonScreenObject.tweenPosition(fameAdd.GetComponent<TweenPosition>(), fameFromPos, fameToPos, 0.5f, null, string.Empty);
			commonScreenObject.tweenAlpha(fameAdd.GetComponent<TweenAlpha>(), 0f, 1f, 1f, null, string.Empty);
		}
		ticketLabel.text = player.getUnusedTickets().ToString();
		fameValue.text = CommonAPI.formatNumber(fame);
		prevFame = fame;
		List<int> list = CommonAPI.convertHalfHoursToIntList(player.getPlayerTimeLong());
		char[] array = list[4].ToString().ToCharArray();
		string text;
		string text2;
		if (array.Length > 1)
		{
			text = array[0].ToString();
			text2 = array[1].ToString();
		}
		else
		{
			text = "0";
			text2 = array[0].ToString();
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
		UILabel[] array2 = clockTimeList;
		foreach (UILabel uILabel in array2)
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
				uILabel.GetComponent<TweenAlpha>().ResetToBeginning();
				uILabel.GetComponent<TweenAlpha>().PlayForward();
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
		seasonIcon.spriteName = CommonAPI.getSeasonIconName(seasonByMonth);
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
		dateYear.transform.localPosition = localPosition;
		dateMonth.transform.localPosition = localPosition2;
		dateDay.transform.localPosition = localPosition3;
		dateDay.text = CommonAPI.formatDateDay(list[3] + list[2] * 7 + 1);
		dateMonth.text = gameData.getTextByRefIdWithDynText("monthLong", "[month]", (list[1] + 1).ToString());
		dateYear.text = gameData.getTextByRefIdWithDynText("yearLong", "[year]", (list[0] + 1).ToString());
		Weather weather = player.getWeather();
		weatherIcon.spriteName = weather.getImage();
		weatherLabel.text = weather.getWeatherName();
		if (currWeatherRefID != weather.getWeatherRefId())
		{
			currWeatherRefID = weather.getWeatherRefId();
			audioController.playWeatherAudio(currWeatherRefID);
		}
		feedDogButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("dogMoodText06").ToUpper(CultureInfo.InvariantCulture);
		updateDogBowl();
		updateRandomScenarioTimer(player.getPlayerTimeLong());
	}

	public void showSaveIcon()
	{
		commonScreenObject.tweenScale(saveIconTween, Vector3.zero, Vector3.one, 1f, null, string.Empty);
	}

	private void setVariables()
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		if (commonScreenObject == null)
		{
			commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		}
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		if (tooltipScript == null)
		{
			tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		}
	}

	public void dogJump()
	{
		commonScreenObject.tweenPosition(feedDogSpriteDog.GetComponent<TweenPosition>(), Vector3.zero, new Vector3(0f, 10f, 0f), 0.4f, null, string.Empty);
	}

	public void updateDogBowl()
	{
		Player player = game.getPlayer();
		if (!(feedDogBg != null))
		{
			return;
		}
		if (player.checkHasDog())
		{
			feedDogBg.alpha = 1f;
			feedDogButton.GetComponent<UIButton>().isEnabled = true;
			switch (player.getDogBowlState())
			{
			case 0:
				feedDogSpriteFood.spriteName = "doghud_food_1";
				feedDogSpriteDog.spriteName = "doghud_dog_1";
				break;
			case 1:
				feedDogSpriteFood.spriteName = "doghud_food_2";
				feedDogSpriteDog.spriteName = "doghud_dog_2";
				break;
			case 2:
				feedDogSpriteFood.spriteName = "doghud_food_3";
				feedDogSpriteDog.spriteName = "doghud_dog_3";
				break;
			}
			int num = 0;
			Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("301");
			if (highestPlayerFurnitureByType != null && highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty)
			{
				num = highestPlayerFurnitureByType.getFurnitureLevel();
			}
			switch (num)
			{
			case 0:
				feedDogSpriteBowl.spriteName = "doghud_bowl_1";
				break;
			case 1:
				feedDogSpriteBowl.spriteName = "doghud_bowl_2";
				break;
			case 2:
				feedDogSpriteBowl.spriteName = "doghud_bowl_3";
				break;
			case 3:
				feedDogSpriteBowl.spriteName = "doghud_bowl_4";
				break;
			case 4:
				feedDogSpriteBowl.spriteName = "doghud_bowl_5";
				break;
			}
		}
		else
		{
			feedDogBg.alpha = 0f;
			feedDogButton.GetComponent<UIButton>().isEnabled = false;
		}
	}

	public void showRandomScenario()
	{
		List<DayEndScenario> allowedRandomScenarioList = shopMenuController.getAllowedRandomScenarioList();
		int index = Random.Range(0, allowedRandomScenarioList.Count);
		if (allowedRandomScenarioList.Count > 0)
		{
			endRandomScenario();
			DayEndScenario aScenario = allowedRandomScenarioList[index];
			viewController.showRandomScenarioPopup(aScenario);
		}
	}

	public bool tryStartRandomScenarioButton(long startTime, int duration)
	{
		if (randomScenarioScale.transform.localScale == Vector3.zero)
		{
			randomScenarioButton.isEnabled = true;
			commonScreenObject.tweenScale(randomScenarioScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
			scenarioStartTime = startTime;
			scenarioDuration = duration;
			updateRandomScenarioTimer(startTime);
			return true;
		}
		return false;
	}

	public void updateRandomScenarioTimer(long currTime)
	{
		if (scenarioDuration > 0)
		{
			long num = scenarioStartTime + scenarioDuration - currTime;
			randomScenarioTimer.fillAmount = (float)num / (float)scenarioDuration;
			if (num <= 0)
			{
				endRandomScenario();
			}
		}
	}

	public void endRandomScenario()
	{
		CommonAPI.debug("endRandomScenario");
		if (randomScenarioScale.transform.localScale != Vector3.zero)
		{
			randomScenarioButton.isEnabled = false;
			commonScreenObject.tweenScale(randomScenarioScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty, isPlayForwards: false);
			scenarioStartTime = 0L;
			scenarioDuration = 0;
		}
	}

	public void hideRandomScenario()
	{
		randomScenarioButton.isEnabled = false;
		randomScenarioScale.transform.localScale = Vector3.zero;
		scenarioStartTime = 0L;
		scenarioDuration = 0;
	}

	public void setTopMenuTutorialState()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		UIButton component = commonScreenObject.findChild(base.gameObject, "button_record").GetComponent<UIButton>();
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "SHOPRECORD", completedTutorialIndex))
		{
			component.isEnabled = false;
		}
		else
		{
			component.isEnabled = true;
		}
		updateDogBowl();
	}

	public void enableShopProfile()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		UIButton component = commonScreenObject.findChild(base.gameObject, "button_record").GetComponent<UIButton>();
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "SHOPRECORD", completedTutorialIndex))
		{
			component.isEnabled = true;
		}
		if (player.checkHasDog())
		{
			feedDogButton.isEnabled = true;
		}
	}

	public void disableShopProfile()
	{
		UIButton component = commonScreenObject.findChild(base.gameObject, "button_record").GetComponent<UIButton>();
		component.isEnabled = false;
		feedDogButton.isEnabled = false;
	}
}
