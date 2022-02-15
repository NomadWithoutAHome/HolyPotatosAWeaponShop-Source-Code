using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIForgeWeaponMenuController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private GameObject listBg;

	private UIScrollBar listScrollbar;

	private UIButton startButton;

	private string selectedWeaponTypeRefID;

	private string selectedWeaponRefID;

	private GameObject weaponBg;

	private UITexture weaponImg;

	private UILabel weaponTitle;

	private UILabel weaponDesc;

	private UIProgressBar atkWeapon;

	private UIProgressBar accWeapon;

	private UIProgressBar spdWeapon;

	private UIProgressBar magWeapon;

	private UIGrid weaponMaterialList;

	private UIGrid weaponTypeList;

	private UIGrid weaponListGrid;

	private UIGrid heroListGrid;

	private UILabel heroListTitle;

	private UILabel heroListWarning;

	private List<Hero> displayHeroList;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("ShortBubble").GetComponent<TooltipTextScript>();
		listBg = commonScreenObject.findChild(base.gameObject, "Lists_bg").gameObject;
		listScrollbar = commonScreenObject.findChild(listBg, "WeaponList_bg/WeaponList_scrollbar").GetComponent<UIScrollBar>();
		selectedWeaponTypeRefID = string.Empty;
		selectedWeaponRefID = string.Empty;
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "Selection_bg").gameObject;
		weaponBg = commonScreenObject.findChild(aObject, "WeaponBg").gameObject;
		weaponImg = commonScreenObject.findChild(weaponBg.gameObject, "WeaponImg").GetComponent<UITexture>();
		weaponTitle = commonScreenObject.findChild(aObject, "WeaponTitleBg/WeaponTitle").GetComponent<UILabel>();
		weaponDesc = commonScreenObject.findChild(aObject, "WeaponTitleBg/WeaponDesc").GetComponent<UILabel>();
		atkWeapon = commonScreenObject.findChild(aObject, "WeaponStatBg/Atk_sprite/Atk_bar").GetComponent<UIProgressBar>();
		accWeapon = commonScreenObject.findChild(aObject, "WeaponStatBg/Acc_sprite/Acc_bar").GetComponent<UIProgressBar>();
		spdWeapon = commonScreenObject.findChild(aObject, "WeaponStatBg/Spd_sprite/Spd_bar").GetComponent<UIProgressBar>();
		magWeapon = commonScreenObject.findChild(aObject, "WeaponStatBg/Mag_sprite/Mag_bar").GetComponent<UIProgressBar>();
		weaponMaterialList = commonScreenObject.findChild(aObject, "MaterialList").GetComponent<UIGrid>();
		startButton = commonScreenObject.findChild(aObject, "Start_button").GetComponent<UIButton>();
		weaponTypeList = commonScreenObject.findChild(listBg, "WeaponType_bg/WeaponType_grid").GetComponent<UIGrid>();
		weaponListGrid = commonScreenObject.findChild(listBg, "WeaponList_bg/Panel_WeaponList/WeaponList_grid").GetComponent<UIGrid>();
		heroListGrid = commonScreenObject.findChild(listBg, "HeroList_bg/HeroList_grid").GetComponent<UIGrid>();
		heroListTitle = commonScreenObject.findChild(listBg, "HeroList_bg/HeroListTitle_label").GetComponent<UILabel>();
		heroListWarning = commonScreenObject.findChild(listBg, "HeroList_bg/HeroListWarning_label").GetComponent<UILabel>();
		setLabels();
	}

	public void setLabels()
	{
		GameData gameData = game.getGameData();
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "ForgingTitle_label":
				uILabel.text = gameData.getTextByRefId("menuForge01").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Start_label":
				uILabel.text = gameData.getTextByRefId("questSelect11").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "WeaponGrowth_label":
				uILabel.text = gameData.getTextByRefId("menuForgeWeapon03").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "HeroListTitle_label":
				uILabel.text = gameData.getTextByRefId("menuForgeWeapon05");
				break;
			}
		}
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Start_button":
		{
			GameData gameData = game.getGameData();
			Player player = game.getPlayer();
			player.setLastSelectWeapon(gameData.getWeaponByRefId(selectedWeaponRefID));
			GameObject gameObject = GameObject.Find("Panel_Tutorial");
			if (gameObject != null)
			{
				GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
				if (component.checkCurrentTutorial("10012"))
				{
					component.nextTutorial();
					viewController.setGameStarted(started: true);
				}
			}
			shopMenuController.startForging();
			break;
		}
		case "Close_button":
			tooltipScript.setInactive();
			viewController.closeForgeMenuNewPopup(resumeFromPlayerPause: false);
			break;
		default:
		{
			string[] array = gameObjectName.Split('_');
			switch (array[0])
			{
			case "WeaponTypeListObj":
				selectWeaponType(array[1]);
				break;
			case "WeaponListObj":
				loadWeaponDetail(array[1]);
				break;
			}
			break;
		}
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		GameData gameData = game.getGameData();
		if (isOver)
		{
			if (hoverName != null && hoverName == "WeaponGrowthInfo_bg")
			{
				tooltipScript.showText(gameData.getTextByRefId("menuForgeWeapon04"));
				return;
			}
			string[] array = hoverName.Split('_');
			if (array[0] == "WeaponMatObj")
			{
				Item itemByRefId = gameData.getItemByRefId(array[1]);
				tooltipScript.showText(itemByRefId.getItemName());
			}
			else if (array[0] == "WeaponListObj")
			{
				Weapon weaponByRefId = gameData.getWeaponByRefId(array[1]);
				if (weaponByRefId.getWeaponUnlocked())
				{
					tooltipScript.showText(weaponByRefId.getWeaponName());
				}
				else
				{
					tooltipScript.showText(gameData.getTextByRefId("menuForgeWeapon02"));
				}
			}
			if (array[0] == "HeroListObj")
			{
				Hero hero = displayHeroList[CommonAPI.parseInt(array[1])];
				tooltipScript.showText(hero.getHeroStandardInfoString());
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted() && !GameObject.Find("blackmask_popup").GetComponent<BoxCollider>().enabled)
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Start_button");
		}
		else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	private void reset()
	{
		GameData gameData = game.getGameData();
		startButton.isEnabled = false;
		atkWeapon.value = 0f;
		spdWeapon.value = 0f;
		accWeapon.value = 0f;
		magWeapon.value = 0f;
		weaponTitle.text = "-";
		weaponDesc.text = "-";
		selectWeaponTypeListDisplay(string.Empty);
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		reset();
		foreach (WeaponType unlockedWeaponType in player.getUnlockedWeaponTypeList())
		{
			GameObject gameObject = commonScreenObject.createPrefab(weaponTypeList.gameObject, "WeaponTypeListObj_" + unlockedWeaponType.getWeaponTypeRefId(), "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/WeaponTypeListObj", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UILabel>().text = unlockedWeaponType.getWeaponTypeName();
			commonScreenObject.findChild(gameObject, "WeaponTypeListObj_bg/Category_sprite").GetComponent<UISprite>().spriteName = "icon_" + unlockedWeaponType.getImage();
			if (!gameData.checkHasUnforgedWeapon(unlockedWeaponType.getWeaponTypeRefId(), itemLockSet))
			{
				commonScreenObject.findChild(gameObject, "WeaponTypeListObj_bg/New_label").GetComponent<UILabel>().alpha = 0f;
			}
		}
		weaponTypeList.Reposition();
		weaponTypeList.enabled = true;
		List<Weapon> weaponList = player.getWeaponList();
		selectWeaponType(weaponList[0].getWeaponTypeRefId());
		GameObject gameObject2 = GameObject.Find("Panel_Tutorial");
		if (gameObject2 != null)
		{
			GUITutorialController component = gameObject2.GetComponent<GUITutorialController>();
			if (component.checkCurrentTutorial("10011"))
			{
				component.nextTutorial();
				setTutorialState();
			}
		}
	}

	private void clearWeaponList()
	{
		while (weaponListGrid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(weaponListGrid.transform.GetChild(0).gameObject);
		}
	}

	private void selectWeaponTypeListDisplay(string aWeaponTypeRefId)
	{
		foreach (Transform child in weaponTypeList.GetChildList())
		{
			GameObject gameObject = commonScreenObject.findChild(child.gameObject, "WeaponTypeListObj_bg").gameObject;
			if (child.gameObject.name == "WeaponTypeListObj_" + aWeaponTypeRefId)
			{
				gameObject.transform.localPosition = new Vector3(10f, 0f, 0f);
				child.GetComponent<UIButton>().isEnabled = false;
			}
			else
			{
				gameObject.transform.localPosition = Vector3.zero;
				child.GetComponent<UIButton>().isEnabled = true;
			}
		}
	}

	private void selectWeaponType(string aWeaponTypeRefId)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		selectWeaponTypeListDisplay(aWeaponTypeRefId);
		if (!(selectedWeaponTypeRefID != aWeaponTypeRefId))
		{
			return;
		}
		selectedWeaponTypeRefID = aWeaponTypeRefId;
		string text = string.Empty;
		updateHeroListDisplay();
		listScrollbar.value = 0f;
		clearWeaponList();
		if (selectedWeaponTypeRefID != "901")
		{
			foreach (Weapon item in gameData.getWeaponListByType(selectedWeaponTypeRefID, checkDLC: true, itemLockSet))
			{
				GameObject gameObject = commonScreenObject.createPrefab(weaponListGrid.gameObject, "WeaponListObj_" + item.getWeaponRefId(), "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/ForgeWeaponObj", Vector3.zero, Vector3.one, Vector3.zero);
				UITexture component = commonScreenObject.findChild(gameObject, "ObjectButton/ObjectImg").GetComponent<UITexture>();
				component.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + item.getImage());
				UIButton component2 = gameObject.GetComponent<UIButton>();
				if (!item.getWeaponUnlocked())
				{
					component.color = Color.black;
					component2.isEnabled = true;
					component2.defaultColor = Color.grey;
					component2.hover = Color.grey;
					component2.pressed = Color.grey;
					component2.disabledColor = Color.grey;
					commonScreenObject.findChild(gameObject, "ObjectButton/Lock_sprite").GetComponent<UISprite>().alpha = 1f;
					commonScreenObject.findChild(gameObject, "ObjectButton/New_sprite").GetComponent<UISprite>().alpha = 0f;
					continue;
				}
				component.color = Color.white;
				component2.isEnabled = true;
				if (checkMats(item))
				{
					component2.ResetDefaultColor();
				}
				else
				{
					component2.defaultColor = new Color(0.5f, 0.5f, 0.5f);
				}
				commonScreenObject.findChild(gameObject, "ObjectButton/Lock_sprite").GetComponent<UISprite>().alpha = 0f;
				if (item.getTimesUsed() < 1)
				{
					commonScreenObject.findChild(gameObject, "ObjectButton/New_sprite").GetComponent<UISprite>().alpha = 1f;
				}
				else
				{
					commonScreenObject.findChild(gameObject, "ObjectButton/New_sprite").GetComponent<UISprite>().alpha = 0f;
				}
				if (text == string.Empty)
				{
					text = item.getWeaponRefId();
				}
			}
		}
		else
		{
			foreach (Weapon item2 in gameData.getWeaponListByType(selectedWeaponTypeRefID, checkDLC: true, itemLockSet))
			{
				if (item2.getWeaponUnlocked())
				{
					GameObject gameObject2 = commonScreenObject.createPrefab(weaponListGrid.gameObject, "WeaponListObj_" + item2.getWeaponRefId(), "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/ForgeWeaponObj", Vector3.zero, Vector3.one, Vector3.zero);
					UITexture component3 = commonScreenObject.findChild(gameObject2, "ObjectButton/ObjectImg").GetComponent<UITexture>();
					component3.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + item2.getImage());
					UIButton component4 = gameObject2.GetComponent<UIButton>();
					component4.isEnabled = true;
					if (checkMats(item2))
					{
						component4.ResetDefaultColor();
					}
					else
					{
						component4.defaultColor = new Color(0.5f, 0.5f, 0.5f);
					}
					commonScreenObject.findChild(gameObject2, "ObjectButton/Lock_sprite").GetComponent<UISprite>().alpha = 0f;
					if (item2.getTimesUsed() < 1)
					{
						commonScreenObject.findChild(gameObject2, "ObjectButton/New_sprite").GetComponent<UISprite>().alpha = 1f;
						component3.color = Color.black;
					}
					else
					{
						commonScreenObject.findChild(gameObject2, "ObjectButton/New_sprite").GetComponent<UISprite>().alpha = 0f;
						component3.color = Color.white;
					}
					if (text == string.Empty && gameData.getLegendaryHeroByWeaponRefId(item2.getWeaponRefId()).getRequestState() != RequestState.RequestStateCompleted)
					{
						text = item2.getWeaponRefId();
					}
				}
			}
		}
		weaponListGrid.Reposition();
		weaponListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		weaponListGrid.enabled = true;
		listScrollbar.value = 0f;
		updateWeaponListDisplay();
		if (text != string.Empty)
		{
			loadWeaponDetail(text);
		}
	}

	public void updateWeaponListDisplay()
	{
		foreach (Transform child in weaponListGrid.GetChildList())
		{
			if (child.gameObject.name == "WeaponListObj_" + selectedWeaponRefID)
			{
				child.GetComponentInChildren<UISprite>().spriteName = "bg_weaponselected";
				child.GetComponent<UIButton>().normalSprite = "bg_weaponselected";
			}
			else
			{
				child.GetComponentInChildren<UISprite>().spriteName = "bg_weapon";
				child.GetComponent<UIButton>().normalSprite = "bg_weapon";
			}
		}
	}

	public void updateHeroListDisplay()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		List<GameObject> list = new List<GameObject>();
		foreach (Transform child in heroListGrid.GetChildList())
		{
			list.Add(child.gameObject);
		}
		list.Reverse();
		foreach (GameObject item in list)
		{
			commonScreenObject.destroyPrefabImmediate(item);
		}
		if (selectedWeaponTypeRefID == "901")
		{
			heroListTitle.text = gameData.getTextByRefId("menuForgeWeapon05");
			heroListWarning.text = "-";
			return;
		}
		CommonAPI.debug("player.getAreaRegion() " + player.getAreaRegion());
		CommonAPI.debug("itemLockSetId " + itemLockSet);
		CommonAPI.debug("selectedWeaponTypeRefID " + selectedWeaponTypeRefID);
		List<string> forgeWeaponMenuHeroList = gameData.getForgeWeaponMenuHeroList(selectedWeaponTypeRefID, player.getAreaRegion(), itemLockSet);
		CommonAPI.debug("fullList " + forgeWeaponMenuHeroList.Count);
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		int num = 0;
		if (forgeWeaponMenuHeroList.Count > 1)
		{
			int num2 = CommonAPI.parseInt(forgeWeaponMenuHeroList[forgeWeaponMenuHeroList.Count - 1]);
			int num3 = 0;
			foreach (string item2 in forgeWeaponMenuHeroList)
			{
				if (num3 == forgeWeaponMenuHeroList.Count - 1)
				{
					break;
				}
				if (num3 < num2)
				{
					list2.Add(item2);
				}
				else
				{
					list3.Add(item2);
				}
				num3++;
			}
			List<Hero> heroListByRefIdList = gameData.getHeroListByRefIdList(list2, itemLockSet);
			List<Hero> heroListByRefIdList2 = gameData.getHeroListByRefIdList(list3, itemLockSet);
			displayHeroList = new List<Hero>();
			foreach (Hero item3 in heroListByRefIdList)
			{
				if (num < 6)
				{
					GameObject gameObject = commonScreenObject.createPrefab(heroListGrid.gameObject, "HeroListObj_" + num, "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/HeroListObj", Vector3.zero, Vector3.one, Vector3.zero);
					UITexture componentInChildren = gameObject.GetComponentInChildren<UITexture>();
					componentInChildren.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + item3.getImage());
					UILabel componentInChildren2 = gameObject.GetComponentInChildren<UILabel>();
					componentInChildren2.text = item3.getJobClassName();
					displayHeroList.Add(item3);
					num++;
				}
			}
			heroListGrid.Reposition();
		}
		if (num < 1)
		{
			heroListWarning.text = gameData.getTextByRefId("menuForgeWeapon06");
		}
		else
		{
			heroListWarning.text = string.Empty;
		}
	}

	public void loadWeaponDetail(string aWeaponRefID)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Weapon weaponByRefId = game.getGameData().getWeaponByRefId(aWeaponRefID);
		if (!weaponByRefId.getWeaponUnlocked() || !(selectedWeaponRefID != aWeaponRefID))
		{
			return;
		}
		selectedWeaponRefID = aWeaponRefID;
		weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
		if (weaponByRefId.getWeaponTypeRefId() == "901" && weaponByRefId.getTimesUsed() < 1)
		{
			weaponImg.color = Color.black;
		}
		else
		{
			weaponImg.color = Color.white;
		}
		weaponTitle.text = weaponByRefId.getWeaponName();
		weaponDesc.text = weaponByRefId.getWeaponDesc();
		atkWeapon.value = weaponByRefId.getAtkMult() - 1f;
		accWeapon.value = weaponByRefId.getAccMult() - 1f;
		spdWeapon.value = weaponByRefId.getSpdMult() - 1f;
		magWeapon.value = weaponByRefId.getMagMult() - 1f;
		clearMaterialObjList();
		Dictionary<string, int> materialList = weaponByRefId.getMaterialList();
		foreach (string key in materialList.Keys)
		{
			Item itemByRefId = gameData.getItemByRefId(key);
			GameObject gameObject = commonScreenObject.createPrefab(weaponMaterialList.gameObject, "WeaponMatObj_" + key, "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/WeaponMatObj", Vector3.zero, Vector3.one, Vector3.zero);
			UITexture componentInChildren = gameObject.GetComponentInChildren<UITexture>();
			componentInChildren.mainTexture = commonScreenObject.loadTexture("Image/materials/" + itemByRefId.getImage());
			int itemNum = itemByRefId.getItemNum();
			int num = materialList[key];
			string text = itemNum + "/" + CommonAPI.formatNumber(num);
			if (itemNum < num)
			{
				text = "[FF4842]" + text + "[-]";
				componentInChildren.color = Color.gray;
			}
			commonScreenObject.findChild(gameObject, "WeaponMat_qty").GetComponent<UILabel>().text = text;
		}
		weaponMaterialList.Reposition();
		weaponMaterialList.enabled = true;
		setStartButtonState();
		updateWeaponListDisplay();
	}

	public void clearMaterialObjList()
	{
		while (weaponMaterialList.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(weaponMaterialList.GetChild(0).gameObject);
		}
	}

	public void setStartButtonState()
	{
		if (selectedWeaponRefID != string.Empty)
		{
			Player player = game.getPlayer();
			GameData gameData = game.getGameData();
			Weapon weaponByRefId = gameData.getWeaponByRefId(selectedWeaponRefID);
			if (weaponByRefId.getWeaponTypeRefId() == "901" && gameData.getLegendaryHeroByWeaponRefId(weaponByRefId.getWeaponRefId()).getRequestState() == RequestState.RequestStateCompleted)
			{
				startButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("errorCommon11").ToUpper(CultureInfo.InvariantCulture);
				startButton.isEnabled = false;
			}
			else if (checkMats(weaponByRefId))
			{
				startButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("questSelect11").ToUpper(CultureInfo.InvariantCulture);
				startButton.isEnabled = true;
			}
			else
			{
				startButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("errorCommon10").ToUpper(CultureInfo.InvariantCulture);
				startButton.isEnabled = false;
			}
		}
	}

	private bool checkMats(Weapon aWeapon)
	{
		GameData gameData = game.getGameData();
		bool result = true;
		Dictionary<string, int> materialList = aWeapon.getMaterialList();
		foreach (string key in materialList.Keys)
		{
			int itemNum = gameData.getItemByRefId(key).getItemNum();
			int num = materialList[key];
			if (itemNum < num)
			{
				result = false;
			}
		}
		return result;
	}

	public void setTutorialState()
	{
		UIButton component = commonScreenObject.findChild(base.gameObject, "Selection_bg/Close_button").GetComponent<UIButton>();
		component.isEnabled = false;
	}
}
