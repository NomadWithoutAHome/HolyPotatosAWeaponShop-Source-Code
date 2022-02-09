using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIForgeProgressNewController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Project currProject;

	private UILabel titleLabel;

	private UILabel percentLabel;

	private UISprite percentSprite;

	private UILabel timeLabel;

	private UISprite atkBg;

	private UISprite spdBg;

	private UISprite accBg;

	private UISprite magBg;

	private UILabel atkLabel;

	private UILabel spdLabel;

	private UILabel accLabel;

	private UILabel magLabel;

	private UILabel atkReqLabel;

	private UILabel spdReqLabel;

	private UILabel accReqLabel;

	private UILabel magReqLabel;

	private UILabel atkAddLabel;

	private UILabel spdAddLabel;

	private UILabel accAddLabel;

	private UILabel magAddLabel;

	private UITexture imageTexture;

	private int dispAtk;

	private int dispSpd;

	private int dispAcc;

	private int dispMag;

	private UIButton terminateButton;

	private Vector3 progressOutPosition;

	private Vector3 progressPosition;

	private GameObject boostDrawer;

	private TweenPosition boostDrawerTween;

	private UILabel boostCountLabel;

	private Vector3 boostDrawerClosed;

	private Vector3 boostDrawerOpen;

	private Vector3 boostDrawerExtend;

	private string boostDrawerState;

	private List<UIButton> boostStatButtonList;

	private TweenAlpha atkAlert;

	private TweenAlpha spdAlert;

	private TweenAlpha accAlert;

	private TweenAlpha magAlert;

	private bool isBoostAlert;

	private bool isFirstForging;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		currProject = null;
		progressOutPosition = new Vector3(240f, -60f, 0f);
		progressPosition = new Vector3(0f, -60f, 0f);
		boostDrawerClosed = new Vector3(-120f, -180f, 0f);
		boostDrawerOpen = new Vector3(-120f, -210f, 0f);
		boostDrawerExtend = new Vector3(-120f, -295f, 0f);
		boostDrawerState = "CLOSED";
		isBoostAlert = false;
		isFirstForging = false;
	}

	public void processClick(string gameobjectName)
	{
		GameData gameData = game.getGameData();
		switch (gameobjectName)
		{
		case "Terminate_button":
		{
			string empty = string.Empty;
			switch (currProject.getProjectType())
			{
			case ProjectType.ProjectTypeContract:
				empty = gameData.getTextByRefId("projectTerminate03");
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: true, gameData.getTextByRefId("projectTerminate05"), empty, PopupType.PopupTypeForgeContractAbandonConfirm, null, colorTag: false, null, map: false, string.Empty);
				break;
			case ProjectType.ProjectTypeWeapon:
			{
				empty = gameData.getTextByRefId("projectTerminate04");
				string empty2 = string.Empty;
				empty2 = ((currProject.getProjectProgressPercent() <= 50) ? ("\n[E54242]" + gameData.getTextByRefId("projectTerminate07") + "[-]") : ("\n[E54242]" + gameData.getTextByRefId("projectTerminate08") + "[-]"));
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: true, gameData.getTextByRefId("projectTerminate06"), empty + empty2, PopupType.PopupTypeForgeWeaponAbandonConfirm, null, colorTag: false, null, map: false, string.Empty);
				break;
			}
			case ProjectType.ProjectTypeUnique:
			{
				empty = gameData.getTextByRefId("projectTerminate04");
				string text = "\n[E54242]" + gameData.getTextByRefId("projectTerminate07") + "[-]";
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: true, gameData.getTextByRefId("projectTerminate06"), empty + text, PopupType.PopupTypeForgeWeaponAbandonConfirm, null, colorTag: false, null, map: false, string.Empty);
				break;
			}
			}
			break;
		}
		case "BoostAtk_bg":
			clickBoostStatButton(WeaponStat.WeaponStatAttack);
			break;
		case "BoostSpd_bg":
			clickBoostStatButton(WeaponStat.WeaponStatSpeed);
			break;
		case "BoostAcc_bg":
			clickBoostStatButton(WeaponStat.WeaponStatAccuracy);
			break;
		case "BoostMag_bg":
			clickBoostStatButton(WeaponStat.WeaponStatMagic);
			break;
		}
	}

	private void Update()
	{
		if (viewController != null && !viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetKey(gameData.getKeyCodeByRefID("200012")))
		{
			clickBoostStatButton(WeaponStat.WeaponStatAttack);
		}
		else if (Input.GetKey(gameData.getKeyCodeByRefID("200013")))
		{
			clickBoostStatButton(WeaponStat.WeaponStatSpeed);
		}
		else if (Input.GetKey(gameData.getKeyCodeByRefID("200014")))
		{
			clickBoostStatButton(WeaponStat.WeaponStatAccuracy);
		}
		else if (Input.GetKey(gameData.getKeyCodeByRefID("200015")) && boostDrawer != null && commonScreenObject.findChild(boostDrawer, "BoostMag_bg").GetComponent<UIButton>().isEnabled)
		{
			clickBoostStatButton(WeaponStat.WeaponStatMagic);
		}
	}

	public void setReference()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		currProject = player.getCurrentProject();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (completedTutorialIndex <= gameData.getTutorialSetOrderIndex("INTRO"))
		{
			isFirstForging = true;
		}
		switch (currProject.getProjectType())
		{
		case ProjectType.ProjectTypeContract:
			setContractProgress();
			break;
		case ProjectType.ProjectTypeWeapon:
			setWeaponProgress();
			break;
		case ProjectType.ProjectTypeUnique:
			setWeaponProgress();
			break;
		}
	}

	public void setWeaponProgress()
	{
		GameData gameData = game.getGameData();
		string aPath = "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/WeaponProgressObj";
		if (currProject.getProjectType() == ProjectType.ProjectTypeUnique)
		{
			aPath = "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/LegendaryProgressObj";
		}
		GameObject gameObject = commonScreenObject.createPrefab(base.gameObject, "WeaponProgressObj", aPath, progressOutPosition, Vector3.one, Vector3.zero);
		titleLabel = commonScreenObject.findChild(gameObject, "Progress_bg/WeaponName/WeaponName_label").GetComponent<UILabel>();
		imageTexture = commonScreenObject.findChild(gameObject, "Progress_bg/ProjectProgress_bar/ProjectImage_texture").GetComponent<UITexture>();
		percentLabel = commonScreenObject.findChild(gameObject, "Progress_bg/ProjectProgress_bar/ProjectProgress_percent").GetComponent<UILabel>();
		percentSprite = commonScreenObject.findChild(gameObject, "Progress_bg/ProjectProgress_bar/ProjectProgress_fg").GetComponent<UISprite>();
		UISprite component = commonScreenObject.findChild(gameObject, "Progress_bg/WeaponStat/atk_sprite").GetComponent<UISprite>();
		UISprite component2 = commonScreenObject.findChild(gameObject, "Progress_bg/WeaponStat/spd_sprite").GetComponent<UISprite>();
		UISprite component3 = commonScreenObject.findChild(gameObject, "Progress_bg/WeaponStat/acc_sprite").GetComponent<UISprite>();
		UISprite component4 = commonScreenObject.findChild(gameObject, "Progress_bg/WeaponStat/mag_sprite").GetComponent<UISprite>();
		atkBg = commonScreenObject.findChild(component.gameObject, "atk_bg").GetComponent<UISprite>();
		spdBg = commonScreenObject.findChild(component2.gameObject, "spd_bg").GetComponent<UISprite>();
		accBg = commonScreenObject.findChild(component3.gameObject, "acc_bg").GetComponent<UISprite>();
		magBg = commonScreenObject.findChild(component4.gameObject, "mag_bg").GetComponent<UISprite>();
		atkLabel = commonScreenObject.findChild(component.gameObject, "atk_label").GetComponent<UILabel>();
		spdLabel = commonScreenObject.findChild(component2.gameObject, "spd_label").GetComponent<UILabel>();
		accLabel = commonScreenObject.findChild(component3.gameObject, "acc_label").GetComponent<UILabel>();
		magLabel = commonScreenObject.findChild(component4.gameObject, "mag_label").GetComponent<UILabel>();
		atkAddLabel = commonScreenObject.findChild(component.gameObject, "atk_label/atkAdd_label").GetComponent<UILabel>();
		spdAddLabel = commonScreenObject.findChild(component2.gameObject, "spd_label/spdAdd_label").GetComponent<UILabel>();
		accAddLabel = commonScreenObject.findChild(component3.gameObject, "acc_label/accAdd_label").GetComponent<UILabel>();
		magAddLabel = commonScreenObject.findChild(component4.gameObject, "mag_label/magAdd_label").GetComponent<UILabel>();
		commonScreenObject.findChild(gameObject, "Progress_bg/WeaponProgressTitle_bg/WeaponProgressTitle_label").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeProgress01").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(gameObject, "Progress_bg/Terminate_button/Terminate_label").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeProgress02").ToUpper(CultureInfo.InvariantCulture);
		dispAtk = 0;
		dispSpd = 0;
		dispAcc = 0;
		dispMag = 0;
		terminateButton = commonScreenObject.findChild(gameObject, "Progress_bg/Terminate_button").GetComponent<UIButton>();
		terminateButton.isEnabled = checkTerminateAllowed();
		boostDrawer = commonScreenObject.findChild(gameObject, "BoostDrawer_bg").gameObject;
		boostDrawerTween = boostDrawer.GetComponent<TweenPosition>();
		boostCountLabel = commonScreenObject.findChild(boostDrawer, "Boost_label").GetComponent<UILabel>();
		boostStatButtonList = new List<UIButton>();
		boostStatButtonList.Add(commonScreenObject.findChild(boostDrawer, "BoostAtk_bg").GetComponent<UIButton>());
		boostStatButtonList.Add(commonScreenObject.findChild(boostDrawer, "BoostSpd_bg").GetComponent<UIButton>());
		boostStatButtonList.Add(commonScreenObject.findChild(boostDrawer, "BoostAcc_bg").GetComponent<UIButton>());
		boostStatButtonList.Add(commonScreenObject.findChild(boostDrawer, "BoostMag_bg").GetComponent<UIButton>());
		atkAlert = commonScreenObject.findChild(boostDrawer, "BoostAtk_bg/BoostAtkAlert_sprite").GetComponent<TweenAlpha>();
		spdAlert = commonScreenObject.findChild(boostDrawer, "BoostSpd_bg/BoostSpdAlert_sprite").GetComponent<TweenAlpha>();
		accAlert = commonScreenObject.findChild(boostDrawer, "BoostAcc_bg/BoostAccAlert_sprite").GetComponent<TweenAlpha>();
		magAlert = commonScreenObject.findChild(boostDrawer, "BoostMag_bg/BoostMagAlert_sprite").GetComponent<TweenAlpha>();
		commonScreenObject.findChild(boostDrawer, "BoostInstruction_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuForgeBoost44");
		commonScreenObject.tweenPosition(gameObject.GetComponent<TweenPosition>(), progressOutPosition, progressPosition, 0.4f, null, string.Empty);
		boostDrawer.transform.localPosition = boostDrawerClosed;
		boostDrawerState = "EXTENDED";
		updateBoostDrawer();
		updateWeaponDisplay();
	}

	public void setContractProgress()
	{
		GameData gameData = game.getGameData();
		GameObject aObject = commonScreenObject.createPrefab(base.gameObject, "ContractProgressObj", "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/ContractProgressObj", progressPosition, Vector3.one, Vector3.zero);
		titleLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractName_bg/ContractName_label").GetComponent<UILabel>();
		percentLabel = commonScreenObject.findChild(aObject, "Progress_bg/ProjectProgress_bar/ProjectProgress_percent").GetComponent<UILabel>();
		percentSprite = commonScreenObject.findChild(aObject, "Progress_bg/ProjectProgress_bar/ProjectProgress_fg").GetComponent<UISprite>();
		timeLabel = commonScreenObject.findChild(aObject, "Progress_bg/TimeRemaining_title/TimeRemaining_label").GetComponent<UILabel>();
		atkLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/atk_sprite/atk_label").GetComponent<UILabel>();
		spdLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/spd_sprite/spd_label").GetComponent<UILabel>();
		accLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/acc_sprite/acc_label").GetComponent<UILabel>();
		magLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/mag_sprite/mag_label").GetComponent<UILabel>();
		atkReqLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/atk_sprite/atkReq_label").GetComponent<UILabel>();
		spdReqLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/spd_sprite/spdReq_label").GetComponent<UILabel>();
		accReqLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/acc_sprite/accReq_label").GetComponent<UILabel>();
		magReqLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/mag_sprite/magReq_label").GetComponent<UILabel>();
		atkAddLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/atk_sprite/atk_label/atkAdd_label").GetComponent<UILabel>();
		spdAddLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/spd_sprite/spd_label/spdAdd_label").GetComponent<UILabel>();
		accAddLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/acc_sprite/acc_label/accAdd_label").GetComponent<UILabel>();
		magAddLabel = commonScreenObject.findChild(aObject, "Progress_bg/ContractStat/mag_sprite/mag_label/magAdd_label").GetComponent<UILabel>();
		commonScreenObject.findChild(aObject, "Progress_bg/ContractProgressTitle_bg/ContractProgressTitle_label").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeProgress03").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(aObject, "Progress_bg/Terminate_button/Terminate_label").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeProgress02").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(aObject, "Progress_bg/ProjectProgress_bar/ProjectProgress_label").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeProgress04").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(aObject, "Progress_bg/TimeRemaining_title").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeProgress05").ToUpper(CultureInfo.InvariantCulture);
		dispAtk = 0;
		dispSpd = 0;
		dispAcc = 0;
		dispMag = 0;
		updateContractDisplay();
	}

	public void updateBoostDrawer()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		int count = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, "00000").Count;
		boostCountLabel.text = gameData.getTextByRefId("menuForgeBoost43") + ": " + currProject.getNumBoost() + "/" + currProject.getMaxBoost();
		if (player.getGameScenario() == "10001" && (completedTutorialIndex < gameData.getTutorialSetOrderIndex("SELL_RESULT") || count < 1))
		{
			boostDrawerState = "CLOSED";
			boostCountLabel.color = Color.grey;
		}
		else if (!currProject.checkCanBoost())
		{
			boostDrawerState = "OPEN";
			boostCountLabel.color = Color.grey;
		}
		else
		{
			boostDrawerState = "EXTENDED";
			if (currProject.getProjectType() == ProjectType.ProjectTypeUnique)
			{
				boostCountLabel.color = Color.black;
			}
			else
			{
				boostCountLabel.color = Color.white;
			}
		}
		if (count > 0)
		{
			shopMenuController.tryStartTutorial("BOOST");
		}
		doDrawerLogic(boostDrawerState);
	}

	private void doDrawerLogic(string aState)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		bool isEnabled = false;
		switch (aState)
		{
		case "CLOSED":
			if (boostDrawer.transform.localPosition != boostDrawerClosed)
			{
				commonScreenObject.tweenPosition(boostDrawerTween, boostDrawer.transform.localPosition, boostDrawerClosed, 0.3f, null, string.Empty);
			}
			break;
		case "OPEN":
			if (boostDrawer.transform.localPosition != boostDrawerOpen)
			{
				commonScreenObject.tweenPosition(boostDrawerTween, boostDrawer.transform.localPosition, boostDrawerOpen, 0.3f, null, string.Empty);
			}
			break;
		case "EXTENDED":
			if (boostDrawer.transform.localPosition != boostDrawerExtend)
			{
				commonScreenObject.tweenPosition(boostDrawerTween, boostDrawer.transform.localPosition, boostDrawerExtend, 0.3f, null, string.Empty);
			}
			isEnabled = true;
			break;
		}
		foreach (UIButton boostStatButton in boostStatButtonList)
		{
			boostStatButton.isEnabled = isEnabled;
			switch (boostStatButton.gameObject.name)
			{
			case "BoostAtk_bg":
			{
				UILabel component5 = commonScreenObject.findChild(boostStatButton.gameObject, "BoostAtk_label").GetComponent<UILabel>();
				component5.text = "+ " + gameData.getTextByRefId("smithStatsShort02");
				UISprite component6 = commonScreenObject.findChild(boostStatButton.gameObject, "Boosted_icon").GetComponent<UISprite>();
				int num2 = currProject.checkBoostPenalty(WeaponStat.WeaponStatAttack);
				if (num2 > 0)
				{
					component6.alpha = 1f;
					component6.GetComponentInChildren<UILabel>().text = num2.ToString();
				}
				else
				{
					component6.alpha = 0f;
				}
				break;
			}
			case "BoostSpd_bg":
			{
				UILabel component9 = commonScreenObject.findChild(boostStatButton.gameObject, "BoostSpd_label").GetComponent<UILabel>();
				component9.text = "+ " + gameData.getTextByRefId("smithStatsShort03");
				UISprite component10 = commonScreenObject.findChild(boostStatButton.gameObject, "Boosted_icon").GetComponent<UISprite>();
				int num4 = currProject.checkBoostPenalty(WeaponStat.WeaponStatSpeed);
				if (num4 > 0)
				{
					component10.alpha = 1f;
					component10.GetComponentInChildren<UILabel>().text = num4.ToString();
				}
				else
				{
					component10.alpha = 0f;
				}
				break;
			}
			case "BoostAcc_bg":
			{
				UILabel component7 = commonScreenObject.findChild(boostStatButton.gameObject, "BoostAcc_label").GetComponent<UILabel>();
				component7.text = "+ " + gameData.getTextByRefId("smithStatsShort04");
				UISprite component8 = commonScreenObject.findChild(boostStatButton.gameObject, "Boosted_icon").GetComponent<UISprite>();
				int num3 = currProject.checkBoostPenalty(WeaponStat.WeaponStatAccuracy);
				if (num3 > 0)
				{
					component8.alpha = 1f;
					component8.GetComponentInChildren<UILabel>().text = num3.ToString();
				}
				else
				{
					component8.alpha = 0f;
				}
				break;
			}
			case "BoostMag_bg":
			{
				if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
				{
					boostStatButton.isEnabled = false;
					UILabel component = commonScreenObject.findChild(boostStatButton.gameObject, "BoostMag_label").GetComponent<UILabel>();
					component.text = "-";
					UISprite component2 = commonScreenObject.findChild(boostStatButton.gameObject, "Boosted_icon").GetComponent<UISprite>();
					component2.alpha = 0f;
					break;
				}
				UILabel component3 = commonScreenObject.findChild(boostStatButton.gameObject, "BoostMag_label").GetComponent<UILabel>();
				component3.text = "+ " + gameData.getTextByRefId("smithStatsShort05");
				UISprite component4 = commonScreenObject.findChild(boostStatButton.gameObject, "Boosted_icon").GetComponent<UISprite>();
				int num = currProject.checkBoostPenalty(WeaponStat.WeaponStatMagic);
				if (num > 0)
				{
					component4.alpha = 1f;
					component4.GetComponentInChildren<UILabel>().text = num.ToString();
				}
				else
				{
					component4.alpha = 0f;
				}
				break;
			}
			}
		}
	}

	public void clickBoostStatButton(WeaponStat stat)
	{
		if (currProject.checkCanBoost() && boostDrawerState == "EXTENDED" && !boostDrawerTween.enabled)
		{
			switch (stat)
			{
			case WeaponStat.WeaponStatAttack:
				shopMenuController.showForgeBoost1Design();
				break;
			case WeaponStat.WeaponStatSpeed:
				shopMenuController.showForgeBoost2Craft();
				break;
			case WeaponStat.WeaponStatAccuracy:
				shopMenuController.showForgeBoost3Polish();
				break;
			case WeaponStat.WeaponStatMagic:
				shopMenuController.showForgeBoost4Enchant();
				break;
			}
			updateBoostDrawer();
		}
	}

	public void updateWeaponDisplay()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		titleLabel.text = currProject.getProjectName(includePrefix: true);
		int projectProgressPercent = currProject.getProjectProgressPercent();
		percentLabel.text = projectProgressPercent + "%";
		percentSprite.fillAmount = (float)projectProgressPercent / 100f;
		imageTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + currProject.getProjectWeapon().getImage());
		popNumber(atkAddLabel, dispAtk, currProject.getAtk());
		popNumber(spdAddLabel, dispSpd, currProject.getSpd());
		popNumber(accAddLabel, dispAcc, currProject.getAcc());
		popNumber(magAddLabel, dispMag, currProject.getMag());
		dispAtk = currProject.getAtk();
		dispSpd = currProject.getSpd();
		dispAcc = currProject.getAcc();
		dispMag = currProject.getMag();
		atkLabel.text = dispAtk.ToString();
		spdLabel.text = dispSpd.ToString();
		accLabel.text = dispAcc.ToString();
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
		{
			magLabel.text = "-";
		}
		else
		{
			magLabel.text = dispMag.ToString();
		}
		if (projectProgressPercent > 20)
		{
			updatePriSecStats();
			if (isFirstForging)
			{
				shopMenuController.tryStartTutorial("FORGE");
			}
		}
		if (projectProgressPercent > 80 || projectProgressPercent < 10)
		{
			checkBoostStatus();
		}
		else if (isBoostAlert)
		{
			isBoostAlert = false;
			atkAlert.ResetToBeginning();
			atkAlert.enabled = false;
			spdAlert.ResetToBeginning();
			spdAlert.enabled = false;
			accAlert.ResetToBeginning();
			accAlert.enabled = false;
			magAlert.ResetToBeginning();
			magAlert.enabled = false;
		}
	}

	private void checkBoostStatus()
	{
		if (currProject.checkBoostPenalty(WeaponStat.WeaponStatAttack) == 0 && currProject.checkBoostPenalty(WeaponStat.WeaponStatSpeed) == 0 && currProject.checkBoostPenalty(WeaponStat.WeaponStatAccuracy) == 0 && currProject.checkBoostPenalty(WeaponStat.WeaponStatMagic) == 0 && !isBoostAlert)
		{
			commonScreenObject.tweenAlpha(atkAlert, 0f, 1f, 0.8f, null, string.Empty);
			commonScreenObject.tweenAlpha(spdAlert, 0f, 1f, 0.8f, null, string.Empty);
			commonScreenObject.tweenAlpha(accAlert, 0f, 1f, 0.8f, null, string.Empty);
			commonScreenObject.tweenAlpha(magAlert, 0f, 1f, 0.8f, null, string.Empty);
			isBoostAlert = true;
		}
		else if ((currProject.checkBoostPenalty(WeaponStat.WeaponStatAttack) > 0 || currProject.checkBoostPenalty(WeaponStat.WeaponStatSpeed) > 0 || currProject.checkBoostPenalty(WeaponStat.WeaponStatAccuracy) > 0 || currProject.checkBoostPenalty(WeaponStat.WeaponStatMagic) > 0) && isBoostAlert)
		{
			atkAlert.ResetToBeginning();
			atkAlert.enabled = false;
			spdAlert.ResetToBeginning();
			spdAlert.enabled = false;
			accAlert.ResetToBeginning();
			accAlert.enabled = false;
			magAlert.ResetToBeginning();
			magAlert.enabled = false;
		}
	}

	private void updatePriSecStats()
	{
		List<WeaponStat> priSecStat = currProject.getPriSecStat();
		if (priSecStat.Count >= 2)
		{
			WeaponStat weaponStat = priSecStat[0];
			WeaponStat weaponStat2 = priSecStat[1];
			if (weaponStat == WeaponStat.WeaponStatAttack)
			{
				atkLabel.effectStyle = UILabel.Effect.Outline;
				atkLabel.fontSize = 14;
				atkBg.color = new Color(0.0196f, 0.788f, 0.659f);
			}
			else if (weaponStat2 == WeaponStat.WeaponStatAttack)
			{
				atkLabel.effectStyle = UILabel.Effect.None;
				atkLabel.fontSize = 12;
				atkBg.color = new Color(0.0235f, 0.522f, 0.439f);
			}
			else
			{
				atkLabel.effectStyle = UILabel.Effect.None;
				atkLabel.fontSize = 12;
				atkBg.color = new Color(0.0235f, 0.157f, 0.196f);
			}
			if (weaponStat == WeaponStat.WeaponStatSpeed)
			{
				spdLabel.effectStyle = UILabel.Effect.Outline;
				spdLabel.fontSize = 14;
				spdBg.color = new Color(0.0196f, 0.788f, 0.659f);
			}
			else if (weaponStat2 == WeaponStat.WeaponStatSpeed)
			{
				spdLabel.effectStyle = UILabel.Effect.None;
				spdLabel.fontSize = 12;
				spdBg.color = new Color(0.0235f, 0.522f, 0.439f);
			}
			else
			{
				spdLabel.effectStyle = UILabel.Effect.None;
				spdLabel.fontSize = 12;
				spdBg.color = new Color(0.0235f, 0.157f, 0.196f);
			}
			if (weaponStat == WeaponStat.WeaponStatAccuracy)
			{
				accLabel.effectStyle = UILabel.Effect.Outline;
				accLabel.fontSize = 14;
				accBg.color = new Color(0.0196f, 0.788f, 0.659f);
			}
			else if (weaponStat2 == WeaponStat.WeaponStatAccuracy)
			{
				accLabel.effectStyle = UILabel.Effect.None;
				accLabel.fontSize = 12;
				accBg.color = new Color(0.0235f, 0.522f, 0.439f);
			}
			else
			{
				accLabel.effectStyle = UILabel.Effect.None;
				accLabel.fontSize = 12;
				accBg.color = new Color(0.0235f, 0.157f, 0.196f);
			}
			if (weaponStat == WeaponStat.WeaponStatMagic)
			{
				magLabel.effectStyle = UILabel.Effect.Outline;
				magLabel.fontSize = 14;
				magBg.color = new Color(0.0196f, 0.788f, 0.659f);
			}
			else if (weaponStat2 == WeaponStat.WeaponStatMagic)
			{
				magLabel.effectStyle = UILabel.Effect.None;
				magLabel.fontSize = 12;
				magBg.color = new Color(0.0235f, 0.522f, 0.439f);
			}
			else
			{
				magLabel.effectStyle = UILabel.Effect.None;
				magLabel.fontSize = 12;
				magBg.color = new Color(0.0235f, 0.157f, 0.196f);
			}
		}
	}

	public void updateContractDisplay()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		titleLabel.text = currProject.getProjectName(includePrefix: false);
		int projectProgressPercent = currProject.getProjectProgressPercent();
		percentLabel.text = projectProgressPercent.ToString();
		percentSprite.fillAmount = (float)projectProgressPercent / 100f;
		long playerTimeLong = player.getPlayerTimeLong();
		long contractTimeLeft = player.getCurrentProject().getContractTimeLeft(playerTimeLong);
		timeLabel.text = CommonAPI.convertHalfHoursToTimeString(contractTimeLeft, showHalfHours: false);
		popNumber(atkAddLabel, dispAtk, currProject.getAtk());
		popNumber(spdAddLabel, dispSpd, currProject.getSpd());
		popNumber(accAddLabel, dispAcc, currProject.getAcc());
		popNumber(magAddLabel, dispMag, currProject.getMag());
		dispAtk = currProject.getAtk();
		dispSpd = currProject.getSpd();
		dispAcc = currProject.getAcc();
		dispMag = currProject.getMag();
		atkLabel.text = dispAtk.ToString();
		spdLabel.text = dispSpd.ToString();
		accLabel.text = dispAcc.ToString();
		magLabel.text = dispMag.ToString();
		atkReqLabel.text = "/ " + currProject.getAtkReq();
		spdReqLabel.text = "/ " + currProject.getSpdReq();
		accReqLabel.text = "/ " + currProject.getAccReq();
		magReqLabel.text = "/ " + currProject.getMagReq();
	}

	public void popNumber(UILabel label, int prevValue, int currValue)
	{
		Vector3 aStartPosition = new Vector3(30f, 0f, 0f);
		Vector3 aEndPosition = new Vector3(45f, 0f, 0f);
		if (currProject.getProjectType() == ProjectType.ProjectTypeContract)
		{
			aStartPosition = new Vector3(60f, 0f, 0f);
			aEndPosition = new Vector3(75f, 0f, 0f);
		}
		if (prevValue > currValue)
		{
			label.text = "[FF4842]-" + (prevValue - currValue) + "[-]";
			commonScreenObject.tweenPosition(label.GetComponent<TweenPosition>(), aStartPosition, aEndPosition, 1f, null, string.Empty);
			commonScreenObject.tweenAlpha(label.GetComponent<TweenAlpha>(), 0f, 1f, 2f, null, string.Empty);
		}
		else if (prevValue < currValue)
		{
			label.text = "[56AE59]+" + (currValue - prevValue) + "[-]";
			commonScreenObject.tweenPosition(label.GetComponent<TweenPosition>(), aStartPosition, aEndPosition, 1f, null, string.Empty);
			commonScreenObject.tweenAlpha(label.GetComponent<TweenAlpha>(), 0f, 1f, 2f, null, string.Empty);
		}
	}

	public void moveFront()
	{
		updateWeaponDisplay();
		base.gameObject.GetComponent<UIPanel>().depth = 12;
		doDrawerLogic("CLOSED");
		terminateButton.isEnabled = false;
	}

	public void moveBack()
	{
		base.gameObject.GetComponent<UIPanel>().depth = 3;
		updateBoostDrawer();
		terminateButton.isEnabled = checkTerminateAllowed();
	}

	private bool checkTerminateAllowed()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "TERMINATE", completedTutorialIndex))
		{
			return true;
		}
		return false;
	}
}
