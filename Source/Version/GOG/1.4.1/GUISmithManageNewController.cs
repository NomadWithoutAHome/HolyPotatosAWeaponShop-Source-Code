using System.Globalization;
using UnityEngine;

public class GUISmithManageNewController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Smith smithInfo;

	private SmithStation currRole;

	private GameObject smithManageBg;

	private GameObject innerCircle;

	private UIButton innerCircleButton;

	private UITexture smithImg;

	private GameObject smithStatsFrame;

	private UISprite expBar;

	private UILabel smithName;

	private UILabel smithLevel;

	private UILabel smithRole;

	private string actionRefID;

	private UISprite smithActivity;

	private UILabel smithSalary;

	private UILabel perMonthLabel;

	private UILabel staLabel;

	private UISprite staBar;

	private UILabel powerValue;

	private UILabel intValue;

	private UILabel techValue;

	private UILabel luckValue;

	private UILabel smithMood;

	private UIButton auto_Button;

	private UILabel autoLabel;

	private UILabel autoOnOffLabel;

	private UIButton design_Button;

	private UIButton craft_Button;

	private UIButton polish_Button;

	private UIButton enchant_Button;

	private UIButton changeJob_Button;

	private UIButton fireSmith_Button;

	private UIButton learnTags_Button;

	private UIButton trainSmith_Button;

	private bool isSwapAnimating;

	private string currentPage;

	private Vector3 topLeftButtonPosition;

	private Vector3 topRightButtonPosition;

	private Vector3 bottomLeftButtonPosition;

	private Vector3 bottomRightButtonPosition;

	private Vector3 bottomCenterButtonPosition;

	private Vector3 changeJobButtonPosition;

	private Vector3 learnTagButtonPosition;

	private Vector3 trainSmithButtonPosition;

	private Vector3 fireSmithButtonPosition;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		GameData gameData = game.getGameData();
		smithManageBg = commonScreenObject.findChild(base.gameObject, "SmithManageBg").gameObject;
		innerCircle = commonScreenObject.findChild(base.gameObject, "InnerCircle").gameObject;
		innerCircleButton = innerCircle.GetComponent<UIButton>();
		smithImg = commonScreenObject.findChild(innerCircle, "SmithImg").GetComponent<UITexture>();
		smithStatsFrame = commonScreenObject.findChild(innerCircle, "SmithStatsFrame").gameObject;
		expBar = commonScreenObject.findChild(smithStatsFrame, "ExpBar").GetComponent<UISprite>();
		smithName = commonScreenObject.findChild(smithStatsFrame, "SmithName").GetComponent<UILabel>();
		smithLevel = commonScreenObject.findChild(smithStatsFrame, "SmithLevel").GetComponent<UILabel>();
		smithRole = commonScreenObject.findChild(smithStatsFrame, "SmithRole").GetComponent<UILabel>();
		smithActivity = commonScreenObject.findChild(innerCircle, "ActivityBubble/SmithActivity").GetComponent<UISprite>();
		smithSalary = commonScreenObject.findChild(smithStatsFrame, "SmithSalary").GetComponent<UILabel>();
		perMonthLabel = commonScreenObject.findChild(smithStatsFrame, "PerMonthLabel").GetComponent<UILabel>();
		smithMood = commonScreenObject.findChild(smithStatsFrame, "SmithMood_bg/SmithMood_label").GetComponent<UILabel>();
		staLabel = commonScreenObject.findChild(smithStatsFrame, "StaLabel").GetComponent<UILabel>();
		staBar = commonScreenObject.findChild(smithStatsFrame, "StaBar").GetComponent<UISprite>();
		powerValue = commonScreenObject.findChild(smithStatsFrame, "SmithStats/PowValue").GetComponent<UILabel>();
		intValue = commonScreenObject.findChild(smithStatsFrame, "SmithStats/IntValue").GetComponent<UILabel>();
		techValue = commonScreenObject.findChild(smithStatsFrame, "SmithStats/TechValue").GetComponent<UILabel>();
		luckValue = commonScreenObject.findChild(smithStatsFrame, "SmithStats/LuckValue").GetComponent<UILabel>();
		auto_Button = commonScreenObject.findChild(base.gameObject, "SmithAssignButtons/Auto_Button").GetComponent<UIButton>();
		autoLabel = commonScreenObject.findChild(auto_Button.gameObject, "AutoLabel").GetComponent<UILabel>();
		autoOnOffLabel = commonScreenObject.findChild(auto_Button.gameObject, "AutoOnOffLabel").GetComponent<UILabel>();
		design_Button = commonScreenObject.findChild(base.gameObject, "SmithAssignButtons/Design_Button").GetComponent<UIButton>();
		craft_Button = commonScreenObject.findChild(base.gameObject, "SmithAssignButtons/Craft_Button").GetComponent<UIButton>();
		polish_Button = commonScreenObject.findChild(base.gameObject, "SmithAssignButtons/Polish_Button").GetComponent<UIButton>();
		enchant_Button = commonScreenObject.findChild(base.gameObject, "SmithAssignButtons/Enchant_Button").GetComponent<UIButton>();
		changeJob_Button = commonScreenObject.findChild(base.gameObject, "SmithManageButtons/ChangeJob_Button").GetComponent<UIButton>();
		changeJob_Button.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmithManagement37").ToUpper(CultureInfo.InvariantCulture);
		fireSmith_Button = commonScreenObject.findChild(base.gameObject, "SmithManageButtons/FireSmith_Button").GetComponent<UIButton>();
		fireSmith_Button.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmith03").ToUpper(CultureInfo.InvariantCulture);
		learnTags_Button = commonScreenObject.findChild(base.gameObject, "SmithManageButtons/LearnTags_Button").GetComponent<UIButton>();
		learnTags_Button.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmith08").ToUpper(CultureInfo.InvariantCulture);
		trainSmith_Button = commonScreenObject.findChild(base.gameObject, "SmithManageButtons/TrainSmith_Button").GetComponent<UIButton>();
		trainSmith_Button.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmith01").ToUpper(CultureInfo.InvariantCulture);
		isSwapAnimating = false;
		currentPage = "ASSIGN";
		topLeftButtonPosition = new Vector3(-149f, 60f, 0f);
		topRightButtonPosition = new Vector3(149f, 60f, 0f);
		bottomLeftButtonPosition = new Vector3(-149f, -60f, 0f);
		bottomRightButtonPosition = new Vector3(149f, -60f, 0f);
		bottomCenterButtonPosition = new Vector3(0f, -80f, 0f);
		changeJobButtonPosition = new Vector3(-160f, 60f, 0f);
		learnTagButtonPosition = new Vector3(160f, 60f, 0f);
		trainSmithButtonPosition = new Vector3(-180f, -30f, 0f);
		fireSmithButtonPosition = new Vector3(180f, -30f, 0f);
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "InnerCircle":
			swapButtonSet();
			return;
		case "ChangeJob_Button":
			viewController.closeSmithManagePopupNew(resume: false);
			viewController.showSmithJobChange(smithInfo);
			return;
		case "FireSmith_Button":
			return;
		case "LearnTags_Button":
			return;
		case "TrainSmith_Button":
			if (smithInfo.checkIsSmithMaxLevel())
			{
				GameData gameData = game.getGameData();
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefIdWithDynText("menuTraining04", "[smithName]", smithInfo.getSmithName()), null, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
			else
			{
				viewController.closeSmithManagePopupNew();
				viewController.showSmithTrainingPopup(smithInfo);
			}
			return;
		}
		Player player = game.getPlayer();
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "Auto":
			currRole = SmithStation.SmithStationAuto;
			shopMenuController.doSmithReassign(smithInfo, currRole);
			viewController.closeSmithManagePopupNew();
			break;
		case "Design":
			currRole = SmithStation.SmithStationDesign;
			shopMenuController.doSmithReassign(smithInfo, currRole);
			viewController.closeSmithManagePopupNew();
			break;
		case "Craft":
			currRole = SmithStation.SmithStationCraft;
			shopMenuController.doSmithReassign(smithInfo, currRole);
			viewController.closeSmithManagePopupNew();
			break;
		case "Polish":
			currRole = SmithStation.SmithStationPolish;
			shopMenuController.doSmithReassign(smithInfo, currRole);
			viewController.closeSmithManagePopupNew();
			break;
		case "Enchant":
			currRole = SmithStation.SmithStationEnchant;
			shopMenuController.doSmithReassign(smithInfo, currRole);
			viewController.closeSmithManagePopupNew();
			break;
		}
	}

	private void resetMenuButtons()
	{
		GameData gameData = game.getGameData();
		perMonthLabel.text = gameData.getTextByRefId("smithStatsShort07");
		auto_Button.isEnabled = true;
		auto_Button.GetComponent<UISprite>().spriteName = "bt_autooff";
		autoOnOffLabel.text = "OFF";
		design_Button.isEnabled = true;
		craft_Button.isEnabled = true;
		polish_Button.isEnabled = true;
		enchant_Button.isEnabled = true;
		changeJob_Button.isEnabled = true;
		changeJob_Button.transform.localPosition = Vector3.zero;
		changeJob_Button.transform.localScale = Vector3.zero;
		fireSmith_Button.isEnabled = true;
		fireSmith_Button.transform.localPosition = Vector3.zero;
		fireSmith_Button.transform.localScale = Vector3.zero;
		learnTags_Button.isEnabled = true;
		learnTags_Button.transform.localPosition = Vector3.zero;
		learnTags_Button.transform.localScale = Vector3.zero;
		trainSmith_Button.isEnabled = true;
		trainSmith_Button.transform.localPosition = Vector3.zero;
		trainSmith_Button.transform.localScale = Vector3.zero;
	}

	public virtual void setReference(Smith aSmith)
	{
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		smithInfo = aSmith;
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smithInfo.getImage() + "_manage");
		smithMood.text = CommonAPI.getMoodString(smithInfo.getMoodState(), showDesc: false);
		expBar.fillAmount = (float)smithInfo.getSmithExp() / (float)smithInfo.getMaxExp();
		smithName.text = smithInfo.getSmithName();
		smithLevel.text = smithInfo.getSmithLevel().ToString();
		smithRole.text = smithInfo.getSmithJob().getSmithJobName();
		smithSalary.text = smithInfo.getSmithSalary().ToString();
		actionRefID = smithInfo.getSmithAction().getRefId();
		switch (actionRefID)
		{
		case "101":
			smithActivity.spriteName = "smith_bored";
			break;
		case "102":
			smithActivity.spriteName = "smith_craft";
			break;
		case "105":
		case "201":
			smithActivity.spriteName = "smith_daydream";
			break;
		case "202":
			smithActivity.spriteName = "smith_coffee";
			break;
		case "203":
			smithActivity.spriteName = "smith_innerchi";
			break;
		case "301":
		case "303":
		case "304":
			smithActivity.spriteName = "smith_shiver";
			break;
		}
		staBar.fillAmount = smithInfo.getRemainingMood() / smithInfo.getSmithMaxMood();
		powerValue.text = CommonAPI.generateSmithStatsTextColorDisplay("000000", smithInfo.getSmithAddedPower(), smithInfo.getSmithPower(), isPositiveGreen: true);
		intValue.text = CommonAPI.generateSmithStatsTextColorDisplay("000000", smithInfo.getSmithAddedIntelligence(), smithInfo.getSmithIntelligence(), isPositiveGreen: true);
		techValue.text = CommonAPI.generateSmithStatsTextColorDisplay("000000", smithInfo.getSmithAddedTechnique(), smithInfo.getSmithTechnique(), isPositiveGreen: true);
		luckValue.text = CommonAPI.generateSmithStatsTextColorDisplay("000000", smithInfo.getSmithAddedLuck(), smithInfo.getSmithLuck(), isPositiveGreen: true);
		currRole = smithInfo.getAssignedRole();
		checkMenuButtons();
		commonScreenObject.tweenScale(smithManageBg.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		commonScreenObject.tweenScale(innerCircle.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		showStationAssignPage(0.25f);
	}

	private void swapButtonSet()
	{
		switch (currentPage)
		{
		case "ASSIGN":
			hideStationAssignPage(0f);
			showSmithManagePage(0.3f);
			currentPage = "MANAGE";
			break;
		case "MANAGE":
			hideSmithManagePage(0f);
			showStationAssignPage(0.3f);
			currentPage = "ASSIGN";
			break;
		}
	}

	public void endSwapAnimation()
	{
		innerCircleButton.isEnabled = true;
	}

	private void showStationAssignPage(float baseDelay)
	{
		auto_Button.GetComponent<TweenPosition>().delay = baseDelay;
		auto_Button.GetComponent<TweenScale>().delay = baseDelay;
		commonScreenObject.tweenPosition(auto_Button.GetComponent<TweenPosition>(), Vector3.zero, bottomCenterButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(auto_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		design_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.05f;
		design_Button.GetComponent<TweenScale>().delay = baseDelay + 0.05f;
		commonScreenObject.tweenPosition(design_Button.GetComponent<TweenPosition>(), Vector3.zero, topLeftButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(design_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		polish_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.075f;
		polish_Button.GetComponent<TweenScale>().delay = baseDelay + 0.075f;
		commonScreenObject.tweenPosition(polish_Button.GetComponent<TweenPosition>(), Vector3.zero, bottomLeftButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(polish_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		craft_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.1f;
		craft_Button.GetComponent<TweenScale>().delay = baseDelay + 0.1f;
		commonScreenObject.tweenPosition(craft_Button.GetComponent<TweenPosition>(), Vector3.zero, topRightButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(craft_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		enchant_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.125f;
		enchant_Button.GetComponent<TweenScale>().delay = baseDelay + 0.125f;
		commonScreenObject.tweenPosition(enchant_Button.GetComponent<TweenPosition>(), Vector3.zero, bottomRightButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(enchant_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, base.gameObject, "endSwapAnimation");
		innerCircleButton.isEnabled = false;
	}

	private void hideStationAssignPage(float baseDelay)
	{
		auto_Button.GetComponent<TweenPosition>().delay = baseDelay;
		auto_Button.GetComponent<TweenScale>().delay = baseDelay;
		commonScreenObject.tweenPosition(auto_Button.GetComponent<TweenPosition>(), Vector3.zero, bottomCenterButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(auto_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		design_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.05f;
		design_Button.GetComponent<TweenScale>().delay = baseDelay + 0.05f;
		commonScreenObject.tweenPosition(design_Button.GetComponent<TweenPosition>(), Vector3.zero, topLeftButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(design_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		polish_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.075f;
		polish_Button.GetComponent<TweenScale>().delay = baseDelay + 0.075f;
		commonScreenObject.tweenPosition(polish_Button.GetComponent<TweenPosition>(), Vector3.zero, bottomLeftButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(polish_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		craft_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.1f;
		craft_Button.GetComponent<TweenScale>().delay = baseDelay + 0.1f;
		commonScreenObject.tweenPosition(craft_Button.GetComponent<TweenPosition>(), Vector3.zero, topRightButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(craft_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		enchant_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.125f;
		enchant_Button.GetComponent<TweenScale>().delay = baseDelay + 0.125f;
		commonScreenObject.tweenPosition(enchant_Button.GetComponent<TweenPosition>(), Vector3.zero, bottomRightButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(enchant_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
	}

	private void showSmithManagePage(float baseDelay)
	{
		changeJob_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.05f;
		changeJob_Button.GetComponent<TweenScale>().delay = baseDelay + 0.05f;
		commonScreenObject.tweenPosition(changeJob_Button.GetComponent<TweenPosition>(), Vector3.zero, changeJobButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(changeJob_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		trainSmith_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.075f;
		trainSmith_Button.GetComponent<TweenScale>().delay = baseDelay + 0.075f;
		commonScreenObject.tweenPosition(trainSmith_Button.GetComponent<TweenPosition>(), Vector3.zero, trainSmithButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(trainSmith_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		learnTags_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.1f;
		learnTags_Button.GetComponent<TweenScale>().delay = baseDelay + 0.1f;
		commonScreenObject.tweenPosition(learnTags_Button.GetComponent<TweenPosition>(), Vector3.zero, learnTagButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(learnTags_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty);
		fireSmith_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.125f;
		fireSmith_Button.GetComponent<TweenScale>().delay = baseDelay + 0.125f;
		commonScreenObject.tweenPosition(fireSmith_Button.GetComponent<TweenPosition>(), Vector3.zero, fireSmithButtonPosition, 0.3f, null, string.Empty);
		commonScreenObject.tweenScale(fireSmith_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, base.gameObject, "endSwapAnimation");
		innerCircleButton.isEnabled = false;
	}

	private void hideSmithManagePage(float baseDelay)
	{
		changeJob_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.05f;
		changeJob_Button.GetComponent<TweenScale>().delay = baseDelay + 0.05f;
		commonScreenObject.tweenPosition(changeJob_Button.GetComponent<TweenPosition>(), Vector3.zero, changeJobButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(changeJob_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		trainSmith_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.075f;
		trainSmith_Button.GetComponent<TweenScale>().delay = baseDelay + 0.075f;
		commonScreenObject.tweenPosition(trainSmith_Button.GetComponent<TweenPosition>(), Vector3.zero, trainSmithButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(trainSmith_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		learnTags_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.1f;
		learnTags_Button.GetComponent<TweenScale>().delay = baseDelay + 0.1f;
		commonScreenObject.tweenPosition(learnTags_Button.GetComponent<TweenPosition>(), Vector3.zero, learnTagButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(learnTags_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
		fireSmith_Button.GetComponent<TweenPosition>().delay = baseDelay + 0.125f;
		fireSmith_Button.GetComponent<TweenScale>().delay = baseDelay + 0.125f;
		commonScreenObject.tweenPosition(fireSmith_Button.GetComponent<TweenPosition>(), Vector3.zero, fireSmithButtonPosition, 0.3f, null, string.Empty, isPlayForwards: false);
		commonScreenObject.tweenScale(fireSmith_Button.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.3f, null, string.Empty, isPlayForwards: false);
	}

	public void checkMenuButtons()
	{
		resetMenuButtons();
		if (shopMenuController.checkSpaceAvailable(SmithStation.SmithStationDesign) == 0)
		{
			design_Button.isEnabled = false;
		}
		if (shopMenuController.checkSpaceAvailable(SmithStation.SmithStationCraft) == 0)
		{
			craft_Button.isEnabled = false;
		}
		if (shopMenuController.checkSpaceAvailable(SmithStation.SmithStationPolish) == 0)
		{
			polish_Button.isEnabled = false;
		}
		if (shopMenuController.checkSpaceAvailable(SmithStation.SmithStationEnchant) == 0)
		{
			enchant_Button.isEnabled = false;
		}
		switch (currRole)
		{
		case SmithStation.SmithStationAuto:
			auto_Button.disabledColor = Color.white;
			auto_Button.isEnabled = false;
			auto_Button.GetComponent<UISprite>().spriteName = "bt_autoon";
			autoOnOffLabel.text = "ON";
			break;
		case SmithStation.SmithStationDesign:
			design_Button.isEnabled = false;
			break;
		case SmithStation.SmithStationCraft:
			craft_Button.isEnabled = false;
			break;
		case SmithStation.SmithStationPolish:
			polish_Button.isEnabled = false;
			break;
		case SmithStation.SmithStationEnchant:
			enchant_Button.isEnabled = false;
			break;
		}
	}

	public void setSmithInfo(Smith aSmith)
	{
		smithInfo = aSmith;
	}
}
