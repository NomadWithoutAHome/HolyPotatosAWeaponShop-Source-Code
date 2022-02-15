using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISellResultController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UISprite sellResultBg;

	private Transform sellResultTransform;

	private UILabel sellPriceLabel;

	private TweenScale sellPriceScale;

	private UILabel smithName;

	private UITexture smithImage;

	private UISprite smithLvlUp;

	private UISprite smithBubble;

	private UILabel smithBubbleText;

	private UITexture smithMood;

	private UISprite smithStatusAlert;

	private UILabel line1Title;

	private UILabel line1Label;

	private UILabel line2Title;

	private UILabel line2Label;

	private UILabel line3Title;

	private UILabel line3Label;

	private UILabel line4Title;

	private UILabel line4Label;

	private UILabel line5Title;

	private UILabel line5Label;

	private UILabel totalTitle;

	private UILabel totalLabel;

	private List<int> priceInterval;

	private UIGrid heroListGrid;

	private UIButton closeButton;

	private Smith smith;

	private int displayPrice;

	private bool hasMerchantLvlUp;

	private bool isAnimating;

	private int animPhase;

	private int buyerCount;

	private List<TweenScale> ratingTweenList;

	private List<TweenScale> heroTextTweenList;

	private List<UISprite> loyaltySpriteList;

	private string statusString;

	private List<int> buyerMaxLevel;

	private List<int> buyerExpBefore;

	private List<int> buyerExpAfter;

	private int expDispPercent;

	private bool isTutorial;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		sellResultTransform = commonScreenObject.findChild(base.gameObject, "SellResult");
		sellResultBg = commonScreenObject.findChild(sellResultTransform.gameObject, "SellResult_bg").GetComponent<UISprite>();
		sellPriceLabel = commonScreenObject.findChild(sellResultTransform.gameObject, "SellImage_bg/SellTotal_label").GetComponent<UILabel>();
		sellPriceScale = sellPriceLabel.GetComponent<TweenScale>();
		smithName = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/SmithName_bg/SmithName_label").GetComponent<UILabel>();
		smithImage = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/SmithImage_texture").GetComponent<UITexture>();
		smithLvlUp = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/SmithLv_bg").GetComponent<UISprite>();
		smithBubble = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/SmithText_bg").GetComponent<UISprite>();
		smithBubbleText = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/SmithText_bg").GetComponentInChildren<UILabel>();
		smithMood = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>();
		smithStatusAlert = commonScreenObject.findChild(sellResultTransform.gameObject, "SmithImage_bg/StatusEffect").GetComponent<UISprite>();
		line1Title = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line1_title").GetComponent<UILabel>();
		line1Label = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line1_value").GetComponent<UILabel>();
		line2Title = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line2_title").GetComponent<UILabel>();
		line2Label = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line2_value").GetComponent<UILabel>();
		line3Title = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line3_title").GetComponent<UILabel>();
		line3Label = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line3_value").GetComponent<UILabel>();
		line4Title = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line4_title").GetComponent<UILabel>();
		line4Label = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line4_value").GetComponent<UILabel>();
		line5Title = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line5_title").GetComponent<UILabel>();
		line5Label = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Line5_value").GetComponent<UILabel>();
		totalTitle = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Total_title").GetComponent<UILabel>();
		totalLabel = commonScreenObject.findChild(sellResultTransform.gameObject, "SellDetails_bg/Total_value").GetComponent<UILabel>();
		priceInterval = new List<int>();
		heroListGrid = commonScreenObject.findChild(sellResultTransform.gameObject, "HeroDetails_grid").GetComponent<UIGrid>();
		closeButton = commonScreenObject.findChild(sellResultTransform.gameObject, "Close_button").GetComponent<UIButton>();
		smith = null;
		displayPrice = 0;
		hasMerchantLvlUp = false;
		isAnimating = false;
		animPhase = 0;
		buyerCount = 0;
		ratingTweenList = new List<TweenScale>();
		heroTextTweenList = new List<TweenScale>();
		loyaltySpriteList = new List<UISprite>();
		statusString = string.Empty;
		buyerMaxLevel = new List<int>();
		buyerExpBefore = new List<int>();
		buyerExpAfter = new List<int>();
		expDispPercent = 0;
		isTutorial = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			smith.returnToShopStandby();
			viewController.closeSellResult(smith);
			if (game.getPlayer().getTutorialState() == TutorialState.TutorialStateBeforeSellWeapon)
			{
				game.getPlayer().setTutorialState(TutorialState.TutorialStateAfterSellWeapon);
			}
			break;
		case "SellResult_bg":
			forceEndAnimations();
			break;
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
			if (array[0] == "HeroImage")
			{
				Hero jobClassByRefId = game.getGameData().getJobClassByRefId(array[1]);
				tooltipScript.showText(jobClassByRefId.getHeroStandardInfoString());
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
		if (closeButton == null)
		{
			closeButton = commonScreenObject.findChild(sellResultTransform.gameObject, "Close_button").GetComponent<UIButton>();
		}
		if (gameData == null)
		{
			gameData = game.getGameData();
		}
		if (!isAnimating && closeButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Close_button");
		}
		else if (isAnimating && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			forceEndAnimations();
		}
	}

	public void setReference(Smith aSmith)
	{
		smith = aSmith;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		setLabels();
		isTutorial = false;
		GameObject gameObject = GameObject.Find("Panel_Tutorial");
		if (gameObject != null)
		{
			GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
			isTutorial = component.checkCurrentTutorial("50001");
			if (isTutorial)
			{
				component.nextTutorial();
			}
		}
		List<string> exploreTask = aSmith.getExploreTask();
		Area exploreArea = aSmith.getExploreArea();
		int merchantLevel = aSmith.getMerchantLevel();
		buyerMaxLevel = new List<int>();
		buyerExpBefore = new List<int>();
		buyerExpAfter = new List<int>();
		expDispPercent = 0;
		SmithMood moodState = aSmith.getMoodState();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < exploreTask.Count; i++)
		{
			Project completedProjectById = player.getCompletedProjectById(exploreTask[i]);
			Hero buyer = completedProjectById.getBuyer();
			if (!(buyer.getHeroRefId() != string.Empty))
			{
				continue;
			}
			completedProjectById.setProjectSaleState(ProjectSaleState.ProjectSaleStateSold);
			Offer selectedOffer = completedProjectById.getSelectedOffer();
			int finalScore = completedProjectById.getFinalScore();
			int finalPrice = completedProjectById.getFinalPrice();
			num += finalPrice;
			int merchantLevel2 = CommonAPI.getMerchantLevel(aSmith.getMerchantExp());
			float num4 = Mathf.Pow((float)merchantLevel2 - 1f, 0.78f) / 20f;
			float moodEffect = CommonAPI.getMoodEffect(moodState, 0.7f, 1f, 0.1f);
			int num5 = (int)((float)finalPrice * num4 * moodEffect * Random.Range(0.95f, 1.05f));
			int num6 = finalPrice + num5;
			completedProjectById.setFinalPrice(num6);
			player.addGold(num6);
			audioController.playGoldGainAudio();
			num2 += num5;
			num3 += num6;
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningWeapon, string.Empty, num6);
			int num7 = 0;
			int heroLevel = buyer.getHeroLevel();
			int heroMaxLevel = buyer.getHeroMaxLevel();
			buyerMaxLevel.Add(heroMaxLevel);
			buyerExpBefore.Add(buyer.getExpPoints());
			int expGrowth = selectedOffer.getExpGrowth();
			buyer.addExpPoints(expGrowth);
			string aName = buyer.getHeroName() + " " + gameData.getTextByRefIdWithDynText("heroStat08", "[level]", buyer.getHeroLevel().ToString());
			if (heroLevel < buyer.getHeroLevel())
			{
				int num8 = buyer.getHeroLevel() - heroLevel;
				for (int j = heroLevel; j <= buyer.getHeroLevel(); j++)
				{
					num7 += gameData.getFameByLevelReached(j);
				}
				if (buyer.getHeroLevel() == heroMaxLevel)
				{
					gameData.addNewWhetsappMsg(aName, gameData.getRandomTextBySetRefId("whetsappHeroMax"), "Image/Hero/" + buyer.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				}
				else
				{
					string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText("whetsappHeroLevelUp", "[level]", (buyer.getHeroLevel() - heroLevel).ToString());
					gameData.addNewWhetsappMsg(aName, randomTextBySetRefIdWithDynText, "Image/Hero/" + buyer.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				}
			}
			player.addTotalHeroExpGain(expGrowth);
			buyerExpAfter.Add(buyer.getExpPoints());
			buyer.addTimesBought(1);
			switch (CommonAPI.convertWeaponScoreToRating(finalScore))
			{
			case "S":
				gameData.addNewWhetsappMsg(aName, gameData.getRandomTextBySetRefId("whetsappHeroHighExp"), "Image/Hero/" + buyer.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				break;
			case "A":
				gameData.addNewWhetsappMsg(aName, gameData.getRandomTextBySetRefId("whetsappHeroHighExp"), "Image/Hero/" + buyer.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				break;
			case "D":
				gameData.addNewWhetsappMsg(aName, gameData.getRandomTextBySetRefId("whetsappHeroLowExp"), "Image/Hero/" + buyer.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				break;
			case "F":
				gameData.addNewWhetsappMsg(aName, gameData.getRandomTextBySetRefId("whetsappHeroLowExp"), "Image/Hero/" + buyer.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				break;
			}
			player.addFame(num7);
			audioController.playFameGainAudio();
			int sellExp = buyer.getSellExp();
			int aExp = finalScore * 2;
			aSmith.addMerchantExp(aExp);
			buyerCount++;
			switch (buyerCount)
			{
			case 1:
				line2Title.text = buyer.getHeroName();
				line2Label.text = "$" + CommonAPI.formatNumber(finalPrice);
				break;
			case 2:
				line3Title.text = buyer.getHeroName();
				line3Label.text = "$" + CommonAPI.formatNumber(finalPrice);
				break;
			case 3:
				line4Title.text = buyer.getHeroName();
				line4Label.text = "$" + CommonAPI.formatNumber(finalPrice);
				break;
			}
			priceInterval.Add(finalPrice);
		}
		statusString = aSmith.tryAddStatusEffect(isTutorial);
		if (statusString != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), statusString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		bool hasMoodLimit = false;
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
		{
			hasMoodLimit = true;
		}
		float aReduce = exploreArea.getMoodFactor() * 3f;
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
		int height = 410 + 80 * buyerCount;
		sellResultBg.height = height;
		Vector3 localPosition = new Vector3(0f, -325f + 80f * (float)(3 - buyerCount) - 10f, 0f);
		closeButton.transform.localPosition = localPosition;
		Vector3 localPosition2 = new Vector3(0f, 5f - 40f * (float)(3 - buyerCount), 0f);
		sellResultTransform.localPosition = localPosition2;
		smithName.text = smith.getSmithName();
		if (merchantLevel < aSmith.getMerchantLevel())
		{
			hasMerchantLvlUp = true;
		}
		smithLvlUp.transform.localScale = Vector3.zero;
		smithImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
		smithBubbleText.text = CommonAPI.getSmithExploreMoodText(moodState, "SELL");
		smithBubble.transform.localScale = Vector3.zero;
		smithMood.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(moodState2));
		audioController.playSmithMoodSfx(moodState2);
		line1Label.text = exploreArea.getAreaName();
		float num9 = CommonAPI.convertRatioToPercent2DP((float)num2 / (float)num);
		switch (buyerCount)
		{
		case 1:
			line3Title.text = gameData.getTextByRefId("sellResult02") + " (" + num9 + "%)";
			line3Label.text = "$" + CommonAPI.formatNumber(num2);
			break;
		case 2:
			line4Title.text = gameData.getTextByRefId("sellResult02") + " (" + num9 + "%)";
			line4Label.text = "$" + CommonAPI.formatNumber(num2);
			break;
		case 3:
			line5Title.text = gameData.getTextByRefId("sellResult02") + " (" + num9 + "%)";
			line5Label.text = "$" + CommonAPI.formatNumber(num2);
			break;
		}
		priceInterval.Add(num2);
		totalLabel.text = "$" + CommonAPI.formatNumber(num3);
		line1Title.alpha = 0f;
		line1Label.alpha = 0f;
		line2Title.alpha = 0f;
		line2Label.alpha = 0f;
		line3Title.alpha = 0f;
		line3Label.alpha = 0f;
		line4Title.alpha = 0f;
		line4Label.alpha = 0f;
		line5Title.alpha = 0f;
		line5Label.alpha = 0f;
		totalTitle.alpha = 0f;
		totalLabel.alpha = 0f;
		sellPriceLabel.text = string.Empty;
		int num10 = 0;
		for (int k = 0; k < exploreTask.Count; k++)
		{
			Project completedProjectById2 = player.getCompletedProjectById(exploreTask[k]);
			Hero buyer2 = completedProjectById2.getBuyer();
			if (buyer2.getHeroRefId() != string.Empty)
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(heroListGrid.gameObject, "HeroSellListObj_" + num10, "Prefab/SellResult/HeroSellListObj", Vector3.zero, Vector3.one, Vector3.zero);
				UITexture component2 = commonScreenObject.findChild(gameObject2, "HeroSellListObj_bg/HeroImage_texture").GetComponent<UITexture>();
				component2.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + buyer2.getImage());
				component2.name = "HeroImage_" + buyer2.getHeroRefId();
				commonScreenObject.findChild(gameObject2, "HeroSellListObj_bg/WeaponRating_label").GetComponent<UILabel>().text = gameData.getTextByRefId("sellResult04");
				commonScreenObject.findChild(gameObject2, "HeroSellListObj_bg/WeaponImage_bg/WeaponImage_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + completedProjectById2.getProjectWeapon().getImage());
				commonScreenObject.findChild(gameObject2, "HeroSellListObj_bg/WeaponName_bg/WeaponName_label").GetComponent<UILabel>().text = completedProjectById2.getProjectName(includePrefix: true);
				UISprite component3 = commonScreenObject.findChild(gameObject2, "HeroSellListObj_bg/WeaponRating_value").GetComponent<UISprite>();
				switch (CommonAPI.convertWeaponScoreToRating(completedProjectById2.getFinalScore()))
				{
				case "S":
					component3.spriteName = "ranking_s";
					break;
				case "A":
					component3.spriteName = "ranking_a";
					break;
				case "B":
					component3.spriteName = "ranking_b";
					break;
				case "C":
					component3.spriteName = "ranking_c";
					break;
				case "D":
					component3.spriteName = "ranking_d";
					break;
				case "F":
					component3.spriteName = "ranking_f";
					break;
				}
				component3.transform.localScale = Vector3.zero;
				ratingTweenList.Add(component3.GetComponent<TweenScale>());
				GameObject gameObject3 = commonScreenObject.findChild(gameObject2, "HeroSellListObj_bg/HeroComment_bg").gameObject;
				gameObject3.GetComponentInChildren<UILabel>().text = CommonAPI.getHeroRatingText(completedProjectById2.getFinalScore());
				gameObject3.transform.localScale = Vector3.zero;
				heroTextTweenList.Add(gameObject3.GetComponent<TweenScale>());
				updateBuyerExpBar(gameObject2, num10);
				num10++;
			}
		}
		smithStatusAlert.alpha = 0f;
		smithStatusAlert.GetComponent<BoxCollider>().enabled = false;
		heroListGrid.Reposition();
		closeButton.isEnabled = false;
	}

	private void updateBuyerExpBar(GameObject heroObj, int i)
	{
		GameData gameData = game.getGameData();
		GameObject aObject = commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/HeroExp_bg").gameObject;
		UISprite component = commonScreenObject.findChild(aObject, "HeroExpCurrent_bar").GetComponent<UISprite>();
		UISprite component2 = commonScreenObject.findChild(aObject, "HeroExpGrowth_bar").GetComponent<UISprite>();
		int num = buyerMaxLevel[i];
		int heroLevelByExp = gameData.getHeroLevelByExp(buyerExpBefore[i]);
		int num2 = buyerExpBefore[i] + (buyerExpAfter[i] - buyerExpBefore[i]) * expDispPercent / 100;
		int num3 = Mathf.Min(num, gameData.getHeroLevelByExp(num2));
		int num4 = buyerExpAfter[i];
		int num5 = Mathf.Min(num, gameData.getHeroLevelByExp(num4));
		float expPercent = gameData.getExpPercent(num2);
		commonScreenObject.findChild(aObject, "HeroExp_label").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("heroStat08", "[level]", num3.ToString());
		if (num3 >= num)
		{
			component.fillAmount = 1f;
			component2.fillAmount = 1f;
			if (heroLevelByExp < num)
			{
				commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg").GetComponent<UISprite>().alpha = 1f;
				commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg/LvUp_label").GetComponent<UILabel>().text = "Lv. MAX!";
			}
			else
			{
				commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg").GetComponent<UISprite>().alpha = 0f;
			}
			return;
		}
		if (num5 == num3)
		{
			float expPercent2 = gameData.getExpPercent(num4);
			component.fillAmount = expPercent;
			component2.fillAmount = expPercent2;
			commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg").GetComponent<UISprite>().alpha = 0f;
		}
		else
		{
			component.fillAmount = expPercent;
			component2.fillAmount = 1f;
			commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg").GetComponent<UISprite>().alpha = 1f;
		}
		if (num3 > heroLevelByExp)
		{
			commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg").GetComponent<UISprite>().alpha = 1f;
			commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg/LvUp_label").GetComponent<UILabel>().text = "Lv. +" + (num3 - heroLevelByExp) + "!";
		}
		else
		{
			commonScreenObject.findChild(heroObj, "HeroSellListObj_bg/LvUp_bg").GetComponent<UISprite>().alpha = 0f;
		}
	}

	public void checkMaxHeroLoyaltyReward(Hero hero)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		string text = string.Empty;
		string aImage = string.Empty;
		List<Hero> heroList = gameData.getHeroList(itemLockSet);
		bool flag = false;
		if (hero.checkLoyaltyRewardGiven() || hero.getHeroLevel() != hero.getHeroMaxLevel())
		{
			return;
		}
		MaxLoyaltyReward maxLoyaltyReward = gameData.tryMaxLoyaltyReward();
		if (!(maxLoyaltyReward.getMaxLoyaltyRewardRefId() != string.Empty))
		{
			return;
		}
		string rewardRefId = maxLoyaltyReward.getRewardRefId();
		int rewardNum = maxLoyaltyReward.getRewardNum();
		switch (maxLoyaltyReward.getRewardType())
		{
		case "ITEM":
		{
			Item itemByRefId = gameData.getItemByRefId(rewardRefId);
			itemByRefId.addItem(rewardNum);
			text = itemByRefId.getItemName() + " x" + rewardNum;
			switch (itemByRefId.getItemType())
			{
			case ItemType.ItemTypeEnhancement:
				aImage = "Image/Enchantment/" + itemByRefId.getImage();
				break;
			case ItemType.ItemTypeMaterial:
				aImage = "Image/materials/" + itemByRefId.getImage();
				break;
			case ItemType.ItemTypeRelic:
				aImage = "Image/relics/" + itemByRefId.getImage();
				break;
			}
			break;
		}
		case "FURNITURE":
		{
			Furniture furnitureByRefId = gameData.getFurnitureByRefId(rewardRefId);
			if (!furnitureByRefId.checkPlayerOwned())
			{
				furnitureByRefId.setPlayerOwned(aOwned: true);
				text = furnitureByRefId.getFurnitureName();
				aImage = "Image/Obstacle/" + furnitureByRefId.getImage();
			}
			break;
		}
		case "DECORATION":
		{
			Decoration decorationByRefId = gameData.getDecorationByRefId(rewardRefId);
			if (!decorationByRefId.checkIsPlayerOwned())
			{
				decorationByRefId.setIsPlayerOwned(aPlayerOwned: true);
				text = decorationByRefId.getDecorationName();
				aImage = "Image/Decoration/" + decorationByRefId.getDecorationImage();
			}
			break;
		}
		}
		hero.setLoyaltyRewardGiven(aGiven: true);
		if (text != string.Empty)
		{
			string aName = hero.getHeroName() + " " + gameData.getTextByRefIdWithDynText("heroStat08", "[level]", hero.getHeroLevel().ToString());
			gameData.addNewWhetsappMsg(aName, gameData.getRandomTextBySetRefIdWithDynText("whetsappHeroMaxReward", "[reward]", text), "Image/Hero/" + hero.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
			viewController.queueItemGetPopup(gameData.getTextByRefIdWithDynText("featureUnlock22", "[heroName]", hero.getHeroName()), aImage, text);
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
			case "SellResultTitle_label":
				uILabel.text = gameData.getTextByRefId("sellResult01").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Line1_title":
				uILabel.text = gameData.getTextByRefId("location");
				break;
			case "Total_title":
				uILabel.text = gameData.getTextByRefId("sellResult03");
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

	public void startAnimations()
	{
		audioController.playSellStartAudio();
		audioController.playSellCoinAudio();
		isAnimating = true;
		StartCoroutine(doAnimations());
	}

	public void forceEndAnimations()
	{
		if (!isAnimating)
		{
			return;
		}
		if (animPhase <= 0)
		{
			line1Title.alpha = 1f;
			line1Label.alpha = 1f;
			for (int i = 0; i <= buyerCount; i++)
			{
				switch (i)
				{
				case 0:
					line2Title.alpha = 1f;
					line2Label.alpha = 1f;
					break;
				case 1:
					line3Title.alpha = 1f;
					line3Label.alpha = 1f;
					break;
				case 2:
					line4Title.alpha = 1f;
					line4Label.alpha = 1f;
					break;
				case 3:
					line5Title.alpha = 1f;
					line5Label.alpha = 1f;
					break;
				}
			}
			totalTitle.alpha = 1f;
			totalLabel.alpha = 1f;
		}
		if (animPhase <= 1)
		{
			audioController.playSellDialogueAudio();
			if (hasMerchantLvlUp)
			{
				smithLvlUp.GetComponent<TweenScale>().enabled = false;
				smithLvlUp.transform.localScale = Vector3.one;
			}
			smithBubble.GetComponent<TweenScale>().enabled = false;
			smithBubble.transform.localScale = Vector3.one;
		}
		if (animPhase <= 2)
		{
			audioController.stopSellCoinAudio();
			audioController.playSellHeroesSlideAudio();
			foreach (Transform child in heroListGrid.GetChildList())
			{
				child.GetComponentInChildren<TweenPosition>().enabled = false;
				child.GetComponentInChildren<TweenAlpha>().enabled = false;
				child.GetComponentInChildren<TweenPosition>().transform.localPosition = Vector3.zero;
				child.GetComponentInChildren<TweenAlpha>().GetComponent<UISprite>().alpha = 1f;
			}
			int num = 0;
			expDispPercent = 100;
			foreach (Transform child2 in heroListGrid.GetChildList())
			{
				GameObject heroObj = child2.gameObject;
				updateBuyerExpBar(heroObj, num);
				num++;
			}
		}
		if (animPhase <= 3)
		{
			audioController.playSellRatingAudio();
			foreach (TweenScale ratingTween in ratingTweenList)
			{
				ratingTween.enabled = false;
				ratingTween.transform.localScale = Vector3.one;
			}
		}
		if (animPhase <= 4)
		{
			audioController.playSellDialogueAudio();
			foreach (TweenScale heroTextTween in heroTextTweenList)
			{
				heroTextTween.enabled = false;
				heroTextTween.transform.localScale = Vector3.one;
			}
			foreach (UISprite loyaltySprite in loyaltySpriteList)
			{
				loyaltySprite.alpha = 1f;
			}
		}
		closeButton.isEnabled = true;
		isAnimating = false;
		animPhase = 5;
	}

	public void goToNextAnimPhase()
	{
		animPhase++;
	}

	private IEnumerator doAnimations()
	{
		while (isAnimating)
		{
			switch (animPhase)
			{
			case 0:
			{
				yield return new WaitForSeconds(0.5f);
				line1Title.alpha = 1f;
				line1Label.alpha = 1f;
				yield return new WaitForSeconds(0.5f);
				for (int i = 0; i <= buyerCount; i++)
				{
					switch (i)
					{
					case 0:
						line2Title.alpha = 1f;
						line2Label.alpha = 1f;
						displayPrice += priceInterval[i];
						sellPriceLabel.text = CommonAPI.formatNumber(displayPrice);
						commonScreenObject.tweenScale(sellPriceScale, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, null, string.Empty);
						yield return new WaitForSeconds(0.5f);
						break;
					case 1:
						line3Title.alpha = 1f;
						line3Label.alpha = 1f;
						displayPrice += priceInterval[i];
						sellPriceLabel.text = CommonAPI.formatNumber(displayPrice);
						commonScreenObject.tweenScale(sellPriceScale, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, null, string.Empty);
						yield return new WaitForSeconds(0.5f);
						break;
					case 2:
						line4Title.alpha = 1f;
						line4Label.alpha = 1f;
						displayPrice += priceInterval[i];
						sellPriceLabel.text = CommonAPI.formatNumber(displayPrice);
						commonScreenObject.tweenScale(sellPriceScale, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, null, string.Empty);
						yield return new WaitForSeconds(0.5f);
						break;
					case 3:
						line5Title.alpha = 1f;
						line5Label.alpha = 1f;
						displayPrice += priceInterval[i];
						sellPriceLabel.text = CommonAPI.formatNumber(displayPrice);
						commonScreenObject.tweenScale(sellPriceScale, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, null, string.Empty);
						yield return new WaitForSeconds(0.5f);
						break;
					}
				}
				totalTitle.alpha = 1f;
				totalLabel.alpha = 1f;
				sellPriceScale.style = UITweener.Style.Loop;
				commonScreenObject.tweenScale(sellPriceScale, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, null, string.Empty);
				yield return new WaitForSeconds(0.5f);
				goToNextAnimPhase();
				break;
			}
			case 1:
				audioController.playSellDialogueAudio();
				if (hasMerchantLvlUp)
				{
					commonScreenObject.tweenScale(smithLvlUp.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				}
				commonScreenObject.tweenScale(smithBubble.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				yield return new WaitForSeconds(0.5f);
				goToNextAnimPhase();
				break;
			case 2:
			{
				audioController.stopSellCoinAudio();
				audioController.playSellHeroesSlideAudio();
				foreach (Transform heroTrans in heroListGrid.GetChildList())
				{
					commonScreenObject.tweenPosition(heroTrans.GetComponentInChildren<TweenPosition>(), new Vector3(-500f, 0f, 0f), Vector3.zero, 0.4f, null, string.Empty);
					commonScreenObject.tweenAlpha(heroTrans.GetComponentInChildren<TweenAlpha>(), 0f, 1f, 0.4f, null, string.Empty);
					yield return new WaitForSeconds(0.3f);
				}
				for (int percent = 0; percent <= 100; percent++)
				{
					int index = 0;
					expDispPercent = percent;
					foreach (Transform child in heroListGrid.GetChildList())
					{
						GameObject heroObj = child.gameObject;
						updateBuyerExpBar(heroObj, index);
						index++;
					}
					yield return new WaitForSeconds(0.01f);
				}
				yield return new WaitForSeconds(0.5f);
				goToNextAnimPhase();
				break;
			}
			case 3:
				audioController.playSellRatingAudio();
				foreach (TweenScale ratingTween in ratingTweenList)
				{
					commonScreenObject.tweenScale(ratingTween, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				}
				yield return new WaitForSeconds(0.5f);
				goToNextAnimPhase();
				break;
			case 4:
				audioController.playSellDialogueAudio();
				foreach (TweenScale heroTextTween in heroTextTweenList)
				{
					commonScreenObject.tweenScale(heroTextTween, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				}
				foreach (UISprite loyaltySprite in loyaltySpriteList)
				{
					loyaltySprite.alpha = 1f;
				}
				goToNextAnimPhase();
				break;
			default:
				closeButton.isEnabled = true;
				isAnimating = false;
				break;
			}
		}
	}
}
