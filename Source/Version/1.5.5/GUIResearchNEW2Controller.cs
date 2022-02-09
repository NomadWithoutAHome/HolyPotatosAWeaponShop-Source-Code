using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIResearchNEW2Controller : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private TooltipTextScript tooltipScript;

	private AudioController audioController;

	private UILabel titleLabel;

	private UIPanel relic1ListPanel;

	private UIGrid relic1ListGrid;

	private UILabel relic1ListWarning;

	private UIButton relic1ListUp;

	private UIButton relic1ListDown;

	private UIPanel relic2ListPanel;

	private UIGrid relic2ListGrid;

	private UILabel relic2ListWarning;

	private UIButton relic2ListUp;

	private UIButton relic2ListDown;

	private UITexture resultTexture;

	private UISprite researchWeaponTypeSprite;

	private UILabel warningLabel;

	private UILabel durationTitleLabel;

	private UILabel durationLabel;

	private UILabel smithBonusLabel;

	private UILabel smithTitleLabel;

	private UIGrid smithListGrid;

	private UIScrollBar smithListScroll;

	private UILabel smithWarningLabel;

	private UIButton startButton;

	private UILabel startLabel;

	private UILabel researchCostLabel;

	private List<Item> playerRelic1List;

	private Dictionary<string, string> playerRelic2List;

	private List<Smith> playerSmithList;

	private List<GameObject> relic1ObjList;

	private List<GameObject> relic2ObjList;

	private string relic1RefId;

	private string relic2RefId;

	private int relic1Index;

	private int relic2Index;

	private string resultWeaponRefId;

	private string smithRefId;

	private WeaponStat weaponStatType;

	private List<GameObject> smithObjectList;

	private UISprite filterSprite;

	private UILabel filterLabel;

	private bool showResearched;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "ResearchTitleBg/ResearchTitle_label").GetComponent<UILabel>();
		relic1ListPanel = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic1List_bg/Relic1List_clipPanel").GetComponent<UIPanel>();
		relic1ListGrid = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic1List_bg/Relic1List_clipPanel/Relic1List_grid").GetComponent<UIGrid>();
		relic1ListWarning = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic1List_bg/Relic1List_warning").GetComponent<UILabel>();
		relic1ListUp = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic1List_bg/Relic1List_up").GetComponent<UIButton>();
		relic1ListDown = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic1List_bg/Relic1List_down").GetComponent<UIButton>();
		relic2ListPanel = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic2List_bg/Relic2List_clipPanel").GetComponent<UIPanel>();
		relic2ListGrid = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic2List_bg/Relic2List_clipPanel/Relic2List_grid").GetComponent<UIGrid>();
		relic2ListWarning = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic2List_bg/Relic2List_warning").GetComponent<UILabel>();
		relic2ListUp = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic2List_bg/Relic2List_up").GetComponent<UIButton>();
		relic2ListDown = commonScreenObject.findChild(base.gameObject, "RelicList_bg/Relic2List_bg/Relic2List_down").GetComponent<UIButton>();
		resultTexture = commonScreenObject.findChild(base.gameObject, "Selection_bg/RelicResult_bg/RelicResult_image").GetComponent<UITexture>();
		researchWeaponTypeSprite = commonScreenObject.findChild(base.gameObject, "Selection_bg/RelicResult_bg/RelicResultWeaponType_sprite").GetComponent<UISprite>();
		warningLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/RelicResult_bg/ResearchWarning_label").GetComponent<UILabel>();
		durationTitleLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchDuration_bg/ResearchDurationTitle_label").GetComponent<UILabel>();
		durationLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchDuration_bg/ResearchDuration_label").GetComponent<UILabel>();
		smithBonusLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchDuration_bg/ResearchDurationBonus_label").GetComponent<UILabel>();
		smithTitleLabel = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_title").GetComponent<UILabel>();
		smithListGrid = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_clipPanel/SmithList_grid").GetComponent<UIGrid>();
		smithListScroll = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_scroll").GetComponent<UIScrollBar>();
		smithWarningLabel = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_warning").GetComponent<UILabel>();
		startButton = commonScreenObject.findChild(base.gameObject, "Start_button").GetComponent<UIButton>();
		startLabel = commonScreenObject.findChild(base.gameObject, "Start_button/Start_label").GetComponent<UILabel>();
		researchCostLabel = commonScreenObject.findChild(base.gameObject, "Start_button/ResearchCost_bg/ResearchCost_label").GetComponent<UILabel>();
		playerRelic1List = new List<Item>();
		playerRelic2List = new Dictionary<string, string>();
		playerSmithList = new List<Smith>();
		relic1ObjList = new List<GameObject>();
		relic2ObjList = new List<GameObject>();
		relic1RefId = string.Empty;
		relic2RefId = string.Empty;
		relic1Index = -1;
		relic2Index = -1;
		resultWeaponRefId = string.Empty;
		smithRefId = string.Empty;
		weaponStatType = WeaponStat.WeaponStatNone;
		smithObjectList = new List<GameObject>();
		filterSprite = commonScreenObject.findChild(base.gameObject, "Filter_toggle").GetComponent<UISprite>();
		filterLabel = commonScreenObject.findChild(filterSprite.gameObject, "Filter_label").GetComponent<UILabel>();
		filterLabel.text = game.getGameData().getTextByRefId("menuResearch28");
		showResearched = false;
		filterSprite.spriteName = "checkbox_a";
	}

	public void processClick(GameObject clickObj)
	{
		string text = clickObj.name;
		switch (text)
		{
		case "Close_button":
			viewController.closeResearch(hide: true, resume: true, resumeFromPlayerPause: false);
			tooltipScript.setInactive();
			return;
		case "Start_button":
			if (game.getPlayer().getInShopSmithList().Count > 1)
			{
				viewController.closeResearch(hide: true, resume: true, resumeFromPlayerPause: true);
				tooltipScript.setInactive();
				startResearch();
			}
			else
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, game.getGameData().getTextByRefId("errorCommon03"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
			return;
		case "Filter_toggle":
			toggleFilter();
			return;
		case "Relic1List_up":
			relic1Index--;
			if (relic1Index < 0)
			{
				relic1Index = 0;
			}
			selectRelic1(relic1ObjList[relic1Index]);
			makeRelic2List();
			refreshResearchDisplay();
			refreshSmithListDisplay(aCreate: true);
			selectSmith(string.Empty);
			return;
		case "Relic1List_down":
			relic1Index++;
			if (relic1Index >= relic1ObjList.Count)
			{
				relic1Index = relic1ObjList.Count - 1;
			}
			selectRelic1(relic1ObjList[relic1Index]);
			makeRelic2List();
			refreshResearchDisplay();
			refreshSmithListDisplay(aCreate: true);
			selectSmith(string.Empty);
			return;
		case "Relic2List_up":
			relic2Index--;
			if (relic2Index < 0)
			{
				relic2Index = 0;
			}
			selectRelic2(relic2ObjList[relic2Index]);
			refreshResearchDisplay();
			refreshSmithListDisplay(aCreate: true);
			selectSmith(string.Empty);
			return;
		case "Relic2List_down":
			relic2Index++;
			if (relic2Index >= relic2ObjList.Count)
			{
				relic2Index = relic2ObjList.Count - 1;
			}
			selectRelic2(relic2ObjList[relic2Index]);
			refreshResearchDisplay();
			refreshSmithListDisplay(aCreate: true);
			selectSmith(string.Empty);
			return;
		}
		string[] array = text.Split('_');
		if (array[0] == "Relic1ListObj")
		{
			selectRelic1(clickObj);
			makeRelic2List();
			refreshResearchDisplay();
			refreshSmithListDisplay(aCreate: true);
			selectSmith(string.Empty);
		}
		else if (array[0] == "Relic2ListObj")
		{
			selectRelic2(clickObj);
			refreshResearchDisplay();
			refreshSmithListDisplay(aCreate: true);
			selectSmith(string.Empty);
		}
		else if (array[1] == "ResearchSmithListObj")
		{
			selectSmith(array[2]);
			refreshSmithListDisplay(aCreate: false);
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
				if (smithByRefId.getSmithRefId() != string.Empty)
				{
					tooltipScript.showText(CommonAPI.getMoodString(smithByRefId.getMoodState(), showDesc: true));
				}
				return;
			}
			string[] array2 = text.Split('_');
			if (array2[0] == "Relic1ListObj" || array2[0] == "Relic2ListObj")
			{
				GameData gameData = game.getGameData();
				Item itemByRefId = gameData.getItemByRefId(array2[2]);
				tooltipScript.showText(itemByRefId.getItemName());
			}
			else if (array2[1] == "ResearchSmithListObj")
			{
				GameData gameData2 = game.getGameData();
				Smith smithByRefId2 = gameData2.getSmithByRefId(array2[2]);
				tooltipScript.showText(smithByRefId2.getSmithStandardInfoString(showFullJobDetails: false));
			}
			else if (array2[0] == "SmithTooltip")
			{
				Smith smithByRefId3 = game.getGameData().getSmithByRefId(array2[1]);
				if (smithByRefId3.getSmithRefId() != string.Empty)
				{
					tooltipScript.showText(smithByRefId3.getSmithStandardInfoString(showFullJobDetails: false));
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
		if (startButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			if (game.getPlayer().getInShopSmithList().Count > 1)
			{
				viewController.closeResearch(hide: true, resume: true, resumeFromPlayerPause: true);
				tooltipScript.setInactive();
				startResearch();
			}
			else
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, game.getGameData().getTextByRefId("errorCommon03"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
		else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			viewController.closeResearch(hide: true, resume: true, resumeFromPlayerPause: false);
			tooltipScript.setInactive();
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		titleLabel.text = gameData.getTextByRefId("menuResearch11").ToUpper(CultureInfo.InvariantCulture);
		smithTitleLabel.text = gameData.getTextByRefId("menuResearch13");
		durationTitleLabel.text = gameData.getTextByRefId("menuResearch14");
		smithBonusLabel.text = gameData.getTextByRefId("menuResearch19");
		startLabel.text = gameData.getTextByRefId("menuResearch04");
		makeRelic1List();
		makeRelic2List();
		refreshResearchDisplay();
		refreshSmithListDisplay(aCreate: true);
		selectSmith(string.Empty);
	}

	public void makeRelic1List()
	{
		GameData gameData = game.getGameData();
		playerRelic1List = gameData.getItemListByType(ItemType.ItemTypeRelic, ownedOnly: true, includeSpecial: true, string.Empty);
		if (playerRelic1List.Count > 0)
		{
			int num = 0;
			GameObject gameObject = null;
			relic1ObjList = new List<GameObject>();
			foreach (Item playerRelic in playerRelic1List)
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(relic1ListGrid.gameObject, "Relic1ListObj_" + num + "_" + playerRelic.getItemRefId(), "Prefab/Research/ResearchNEW/RelicListObj", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject2.GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/relics/" + playerRelic.getImage());
				gameObject2.GetComponentInChildren<UILabel>().text = playerRelic.getItemNum().ToString();
				relic1ObjList.Add(gameObject2);
				if (num == 0)
				{
					gameObject = gameObject2;
				}
				num++;
			}
			relic1ListGrid.Reposition();
			relic1ListGrid.enabled = true;
			if (gameObject != null)
			{
				selectRelic1(gameObject);
			}
			relic1ListWarning.text = string.Empty;
		}
		else
		{
			relic1RefId = string.Empty;
			relic1Index = -1;
			relic1ListWarning.text = gameData.getTextByRefId("menuResearch24");
		}
		updateRelic1ListArrows();
	}

	public void makeRelic2List()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		bool flag = true;
		clearRelic2List();
		if (relic1RefId != string.Empty)
		{
			playerRelic2List = gameData.getWeaponRelicListByRelicRefId(relic1RefId, itemLockSet);
			if (playerRelic2List.Count > 0)
			{
				int num = 0;
				GameObject gameObject = null;
				foreach (string key in playerRelic2List.Keys)
				{
					bool flag2 = true;
					if (!showResearched)
					{
						Weapon weaponByRelicCombi = gameData.getWeaponByRelicCombi(relic1RefId, key, itemLockSet);
						if (weaponByRelicCombi.getWeaponUnlocked())
						{
							flag2 = false;
						}
					}
					Item itemByRefId = gameData.getItemByRefId(key);
					if (itemByRefId.getItemNum() > 0 && flag2)
					{
						GameObject gameObject2 = commonScreenObject.createPrefab(relic2ListGrid.gameObject, "Relic2ListObj_" + num + "_" + key, "Prefab/Research/ResearchNEW/RelicListObj", Vector3.zero, Vector3.one, Vector3.zero);
						gameObject2.GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/relics/" + itemByRefId.getImage());
						gameObject2.GetComponentInChildren<UILabel>().text = itemByRefId.getItemNum().ToString();
						if (num == 0)
						{
							gameObject = gameObject2;
						}
						relic2ObjList.Add(gameObject2);
						num++;
					}
				}
				relic2ListGrid.Reposition();
				relic2ListGrid.enabled = true;
				if (gameObject != null)
				{
					selectRelic2(gameObject);
					refreshResearchDisplay();
				}
				if (num > 0)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			relic2ListWarning.text = gameData.getTextByRefId("menuResearch25");
		}
		else
		{
			relic2ListWarning.text = string.Empty;
		}
		updateRelic2ListArrows();
	}

	public void clearRelic2List()
	{
		relic2RefId = string.Empty;
		relic2Index = -1;
		foreach (Transform child in relic2ListGrid.GetChildList())
		{
			commonScreenObject.destroyPrefabImmediate(child.gameObject);
		}
		relic2ObjList = new List<GameObject>();
	}

	public void selectRelic1(GameObject selectRelic)
	{
		selectRelic.GetComponent<UICenterOnSelect>().centerOnThis();
		string text = selectRelic.name.Split('_')[2];
		if (relic1RefId != text)
		{
			relic1RefId = text;
			relic1Index = CommonAPI.parseInt(selectRelic.name.Split('_')[1]);
		}
		updateRelic1ListArrows();
	}

	public void selectRelic2(GameObject selectRelic)
	{
		selectRelic.GetComponent<UICenterOnSelect>().centerOnThis();
		string text = selectRelic.name.Split('_')[2];
		if (relic2RefId != text)
		{
			relic2RefId = text;
			relic2Index = CommonAPI.parseInt(selectRelic.name.Split('_')[1]);
		}
		updateRelic2ListArrows();
	}

	public void refreshResearchDisplay()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		if (relic1RefId != string.Empty)
		{
			Item itemByRefId = gameData.getItemByRefId(relic1RefId);
		}
		if (relic2RefId != string.Empty)
		{
			Item itemByRefId2 = gameData.getItemByRefId(relic2RefId);
		}
		weaponStatType = WeaponStat.WeaponStatNone;
		if (relic1RefId != string.Empty && relic2RefId != string.Empty)
		{
			Dictionary<string, string> weaponRelicListByRelicRefId = gameData.getWeaponRelicListByRelicRefId(relic1RefId, itemLockSet);
			string aRefId = weaponRelicListByRelicRefId[relic2RefId];
			Weapon weaponByRefId = gameData.getWeaponByRefId(aRefId);
			player.setLastSelectWeapon(weaponByRefId);
			int researchCost = weaponByRefId.getResearchCost();
			int researchDuration = weaponByRefId.getResearchDuration();
			resultTexture.color = Color.black;
			resultTexture.alpha = 1f;
			resultTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
			researchCostLabel.text = CommonAPI.formatNumber(researchCost);
			switch (weaponByRefId.getResearchType())
			{
			case WeaponStat.WeaponStatAttack:
				researchWeaponTypeSprite.spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				researchWeaponTypeSprite.spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				researchWeaponTypeSprite.spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				researchWeaponTypeSprite.spriteName = "ico_enh";
				break;
			}
			researchWeaponTypeSprite.color = Color.white;
			if (weaponByRefId.getWeaponUnlocked())
			{
				resultTexture.color = Color.white;
				warningLabel.text = gameData.getTextByRefId("menuResearch17");
				startButton.isEnabled = false;
			}
			else if (player.getPlayerGold() < researchCost)
			{
				warningLabel.text = gameData.getTextByRefId("menuResearch16");
				startButton.isEnabled = false;
			}
			else
			{
				warningLabel.text = string.Empty;
				startButton.isEnabled = false;
				weaponStatType = weaponByRefId.getResearchType();
			}
		}
		else
		{
			resultTexture.alpha = 0f;
			researchCostLabel.text = "-";
			warningLabel.text = gameData.getTextByRefId("menuResearch22");
			researchWeaponTypeSprite.color = Color.clear;
		}
		if (!player.checkCanResearch())
		{
			warningLabel.text = gameData.getTextByRefId("menuResearch18");
		}
		refreshSmithListDisplay(aCreate: true);
		updateDurationLabel();
		updateStartButtonState();
	}

	public void refreshRelicListDisplay()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		foreach (Item playerRelic in playerRelic1List)
		{
			GameObject gameObject = commonScreenObject.findChild(relic1ListGrid.gameObject, "RelicListObj_" + playerRelic.getItemRefId()).gameObject;
			UISprite component = commonScreenObject.findChild(gameObject, "Relic_mask").GetComponent<UISprite>();
			if (relic1RefId != string.Empty && relic2RefId != string.Empty)
			{
				gameObject.GetComponent<UIButton>().isEnabled = false;
				gameObject.GetComponent<UIButton>().normalSprite = "bg_weapon";
				component.alpha = 0.5f;
				if (playerRelic.getItemRefId() == relic1RefId || playerRelic.getItemRefId() == relic2RefId)
				{
					gameObject.GetComponent<UIButton>().isEnabled = true;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weaponselected";
					component.alpha = 0f;
				}
			}
			else if (relic1RefId != string.Empty && relic2RefId == string.Empty)
			{
				Dictionary<string, string> weaponRelicListByRelicRefId = gameData.getWeaponRelicListByRelicRefId(relic1RefId, itemLockSet);
				if (weaponRelicListByRelicRefId.ContainsKey(playerRelic.getItemRefId()))
				{
					gameObject.GetComponent<UIButton>().isEnabled = true;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weapon";
					component.alpha = 0f;
				}
				else
				{
					gameObject.GetComponent<UIButton>().isEnabled = false;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weapon";
					component.alpha = 0.5f;
				}
				if (playerRelic.getItemRefId() == relic1RefId)
				{
					gameObject.GetComponent<UIButton>().isEnabled = true;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weaponselected";
					component.alpha = 0f;
				}
			}
			else if (relic1RefId == string.Empty && relic2RefId != string.Empty)
			{
				Dictionary<string, string> weaponRelicListByRelicRefId2 = gameData.getWeaponRelicListByRelicRefId(relic2RefId, itemLockSet);
				if (weaponRelicListByRelicRefId2.ContainsKey(playerRelic.getItemRefId()))
				{
					gameObject.GetComponent<UIButton>().isEnabled = true;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weapon";
					component.alpha = 0f;
				}
				else
				{
					gameObject.GetComponent<UIButton>().isEnabled = false;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weapon";
					component.alpha = 0.5f;
				}
				if (playerRelic.getItemRefId() == relic2RefId)
				{
					gameObject.GetComponent<UIButton>().isEnabled = true;
					gameObject.GetComponent<UIButton>().normalSprite = "bg_weaponselected";
					component.alpha = 0f;
				}
			}
			else
			{
				gameObject.GetComponent<UIButton>().isEnabled = true;
				gameObject.GetComponent<UIButton>().normalSprite = "bg_weapon";
				component.alpha = 0f;
			}
		}
	}

	private void toggleFilter()
	{
		if (showResearched)
		{
			showResearched = false;
			filterSprite.spriteName = "checkbox_a";
		}
		else
		{
			showResearched = true;
			filterSprite.spriteName = "checkbox_b";
		}
		makeRelic2List();
		refreshResearchDisplay();
		refreshSmithListDisplay(aCreate: true);
		selectSmith(string.Empty);
	}

	private int compareSmithListByAtk(Smith aSmith, Smith bSmith)
	{
		return aSmith.getSmithPower().CompareTo(bSmith.getSmithPower());
	}

	private int compareSmithListBySpd(Smith aSmith, Smith bSmith)
	{
		return aSmith.getSmithIntelligence().CompareTo(bSmith.getSmithIntelligence());
	}

	private int compareSmithListByAcc(Smith aSmith, Smith bSmith)
	{
		return aSmith.getSmithTechnique().CompareTo(bSmith.getSmithTechnique());
	}

	private int compareSmithListByMag(Smith aSmith, Smith bSmith)
	{
		return aSmith.getSmithLuck().CompareTo(bSmith.getSmithLuck());
	}

	private void refreshSmithListDisplay(bool aCreate)
	{
		if (aCreate)
		{
			smithRefId = string.Empty;
			foreach (GameObject smithObject in smithObjectList)
			{
				commonScreenObject.destroyPrefabImmediate(smithObject);
			}
			smithObjectList.Clear();
		}
		playerSmithList = new List<Smith>(game.getPlayer().getSmithList());
		List<Smith> list = new List<Smith>();
		List<Smith> list2 = new List<Smith>();
		List<Smith> list3 = new List<Smith>();
		switch (weaponStatType)
		{
		case WeaponStat.WeaponStatAttack:
			playerSmithList.Sort(compareSmithListByAtk);
			playerSmithList.Reverse();
			break;
		case WeaponStat.WeaponStatSpeed:
			playerSmithList.Sort(compareSmithListBySpd);
			playerSmithList.Reverse();
			break;
		case WeaponStat.WeaponStatAccuracy:
			playerSmithList.Sort(compareSmithListByAcc);
			playerSmithList.Reverse();
			break;
		case WeaponStat.WeaponStatMagic:
			playerSmithList.Sort(compareSmithListByMag);
			playerSmithList.Reverse();
			break;
		default:
			playerSmithList.Clear();
			break;
		}
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.checkSmithInShopOrStandby())
			{
				list2.Add(playerSmith);
			}
			else
			{
				list3.Add(playerSmith);
			}
		}
		list.AddRange(list2);
		list.AddRange(list3);
		int num = 0;
		foreach (Smith item in list)
		{
			GameObject gameObject = commonScreenObject.createPrefab(smithListGrid.gameObject, string.Empty + num + "_ResearchSmithListObj_" + item.getSmithRefId(), "Prefab/Research/ResearchNEW/ResearchSmithListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithTooltip").gameObject.SetActive(value: false);
			commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithImage_texture").GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + item.getImage());
			commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(item.getMoodState()));
			commonScreenObject.findChild(gameObject, "SmithName_bg/SmithName_label").GetComponent<UILabel>().text = item.getSmithName();
			SmithJobClass smithJob = item.getSmithJob();
			commonScreenObject.findChild(gameObject, "SmithLevel_bg/SmithLevel_label").GetComponent<UILabel>().text = smithJob.getSmithJobName() + " Lv" + item.getSmithLevel();
			commonScreenObject.findChild(gameObject, "SmithLevel_bg/SmithLevel_bg").GetComponent<UISprite>().fillAmount = (float)item.getSmithExp() / (float)item.getMaxExp();
			UILabel component = commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithStatus_label").GetComponent<UILabel>();
			if (item.checkSmithInShopOrStandby())
			{
				component.GetComponent<UILabel>().alpha = 0f;
			}
			else
			{
				component.text = gameData.getTextByRefId("errorCommon09").ToUpper(CultureInfo.InvariantCulture);
				component.GetComponent<UILabel>().alpha = 1f;
			}
			commonScreenObject.findChild(gameObject, "SmithStat_bg").GetComponent<UISprite>().alpha = 1f;
			commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>().alpha = 1f;
			if (item.getSmithRefId() == smithRefId)
			{
				gameObject.GetComponent<UIButton>().isEnabled = false;
			}
			else
			{
				gameObject.GetComponent<UIButton>().isEnabled = true;
			}
			switch (weaponStatType)
			{
			case WeaponStat.WeaponStatAttack:
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = item.getSmithPower().ToString();
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>().spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = item.getSmithIntelligence().ToString();
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>().spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = item.getSmithTechnique().ToString();
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>().spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = item.getSmithLuck().ToString();
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>().spriteName = "ico_enh";
				break;
			default:
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_label").GetComponent<UILabel>().text = string.Empty;
				commonScreenObject.findChild(gameObject, "SmithStat_bg").GetComponent<UISprite>().alpha = 0f;
				commonScreenObject.findChild(gameObject, "SmithStat_bg/SmithStat_sprite").GetComponent<UISprite>().alpha = 0f;
				break;
			}
			smithObjectList.Add(gameObject);
			num++;
		}
		if (num > 0)
		{
			smithWarningLabel.text = string.Empty;
		}
		else
		{
			smithWarningLabel.text = game.getGameData().getTextByRefId("menuResearch12");
		}
		smithListGrid.Reposition();
		smithListScroll.value = 0f;
		smithListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		smithListGrid.enabled = true;
		updateStartButtonState();
	}

	private void selectSmith(string aSmithRefId)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (aSmithRefId.Length == 0 || aSmithRefId == null)
		{
			smithRefId = string.Empty;
		}
		else if (player.checkCanResearch())
		{
			Smith smithByRefID = player.getSmithByRefID(aSmithRefId);
			if (!smithByRefID.checkSmithInShopOrStandby())
			{
				return;
			}
			smithRefId = aSmithRefId;
		}
		updateDurationLabel();
		updateStartButtonState();
	}

	public void updateDurationLabel()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (relic1RefId != string.Empty && relic2RefId != string.Empty)
		{
			Weapon lastSelectWeapon = player.getLastSelectWeapon();
			int researchDuration = lastSelectWeapon.getResearchDuration();
			int num = researchDuration;
			if (smithRefId != string.Empty)
			{
				Smith smithByRefID = player.getSmithByRefID(smithRefId);
				float researchDurationModifier = getResearchDurationModifier(lastSelectWeapon.getResearchType(), smithByRefID);
				num = (int)((float)researchDuration * researchDurationModifier);
				smithBonusLabel.text = gameData.getTextByRefIdWithDynText("menuResearch26", "[bonus]", "-" + CommonAPI.convertHalfHoursToTimeString(researchDuration - num));
			}
			else
			{
				smithBonusLabel.text = string.Empty;
			}
			durationLabel.text = CommonAPI.convertHalfHoursToTimeString(num);
		}
		else
		{
			durationLabel.text = string.Empty;
			smithBonusLabel.text = string.Empty;
		}
	}

	public void updateStartButtonState()
	{
		Player player = game.getPlayer();
		Weapon lastSelectWeapon = player.getLastSelectWeapon();
		if (smithRefId != string.Empty && relic1RefId != string.Empty && relic2RefId != string.Empty && player.checkCanResearch() && !lastSelectWeapon.getWeaponUnlocked())
		{
			startButton.isEnabled = true;
		}
		else
		{
			startButton.isEnabled = false;
		}
	}

	public void updateRelic1ListArrows()
	{
		if (relic1Index < 1)
		{
			relic1ListUp.isEnabled = false;
		}
		else
		{
			relic1ListUp.isEnabled = true;
		}
		if (relic1Index >= relic1ObjList.Count - 1)
		{
			relic1ListDown.isEnabled = false;
		}
		else
		{
			relic1ListDown.isEnabled = true;
		}
	}

	public void updateRelic2ListArrows()
	{
		if (relic2Index < 1)
		{
			relic2ListUp.isEnabled = false;
		}
		else
		{
			relic2ListUp.isEnabled = true;
		}
		if (relic2Index >= relic2ObjList.Count - 1)
		{
			relic2ListDown.isEnabled = false;
		}
		else
		{
			relic2ListDown.isEnabled = true;
		}
	}

	public void startResearch()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Smith smithByRefID = player.getSmithByRefID(smithRefId);
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("904");
		Weapon lastSelectWeapon = player.getLastSelectWeapon();
		float researchDurationModifier = getResearchDurationModifier(lastSelectWeapon.getResearchType(), smithByRefID);
		float moodEffect = CommonAPI.getMoodEffect(smithByRefID.getMoodState(), 1.6f, 1f, -0.2f);
		int num = (int)((float)lastSelectWeapon.getResearchDuration() * researchDurationModifier * moodEffect);
		smithByRefID.setSmithAction(smithActionByRefId, num);
		List<Item> itemListByRefId = gameData.getItemListByRefId(lastSelectWeapon.getRelicList());
		foreach (Item item in itemListByRefId)
		{
			item.tryUseItem(1, isUse: true);
		}
		player.reduceGold(lastSelectWeapon.getResearchCost(), allowNegative: true);
		audioController.playPurchaseAudio();
		player.startResearch(lastSelectWeapon, smithByRefID, num);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseResearch, string.Empty, -1 * lastSelectWeapon.getResearchCost());
		GameObject gameObject = GameObject.Find("Panel_BottomMenu");
		if (gameObject != null)
		{
			gameObject.GetComponent<BottomMenuController>().refreshBottomButtons();
		}
	}

	private float getResearchDurationModifier(WeaponStat aStat, Smith aSmith)
	{
		float result = 1f;
		switch (aStat)
		{
		case WeaponStat.WeaponStatAttack:
			result = Mathf.Max(0.1f, 1f - (float)aSmith.getSmithPower() / 1000f);
			break;
		case WeaponStat.WeaponStatSpeed:
			result = Mathf.Max(0.1f, 1f - (float)aSmith.getSmithIntelligence() / 1000f);
			break;
		case WeaponStat.WeaponStatAccuracy:
			result = Mathf.Max(0.1f, 1f - (float)aSmith.getSmithTechnique() / 1000f);
			break;
		case WeaponStat.WeaponStatMagic:
			result = Mathf.Max(0.1f, 1f - (float)aSmith.getSmithLuck() / 1000f);
			break;
		}
		return result;
	}

	private void findRelicClash()
	{
		GameData gameData = game.getGameData();
		List<Item> itemListByType = gameData.getItemListByType(ItemType.ItemTypeRelic, ownedOnly: false, includeSpecial: true, string.Empty);
		for (int i = 0; i < itemListByType.Count - 1; i++)
		{
			for (int j = i + 1; j < itemListByType.Count; j++)
			{
				List<Weapon> weaponListByRelicCombi = gameData.getWeaponListByRelicCombi(checkDLC: false, itemListByType[i].getItemRefId(), itemListByType[j].getItemRefId(), string.Empty);
				if (weaponListByRelicCombi.Count <= 1)
				{
					continue;
				}
				string text = string.Empty;
				foreach (Weapon item in weaponListByRelicCombi)
				{
					string text2 = text;
					text = text2 + item.getWeaponRefId() + " " + item.getWeaponName() + " | ";
				}
				CommonAPI.debug(text);
			}
		}
	}
}
