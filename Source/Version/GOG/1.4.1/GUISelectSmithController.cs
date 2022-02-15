using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISelectSmithController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private List<Smith> displayList;

	private List<Smith> inHouseSmithList;

	private List<Smith> outsourceSmithList;

	private List<Smith> awaySmithList;

	private string selectedSmithRefId;

	private UIButton closeButton;

	private UIButton selectButton;

	private UIButton skipButton;

	private UIGrid smithSelectGrid;

	private UIScrollBar smithSelectScroll;

	private SmithSelectPopupType popupType;

	private UILabel titleLabel;

	private UITexture banner;

	private UILabel messageLabel;

	private List<BoxCollider> statTooltipList;

	private GameObject weaponDetailsDrawer;

	private bool weaponDetailsDrawerIsOpen;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
		selectButton = commonScreenObject.findChild(base.gameObject, "Select_button").GetComponent<UIButton>();
		skipButton = commonScreenObject.findChild(base.gameObject, "Skip_button").GetComponent<UIButton>();
		smithSelectGrid = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_panel/SmithList_grid").GetComponent<UIGrid>();
		smithSelectScroll = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_scrollbar").GetComponent<UIScrollBar>();
		popupType = SmithSelectPopupType.SmithSelectPopupTypeBlank;
		titleLabel = commonScreenObject.findChild(base.gameObject, "SelectSmith_bg/SelectSmithTitle_bg/SelectSmithTitle_label").GetComponent<UILabel>();
		banner = commonScreenObject.findChild(base.gameObject, "SelectSmithBanner_texture").GetComponent<UITexture>();
		messageLabel = commonScreenObject.findChild(base.gameObject, "SelectSmithMsg_label").GetComponent<UILabel>();
		statTooltipList = new List<BoxCollider>();
		weaponDetailsDrawer = null;
		weaponDetailsDrawerIsOpen = false;
	}

	public void processClick(string gameobjectName)
	{
		switch (gameobjectName)
		{
		case "Close_button":
			viewController.closeSelectSmithNewPopup(doResume: true);
			break;
		case "DrawerTab_bg":
			openCloseWeaponDetails();
			break;
		case "Skip_button":
			viewController.closeSelectSmithNewPopup(doResume: true);
			break;
		case "Select_button":
			setSelectedSmith();
			switch (popupType)
			{
			case SmithSelectPopupType.SmithSelectPopupTypeBoostDesign:
				shopMenuController.startDesignBoost(sorted: true);
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostCraft:
				shopMenuController.startCraftBoost(sorted: true);
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostPolish:
				shopMenuController.startPolishBoost(sorted: true);
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant:
				shopMenuController.startEnchantBoost(sorted: true);
				break;
			}
			viewController.closeSelectSmithNewPopup(doResume: false);
			break;
		default:
		{
			string[] array = gameobjectName.Split('_');
			switch (array[0])
			{
			case "SmithList1":
				updateDisplaySmith(gameobjectName, array[2]);
				break;
			case "SmithList2":
				updateDisplaySmith(gameobjectName, array[2]);
				break;
			}
			break;
		}
		}
	}

	public void processHover(bool isOver, GameObject hoverObj)
	{
		string text = hoverObj.name;
		if (isOver)
		{
			if (text != null && text == "SmithMood_texture")
			{
				GameObject gameObject = hoverObj.transform.parent.transform.parent.gameObject;
				string[] array = gameObject.name.Split('_');
				Smith smithByRefId = game.getGameData().getSmithByRefId(array[2]);
				CommonAPI.debug(array[2] + " SmithMood_texture " + smithByRefId.getSmithName());
				if (smithByRefId.getSmithRefId() != string.Empty)
				{
					tooltipScript.showText(CommonAPI.getMoodString(smithByRefId.getMoodState(), showDesc: true));
				}
				else if (array[0] == "SmithList2")
				{
					tooltipScript.showText(CommonAPI.getMoodString(SmithMood.SmithMoodNormal, showDesc: true));
				}
				return;
			}
			string[] array2 = text.Split('_');
			if (array2[0] == "SmithList1" || array2[0] == "SmithList2" || array2[0] == "SmithList3")
			{
				Smith smithByRefId2 = game.getGameData().getSmithByRefId(array2[2]);
				if (smithByRefId2.getSmithRefId() != string.Empty)
				{
					tooltipScript.showText(smithByRefId2.getSmithStandardInfoString(showFullJobDetails: false));
				}
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
		if (selectButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Select_button");
		}
		else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	public void setReference(SmithSelectPopupType aPopupType)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Project currentProject = player.getCurrentProject();
		popupType = aPopupType;
		skipButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral14");
		skipButton.isEnabled = false;
		switch (popupType)
		{
		case SmithSelectPopupType.SmithSelectPopupTypeBoostDesign:
			displayList = player.getDesignSmithArray(includeAway: true);
			displayList.AddRange(gameData.getOutsourceSmithList("DESIGN", player.getShopLevelInt(), player.getPlayerGold()));
			titleLabel.text = gameData.getTextByRefId("menuForgeBoost04").ToUpper(CultureInfo.InvariantCulture);
			banner.mainTexture = commonScreenObject.loadTexture("Image/banner/DESIGN");
			messageLabel.text = gameData.getTextByRefId("menuForgeBoost01");
			if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatAttack) > 0)
			{
				UILabel uILabel3 = messageLabel;
				uILabel3.text = uILabel3.text + "\n[E54242]" + gameData.getTextByRefId("menuForgeBoost45") + "[-]";
			}
			selectButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuForgeBoost05");
			sortSmithsForBoost();
			displayBoostSmiths();
			showWeaponDetails();
			break;
		case SmithSelectPopupType.SmithSelectPopupTypeBoostCraft:
			displayList = player.getCraftSmithArray(includeAway: true);
			displayList.AddRange(gameData.getOutsourceSmithList("CRAFT", player.getShopLevelInt(), player.getPlayerGold()));
			titleLabel.text = gameData.getTextByRefId("menuForgeBoost15").ToUpper(CultureInfo.InvariantCulture);
			banner.mainTexture = commonScreenObject.loadTexture("Image/banner/CRAFT");
			messageLabel.text = gameData.getTextByRefId("menuForgeBoost11");
			if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatSpeed) > 0)
			{
				UILabel uILabel2 = messageLabel;
				uILabel2.text = uILabel2.text + "\n[E54242]" + gameData.getTextByRefId("menuForgeBoost45") + "[-]";
			}
			selectButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuForgeBoost14");
			sortSmithsForBoost();
			displayBoostSmiths();
			showWeaponDetails();
			break;
		case SmithSelectPopupType.SmithSelectPopupTypeBoostPolish:
			displayList = player.getPolishSmithArray(includeAway: true);
			displayList.AddRange(gameData.getOutsourceSmithList("POLISH", player.getShopLevelInt(), player.getPlayerGold()));
			titleLabel.text = gameData.getTextByRefId("menuForgeBoost21").ToUpper(CultureInfo.InvariantCulture);
			banner.mainTexture = commonScreenObject.loadTexture("Image/banner/POLISH");
			messageLabel.text = gameData.getTextByRefId("menuForgeBoost17");
			if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatAccuracy) > 0)
			{
				UILabel uILabel4 = messageLabel;
				uILabel4.text = uILabel4.text + "\n[E54242]" + gameData.getTextByRefId("menuForgeBoost45") + "[-]";
			}
			selectButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuForgeBoost20");
			sortSmithsForBoost();
			displayBoostSmiths();
			showWeaponDetails();
			break;
		case SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant:
			displayList = player.getEnchantSmithArray(includeAway: true);
			displayList.AddRange(gameData.getOutsourceSmithList("ENCHANT", player.getShopLevelInt(), player.getPlayerGold()));
			titleLabel.text = gameData.getTextByRefId("menuForgeBoost40").ToUpper(CultureInfo.InvariantCulture);
			banner.mainTexture = commonScreenObject.loadTexture("Image/banner/ENCHANT");
			messageLabel.text = gameData.getTextByRefId("menuForgeBoost23");
			if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatMagic) > 0)
			{
				UILabel uILabel = messageLabel;
				uILabel.text = uILabel.text + "\n[E54242]" + gameData.getTextByRefId("menuForgeBoost45") + "[-]";
			}
			selectButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuForgeBoost29");
			sortSmithsForBoost();
			displayBoostSmiths();
			showWeaponDetails();
			break;
		}
		updateDisplaySmith(string.Empty, string.Empty);
	}

	private void openCloseWeaponDetails()
	{
		if (!(weaponDetailsDrawer != null))
		{
			return;
		}
		TweenPosition component = commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg").GetComponent<TweenPosition>();
		if (component.enabled)
		{
			return;
		}
		if (!weaponDetailsDrawerIsOpen)
		{
			foreach (BoxCollider statTooltip in statTooltipList)
			{
				statTooltip.enabled = true;
			}
			weaponDetailsDrawerIsOpen = true;
			commonScreenObject.tweenPosition(component, new Vector3(100f, 0f, 0f), new Vector3(360f, 0f, 0f), 0.4f, null, string.Empty);
			return;
		}
		foreach (BoxCollider statTooltip2 in statTooltipList)
		{
			statTooltip2.enabled = false;
		}
		weaponDetailsDrawerIsOpen = false;
		commonScreenObject.tweenPosition(component, new Vector3(360f, 0f, 0f), new Vector3(100f, 0f, 0f), 0.4f, null, string.Empty);
	}

	private void showWeaponDetails()
	{
		Player player = game.getPlayer();
		Project currentProject = player.getCurrentProject();
		weaponDetailsDrawer = commonScreenObject.createPrefab(base.gameObject, "Panel_WeaponStatsDrawer", "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/Panel_WeaponStatsDrawer", Vector3.zero, Vector3.one, Vector3.zero);
		commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/WeaponTitle_bg/WeaponTitle").GetComponent<UILabel>().text = currentProject.getProjectName(includePrefix: true);
		commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/WeaponTitle_bg/WeaponDesc").GetComponent<UILabel>().text = currentProject.getProjectDesc();
		commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/Weapon_bg/Weapon_image").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + currentProject.getProjectWeapon().getImage());
		commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/DrawerTab_bg/DrawerTab_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuForgeBoost46");
		BoxCollider component = commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/WeaponStat_bg/Atk_sprite").GetComponent<BoxCollider>();
		BoxCollider component2 = commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/WeaponStat_bg/Spd_sprite").GetComponent<BoxCollider>();
		BoxCollider component3 = commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/WeaponStat_bg/Acc_sprite").GetComponent<BoxCollider>();
		BoxCollider component4 = commonScreenObject.findChild(weaponDetailsDrawer, "WeaponDrawer_bg/WeaponStat_bg/Mag_sprite").GetComponent<BoxCollider>();
		statTooltipList = new List<BoxCollider>();
		statTooltipList.Add(component);
		statTooltipList.Add(component2);
		statTooltipList.Add(component3);
		statTooltipList.Add(component4);
		foreach (BoxCollider statTooltip in statTooltipList)
		{
			statTooltip.enabled = false;
		}
		commonScreenObject.findChild(component.gameObject, "Atk_label").GetComponent<UILabel>().text = currentProject.getAtk().ToString();
		commonScreenObject.findChild(component2.gameObject, "Spd_label").GetComponent<UILabel>().text = currentProject.getSpd().ToString();
		commonScreenObject.findChild(component3.gameObject, "Acc_label").GetComponent<UILabel>().text = currentProject.getAcc().ToString();
		commonScreenObject.findChild(component4.gameObject, "Mag_label").GetComponent<UILabel>().text = currentProject.getMag().ToString();
	}

	private void displayBoostSmiths()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = 0;
		foreach (Smith inHouseSmith in inHouseSmithList)
		{
			GameObject aObject = commonScreenObject.createPrefab(smithSelectGrid.gameObject, "SmithList1_" + num + "_" + inHouseSmith.getSmithRefId(), "Prefab/SmithSelect/BoostSmithListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "SmithImage_bg/SmithTooltip").gameObject.SetActive(value: false);
			commonScreenObject.findChild(aObject, "SmithImage_bg/SmithImage_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + inHouseSmith.getImage());
			commonScreenObject.findChild(aObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(inHouseSmith.getMoodState()));
			commonScreenObject.findChild(aObject, "SmithName_bg/SmithName_label").GetComponent<UILabel>().text = inHouseSmith.getSmithName();
			SmithJobClass smithJob = inHouseSmith.getSmithJob();
			commonScreenObject.findChild(aObject, "SmithLevel_bg/SmithLevel_label").GetComponent<UILabel>().text = inHouseSmith.getCurrentJobClassLevelString();
			commonScreenObject.findChild(aObject, "SmithLevel_bg/SmithLevel_bg").GetComponent<UISprite>().fillAmount = (float)inHouseSmith.getSmithExp() / (float)inHouseSmith.getMaxExp();
			commonScreenObject.findChild(aObject, "SmithOutsourceCost_bg").gameObject.SetActive(value: false);
			commonScreenObject.findChild(aObject, "SmithImage_bg/SmithStatus_label").GetComponent<UILabel>().text = string.Empty;
			UISprite component = commonScreenObject.findChild(aObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>();
			switch (popupType)
			{
			case SmithSelectPopupType.SmithSelectPopupTypeBoostDesign:
				commonScreenObject.findChild(aObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = inHouseSmith.getSmithPower().ToString();
				component.spriteName = "ico_atk";
				component.gameObject.name = "SmithStat_atk";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostCraft:
				commonScreenObject.findChild(aObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = inHouseSmith.getSmithIntelligence().ToString();
				component.spriteName = "ico_speed";
				component.gameObject.name = "SmithStat_spd";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostPolish:
				commonScreenObject.findChild(aObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = inHouseSmith.getSmithTechnique().ToString();
				component.spriteName = "ico_acc";
				component.gameObject.name = "SmithStat_acc";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant:
				commonScreenObject.findChild(aObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = inHouseSmith.getSmithLuck().ToString();
				component.spriteName = "ico_enh";
				component.gameObject.name = "SmithStat_mag";
				break;
			}
			num++;
		}
		foreach (Smith outsourceSmith in outsourceSmithList)
		{
			GameObject aObject2 = commonScreenObject.createPrefab(smithSelectGrid.gameObject, "SmithList2_" + num + "_" + outsourceSmith.getSmithRefId(), "Prefab/SmithSelect/BoostSmithListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject2, "SmithImage_bg/SmithTooltip").gameObject.SetActive(value: false);
			UISprite component2 = commonScreenObject.findChild(aObject2, "SmithImage_bg").GetComponent<UISprite>();
			UITexture component3 = commonScreenObject.findChild(component2.gameObject, "SmithImage_texture").GetComponent<UITexture>();
			component3.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + outsourceSmith.getImage());
			commonScreenObject.findChild(aObject2, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(outsourceSmith.getMoodState()));
			commonScreenObject.findChild(aObject2, "SmithName_bg/SmithName_label").GetComponent<UILabel>().text = outsourceSmith.getSmithName();
			SmithJobClass smithJob2 = outsourceSmith.getSmithJob();
			commonScreenObject.findChild(aObject2, "SmithLevel_bg").gameObject.SetActive(value: false);
			commonScreenObject.findChild(aObject2, "SmithOutsourceCost_bg/SmithOutsourceCost_cost").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(outsourceSmith.getSmithHireCost());
			commonScreenObject.findChild(aObject2, "SmithOutsourceCost_bg/SmithOutsourceCost_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuForgeBoost47").ToUpper(CultureInfo.InvariantCulture);
			if (player.getPlayerGold() >= outsourceSmith.getSmithHireCost())
			{
				commonScreenObject.findChild(aObject2, "SmithImage_bg/SmithStatus_label").GetComponent<UILabel>().text = string.Empty;
			}
			else
			{
				commonScreenObject.findChild(aObject2, "SmithImage_bg/SmithStatus_label").GetComponent<UILabel>().text = gameData.getTextByRefId("errorCommon05");
				component2.color = Color.grey;
				component3.color = Color.grey;
			}
			UISprite component4 = commonScreenObject.findChild(aObject2, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>();
			switch (popupType)
			{
			case SmithSelectPopupType.SmithSelectPopupTypeBoostDesign:
				commonScreenObject.findChild(aObject2, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = outsourceSmith.getSmithPower().ToString();
				component4.spriteName = "ico_atk";
				component4.gameObject.name = "SmithStat_atk";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostCraft:
				commonScreenObject.findChild(aObject2, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = outsourceSmith.getSmithIntelligence().ToString();
				component4.spriteName = "ico_speed";
				component4.gameObject.name = "SmithStat_spd";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostPolish:
				commonScreenObject.findChild(aObject2, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = outsourceSmith.getSmithTechnique().ToString();
				component4.spriteName = "ico_acc";
				component4.gameObject.name = "SmithStat_acc";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant:
				commonScreenObject.findChild(aObject2, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = outsourceSmith.getSmithLuck().ToString();
				component4.spriteName = "ico_enh";
				component4.gameObject.name = "SmithStat_mag";
				break;
			}
			num++;
		}
		foreach (Smith awaySmith in awaySmithList)
		{
			GameObject aObject3 = commonScreenObject.createPrefab(smithSelectGrid.gameObject, "SmithList3_" + num + "_" + awaySmith.getSmithRefId(), "Prefab/SmithSelect/BoostSmithListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject3, "SmithImage_bg/SmithTooltip").gameObject.SetActive(value: false);
			UISprite component5 = commonScreenObject.findChild(aObject3, "SmithImage_bg").GetComponent<UISprite>();
			UITexture component6 = commonScreenObject.findChild(component5.gameObject, "SmithImage_texture").GetComponent<UITexture>();
			component6.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + awaySmith.getImage());
			commonScreenObject.findChild(component5.gameObject, "SmithMood_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(awaySmith.getMoodState()));
			commonScreenObject.findChild(aObject3, "SmithName_bg/SmithName_label").GetComponent<UILabel>().text = awaySmith.getSmithName();
			SmithJobClass smithJob3 = awaySmith.getSmithJob();
			commonScreenObject.findChild(aObject3, "SmithLevel_bg/SmithLevel_label").GetComponent<UILabel>().text = awaySmith.getCurrentJobClassLevelString();
			commonScreenObject.findChild(aObject3, "SmithLevel_bg/SmithLevel_bg").GetComponent<UISprite>().fillAmount = (float)awaySmith.getSmithExp() / (float)awaySmith.getMaxExp();
			commonScreenObject.findChild(aObject3, "SmithOutsourceCost_bg").gameObject.SetActive(value: false);
			commonScreenObject.findChild(component5.gameObject, "SmithStatus_label").GetComponent<UILabel>().text = gameData.getTextByRefId("errorCommon09");
			component5.color = Color.grey;
			component6.color = Color.grey;
			UISprite component7 = commonScreenObject.findChild(aObject3, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>();
			switch (popupType)
			{
			case SmithSelectPopupType.SmithSelectPopupTypeBoostDesign:
				commonScreenObject.findChild(aObject3, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = awaySmith.getSmithPower().ToString();
				component7.spriteName = "ico_atk";
				component7.gameObject.name = "SmithStat_atk";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostCraft:
				commonScreenObject.findChild(aObject3, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = awaySmith.getSmithIntelligence().ToString();
				component7.spriteName = "ico_speed";
				component7.gameObject.name = "SmithStat_spd";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostPolish:
				commonScreenObject.findChild(aObject3, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = awaySmith.getSmithTechnique().ToString();
				component7.spriteName = "ico_acc";
				component7.gameObject.name = "SmithStat_acc";
				break;
			case SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant:
				commonScreenObject.findChild(aObject3, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = awaySmith.getSmithLuck().ToString();
				component7.spriteName = "ico_enh";
				component7.gameObject.name = "SmithStat_mag";
				break;
			}
			num++;
		}
		smithSelectGrid.Reposition();
		smithSelectScroll.value = 0f;
		smithSelectGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		smithSelectGrid.enabled = true;
	}

	private void sortSmithsForBoost()
	{
		List<int> list = new List<int>();
		switch (popupType)
		{
		case SmithSelectPopupType.SmithSelectPopupTypeBoostDesign:
			foreach (Smith display in displayList)
			{
				list.Add(display.getSmithPower());
			}
			break;
		case SmithSelectPopupType.SmithSelectPopupTypeBoostCraft:
			foreach (Smith display2 in displayList)
			{
				list.Add(display2.getSmithIntelligence());
			}
			break;
		case SmithSelectPopupType.SmithSelectPopupTypeBoostPolish:
			foreach (Smith display3 in displayList)
			{
				list.Add(display3.getSmithTechnique());
			}
			break;
		case SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant:
			foreach (Smith display4 in displayList)
			{
				list.Add(display4.getSmithLuck());
			}
			break;
		}
		List<int> list2 = CommonAPI.sortIndices(list, isAscending: false);
		List<Smith> list3 = new List<Smith>();
		string text = "sortResult ";
		foreach (int item in list2)
		{
			list3.Add(displayList[item]);
			text = text + list[item] + " ";
		}
		inHouseSmithList = new List<Smith>();
		outsourceSmithList = new List<Smith>();
		awaySmithList = new List<Smith>();
		string text2 = "sortSmiths ";
		for (int i = 0; i < list3.Count; i++)
		{
			Smith smith = list3[i];
			text2 = text2 + smith.getSmithName() + " ";
			if (smith.checkOutsource())
			{
				outsourceSmithList.Add(smith);
			}
			else if (smith.checkSmithInShopOrStandby())
			{
				inHouseSmithList.Add(smith);
			}
			else
			{
				awaySmithList.Add(smith);
			}
		}
		outsourceSmithList.Reverse();
	}

	private void setSelectedSmith()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Smith smith = gameData.getSmithByRefId(selectedSmithRefId);
		if (smith.getSmithRefId() == string.Empty)
		{
			smith = gameData.getOutsourceSmithByRefId(selectedSmithRefId);
		}
		player.setLastSelectSmith(smith);
	}

	private void updateDisplaySmith(string smithListObjName, string aSelect)
	{
		if (smithListObjName.Split('_')[0] == "SmithList2" && game.getGameData().getOutsourceSmithByRefId(aSelect).getSmithHireCost() > game.getPlayer().getPlayerGold())
		{
			return;
		}
		selectedSmithRefId = aSelect;
		foreach (Transform child in smithSelectGrid.GetChildList())
		{
			if (child.gameObject.name == smithListObjName)
			{
				commonScreenObject.findChild(child.gameObject, "SmithImage_bg").GetComponent<UISprite>().spriteName = "bg_weaponselected";
			}
			else
			{
				commonScreenObject.findChild(child.gameObject, "SmithImage_bg").GetComponent<UISprite>().spriteName = "bg_weapon";
			}
		}
		if (selectedSmithRefId == string.Empty)
		{
			selectButton.isEnabled = false;
		}
		else
		{
			selectButton.isEnabled = true;
		}
	}
}
