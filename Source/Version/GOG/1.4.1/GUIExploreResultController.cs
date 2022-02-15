using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIExploreResultController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UISprite exploreResultBg;

	private UILabel smithName;

	private UITexture smithImage;

	private UISprite smithLvlUp;

	private UISprite smithBubble;

	private UILabel smithBubbleText;

	private UITexture smithMood;

	private UISprite smithStatusAlert;

	private UILabel locationTitle;

	private UILabel locationLabel;

	private UISprite matListBg;

	private UIGrid matListGrid;

	private UILabel noMatsLabel;

	private UISprite relicListBg;

	private UIGrid relicListGrid;

	private UILabel noRelicsLabel;

	private UISprite enchantListBg;

	private UIGrid enchantListGrid;

	private UILabel noEnchantsLabel;

	private UILabel matCount;

	private ParticleSystem matFireworks;

	private UILabel relicCount;

	private ParticleSystem relicFireworks;

	private UILabel enchantCount;

	private ParticleSystem enchantFireworks;

	private UIButton closeButton;

	private UIButton repeatButton;

	private Smith smith;

	private Area taskArea;

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
		exploreResultBg = commonScreenObject.findChild(base.gameObject, "ExploreResult_bg").GetComponent<UISprite>();
		smithName = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/SmithName_bg/SmithName_label").GetComponent<UILabel>();
		smithImage = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/SmithImage_texture").GetComponent<UITexture>();
		smithLvlUp = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/SmithLv_bg").GetComponent<UISprite>();
		smithBubble = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/SmithText_bg").GetComponent<UISprite>();
		smithBubbleText = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/SmithText_bg").GetComponentInChildren<UILabel>();
		smithMood = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>();
		smithStatusAlert = commonScreenObject.findChild(exploreResultBg.gameObject, "SmithImage_bg/StatusEffect").GetComponent<UISprite>();
		locationTitle = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreLocation_bg/1Location_title").GetComponentInChildren<UILabel>();
		locationLabel = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreLocation_bg/1Location_value").GetComponentInChildren<UILabel>();
		matListBg = commonScreenObject.findChild(exploreResultBg.gameObject, "MaterialList_bg").GetComponentInChildren<UISprite>();
		matListGrid = commonScreenObject.findChild(exploreResultBg.gameObject, "MaterialList_bg/MaterialList_grid").GetComponentInChildren<UIGrid>();
		noMatsLabel = commonScreenObject.findChild(exploreResultBg.gameObject, "MaterialList_bg/NoMaterials_label").GetComponentInChildren<UILabel>();
		relicListBg = commonScreenObject.findChild(exploreResultBg.gameObject, "RelicList_bg").GetComponentInChildren<UISprite>();
		relicListGrid = commonScreenObject.findChild(exploreResultBg.gameObject, "RelicList_bg/RelicList_grid").GetComponentInChildren<UIGrid>();
		noRelicsLabel = commonScreenObject.findChild(exploreResultBg.gameObject, "RelicList_bg/NoRelics_label").GetComponentInChildren<UILabel>();
		enchantListBg = commonScreenObject.findChild(exploreResultBg.gameObject, "EnchantmentList_bg").GetComponentInChildren<UISprite>();
		enchantListGrid = commonScreenObject.findChild(exploreResultBg.gameObject, "EnchantmentList_bg/EnchantmentList_grid").GetComponentInChildren<UIGrid>();
		noEnchantsLabel = commonScreenObject.findChild(exploreResultBg.gameObject, "EnchantmentList_bg/NoEnchantments_label").GetComponentInChildren<UILabel>();
		matCount = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreResultBanner/MatsFound_bg/MatsFound_label").GetComponentInChildren<UILabel>();
		matFireworks = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreResultBanner/MatsFound_bg/Mats_particles").GetComponentInChildren<ParticleSystem>();
		relicCount = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreResultBanner/RelicsFound_bg/RelicsFound_label").GetComponentInChildren<UILabel>();
		relicFireworks = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreResultBanner/RelicsFound_bg/Relics_particles").GetComponentInChildren<ParticleSystem>();
		enchantCount = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreResultBanner/EnchantsFound_bg/EnchantsFound_label").GetComponentInChildren<UILabel>();
		enchantFireworks = commonScreenObject.findChild(exploreResultBg.gameObject, "ExploreResultBanner/EnchantsFound_bg/Enchants_particles").GetComponentInChildren<ParticleSystem>();
		closeButton = commonScreenObject.findChild(exploreResultBg.gameObject, "Close_button").GetComponent<UIButton>();
		repeatButton = commonScreenObject.findChild(exploreResultBg.gameObject, "Repeat_button").GetComponent<UIButton>();
		statusString = string.Empty;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Repeat_button":
		{
			smith.returnToShopStandby();
			bool flag = sendSmithRepeatExplore();
			GameObject gameObject2 = GameObject.Find("Panel_BottomMenu");
			if (gameObject2 != null)
			{
				gameObject2.GetComponent<BottomMenuController>().refreshBottomButtons();
			}
			if (flag)
			{
				viewController.closeExploreResult();
			}
			else
			{
				viewController.closeExploreResult(smith);
			}
			break;
		}
		case "Close_button":
		{
			smith.returnToShopStandby();
			GameObject gameObject = GameObject.Find("Panel_BottomMenu");
			if (gameObject != null)
			{
				gameObject.GetComponent<BottomMenuController>().refreshBottomButtons();
			}
			viewController.closeExploreResult(smith);
			break;
		}
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
			case "Repeat_button":
				if (taskArea != null)
				{
					string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("exploreResult04", "[areaName]", taskArea.getAreaName());
					tooltipScript.showText(textByRefIdWithDynText);
				}
				return;
			}
			string[] array = hoverName.Split('_');
			if (array[0] == "ExploreListObj")
			{
				Item item = itemList[CommonAPI.parseInt(array[1])];
				tooltipScript.showText(item.getItemStandardTooltipString(taskArea));
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
			case "ExploreResultTitle_label":
				uILabel.text = gameData.getTextByRefId("exploreResult01").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "NoMaterials_label":
				uILabel.text = gameData.getTextByRefId("menuGeneral06").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "NoRelics_label":
				uILabel.text = gameData.getTextByRefId("menuGeneral06").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "NoEnchantments_label":
				uILabel.text = gameData.getTextByRefId("menuGeneral06").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "1Location_title":
				uILabel.text = gameData.getTextByRefId("location");
				break;
			case "MaterialTitle_label":
				uILabel.text = gameData.getTextByRefId("materials").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "RelicTitle_label":
				uILabel.text = gameData.getTextByRefId("relics").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "EnchantmentTitle_label":
				uILabel.text = gameData.getTextByRefId("enchantments").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Enchants_label":
				uILabel.text = gameData.getTextByRefId("enchantments").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Mats_label":
				uILabel.text = gameData.getTextByRefId("materials").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Relics_label":
				uILabel.text = gameData.getTextByRefId("relics").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "SmithLv_label":
				uILabel.text = gameData.getTextByRefId("smithStats20").ToUpper(CultureInfo.InvariantCulture);
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
		taskArea = aSmith.getExploreArea();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		setLabels();
		Dictionary<string, int> exploreFoundItems = getExploreFoundItems();
		SmithMood moodState = aSmith.getMoodState();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		itemList = new List<Item>();
		foreach (string key in exploreFoundItems.Keys)
		{
			Item itemByRefId = gameData.getItemByRefId(key);
			ExploreItem aItem = taskArea.getExploreItemList()[key];
			int num5 = exploreFoundItems[key];
			itemList.Add(itemByRefId);
			switch (itemByRefId.getItemType())
			{
			case ItemType.ItemTypeMaterial:
			{
				GameObject gameObject3 = commonScreenObject.createPrefab(matListGrid.gameObject, "ExploreListObj_" + num4, "Prefab/Explore/ExploreListObj", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject3.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/materials/" + itemByRefId.getImage());
				gameObject3.GetComponentInChildren<UILabel>().text = num5.ToString();
				commonScreenObject.findChild(gameObject3, "Item_frame").GetComponent<UISprite>().color = CommonAPI.getRarityColor(aItem, itemByRefId.getItemType());
				num++;
				break;
			}
			case ItemType.ItemTypeEnhancement:
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(enchantListGrid.gameObject, "ExploreListObj_" + num4, "Prefab/Explore/ExploreListObj", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject2.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Enchantment/" + itemByRefId.getImage());
				gameObject2.GetComponentInChildren<UILabel>().text = num5.ToString();
				commonScreenObject.findChild(gameObject2, "Item_frame").GetComponent<UISprite>().color = CommonAPI.getRarityColor(aItem, itemByRefId.getItemType());
				num2++;
				break;
			}
			case ItemType.ItemTypeRelic:
			{
				GameObject gameObject = commonScreenObject.createPrefab(relicListGrid.gameObject, "ExploreListObj_" + num4, "Prefab/Explore/ExploreListObj", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/relics/" + itemByRefId.getImage());
				gameObject.GetComponentInChildren<UILabel>().text = num5.ToString();
				commonScreenObject.findChild(gameObject, "Item_frame").GetComponent<UISprite>().color = CommonAPI.getRarityColor(aItem, itemByRefId.getItemType());
				num3++;
				break;
			}
			}
			num4++;
		}
		matListGrid.enabled = true;
		matListGrid.Reposition();
		enchantListGrid.enabled = true;
		enchantListGrid.Reposition();
		relicListGrid.enabled = true;
		relicListGrid.Reposition();
		matCount.text = num.ToString();
		if (num > 0)
		{
			matFireworks.Play();
			noMatsLabel.alpha = 0f;
		}
		enchantCount.text = num2.ToString();
		if (num2 > 0)
		{
			enchantFireworks.Play();
			noEnchantsLabel.alpha = 0f;
		}
		relicCount.text = num3.ToString();
		if (num3 > 0)
		{
			relicFireworks.Play();
			noRelicsLabel.alpha = 0f;
		}
		int[] values = new int[4] { num, num2, num3, 1 };
		int num6 = Mathf.Max(values);
		int num7 = (num6 + 1) / 2 * 60;
		matListBg.height = 25 + num7;
		relicListBg.height = 25 + num7;
		enchantListBg.height = 25 + num7;
		exploreResultBg.height = 300 + num7;
		exploreResultBg.transform.localPosition = new Vector3(0f, 150f + (float)num7 / 2f, 0f);
		closeButton.transform.localPosition = new Vector3(-120f, -315f - (float)num7, 0f);
		repeatButton.transform.localPosition = new Vector3(100f, -315f - (float)num7, 0f);
		int exploreLevel = CommonAPI.getExploreLevel(smith.getExploreExp());
		int aExp = (int)((float)aSmith.getExploreArea().getExpGrowth() * Random.Range(0.9f, 1.1f));
		aSmith.addExploreExp(aExp);
		locationLabel.text = taskArea.getAreaName();
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
		float aReduce = aSmith.getExploreArea().getMoodFactor() * 1f;
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
		taskArea.removeAreaSmithRefID(aSmith.getSmithRefId());
		smithName.text = smith.getSmithName();
		smithLvlUp.transform.localScale = Vector3.zero;
		if (exploreLevel < aSmith.getExploreLevel())
		{
			commonScreenObject.tweenScale(smithLvlUp.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		}
		smithImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
		if (num4 > 0)
		{
			smithBubbleText.text = CommonAPI.getSmithExploreMoodText(moodState, "EXPLORE");
		}
		else
		{
			smithBubbleText.text = gameData.getRandomTextBySetRefId("smithExploreEmpty");
		}
		commonScreenObject.tweenScale(smithBubble.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		smithMood.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(moodState2));
		audioController.playSmithMoodSfx(moodState2);
		if (checkRepeatExploreAvailable(taskArea, showPopup: false))
		{
			string textByRefId = gameData.getTextByRefId("exploreResult02");
			repeatButton.GetComponentInChildren<UILabel>().text = textByRefId;
			repeatButton.isEnabled = true;
		}
		else
		{
			repeatButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("exploreResult03");
			repeatButton.isEnabled = false;
		}
	}

	private bool checkRepeatExploreAvailable(Area aArea, bool showPopup)
	{
		Player player = game.getPlayer();
		if (player.getAreaRegion() != aArea.getRegion())
		{
			return false;
		}
		if (aArea.getAreaSmithRefID(smith.getSmithRefId()).Count <= 2)
		{
			return true;
		}
		if (showPopup)
		{
			string textByRefId = game.getGameData().getTextByRefId("menuSmithManagement53");
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
		}
		return false;
	}

	private bool sendSmithRepeatExplore()
	{
		Player player = game.getPlayer();
		if (checkRepeatExploreAvailable(taskArea, showPopup: true))
		{
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("901");
			taskArea.addTimesExplored(1);
			List<int> list = new List<int>();
			list.Add(taskArea.getTravelTime());
			list.Add(10);
			list.Add(taskArea.getTravelTime());
			list.Add(-1);
			List<SmithExploreState> list2 = new List<SmithExploreState>();
			list2.Add(SmithExploreState.SmithExploreStateTravelToExplore);
			list2.Add(SmithExploreState.SmithExploreStateExplore);
			list2.Add(SmithExploreState.SmithExploreStateExploreTravelHome);
			list2.Add(SmithExploreState.SmithExploreStateExploreReturned);
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(taskArea.getAreaRefId(), seasonByMonth);
			smith.setSmithAction(smithActionByRefId, taskArea.getTravelTime() * 2 + 10);
			smith.setExploreStateList(list2, list);
			smith.setExploreArea(taskArea);
			smith.setAreaStatusList(areaStatusListByAreaAndSeason);
			taskArea.addAreaSmithRefID(smith.getSmithRefId());
			return true;
		}
		return false;
	}

	private Dictionary<string, int> getExploreFoundItems()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		Dictionary<string, ExploreItem> exploreItemList = taskArea.getExploreItemList();
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		int num = 0;
		foreach (string key in exploreItemList.Keys)
		{
			if (gameData.getItemByRefId(key).checkScenarioAllow(itemLockSet))
			{
				list.Add(key);
				list2.Add(exploreItemList[key].getProbability());
				num += exploreItemList[key].getProbability();
			}
		}
		if (num < 1000)
		{
			list.Add(string.Empty);
			list2.Add(1000 - num);
		}
		int exploreLevel = CommonAPI.getExploreLevel(smith.getExploreExp());
		float moodEffect = CommonAPI.getMoodEffect(smith.getMoodState(), 0.7f, 1f, 0.1f);
		int num2 = Mathf.FloorToInt((10f + 0.9f * Mathf.Pow(exploreLevel - 1, 1.1f)) * moodEffect);
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 0; i < num2; i++)
		{
			int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list2);
			if (list[weightedRandomIndex] != string.Empty)
			{
				if (dictionary.ContainsKey(list[weightedRandomIndex]))
				{
					dictionary[list[weightedRandomIndex]] = dictionary[list[weightedRandomIndex]] + 1;
				}
				else
				{
					dictionary.Add(list[weightedRandomIndex], 1);
				}
			}
		}
		foreach (string key2 in dictionary.Keys)
		{
			Item itemByRefId = game.getGameData().getItemByRefId(key2);
			if (itemByRefId.getItemRefId().Length > 0)
			{
				itemByRefId.addItem(dictionary[key2]);
				itemByRefId.addItemFromExplore(dictionary[key2]);
			}
			taskArea.setExploreItemFound(key2);
		}
		return dictionary;
	}
}
