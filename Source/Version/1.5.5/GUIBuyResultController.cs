using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIBuyResultController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel smithName;

	private UITexture smithImage;

	private UISprite smithLvlUp;

	private UISprite smithBubble;

	private UILabel smithBubbleText;

	private UITexture smithMood;

	private UISprite smithStatusAlert;

	private UILabel locationTitle;

	private UILabel locationLabel;

	private UILabel originalTitle;

	private UILabel originalLabel;

	private UILabel discountTitle;

	private UILabel discountLabel;

	private UILabel finalTitle;

	private UILabel finalLabel;

	private UIGrid matListGrid;

	private UILabel noMatsLabel;

	private UIButton closeButton;

	private Smith smith;

	private List<Item> itemList;

	private string statusString;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		smithName = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/SmithName_bg/SmithName_label").GetComponent<UILabel>();
		smithImage = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/SmithImage_texture").GetComponent<UITexture>();
		smithLvlUp = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/SmithLv_bg").GetComponent<UISprite>();
		smithBubble = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/SmithText_bg").GetComponent<UISprite>();
		smithBubbleText = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/SmithText_bg").GetComponentInChildren<UILabel>();
		smithMood = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>();
		smithStatusAlert = commonScreenObject.findChild(base.gameObject, "SmithImage_bg/StatusEffect").GetComponent<UISprite>();
		locationTitle = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/1Location_title").GetComponentInChildren<UILabel>();
		locationLabel = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/1Location_value").GetComponentInChildren<UILabel>();
		originalTitle = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/2Original_title").GetComponentInChildren<UILabel>();
		originalLabel = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/2Original_value").GetComponentInChildren<UILabel>();
		discountTitle = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/3Discount_title").GetComponentInChildren<UILabel>();
		discountLabel = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/3Discount_value").GetComponentInChildren<UILabel>();
		finalTitle = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/4Final_title").GetComponentInChildren<UILabel>();
		finalLabel = commonScreenObject.findChild(base.gameObject, "BuyDetails_bg/4Final_value").GetComponentInChildren<UILabel>();
		matListGrid = commonScreenObject.findChild(base.gameObject, "MaterialList_bg/MaterialList_grid").GetComponentInChildren<UIGrid>();
		noMatsLabel = commonScreenObject.findChild(base.gameObject, "MaterialList_bg/NoMats_label").GetComponentInChildren<UILabel>();
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
		statusString = string.Empty;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Close_button")
		{
			smith.returnToShopStandby();
			GameObject gameObject = GameObject.Find("Panel_BottomMenu");
			if (gameObject != null)
			{
				gameObject.GetComponent<BottomMenuController>().refreshBottomButtons();
			}
			viewController.closeBuyResult(smith);
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			switch (hoverName)
			{
			case "StatusEffect":
				tooltipScript.showText(statusString);
				return;
			case "SmithMood_texture":
				tooltipScript.showText(CommonAPI.getMoodString(smith.getMoodState(), showDesc: true));
				return;
			case "SmithImage_bg":
				tooltipScript.showText(smith.getSmithStandardInfoString(showFullJobDetails: false));
				return;
			}
			string[] array = hoverName.Split('_');
			if (array[0] == "BuyListObj")
			{
				Item item = itemList[CommonAPI.parseInt(array[1])];
				tooltipScript.showText(item.getItemStandardTooltipString());
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (closeButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Close_button");
		}
	}

	private void setLabels()
	{
		GameData gameData = game.getGameData();
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "BuyResultTitle_label":
				uILabel.text = gameData.getTextByRefId("buyResult01").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "1Location_title":
				uILabel.text = gameData.getTextByRefId("location");
				break;
			case "2Original_title":
				uILabel.text = gameData.getTextByRefId("buyResult02");
				break;
			case "3Discount_title":
				uILabel.text = gameData.getTextByRefId("buyResult03");
				break;
			case "4Final_title":
				uILabel.text = gameData.getTextByRefId("buyResult04");
				break;
			case "MaterialTitle_label":
				uILabel.text = gameData.getTextByRefId("materials").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "NoMats_label":
				uILabel.text = gameData.getTextByRefId("menuGeneral06").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "SmithLv_label":
				uILabel.text = gameData.getTextByRefId("smithStats19").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Close_label":
				uILabel.text = gameData.getTextByRefId("menuGeneral05").ToUpper(CultureInfo.InvariantCulture);
				break;
			}
		}
	}

	public void setReference(Smith aSmith)
	{
		smith = aSmith;
		Area exploreArea = aSmith.getExploreArea();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		setLabels();
		SmithMood moodState = aSmith.getMoodState();
		List<string> exploreTask = aSmith.getExploreTask();
		itemList = new List<Item>();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (string item in exploreTask)
		{
			string[] array = item.Split('@');
			Item itemByRefId = gameData.getItemByRefId(array[0]);
			itemList.Add(itemByRefId);
			int num4 = CommonAPI.parseInt(array[1]);
			if (num4 > 0)
			{
				int cost = exploreArea.getShopItemList()[itemByRefId.getItemRefId()].getCost();
				itemByRefId.addItem(num4);
				itemByRefId.addItemFromBuy(num4);
				num += cost * num4;
				num2 += (int)(0.1f * Mathf.Pow(cost, 0.7f) + 0.8f * Mathf.Pow(num4, 1.1f));
				ItemType itemType = itemByRefId.getItemType();
				if (itemType == ItemType.ItemTypeMaterial)
				{
					GameObject gameObject = commonScreenObject.createPrefab(matListGrid.gameObject, "BuyListObj_" + num3, "Prefab/BuyMaterial/BuyListObj", Vector3.zero, Vector3.one, Vector3.zero);
					gameObject.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/materials/" + itemByRefId.getImage());
					gameObject.GetComponentInChildren<UILabel>().text = num4.ToString();
					noMatsLabel.alpha = 0f;
				}
			}
			num3++;
		}
		matListGrid.enabled = true;
		matListGrid.Reposition();
		int merchantLevel = CommonAPI.getMerchantLevel(aSmith.getMerchantExp());
		float num5 = Mathf.Pow(merchantLevel, 0.78f) / 20f * Random.Range(0.95f, 1.05f);
		aSmith.addMerchantExp(num2);
		float moodEffect = CommonAPI.getMoodEffect(moodState, 0.7f, 1f, 0.1f);
		int num6 = (int)((float)num * num5 * moodEffect);
		int aNumber = num - num6;
		player.addGold(num6);
		audioController.playGoldGainAudio();
		float num7 = CommonAPI.convertRatioToPercent2DP((float)num6 / (float)num);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningBuyDiscount, string.Empty, num6);
		locationLabel.text = exploreArea.getAreaName();
		originalLabel.text = "$" + CommonAPI.formatNumber(num);
		discountTitle.text = gameData.getTextByRefId("buyResult03") + " (" + num7 + "%)";
		discountLabel.text = "-$" + CommonAPI.formatNumber(num6);
		finalLabel.text = "$" + CommonAPI.formatNumber(aNumber);
		statusString = aSmith.tryAddStatusEffect();
		if (statusString != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), statusString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		smithStatusAlert.alpha = 0f;
		smithStatusAlert.GetComponent<BoxCollider>().enabled = false;
		bool hasMoodLimit = false;
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
		{
			hasMoodLimit = true;
		}
		float aReduce = aSmith.getExploreArea().getMoodFactor() * 2f;
		aSmith.reduceSmithMood(aReduce, hasMoodLimit);
		SmithMood moodState2 = aSmith.getMoodState();
		if (moodState != moodState2)
		{
			string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
			if (whetsappMoodString != string.Empty)
			{
				gameData.addNewWhetsappMsg(aSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			}
		}
		exploreArea.removeAreaSmithRefID(aSmith.getSmithRefId());
		smithName.text = smith.getSmithName();
		smithLvlUp.transform.localScale = Vector3.zero;
		if (merchantLevel < aSmith.getMerchantLevel())
		{
			commonScreenObject.tweenScale(smithLvlUp.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		}
		smithImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
		smithBubbleText.text = CommonAPI.getSmithExploreMoodText(moodState, "BUY");
		commonScreenObject.tweenScale(smithBubble.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		smithMood.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(moodState2));
		audioController.playSmithMoodSfx(moodState2);
	}
}
