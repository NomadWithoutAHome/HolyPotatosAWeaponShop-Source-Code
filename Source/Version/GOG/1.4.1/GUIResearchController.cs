using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIResearchController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private TooltipTextScript tooltipScript;

	private AudioController audioController;

	private UILabel titleLabel;

	private UILabel relicTitleLabel;

	private UILabel smithTitleLabel;

	private UILabel durationTitleLabel;

	private UILabel costTitleLabel;

	private UILabel startLabel;

	private UIGrid relicListGrid;

	private UIScrollBar relicListScroll;

	private UITexture relic1Texture;

	private UITexture relic2Texture;

	private UILabel relic1Label;

	private UILabel relic2Label;

	private UITexture resultTexture;

	private UILabel warningLabel;

	private UILabel researchCostLabel;

	private UILabel durationLabel;

	private UISprite researchWeaponTypeSprite;

	private UIGrid smithListGrid;

	private UIScrollBar smithListScroll;

	private UITexture smithTexture;

	private UILabel smithNameLabel;

	private UILabel smithStatLabel;

	private UISprite smithWeaponTypeSprite;

	private UISprite smithStatBg;

	private UISprite smithMask;

	private UILabel warningSmithLabel;

	private UIButton startButton;

	private List<Item> playerRelicList;

	private List<Smith> playerSmithList;

	private string relic1RefId;

	private string relic2RefId;

	private string resultWeaponRefId;

	private string smithRefId;

	private WeaponStat weaponStatType;

	private List<GameObject> smithObjectList;

	private int refreshIndex;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "ResearchTitleBg/ResearchTitle_label").GetComponent<UILabel>();
		relicTitleLabel = commonScreenObject.findChild(base.gameObject, "RelicList_bg/RelicList_title").GetComponent<UILabel>();
		smithTitleLabel = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_title").GetComponent<UILabel>();
		durationTitleLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchDuration_bg/ResearchDurationTitle_label").GetComponent<UILabel>();
		costTitleLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchCost_bg/ResearchCostTitle_label").GetComponent<UILabel>();
		startLabel = commonScreenObject.findChild(base.gameObject, "Start_button/Start_label").GetComponent<UILabel>();
		relicListGrid = commonScreenObject.findChild(base.gameObject, "RelicList_bg/RelicList_clipPanel/RelicList_grid").GetComponent<UIGrid>();
		relicListScroll = commonScreenObject.findChild(base.gameObject, "RelicList_bg/RelicList_scroll").GetComponent<UIScrollBar>();
		relic1Texture = commonScreenObject.findChild(base.gameObject, "Selection_bg/Relic1_bg/Relic1_image").GetComponent<UITexture>();
		relic2Texture = commonScreenObject.findChild(base.gameObject, "Selection_bg/Relic2_bg/Relic2_image").GetComponent<UITexture>();
		relic1Label = commonScreenObject.findChild(base.gameObject, "Selection_bg/Relic1_bg/Relic1_label").GetComponent<UILabel>();
		relic2Label = commonScreenObject.findChild(base.gameObject, "Selection_bg/Relic2_bg/Relic2_label").GetComponent<UILabel>();
		resultTexture = commonScreenObject.findChild(base.gameObject, "Selection_bg/RelicResult_bg/RelicResult_image").GetComponent<UITexture>();
		warningLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/RelicResult_bg/ResearchWarning_label").GetComponent<UILabel>();
		researchCostLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchCost_bg/ResearchCost_label").GetComponent<UILabel>();
		durationLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/ResearchDuration_bg/ResearchDuration_label").GetComponent<UILabel>();
		researchWeaponTypeSprite = commonScreenObject.findChild(base.gameObject, "Selection_bg/RelicResult_bg/RelicResultWeaponType_sprite").GetComponent<UISprite>();
		smithListGrid = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_clipPanel/SmithList_grid").GetComponent<UIGrid>();
		smithListScroll = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_scroll").GetComponent<UIScrollBar>();
		smithTexture = commonScreenObject.findChild(base.gameObject, "Selection_bg/SmithResult_bg/SmithResult_image").GetComponent<UITexture>();
		smithNameLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/SmithResult_bg/SmithResultStatus_name").GetComponent<UILabel>();
		smithStatLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/SmithResult_bg/SmithResultStatus_statvalue").GetComponent<UILabel>();
		smithWeaponTypeSprite = commonScreenObject.findChild(base.gameObject, "Selection_bg/SmithResult_bg/SmithResultStatus_stattype").GetComponent<UISprite>();
		smithStatBg = commonScreenObject.findChild(base.gameObject, "Selection_bg/SmithResult_bg/SmithResultStatus_statbg").GetComponent<UISprite>();
		smithMask = commonScreenObject.findChild(base.gameObject, "SmithList_bg/SmithList_maskPanel/SmithList_mask").GetComponent<UISprite>();
		warningSmithLabel = commonScreenObject.findChild(base.gameObject, "Selection_bg/SmithResult_bg/SmithResult_warning").GetComponent<UILabel>();
		startButton = commonScreenObject.findChild(base.gameObject, "Start_button").GetComponent<UIButton>();
		startButton.GetComponentInChildren<UILabel>().text = "Do Research";
		relic1RefId = string.Empty;
		relic2RefId = string.Empty;
		resultWeaponRefId = string.Empty;
		weaponStatType = WeaponStat.WeaponStatNone;
		smithObjectList = new List<GameObject>();
		refreshIndex = 0;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
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
		case "Relic1_bg":
			deselectRelic(1);
			tooltipScript.setInactive();
			return;
		case "Relic2_bg":
			deselectRelic(2);
			tooltipScript.setInactive();
			return;
		}
		string[] array = gameObjectName.Split('_');
		if (array[0] == "RelicListObj")
		{
			selectRelic(array[1]);
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
			switch (text)
			{
			case "Relic1_bg":
				if (relic1RefId != string.Empty)
				{
					GameData gameData = game.getGameData();
					Item itemByRefId = gameData.getItemByRefId(relic1RefId);
					tooltipScript.showText(itemByRefId.getItemName());
				}
				return;
			case "Relic2_bg":
				if (relic2RefId != string.Empty)
				{
					GameData gameData2 = game.getGameData();
					Item itemByRefId2 = gameData2.getItemByRefId(relic2RefId);
					tooltipScript.showText(itemByRefId2.getItemName());
				}
				return;
			case "SmithMood_texture":
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
			}
			string[] array2 = text.Split('_');
			if (array2[0] == "RelicListObj")
			{
				GameData gameData3 = game.getGameData();
				Item itemByRefId3 = gameData3.getItemByRefId(array2[1]);
				tooltipScript.showText(itemByRefId3.getItemName());
			}
			else if (array2[1] == "ResearchSmithListObj")
			{
				GameData gameData4 = game.getGameData();
				Smith smithByRefId2 = gameData4.getSmithByRefId(array2[2]);
				tooltipScript.showText(smithByRefId2.getSmithStandardInfoString(showFullJobDetails: false));
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		titleLabel.text = gameData.getTextByRefId("menuResearch11").ToUpper(CultureInfo.InvariantCulture);
		relicTitleLabel.text = gameData.getTextByRefId("menuResearch12");
		smithTitleLabel.text = gameData.getTextByRefId("menuResearch13");
		durationTitleLabel.text = gameData.getTextByRefId("menuResearch14");
		costTitleLabel.text = gameData.getTextByRefId("menuResearch19");
		startLabel.text = gameData.getTextByRefId("menuResearch20");
		relic1Label.text = gameData.getTextByRefId("menuResearch15");
		relic2Label.text = gameData.getTextByRefId("menuResearch15");
		playerRelicList = game.getGameData().getItemListByType(ItemType.ItemTypeRelic, ownedOnly: true, includeSpecial: true, string.Empty);
		foreach (Item playerRelic in playerRelicList)
		{
			GameObject gameObject = commonScreenObject.createPrefab(relicListGrid.gameObject, "RelicListObj_" + playerRelic.getItemRefId(), "Prefab/Research/RelicListObj", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/relics/" + playerRelic.getImage());
			gameObject.GetComponentInChildren<UILabel>().text = string.Empty + playerRelic.getItemNum();
		}
		relicListGrid.Reposition();
		relicListScroll.value = 0f;
		relicListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		relicListGrid.enabled = true;
		refreshResearchDisplay();
		refreshRelicListDisplay();
		refreshSmithListDisplay(aCreate: true);
		selectSmith(string.Empty);
	}

	private void deselectRelic(int aIndex)
	{
		enableSmithSelection(aEnable: false);
		switch (aIndex)
		{
		case 1:
			relic1RefId = string.Empty;
			refreshResearchDisplay();
			refreshRelicListDisplay();
			break;
		case 2:
			relic2RefId = string.Empty;
			refreshResearchDisplay();
			refreshRelicListDisplay();
			break;
		}
	}

	private void selectRelic(string aRelicRefId)
	{
		if (relic1RefId == aRelicRefId)
		{
			deselectRelic(1);
		}
		else if (relic2RefId == aRelicRefId)
		{
			deselectRelic(2);
		}
		else if (relic1RefId == string.Empty)
		{
			relic1RefId = aRelicRefId;
			refreshResearchDisplay();
			refreshRelicListDisplay();
		}
		else if (relic2RefId != aRelicRefId)
		{
			relic2RefId = aRelicRefId;
			refreshResearchDisplay();
			refreshRelicListDisplay();
		}
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
			relic1Texture.alpha = 1f;
			relic1Label.text = "Remove";
			relic1Texture.mainTexture = commonScreenObject.loadTexture("Image/relics/" + itemByRefId.getImage());
		}
		else
		{
			relic1Texture.alpha = 0f;
			relic1Label.text = string.Empty;
		}
		if (relic2RefId != string.Empty)
		{
			Item itemByRefId2 = gameData.getItemByRefId(relic2RefId);
			relic2Texture.alpha = 1f;
			relic2Label.text = "Remove";
			relic2Texture.mainTexture = commonScreenObject.loadTexture("Image/relics/" + itemByRefId2.getImage());
		}
		else
		{
			relic2Texture.alpha = 0f;
			relic2Label.text = string.Empty;
		}
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
			durationLabel.text = CommonAPI.convertHalfHoursToTimeString(researchDuration);
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
				refreshSmithListDisplay(aCreate: true);
				if (player.checkCanResearch())
				{
					enableSmithSelection(aEnable: true);
				}
			}
		}
		else
		{
			resultTexture.alpha = 0f;
			researchCostLabel.text = "-";
			durationLabel.text = string.Empty;
			warningLabel.text = gameData.getTextByRefId("menuResearch22");
			warningSmithLabel.text = gameData.getTextByRefId("menuResearch23");
			researchWeaponTypeSprite.color = Color.clear;
			startButton.isEnabled = false;
		}
		if (!player.checkCanResearch())
		{
			warningLabel.text = gameData.getTextByRefId("menuResearch18");
			startButton.isEnabled = false;
		}
	}

	public void refreshRelicListDisplay()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		foreach (Item playerRelic in playerRelicList)
		{
			GameObject gameObject = commonScreenObject.findChild(relicListGrid.gameObject, "RelicListObj_" + playerRelic.getItemRefId()).gameObject;
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
				Dictionary<string, string> weaponRelicListByRelicRefId = game.getGameData().getWeaponRelicListByRelicRefId(relic1RefId, itemLockSet);
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
				Dictionary<string, string> weaponRelicListByRelicRefId2 = game.getGameData().getWeaponRelicListByRelicRefId(relic2RefId, itemLockSet);
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
			foreach (GameObject smithObject in smithObjectList)
			{
				commonScreenObject.destroyPrefabImmediate(smithObject);
			}
			smithObjectList.Clear();
		}
		playerSmithList = game.getPlayer().getSmithList();
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
			GameObject gameObject = commonScreenObject.createPrefab(smithListGrid.gameObject, string.Empty + num + "_ResearchSmithListObj_" + item.getSmithRefId(), "Prefab/Research/ResearchSmithListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithImage_texture").GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + item.getImage());
			commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(item.getMoodState()));
			commonScreenObject.findChild(gameObject, "SmithName_bg/SmithName_label").GetComponent<UILabel>().text = item.getSmithName();
			SmithJobClass smithJob = item.getSmithJob();
			commonScreenObject.findChild(gameObject, "SmithLevel_bg/SmithLevel_label").GetComponent<UILabel>().text = smithJob.getSmithJobName() + " Lv" + item.getSmithLevel();
			commonScreenObject.findChild(gameObject, "SmithLevel_bg/SmithLevel_bg").GetComponent<UISprite>().fillAmount = (float)item.getSmithExp() / (float)item.getMaxExp();
			if (item.checkSmithInShopOrStandby())
			{
				commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithStatus_label").GetComponent<UILabel>().alpha = 0f;
			}
			else
			{
				commonScreenObject.findChild(gameObject, "SmithImage_bg/SmithStatus_label").GetComponent<UILabel>().alpha = 1f;
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
		smithListGrid.Reposition();
		smithListScroll.value = 0f;
		smithListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		smithListGrid.enabled = true;
	}

	private void enableSmithSelection(bool aEnable)
	{
		if (!aEnable)
		{
			selectSmith(string.Empty);
			return;
		}
		smithMask.alpha = 0f;
		smithMask.GetComponent<BoxCollider>().enabled = false;
	}

	private void selectSmith(string aSmithRefId)
	{
		Player player = game.getPlayer();
		if (aSmithRefId.Length == 0 || aSmithRefId == null)
		{
			smithRefId = string.Empty;
			smithStatBg.alpha = 0f;
			smithTexture.alpha = 0f;
			smithNameLabel.text = string.Empty;
			smithStatLabel.text = string.Empty;
			smithWeaponTypeSprite.alpha = 0f;
			smithMask.alpha = 0.7f;
			smithMask.GetComponent<BoxCollider>().enabled = true;
			warningSmithLabel.text = game.getGameData().getTextByRefId("menuResearch23");
		}
		else
		{
			if (!player.checkCanResearch())
			{
				return;
			}
			Smith smithByRefID = player.getSmithByRefID(aSmithRefId);
			if (smithByRefID.checkSmithInShopOrStandby())
			{
				warningSmithLabel.text = string.Empty;
				smithRefId = aSmithRefId;
				smithMask.alpha = 0f;
				smithMask.GetComponent<BoxCollider>().enabled = false;
				smithTexture.alpha = 1f;
				smithTexture.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smithByRefID.getImage() + "_manage");
				smithNameLabel.text = smithByRefID.getSmithName();
				smithStatBg.alpha = 1f;
				smithWeaponTypeSprite.alpha = 1f;
				switch (weaponStatType)
				{
				case WeaponStat.WeaponStatAttack:
					smithStatLabel.text = smithByRefID.getSmithPower().ToString();
					smithWeaponTypeSprite.spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					smithStatLabel.text = smithByRefID.getSmithIntelligence().ToString();
					smithWeaponTypeSprite.spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					smithStatLabel.text = smithByRefID.getSmithTechnique().ToString();
					smithWeaponTypeSprite.spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					smithStatLabel.text = smithByRefID.getSmithLuck().ToString();
					smithWeaponTypeSprite.spriteName = "ico_enh";
					break;
				default:
					smithStatLabel.text = string.Empty;
					smithWeaponTypeSprite.alpha = 0f;
					break;
				}
				Weapon lastSelectWeapon = game.getPlayer().getLastSelectWeapon();
				float researchDurationModifier = getResearchDurationModifier(lastSelectWeapon.getResearchType(), smithByRefID);
				int num = (int)((float)lastSelectWeapon.getResearchDuration() * researchDurationModifier);
				durationLabel.text = CommonAPI.convertHalfHoursToTimeString(num);
				startButton.isEnabled = true;
			}
		}
	}

	public void startResearch()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Smith smithByRefID = game.getPlayer().getSmithByRefID(smithRefId);
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
}
