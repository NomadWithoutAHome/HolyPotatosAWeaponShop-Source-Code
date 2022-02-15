using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GUISmithSelectNewController : MonoBehaviour
{
	private PopEventType popEventType;

	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GameObject weaponBg;

	private UILabel weaponBoostTitleLabel;

	private UILabel weaponName;

	private UILabel weaponDesc;

	private UILabel weaponStatsLabel;

	private UILabel atkValue;

	private UILabel accValue;

	private UILabel spdValue;

	private UILabel magValue;

	private UITexture weaponImage;

	private List<Smith> smithSelectionList;

	private GameObject detailBg;

	private UISprite selectSmithBg;

	private UILabel selectSmithLabel;

	private UILabel totalLabel;

	private UILabel totalCost;

	private UIButton smithButtonLeft;

	private UIButton smithButtonRight;

	private UIButton startButton;

	private UIButton skipButton;

	private UILabel startLabel;

	private UILabel skipLabel;

	private UIGrid smithListGrid;

	private int selectedIndex;

	private Smith selectedSmith;

	private GameObject selectedSmithObject;

	private string smithNamePrefix;

	private Color transparentColor;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		weaponBg = commonScreenObject.findChild(base.gameObject, "WeaponBg").gameObject;
		weaponBoostTitleLabel = commonScreenObject.findChild(weaponBg, "WeaponBoost_title").GetComponent<UILabel>();
		weaponName = commonScreenObject.findChild(weaponBg, "WeaponDetail_bg/Weapon_name").GetComponent<UILabel>();
		weaponDesc = commonScreenObject.findChild(weaponBg, "WeaponDetail_bg/Weapon_desc").GetComponent<UILabel>();
		weaponStatsLabel = commonScreenObject.findChild(weaponBg, "WeaponStats_bg/WeaponStats_label").GetComponent<UILabel>();
		atkValue = commonScreenObject.findChild(weaponBg, "WeaponStats_bg/AtkReq/AtkValue").GetComponent<UILabel>();
		accValue = commonScreenObject.findChild(weaponBg, "WeaponStats_bg/AccReq/AccValue").GetComponent<UILabel>();
		spdValue = commonScreenObject.findChild(weaponBg, "WeaponStats_bg/SpdReq/SpdValue").GetComponent<UILabel>();
		magValue = commonScreenObject.findChild(weaponBg, "WeaponStats_bg/MagReq/MagValue").GetComponent<UILabel>();
		weaponImage = commonScreenObject.findChild(weaponBg, "Weapon_image").GetComponent<UITexture>();
		smithSelectionList = new List<Smith>();
		detailBg = commonScreenObject.findChild(base.gameObject, "DetailBg").gameObject;
		selectSmithBg = commonScreenObject.findChild(detailBg, "SelectSmithBg").GetComponent<UISprite>();
		selectSmithLabel = commonScreenObject.findChild(selectSmithBg.gameObject, "SelectSmithLabel").GetComponent<UILabel>();
		totalLabel = commonScreenObject.findChild(detailBg, "TotalLabel").GetComponent<UILabel>();
		totalCost = commonScreenObject.findChild(detailBg, "TotalCost").GetComponent<UILabel>();
		smithButtonLeft = commonScreenObject.findChild(detailBg, "SmithButton_left").GetComponent<UIButton>();
		smithButtonRight = commonScreenObject.findChild(detailBg, "SmithButton_right").GetComponent<UIButton>();
		startButton = commonScreenObject.findChild(detailBg, "StartButton").GetComponent<UIButton>();
		skipButton = commonScreenObject.findChild(detailBg, "SkipButton").GetComponent<UIButton>();
		startLabel = commonScreenObject.findChild(startButton.gameObject, "StartLabel").GetComponent<UILabel>();
		skipLabel = commonScreenObject.findChild(skipButton.gameObject, "SkipLabel").GetComponent<UILabel>();
		smithListGrid = commonScreenObject.findChild(detailBg, "SmithListPanel/SmithListGrid").GetComponent<UIGrid>();
		selectedIndex = -1;
		selectedSmith = null;
		selectedSmithObject = null;
		smithNamePrefix = "Smith_";
		transparentColor = Color.white;
		transparentColor.a = 0f;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "StartButton":
			switch (popEventType)
			{
			case PopEventType.PopEventTypeBoost1Design:
				shopMenuController.insertStoredInput("design", selectedIndex + 1);
				shopMenuController.startDesignBoost(sorted: true);
				break;
			case PopEventType.PopEventTypeBoost2Craft:
				shopMenuController.insertStoredInput("craft", selectedIndex + 1);
				shopMenuController.startCraftBoost(sorted: true);
				break;
			case PopEventType.PopEventTypeBoost3Polish:
				shopMenuController.insertStoredInput("polish", selectedIndex + 1);
				shopMenuController.startPolishBoost(sorted: true);
				break;
			case PopEventType.PopEventTypeBoost4Enchant:
				shopMenuController.insertStoredInput("enchant", selectedIndex + 1);
				shopMenuController.showForgeBoost4EnchantItemSelect(0, 100, noScroll: true);
				break;
			}
			viewController.closeSelectSmithNewPopup(doResume: false);
			break;
		case "SkipButton":
			switch (popEventType)
			{
			case PopEventType.PopEventTypeBoost1Design:
				shopMenuController.skipBoost(ProjectPhase.ProjectPhaseDesignDone);
				break;
			case PopEventType.PopEventTypeBoost2Craft:
				shopMenuController.skipBoost(ProjectPhase.ProjectPhaseCraftDone);
				break;
			case PopEventType.PopEventTypeBoost3Polish:
				shopMenuController.skipBoost(ProjectPhase.ProjectPhasePolishDone);
				break;
			case PopEventType.PopEventTypeBoost4Enchant:
				shopMenuController.skipBoost(ProjectPhase.ProjectPhaseEnchantDone);
				break;
			}
			viewController.closeSelectSmithNewPopup(doResume: true);
			break;
		case "SmithButton_left":
		{
			string[] array2 = selectedSmithObject.name.Split('_');
			int a2 = CommonAPI.parseInt(array2[1]) - 2;
			a2 = Mathf.Max(a2, 0);
			selectSmith(array2[0] + "_" + a2);
			break;
		}
		case "SmithButton_right":
		{
			string[] array = selectedSmithObject.name.Split('_');
			int a = CommonAPI.parseInt(array[1]) + 2;
			a = Mathf.Min(a, smithSelectionList.Count - 1);
			selectSmith(array[0] + "_" + a);
			break;
		}
		default:
			selectSmith(gameObjectName);
			break;
		}
	}

	private void reset()
	{
		GameData gameData = game.getGameData();
		startButton.isEnabled = false;
		skipButton.isEnabled = false;
		weaponBoostTitleLabel.text = string.Empty;
		weaponStatsLabel.text = gameData.getTextByRefId("weaponStats11");
		totalLabel.text = gameData.getTextByRefId("questSelect10");
		startLabel.text = gameData.getTextByRefId("questSelect11");
		skipLabel.text = "Skip";
		totalCost.text = "$ ---";
	}

	public void setReference(PopEventType aPopEventType)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		reset();
		Project currentProject = player.getCurrentProject();
		Weapon projectWeapon = currentProject.getProjectWeapon();
		weaponName.text = projectWeapon.getWeaponName();
		weaponDesc.text = projectWeapon.getWeaponDesc();
		atkValue.text = currentProject.getAtk().ToString();
		accValue.text = currentProject.getAcc().ToString();
		spdValue.text = currentProject.getSpd().ToString();
		magValue.text = currentProject.getMag().ToString();
		weaponImage.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + projectWeapon.getImage());
		popEventType = aPopEventType;
		smithSelectionList = new List<Smith>();
		bool flag = true;
		switch (popEventType)
		{
		case PopEventType.PopEventTypeBoost1Design:
			weaponBoostTitleLabel.text = gameData.getTextByRefId("projectProgress02");
			selectSmithBg.spriteName = "title_design";
			selectSmithLabel.text = gameData.getTextByRefId("menuForgeBoost35");
			smithSelectionList.AddRange(player.getDesignSmithArray(includeAway: false));
			if (smithSelectionList.Count == 0)
			{
				flag = false;
			}
			smithSelectionList = smithSelectionList.OrderByDescending((Smith x) => x.getSmithPower()).ToList();
			smithSelectionList.AddRange(gameData.getOutsourceSmithList("DESIGN", player.getShopLevelInt(), player.getPlayerGold()));
			break;
		case PopEventType.PopEventTypeBoost2Craft:
			weaponBoostTitleLabel.text = gameData.getTextByRefId("projectProgress03");
			selectSmithBg.spriteName = "title_craft";
			selectSmithLabel.text = gameData.getTextByRefId("menuForgeBoost36");
			smithSelectionList.AddRange(player.getCraftSmithArray(includeAway: false));
			if (smithSelectionList.Count == 0)
			{
				flag = false;
			}
			smithSelectionList = smithSelectionList.OrderByDescending((Smith x) => x.getSmithIntelligence()).ToList();
			smithSelectionList.AddRange(gameData.getOutsourceSmithList("CRAFT", player.getShopLevelInt(), player.getPlayerGold()));
			break;
		case PopEventType.PopEventTypeBoost3Polish:
			weaponBoostTitleLabel.text = gameData.getTextByRefId("projectProgress04");
			selectSmithBg.spriteName = "title_polish";
			selectSmithLabel.text = gameData.getTextByRefId("menuForgeBoost37");
			smithSelectionList.AddRange(player.getPolishSmithArray(includeAway: false));
			if (smithSelectionList.Count == 0)
			{
				flag = false;
			}
			smithSelectionList = smithSelectionList.OrderByDescending((Smith x) => x.getSmithTechnique()).ToList();
			smithSelectionList.AddRange(gameData.getOutsourceSmithList("POLISH", player.getShopLevelInt(), player.getPlayerGold()));
			break;
		case PopEventType.PopEventTypeBoost4Enchant:
			weaponBoostTitleLabel.text = gameData.getTextByRefId("projectProgress05");
			selectSmithBg.spriteName = "title_enhance";
			selectSmithLabel.text = gameData.getTextByRefId("menuForgeBoost38");
			smithSelectionList.AddRange(player.getEnchantSmithArray(includeAway: false));
			if (smithSelectionList.Count == 0)
			{
				flag = false;
			}
			smithSelectionList = smithSelectionList.OrderByDescending((Smith x) => x.getSmithLuck()).ToList();
			smithSelectionList.AddRange(gameData.getOutsourceSmithList("ENCHANT", player.getShopLevelInt(), player.getPlayerGold()));
			break;
		}
		if (flag)
		{
			skipButton.isEnabled = false;
		}
		else
		{
			skipButton.isEnabled = true;
		}
		int num = 0;
		foreach (Smith smithSelection in smithSelectionList)
		{
			GameObject aObject = commonScreenObject.createPrefab(smithListGrid.gameObject, smithNamePrefix + num, "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/SmithSelectNEW", Vector3.zero, Vector3.one, Vector3.zero);
			if (smithSelection.checkOutsource())
			{
				commonScreenObject.findChild(aObject, "OutsourceFrame/OutsourceLabel").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("smithStats08", "[stat]", string.Empty);
				commonScreenObject.findChild(aObject, "OutsourceFrame/OutsourceCost").GetComponent<UILabel>().text = smithSelection.getSmithHireCost().ToString();
			}
			else
			{
				commonScreenObject.findChild(aObject, "OutsourceFrame").gameObject.SetActive(value: false);
			}
			commonScreenObject.findChild(aObject, "SmithStatBg/SmithName").GetComponent<UILabel>().text = smithSelection.getSmithName();
			commonScreenObject.findChild(aObject, "SmithStatBg/SmithLevel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort01") + " " + smithSelection.getSmithLevel();
			commonScreenObject.findChild(aObject, "SmithStatBg/SmithExpBar").GetComponent<UISprite>().fillAmount = (float)smithSelection.getSmithExp() / (float)smithSelection.getMaxExp();
			commonScreenObject.findChild(aObject, "SmithSelectBg/SmithImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smithSelection.getImage());
			switch (popEventType)
			{
			case PopEventType.PopEventTypeBoost1Design:
				commonScreenObject.findChild(aObject, "SmithStatBg/StatsIcon").GetComponent<UISprite>().spriteName = "ico_atk";
				commonScreenObject.findChild(aObject, "SmithStatBg/SmithStats").GetComponent<UILabel>().text = smithSelection.getSmithIntelligence().ToString();
				break;
			case PopEventType.PopEventTypeBoost2Craft:
				commonScreenObject.findChild(aObject, "SmithStatBg/StatsIcon").GetComponent<UISprite>().spriteName = "ico_speed";
				commonScreenObject.findChild(aObject, "SmithStatBg/SmithStats").GetComponent<UILabel>().text = smithSelection.getSmithPower().ToString();
				break;
			case PopEventType.PopEventTypeBoost3Polish:
				commonScreenObject.findChild(aObject, "SmithStatBg/StatsIcon").GetComponent<UISprite>().spriteName = "ico_acc";
				commonScreenObject.findChild(aObject, "SmithStatBg/SmithStats").GetComponent<UILabel>().text = smithSelection.getSmithTechnique().ToString();
				break;
			case PopEventType.PopEventTypeBoost4Enchant:
				commonScreenObject.findChild(aObject, "SmithStatBg/StatsIcon").GetComponent<UISprite>().spriteName = "ico_enh";
				commonScreenObject.findChild(aObject, "SmithStatBg/SmithStats").GetComponent<UILabel>().text = smithSelection.getSmithLuck().ToString();
				break;
			}
			commonScreenObject.findChild(aObject, "SelectFrame").GetComponent<UISprite>().enabled = false;
			num++;
		}
		selectSmith(smithNamePrefix + "0");
	}

	private void selectSmith(string gameObjectName)
	{
		if (selectedIndex != -1 && selectedSmithObject != null)
		{
			commonScreenObject.findChild(selectedSmithObject, "SelectFrame").GetComponent<UISprite>().enabled = false;
		}
		string[] array = gameObjectName.Split('_');
		selectedIndex = CommonAPI.parseInt(array[1]);
		selectedSmith = smithSelectionList[selectedIndex];
		selectedSmithObject = GameObject.Find(gameObjectName);
		commonScreenObject.findChild(selectedSmithObject, "SelectFrame").GetComponent<UISprite>().enabled = true;
		if (selectedSmith.checkOutsource())
		{
			totalCost.text = "$" + selectedSmith.getSmithHireCost();
		}
		else
		{
			totalCost.text = "$0";
		}
		startButton.isEnabled = true;
		selectedSmithObject.GetComponent<UICenterOnSelect>().centerOnThis();
		if (selectedIndex == 0)
		{
			smithButtonLeft.isEnabled = false;
		}
		else
		{
			smithButtonLeft.isEnabled = true;
		}
		if (selectedIndex == smithSelectionList.Count - 1)
		{
			smithButtonRight.isEnabled = false;
		}
		else
		{
			smithButtonRight.isEnabled = true;
		}
	}
}
