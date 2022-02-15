using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIObjectiveController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TweenPosition objectiveTween;

	private UILabel objectiveTitle;

	private UILabel objectiveText;

	private UILabel objectivePercent;

	private UILabel timeLeftTitle;

	private UILabel timeLeftLabel;

	private TweenScale starTween;

	private ParticleSystem starParticles;

	private Vector3 showPosition;

	private Vector3 hidePosition;

	private int alertTime;

	private Objective currentObjective;

	public void refreshObjectiveVariables()
	{
		setVariables();
		Player player = game.getPlayer();
		Objective objective = player.getCurrentObjective();
		if (objective != currentObjective)
		{
			if (objective == null || objective.getObjectiveRefId() == string.Empty)
			{
				startFirstObjective();
			}
			setNewObjective(player.getCurrentObjective());
		}
		if (currentObjective != null && currentObjective.getObjectiveRefId() != string.Empty)
		{
			GameData gameData = game.getGameData();
			float objectiveProgress = getObjectiveProgress();
			objectiveProgress = Mathf.Max(0f, objectiveProgress);
			string text = (int)(objectiveProgress * 100f) + "%";
			objectivePercent.text = text;
			int timeLeft = currentObjective.getTimeLeft(player.getPlayerTimeLong());
			string text2 = CommonAPI.convertHalfHoursToTimeString(timeLeft);
			timeLeftLabel.text = text2;
			if (currentObjective.getTimeLimit() > 0 && timeLeft < alertTime)
			{
				timeLeftLabel.color = Color.red;
				if (!starTween.enabled)
				{
					starTween.style = UITweener.Style.Loop;
					commonScreenObject.tweenScale(starTween, Vector3.one, new Vector3(0.9f, 1.1f, 1f), 0.8f, null, string.Empty);
				}
			}
			else
			{
				timeLeftLabel.color = Color.white;
				if (starTween.enabled && starTween.style == UITweener.Style.Loop)
				{
					starTween.ResetToBeginning();
					starTween.enabled = false;
				}
			}
		}
		else
		{
			objectiveText.text = string.Empty;
			timeLeftTitle.alpha = 0f;
			timeLeftLabel.alpha = 0f;
			commonScreenObject.tweenPosition(objectiveTween, objectiveTween.gameObject.transform.localPosition, hidePosition, 0.4f, null, string.Empty);
		}
	}

	private void setObjectiveDisplay()
	{
		if (currentObjective != null && currentObjective.getObjectiveRefId() != string.Empty)
		{
			objectiveText.text = currentObjective.getObjectiveDesc();
			if (currentObjective.getTimeLimit() > 0)
			{
				timeLeftTitle.alpha = 1f;
				timeLeftLabel.alpha = 1f;
			}
			else
			{
				timeLeftTitle.alpha = 0f;
				timeLeftLabel.alpha = 0f;
			}
			commonScreenObject.tweenPosition(objectiveTween, objectiveTween.gameObject.transform.localPosition, showPosition, 0.4f, null, string.Empty);
		}
		else
		{
			objectiveText.text = string.Empty;
			timeLeftTitle.alpha = 0f;
			timeLeftLabel.alpha = 0f;
			objectiveTween.gameObject.transform.localPosition = hidePosition;
		}
	}

	public void resetObjectiveDisplay()
	{
		currentObjective = null;
		GameObject.Find("Objective_bg").GetComponent<TweenPosition>().gameObject.transform.localPosition = new Vector3(250f, -90f, 0f);
	}

	public void setNewObjective(Objective aDisplayObjective)
	{
		setVariables();
		if (currentObjective != null && !(currentObjective.getObjectiveRefId() != aDisplayObjective.getObjectiveRefId()))
		{
			return;
		}
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		currentObjective = aDisplayObjective;
		bool flag = false;
		if (!currentObjective.checkObjectiveStarted())
		{
			flag = true;
			currentObjective.setObjectiveStarted(aStarted: true);
			currentObjective.setObjectiveSuccess(aSuccess: false);
			currentObjective.setObjectiveEnded(aEnded: false);
			if (currentObjective.getTimeLimit() > 0)
			{
				currentObjective.setStartTime(player.getPlayerTimeLong());
			}
			if (currentObjective.checkCountFromObjectiveStart() && currentObjective.getInitCount() == 0)
			{
				currentObjective.setInitCount(getInitialCount());
			}
		}
		setObjectiveDisplay();
		string startDialogueRefId = currentObjective.getStartDialogueRefId();
		CommonAPI.debug("currentObjective " + currentObjective.getObjectiveRefId() + " startDialogueRefId " + startDialogueRefId);
		if (flag && startDialogueRefId != string.Empty)
		{
			viewController.showDialoguePopup(startDialogueRefId, gameData.getDialogueBySetId(startDialogueRefId), PopupType.PopupTypeNewObjective);
		}
		else
		{
			showNewObjectiveAnims();
		}
	}

	public void showNewObjectiveAnims()
	{
		starTween.style = UITweener.Style.Once;
		commonScreenObject.tweenScale(starTween, Vector3.one, new Vector3(0.9f, 1.1f, 1f), 0.8f, null, string.Empty);
		starParticles.Play();
	}

	public void startFirstObjective()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string firstObjectiveRefId = gameScenarioByRefId.getFirstObjectiveRefId();
		Objective objectiveByRefId = gameData.getObjectiveByRefId(firstObjectiveRefId);
		if (!objectiveByRefId.checkObjectiveEnded())
		{
			CommonAPI.debug("startFirstObjective " + firstObjectiveRefId);
			player.setCurrentObjective(objectiveByRefId);
		}
	}

	public bool checkObjectiveStatus(bool forceObjectiveSuccess = false)
	{
		if (currentObjective.getObjectiveRefId() == string.Empty)
		{
			return false;
		}
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (getObjectiveProgress() >= 1f || forceObjectiveSuccess)
		{
			CommonAPI.debug(currentObjective.getObjectiveRefId() + " OBJECTIVE CLEARED");
			string successDialogueRefId = currentObjective.getSuccessDialogueRefId();
			if (successDialogueRefId != string.Empty)
			{
				viewController.showDialoguePopup(successDialogueRefId, gameData.getDialogueBySetId(successDialogueRefId));
			}
			viewController.queueObjectiveCompletePopup(currentObjective.getObjectiveDesc());
			currentObjective.setObjectiveSuccess(aSuccess: true);
			currentObjective.setObjectiveEnded(aEnded: true);
			doObjectiveSuccessSpecialEvents();
			string successNextObjective = currentObjective.getSuccessNextObjective();
			if (successNextObjective != string.Empty && successNextObjective != "-1" && successNextObjective != currentObjective.getObjectiveRefId())
			{
				CommonAPI.debug("setCurrentObjective " + currentObjective.getSuccessNextObjective());
				player.setCurrentObjective(gameData.getObjectiveByRefId(currentObjective.getSuccessNextObjective()));
			}
			else
			{
				CommonAPI.debug("setCurrentObjective EMPTY");
				player.setCurrentObjective(new Objective());
			}
			GameObject gameObject = GameObject.Find("Panel_BottomMenu");
			if (gameObject != null)
			{
				BottomMenuController component = gameObject.GetComponent<BottomMenuController>();
				component.setTutorialState(string.Empty);
				component.refreshBottomButtons();
			}
		}
		else if (currentObjective.getTimeLimit() > 0 && currentObjective.getTimeLeft(player.getPlayerTimeLong()) <= 0)
		{
			string failDialogueRefId = currentObjective.getFailDialogueRefId();
			if (failDialogueRefId != string.Empty)
			{
				viewController.showDialoguePopup(failDialogueRefId, gameData.getDialogueBySetId(failDialogueRefId));
			}
			currentObjective.setObjectiveEnded(aEnded: true);
			doObjectiveFailSpecialEvents();
			if (currentObjective.getFailNextObjective() != string.Empty)
			{
				player.setCurrentObjective(gameData.getObjectiveByRefId(currentObjective.getFailNextObjective()));
			}
			else
			{
				player.setCurrentObjective(new Objective());
			}
		}
		return false;
	}

	private void doObjectiveSuccessSpecialEvents()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (currentObjective.getObjectiveRefId())
		{
		case "1004":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/1-buy", gameData.getTextByRefId("featureUnlock02"));
			break;
		case "1009":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/3-unlock", gameData.getTextByRefId("featureUnlock03"));
			break;
		case "1010":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/6-explore", gameData.getTextByRefId("featureUnlock04"));
			break;
		case "1012":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/5-contract", gameData.getTextByRefId("featureUnlock07"));
			break;
		case "1013":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "1014":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "1015":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/8-research", gameData.getTextByRefId("featureUnlock08"));
			break;
		case "1018":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/9-vacation", gameData.getTextByRefId("featureUnlock09"));
			break;
		case "1020":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/7-shopupgrade", gameData.getTextByRefId("featureUnlock10"));
			break;
		case "1023":
		{
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/10-magic", gameData.getTextByRefId("featureUnlock15"));
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock13"), "Image/unlock/12-volundr", gameData.getTextByRefId("featureUnlock14"));
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock18"), "Image/unlock/unlock_training", gameData.getTextByRefId("featureUnlock19"));
			Smith smithByRefId = gameData.getSmithByRefId("10008");
			shopMenuController.doHireSmith(smithByRefId);
			smithByRefId.setAssignedRole(SmithStation.SmithStationEnchant);
			player.updateSmithStations();
			GameObject.Find("StationController").GetComponent<StationController>().assignSmithStations();
			break;
		}
		case "1026":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "1027":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "2001":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/unlock_journal", gameData.getTextByRefId("featureUnlock27"));
			break;
		case "8030":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "8031":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "8040":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "8041":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "8050":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "8051":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "8099":
			player.reduceGold(currentObjective.getReqCount(), allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName20"), -currentObjective.getReqCount());
			break;
		case "9901":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock20"), "Image/unlock/hire_unlock", gameData.getTextByRefId("featureUnlock21"));
			shopMenuController.GetComponent<ShopMenuController>().tryStartTutorial("HIRE_FIRE");
			break;
		case "9903":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock05"), "Image/unlock/unlock_dog", gameData.getTextByRefId("featureUnlock24"));
			break;
		case "9904":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock25"), "Image/unlock/11-patata", gameData.getTextByRefId("featureUnlock26"));
			break;
		case "100004":
		{
			Item itemByRefId2 = gameData.getItemByRefId("110001");
			itemByRefId2.tryUseItem(5, isUse: true);
			break;
		}
		case "100007":
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock01"), "Image/unlock/8-research", gameData.getTextByRefId("featureUnlock28"));
			break;
		case "100009":
		{
			Item itemByRefId = gameData.getItemByRefId("110002");
			itemByRefId.addItem(1);
			break;
		}
		}
		List<GameLock> gameLockListByTypeAndRefId = gameData.getGameLockListByTypeAndRefId("OBJECTIVE", currentObjective.getObjectiveRefId());
		foreach (GameLock item in gameLockListByTypeAndRefId)
		{
			shopMenuController.unlockFeature(item.getLockFeature());
		}
	}

	private void doObjectiveFailSpecialEvents()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (currentObjective.getObjectiveRefId())
		{
		case "1014":
			break;
		case "1027":
			break;
		}
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
		if (viewController == null)
		{
			viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		}
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		if (audioController == null)
		{
			audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		}
		if (objectiveTween == null)
		{
			GameData gameData = game.getGameData();
			objectiveTween = commonScreenObject.findChild(base.gameObject, "Objective_bg").GetComponent<TweenPosition>();
			objectiveTitle = commonScreenObject.findChild(objectiveTween.gameObject, "Objective_title").GetComponent<UILabel>();
			objectiveTitle.text = gameData.getTextByRefId("objectivePopup01").ToUpper(CultureInfo.InvariantCulture);
			objectiveText = commonScreenObject.findChild(objectiveTween.gameObject, "Objective_text").GetComponent<UILabel>();
			objectivePercent = commonScreenObject.findChild(objectiveTween.gameObject, "Objective_percent").GetComponent<UILabel>();
			timeLeftTitle = commonScreenObject.findChild(objectiveTween.gameObject, "ObjectiveTimeLeft_title").GetComponent<UILabel>();
			timeLeftTitle.text = gameData.getTextByRefId("objectivePopup02");
			timeLeftLabel = commonScreenObject.findChild(objectiveTween.gameObject, "ObjectiveTimeLeft_text").GetComponent<UILabel>();
			starTween = commonScreenObject.findChild(objectiveTween.gameObject, "Objective_star").GetComponent<TweenScale>();
			starParticles = starTween.GetComponent<ParticleSystem>();
			showPosition = new Vector3(0f, -90f, 0f);
			hidePosition = new Vector3(250f, -90f, 0f);
			alertTime = 48;
		}
	}

	public float getObjectiveProgress()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		UnlockCondition successCondition = currentObjective.getSuccessCondition();
		int reqCount = currentObjective.getReqCount();
		string checkString = currentObjective.getCheckString();
		int checkNum = currentObjective.getCheckNum();
		int num = currentObjective.getInitCount();
		float num2 = 0f;
		switch (successCondition)
		{
		case UnlockCondition.UnlockConditionTime:
		{
			int playerDays = player.getPlayerDays();
			num2 = (float)(playerDays - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionShopLevel:
		{
			int shopLevelInt = player.getShopLevelInt();
			num2 = (float)(shopLevelInt - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionFame:
		{
			int fame = player.getFame();
			num2 = (float)(fame - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionStarch:
			if (checkString == "EARNINGS")
			{
				int totalEarnings = player.getTotalEarnings();
				num2 = (float)(totalEarnings - num) / (float)reqCount;
			}
			if (checkString == "WEAPON")
			{
				int weaponEarnings = player.getWeaponEarnings();
				num2 = (float)(weaponEarnings - num) / (float)reqCount;
			}
			else
			{
				int playerGold = player.getPlayerGold();
				num2 = (float)(playerGold - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionForgeCount:
			if (checkString != string.Empty)
			{
				int completedWeaponCountByWeaponRefId = player.getCompletedWeaponCountByWeaponRefId(checkString);
				num2 = (float)(completedWeaponCountByWeaponRefId - num) / (float)reqCount;
			}
			else
			{
				int completedWeaponCount = player.getCompletedWeaponCount();
				num2 = (float)(completedWeaponCount - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionForgeTypeCount:
			if (checkString != string.Empty)
			{
				int completedWeaponCountByWeaponTypeRefId = player.getCompletedWeaponCountByWeaponTypeRefId(checkString);
				num2 = (float)(completedWeaponCountByWeaponTypeRefId - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionContractCount:
		{
			int totalContractCompleteCount2 = gameData.getTotalContractCompleteCount();
			num2 = (float)(totalContractCompleteCount2 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionHeroExp:
			if (checkString != string.Empty)
			{
				int expPoints = gameData.getHeroByHeroRefID(checkString).getExpPoints();
				num2 = (float)(expPoints - num) / (float)reqCount;
			}
			else if (checkNum != -1)
			{
				int totalExpByRegion = gameData.getTotalExpByRegion(checkNum, itemLockSet);
				num2 = (float)(totalExpByRegion - num) / (float)reqCount;
			}
			else
			{
				int totalHeroExpGain = player.getTotalHeroExpGain();
				num2 = (float)(totalHeroExpGain - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionHeroMax:
			if (checkString != string.Empty)
			{
				int num7 = gameData.countMaxLevelHeroInArea(checkString);
				num2 = (float)(num7 - num) / (float)reqCount;
			}
			else if (checkNum != -1)
			{
				int totalMaxLevelHeroesByRegion = gameData.getTotalMaxLevelHeroesByRegion(checkNum);
				num2 = (float)(totalMaxLevelHeroesByRegion - num) / (float)reqCount;
			}
			else
			{
				int count6 = gameData.getMaxLevelHeroList().Count;
				num2 = (float)(count6 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionHeroLevel:
			if (checkString != string.Empty)
			{
				Hero heroByHeroRefID = gameData.getHeroByHeroRefID(checkString);
				num2 = (float)(heroByHeroRefID.getHeroLevel() - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionHeroLevelCount:
			if (checkNum != -1 && checkString != string.Empty)
			{
				int count2 = gameData.getMinLevelHeroListByTier(CommonAPI.parseInt(checkString), checkNum).Count;
				num2 = (float)(count2 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionResearchWeapon:
		{
			if (checkString != string.Empty)
			{
				if (player.getWeaponByRefId(checkString).getWeaponRefId() == checkString)
				{
					num2 = 1f;
				}
				break;
			}
			int researchCount = player.getResearchCount();
			if (num > researchCount)
			{
				num = researchCount;
				currentObjective.setInitCount(num);
			}
			num2 = (float)(researchCount - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionResearchType:
			if (checkString != string.Empty)
			{
				int count7 = player.getUnlockedWeaponListByType(checkString).Count;
				num2 = (float)(count7 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionUpgradeTotal:
		{
			int num6 = player.countWorkstationUpgrades();
			num2 = (float)(num6 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionWorkstationLevel:
		{
			if (checkString != string.Empty)
			{
				int furnitureLevel = player.getHighestPlayerFurnitureByType(checkString).getFurnitureLevel();
				num2 = (float)(furnitureLevel - num) / (float)reqCount;
				break;
			}
			List<Furniture> currentShopWorkstationList = player.getCurrentShopWorkstationList();
			foreach (Furniture item in currentShopWorkstationList)
			{
				if (item.getFurnitureLevel() >= reqCount)
				{
					num2 += 0.25f;
				}
			}
			break;
		}
		case UnlockCondition.UnlockConditionDecoCount:
			if (checkString != string.Empty)
			{
				if (player.getOwnedDecorationByRefId(checkString).getDecorationRefId() == checkString)
				{
					num2 = 1f;
				}
			}
			else
			{
				int count = player.getOwnedDecorationList().Count;
				num2 = (float)(count - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionDecoEquip:
			if (checkString != string.Empty)
			{
				if (player.getOwnedDecorationByRefId(checkString).checkIsCurrentDisplay())
				{
					num2 = 1f;
				}
			}
			else
			{
				int count10 = player.getDisplayDecorationList().Count;
				num2 = (float)(count10 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionFurnitureEquip:
			if (checkString != string.Empty)
			{
				if (gameData.getFurnitureByRefId(checkString).checkShowInShop())
				{
					num2 = 1f;
				}
			}
			else
			{
				int count8 = player.getCurrentShopFurnitureList().Count;
				num2 = (float)(count8 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionRequestComplete:
		{
			int totalContractCompleteCount = gameData.getTotalContractCompleteCount();
			num2 = (float)(totalContractCompleteCount - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionLegendaryComplete:
			if (checkString != string.Empty)
			{
				if (player.checkLegendaryCompleted(checkString))
				{
					num2 = 1f;
				}
			}
			else
			{
				int count3 = player.getCompletedLegendaryList().Count;
				num2 = (float)(count3 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionArea:
			if (checkString != string.Empty)
			{
				if (gameData.getAreaByRefID(checkString).checkIsUnlock())
				{
					num2 = 1f;
				}
			}
			else
			{
				List<Area> unlockedAreaList = gameData.getUnlockedAreaList(itemLockSet);
				num2 = (float)(unlockedAreaList.Count - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionRegion:
		{
			int areaRegion = player.getAreaRegion();
			num2 = ((!currentObjective.checkCountFromObjectiveStart()) ? ((areaRegion - num < reqCount) ? 0f : 1f) : ((float)(areaRegion - num) / (float)reqCount));
			break;
		}
		case UnlockCondition.UnlockConditionExploreItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId3 = gameData.getItemByRefId(checkString);
				num2 = (float)(itemByRefId3.getItemFromExplore() - num) / (float)reqCount;
				break;
			}
			List<Item> itemList3 = gameData.getItemList(ownedOnly: false);
			int num13 = 0;
			foreach (Item item2 in itemList3)
			{
				num13 += item2.getItemFromExplore();
			}
			num2 = (float)(num13 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionBuyItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId2 = gameData.getItemByRefId(checkString);
				num2 = (float)(itemByRefId2.getItemFromBuy() - num) / (float)reqCount;
				break;
			}
			List<Item> itemList2 = gameData.getItemList(ownedOnly: false);
			int num11 = 0;
			foreach (Item item3 in itemList2)
			{
				num11 += item3.getItemFromBuy();
			}
			num2 = (float)(num11 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionOwnItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId = gameData.getItemByRefId(checkString);
				num2 = (float)(itemByRefId.getItemNum() - num) / (float)reqCount;
				break;
			}
			List<Item> itemList = gameData.getItemList(ownedOnly: false);
			int num9 = 0;
			foreach (Item item4 in itemList)
			{
				num9 += item4.getItemNum();
			}
			int num10 = Mathf.Max(num9 - num, 0);
			num2 = (float)num10 / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionWeaponsSold:
			if (checkString != string.Empty)
			{
				int count4 = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, checkString).Count;
				num2 = (float)(count4 - num) / (float)reqCount;
			}
			else
			{
				int count5 = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, "00000").Count;
				num2 = (float)(count5 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionExplore:
		{
			if (checkString != string.Empty)
			{
				string[] array2 = checkString.Split('_');
				if (!(array2[0] == "SCENARIO"))
				{
					Area areaByRefID2 = gameData.getAreaByRefID(checkString);
					num2 = (float)(areaByRefID2.checkTimesExplored() - num) / (float)reqCount;
				}
				break;
			}
			List<Area> areaList2 = gameData.getAreaList(string.Empty);
			int num5 = 0;
			foreach (Area item5 in areaList2)
			{
				num5 += item5.checkTimesExplored();
			}
			num2 = (float)(num5 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionBuy:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID = gameData.getAreaByRefID(checkString);
				num2 = (float)(areaByRefID.checkTimesBuyItems() - num) / (float)reqCount;
				break;
			}
			List<Area> areaList = gameData.getAreaList(string.Empty);
			int num3 = 0;
			foreach (Area item6 in areaList)
			{
				num3 += item6.checkTimesBuyItems();
			}
			num2 = (float)(num3 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionSell:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID5 = gameData.getAreaByRefID(checkString);
				num2 = (float)(areaByRefID5.checkTimesSell() - num) / (float)reqCount;
				break;
			}
			List<Area> areaList5 = gameData.getAreaList(string.Empty);
			int num15 = 0;
			foreach (Area item7 in areaList5)
			{
				num15 += item7.checkTimesSell();
			}
			num2 = (float)(num15 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionTraining:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID4 = gameData.getAreaByRefID(checkString);
				num2 = (float)(areaByRefID4.checkTimesTrain() - num) / (float)reqCount;
				break;
			}
			List<Area> areaList4 = gameData.getAreaList(string.Empty);
			int num14 = 0;
			foreach (Area item8 in areaList4)
			{
				num14 += item8.checkTimesTrain();
			}
			num2 = (float)(num14 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionVacation:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID3 = gameData.getAreaByRefID(checkString);
				num2 = (float)(areaByRefID3.checkTimesVacation() - num) / (float)reqCount;
				break;
			}
			List<Area> areaList3 = gameData.getAreaList(string.Empty);
			int num12 = 0;
			foreach (Area item9 in areaList3)
			{
				num12 += item9.checkTimesVacation();
			}
			num2 = (float)(num12 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionSmith:
			if (checkString != string.Empty)
			{
				Smith smithByRefId = gameData.getSmithByRefId(checkString);
				if (smithByRefId.checkPlayerOwned())
				{
					num2 = 1f;
				}
			}
			else
			{
				int count9 = player.getSmithList().Count;
				num2 = (float)(count9 - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionSmithHire:
		{
			if (checkString != string.Empty)
			{
				if (gameData.getSmithByRefId(checkString).getTimesHired() > 0)
				{
					num2 = 1f;
				}
				break;
			}
			List<Smith> smithList2 = gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
			int num8 = 0;
			foreach (Smith item10 in smithList2)
			{
				if (item10.getTimesHired() > 0)
				{
					num8++;
				}
			}
			num2 = (float)(num8 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionSmithStat:
		{
			SmithStat stat = SmithStat.SmithStatBlank;
			switch (checkString)
			{
			case "ATK":
				stat = SmithStat.SmithStatPower;
				break;
			case "SPD":
				stat = SmithStat.SmithStatIntelligence;
				break;
			case "ACC":
				stat = SmithStat.SmithStatTechnique;
				break;
			case "MAG":
				stat = SmithStat.SmithStatLuck;
				break;
			case "ALL":
				stat = SmithStat.SmithStatAll;
				break;
			}
			num2 = (float)(player.getSmithTotalStat(stat) - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionSmithJobClass:
		{
			string[] array = checkString.Split('@');
			if (array.Length <= 0)
			{
				break;
			}
			string text = array[0];
			if (array.Length > 1)
			{
				string text2 = array[1];
				Smith smithByRefID = player.getSmithByRefID(text2);
				if (smithByRefID.getSmithRefId() == text2 && smithByRefID.getSmithJob().getSmithJobRefId() == text)
				{
					num2 = ((checkNum != -1) ? ((float)smithByRefID.getSmithLevel() / (float)checkNum) : 1f);
				}
				break;
			}
			List<Smith> smithList = player.getSmithList();
			int num4 = 0;
			foreach (Smith item11 in smithList)
			{
				if (item11.getSmithJob().getSmithJobRefId() == text && (checkNum == -1 || item11.getSmithLevel() >= checkNum))
				{
					num4++;
				}
			}
			num2 = (float)(num4 - num) / (float)reqCount;
			break;
		}
		case UnlockCondition.UnlockConditionObjective:
			if (checkString != string.Empty)
			{
				Objective objectiveByRefId = gameData.getObjectiveByRefId(checkString);
				if (objectiveByRefId.checkObjectiveSuccess())
				{
					num2 = 1f;
				}
			}
			else
			{
				List<Objective> succeededObjectiveList = gameData.getSucceededObjectiveList();
				num2 = (float)(succeededObjectiveList.Count - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionCode:
			if (checkString != string.Empty)
			{
				Code codeByRefId = gameData.getCodeByRefId(checkString);
				if (codeByRefId.checkUsed())
				{
					num2 = 1f;
				}
			}
			else
			{
				List<Code> usedCodesList = gameData.getUsedCodesList();
				num2 = (float)(usedCodesList.Count - num) / (float)reqCount;
			}
			break;
		case UnlockCondition.UnlockConditionNone:
			num2 = 1f;
			break;
		}
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		return num2;
	}

	public int getInitialCount()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		UnlockCondition successCondition = currentObjective.getSuccessCondition();
		string checkString = currentObjective.getCheckString();
		int checkNum = currentObjective.getCheckNum();
		int result = 0;
		switch (successCondition)
		{
		case UnlockCondition.UnlockConditionTime:
			result = player.getPlayerDays();
			break;
		case UnlockCondition.UnlockConditionShopLevel:
			result = player.getShopLevelInt();
			break;
		case UnlockCondition.UnlockConditionFame:
			result = player.getFame();
			break;
		case UnlockCondition.UnlockConditionStarch:
			if (checkString == "EARNINGS")
			{
				result = player.getTotalEarnings();
			}
			result = ((!(checkString == "WEAPON")) ? player.getPlayerGold() : player.getWeaponEarnings());
			break;
		case UnlockCondition.UnlockConditionForgeCount:
			result = ((!(checkString != string.Empty)) ? player.getCompletedWeaponCount() : player.getCompletedWeaponCountByWeaponRefId(checkString));
			break;
		case UnlockCondition.UnlockConditionForgeTypeCount:
			if (checkString != string.Empty)
			{
				result = player.getCompletedWeaponCountByWeaponTypeRefId(checkString);
			}
			break;
		case UnlockCondition.UnlockConditionContractCount:
			result = gameData.getTotalContractCompleteCount();
			break;
		case UnlockCondition.UnlockConditionHeroExp:
			result = ((!(checkString != string.Empty)) ? ((checkNum == -1) ? player.getTotalHeroExpGain() : gameData.getTotalExpByRegion(checkNum, itemLockSet)) : gameData.getHeroByHeroRefID(checkString).getExpPoints());
			break;
		case UnlockCondition.UnlockConditionHeroMax:
			if (checkString == string.Empty)
			{
				result = ((checkNum == -1) ? gameData.getMaxLevelHeroList().Count : gameData.countMaxLevelHeroInArea(checkString));
			}
			break;
		case UnlockCondition.UnlockConditionHeroLevel:
			if (checkString != string.Empty)
			{
				result = gameData.getHeroByHeroRefID(checkString).getHeroLevel();
			}
			break;
		case UnlockCondition.UnlockConditionHeroLevelCount:
			if (checkNum != -1 && checkString != string.Empty)
			{
				result = gameData.getMinLevelHeroListByTier(CommonAPI.parseInt(checkString), checkNum).Count;
			}
			break;
		case UnlockCondition.UnlockConditionResearchWeapon:
			if (checkString == string.Empty)
			{
				result = player.getResearchCount();
			}
			break;
		case UnlockCondition.UnlockConditionResearchType:
			if (checkString != string.Empty)
			{
				result = player.getUnlockedWeaponListByType(checkString).Count;
			}
			break;
		case UnlockCondition.UnlockConditionUpgradeTotal:
			result = player.countWorkstationUpgrades();
			break;
		case UnlockCondition.UnlockConditionWorkstationLevel:
			if (checkString != string.Empty)
			{
				result = player.getHighestPlayerFurnitureByType(checkString).getFurnitureLevel();
			}
			break;
		case UnlockCondition.UnlockConditionDecoCount:
			if (checkString == string.Empty)
			{
				result = player.getOwnedDecorationList().Count;
			}
			break;
		case UnlockCondition.UnlockConditionDecoEquip:
			if (checkString == string.Empty)
			{
				result = player.getDisplayDecorationList().Count;
			}
			break;
		case UnlockCondition.UnlockConditionFurnitureEquip:
			if (checkString == string.Empty)
			{
				result = player.getCurrentShopFurnitureList().Count;
			}
			break;
		case UnlockCondition.UnlockConditionRequestComplete:
			result = gameData.getTotalContractCompleteCount();
			break;
		case UnlockCondition.UnlockConditionLegendaryComplete:
			if (checkString == string.Empty)
			{
				result = player.getCompletedLegendaryList().Count;
			}
			break;
		case UnlockCondition.UnlockConditionArea:
			if (checkString == string.Empty)
			{
				List<Area> unlockedAreaList = gameData.getUnlockedAreaList(itemLockSet);
				result = unlockedAreaList.Count;
			}
			break;
		case UnlockCondition.UnlockConditionRegion:
			result = player.getAreaRegion();
			break;
		case UnlockCondition.UnlockConditionExploreItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId3 = gameData.getItemByRefId(checkString);
				result = itemByRefId3.getItemFromExplore();
				break;
			}
			List<Item> itemList3 = gameData.getItemList(ownedOnly: false);
			int num6 = 0;
			foreach (Item item in itemList3)
			{
				num6 += item.getItemFromExplore();
			}
			result = num6;
			break;
		}
		case UnlockCondition.UnlockConditionBuyItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId2 = gameData.getItemByRefId(checkString);
				result = itemByRefId2.getItemFromBuy();
				break;
			}
			List<Item> itemList2 = gameData.getItemList(ownedOnly: false);
			int num4 = 0;
			foreach (Item item2 in itemList2)
			{
				num4 += item2.getItemFromBuy();
			}
			result = num4;
			break;
		}
		case UnlockCondition.UnlockConditionOwnItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId = gameData.getItemByRefId(checkString);
				result = itemByRefId.getItemNum();
				break;
			}
			List<Item> itemList = gameData.getItemList(ownedOnly: false);
			int num2 = 0;
			foreach (Item item3 in itemList)
			{
				num2 += item3.getItemNum();
			}
			result = num2;
			break;
		}
		case UnlockCondition.UnlockConditionWeaponsSold:
			result = ((!(checkString != string.Empty)) ? player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, "00000").Count : player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, checkString).Count);
			break;
		case UnlockCondition.UnlockConditionExplore:
		{
			if (checkString != string.Empty)
			{
				string[] array2 = checkString.Split('_');
				if (!(array2[0] == "SCENARIO"))
				{
					Area areaByRefID5 = gameData.getAreaByRefID(checkString);
					result = areaByRefID5.checkTimesExplored();
				}
				break;
			}
			List<Area> areaList5 = gameData.getAreaList(string.Empty);
			int num10 = 0;
			foreach (Area item4 in areaList5)
			{
				num10 += item4.checkTimesExplored();
			}
			result = num10;
			break;
		}
		case UnlockCondition.UnlockConditionBuy:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID4 = gameData.getAreaByRefID(checkString);
				result = areaByRefID4.checkTimesBuyItems();
				break;
			}
			List<Area> areaList4 = gameData.getAreaList(string.Empty);
			int num9 = 0;
			foreach (Area item5 in areaList4)
			{
				num9 += item5.checkTimesBuyItems();
			}
			result = num9;
			break;
		}
		case UnlockCondition.UnlockConditionSell:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID3 = gameData.getAreaByRefID(checkString);
				result = areaByRefID3.checkTimesSell();
				break;
			}
			List<Area> areaList3 = gameData.getAreaList(string.Empty);
			int num8 = 0;
			foreach (Area item6 in areaList3)
			{
				num8 += item6.checkTimesSell();
			}
			result = num8;
			break;
		}
		case UnlockCondition.UnlockConditionTraining:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID2 = gameData.getAreaByRefID(checkString);
				result = areaByRefID2.checkTimesTrain();
				break;
			}
			List<Area> areaList2 = gameData.getAreaList(string.Empty);
			int num7 = 0;
			foreach (Area item7 in areaList2)
			{
				num7 += item7.checkTimesTrain();
			}
			result = num7;
			break;
		}
		case UnlockCondition.UnlockConditionVacation:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID = gameData.getAreaByRefID(checkString);
				result = areaByRefID.checkTimesVacation();
				break;
			}
			List<Area> areaList = gameData.getAreaList(string.Empty);
			int num5 = 0;
			foreach (Area item8 in areaList)
			{
				num5 += item8.checkTimesVacation();
			}
			result = num5;
			break;
		}
		case UnlockCondition.UnlockConditionSmith:
			if (checkString == string.Empty)
			{
				result = player.getSmithList().Count;
			}
			break;
		case UnlockCondition.UnlockConditionSmithHire:
		{
			if (!(checkString == string.Empty))
			{
				break;
			}
			List<Smith> smithList2 = gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
			int num3 = 0;
			foreach (Smith item9 in smithList2)
			{
				if (item9.getTimesHired() > 0)
				{
					num3++;
				}
			}
			result = num3;
			break;
		}
		case UnlockCondition.UnlockConditionSmithStat:
		{
			SmithStat stat = SmithStat.SmithStatBlank;
			switch (checkString)
			{
			case "ATK":
				stat = SmithStat.SmithStatPower;
				break;
			case "SPD":
				stat = SmithStat.SmithStatIntelligence;
				break;
			case "ACC":
				stat = SmithStat.SmithStatTechnique;
				break;
			case "MAG":
				stat = SmithStat.SmithStatLuck;
				break;
			case "ALL":
				stat = SmithStat.SmithStatAll;
				break;
			}
			result = player.getSmithTotalStat(stat);
			break;
		}
		case UnlockCondition.UnlockConditionSmithJobClass:
		{
			string[] array = checkString.Split('@');
			if (array.Length <= 0)
			{
				break;
			}
			string text = array[0];
			if (array.Length > 1)
			{
				break;
			}
			List<Smith> smithList = player.getSmithList();
			int num = 0;
			foreach (Smith item10 in smithList)
			{
				if (item10.getSmithJob().getSmithJobRefId() == text && (checkNum == -1 || item10.getSmithLevel() >= checkNum))
				{
					num++;
				}
			}
			result = num;
			break;
		}
		case UnlockCondition.UnlockConditionObjective:
			if (checkString == string.Empty)
			{
				List<Objective> succeededObjectiveList = gameData.getSucceededObjectiveList();
				result = succeededObjectiveList.Count;
			}
			break;
		case UnlockCondition.UnlockConditionCode:
			if (checkString == string.Empty)
			{
				List<Code> usedCodesList = gameData.getUsedCodesList();
				result = usedCodesList.Count;
			}
			break;
		case UnlockCondition.UnlockConditionNone:
			return 0;
		}
		return result;
	}
}
