using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIShopUpgradeHUDController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private GUIDecorationController decoController;

	private GUIGridController gridController;

	private GUICharacterAnimationController charAnimCtr;

	private UISprite button_decoration;

	private UILabel decorationLabel;

	private UISprite button_enhancement;

	private UILabel enhancementLabel;

	private UISprite button_workshop;

	private UILabel workshopLabel;

	private UIGrid filterGrid;

	private UIScrollBar itemList_scrollbar;

	private UIGrid itemListGrid;

	private GameObject button_upgrade;

	private UILabel shoplevelLabel;

	private List<string> decoFilterList;

	private List<string> workshopFilterList;

	private List<string> enhanceFilterList;

	private Dictionary<string, GameObject> filterListObj;

	private Dictionary<string, GameObject> itemListObj;

	private List<Furniture> workshopList;

	private List<Decoration> decorationList;

	private UpgradeCategory selectedCategory;

	private string selectedFilter;

	private string selectedFilterIndex;

	private List<GameObject> previewGameObject;

	private int shopLevel;

	private Vector3 decoOrigPos;

	private Vector3 enhanceOrigPos;

	private Vector3 workshopOrigPos;

	private Color32 categoryColor;

	private Color32 categoryColorDisabled;

	private Color32 previewColor;

	private string filterPrefix;

	private string decoPrefix;

	private string enhancePrefix;

	private string workshopPrefix;

	private string buyPrefix;

	private string equipPrefix;

	private string equippedPrefix;

	private string ownedPrefix;

	private bool isOverMenu;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		decoController = GameObject.Find("GUIDecorationController").GetComponent<GUIDecorationController>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		charAnimCtr = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
		button_decoration = commonScreenObject.findChild(base.gameObject, "button_decoration").GetComponent<UISprite>();
		decorationLabel = commonScreenObject.findChild(button_decoration.gameObject, "decorationLabel").GetComponent<UILabel>();
		button_enhancement = commonScreenObject.findChild(base.gameObject, "button_enhancement").GetComponent<UISprite>();
		enhancementLabel = commonScreenObject.findChild(button_enhancement.gameObject, "enhancementLabel").GetComponent<UILabel>();
		button_workshop = commonScreenObject.findChild(base.gameObject, "button_workshop").GetComponent<UISprite>();
		workshopLabel = commonScreenObject.findChild(button_workshop.gameObject, "workshopLabel").GetComponent<UILabel>();
		filterGrid = commonScreenObject.findChild(base.gameObject, "filterGrid").GetComponent<UIGrid>();
		itemList_scrollbar = commonScreenObject.findChild(base.gameObject, "ItemListBg/ItemList_scrollbar").GetComponent<UIScrollBar>();
		itemListGrid = commonScreenObject.findChild(base.gameObject, "ItemListBg/Panel_ItemList/ItemListGrid").GetComponent<UIGrid>();
		button_upgrade = commonScreenObject.findChild(base.gameObject, "button_upgrade").gameObject;
		shoplevelLabel = commonScreenObject.findChild(base.gameObject, "button_upgrade/shoplevelLabel").GetComponent<UILabel>();
		decoFilterList = new List<string>();
		decoFilterList.Add(gameData.getTextByRefId("upgradeMenu04"));
		decoFilterList.Add(gameData.getTextByRefId("upgradeMenu16"));
		decoFilterList.Add(gameData.getTextByRefId("upgradeMenu17"));
		decoFilterList.Add(gameData.getTextByRefId("upgradeMenu18"));
		workshopFilterList = new List<string>();
		workshopFilterList.Add(gameData.getTextByRefId("upgradeMenu04"));
		workshopFilterList.Add(gameData.getTextByRefId("upgradeMenu05"));
		workshopFilterList.Add(gameData.getTextByRefId("upgradeMenu06"));
		workshopFilterList.Add(gameData.getTextByRefId("upgradeMenu07"));
		workshopFilterList.Add(gameData.getTextByRefId("upgradeMenu08"));
		enhanceFilterList = new List<string>();
		enhanceFilterList.Add(gameData.getTextByRefId("upgradeMenu04"));
		enhanceFilterList.Add(gameData.getTextByRefId("upgradeMenu12"));
		enhanceFilterList.Add(gameData.getTextByRefId("upgradeMenu13"));
		enhanceFilterList.Add(gameData.getTextByRefId("upgradeMenu14"));
		filterListObj = new Dictionary<string, GameObject>();
		itemListObj = new Dictionary<string, GameObject>();
		workshopList = new List<Furniture>();
		decorationList = new List<Decoration>();
		selectedCategory = UpgradeCategory.UpgradeCategoryBlank;
		selectedFilter = string.Empty;
		selectedFilterIndex = string.Empty;
		previewGameObject = new List<GameObject>();
		shopLevel = game.getPlayer().getShopLevelInt();
		decoOrigPos = button_decoration.transform.localPosition;
		enhanceOrigPos = button_enhancement.transform.localPosition;
		workshopOrigPos = button_workshop.transform.localPosition;
		categoryColor = new Color32(254, byte.MaxValue, 2, byte.MaxValue);
		categoryColorDisabled = new Color32(254, byte.MaxValue, 2, 0);
		previewColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 128);
		filterPrefix = "filter_";
		decoPrefix = "deco_";
		enhancePrefix = "enhance_";
		workshopPrefix = "workshop_";
		buyPrefix = "buy";
		equipPrefix = "equip";
		equippedPrefix = "equipped";
		ownedPrefix = "owned";
		isOverMenu = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "closeButton":
			viewController.closeShopUpgradeMenu(resume: true, resumeFromPlayerPause: false);
			return;
		case "button_decoration":
			if (selectedCategory != UpgradeCategory.UpgradeCategoryDecoration)
			{
				selectCategory(UpgradeCategory.UpgradeCategoryDecoration);
			}
			return;
		case "button_enhancement":
			if (selectedCategory != UpgradeCategory.UpgradeCategoryEnhancement)
			{
				selectCategory(UpgradeCategory.UpgradeCategoryEnhancement);
			}
			return;
		case "button_workshop":
			if (selectedCategory != 0)
			{
				selectCategory(UpgradeCategory.UpgradeCategoryWorkshop);
			}
			return;
		case "button_upgrade":
			upgradeShop();
			return;
		}
		string[] array = gameObjectName.Split('_');
		GameData gameData = game.getGameData();
		switch (array[0])
		{
		case "equipdeco":
			equipDeco(gameObjectName);
			break;
		case "buydeco":
			buyDeco(gameObjectName);
			break;
		case "filter":
			selectFilter(gameObjectName);
			break;
		case "buyworkshop":
			buyWorkshop(gameObjectName);
			break;
		case "buyenhance":
			buyEnhance(gameObjectName);
			break;
		}
	}

	public void processHover(bool isOver, string gameObjectName)
	{
		if (isOver)
		{
			isOverMenu = true;
			string[] array = gameObjectName.Split('_');
			GameData gameData = game.getGameData();
			switch (array[0])
			{
			case "buydeco":
			case "equipDeco":
			case "deco":
			{
				Decoration decorationByRefId = gameData.getDecorationByRefId(array[1]);
				if (decorationByRefId.getShopLevelReq() > shopLevel)
				{
					break;
				}
				List<DecorationPosition> decoPosByRefIdAndShopLevel = gameData.getDecoPosByRefIdAndShopLevel(array[1], shopLevel);
				int num = 0;
				{
					foreach (DecorationPosition item in decoPosByRefIdAndShopLevel)
					{
						GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Panel_Obstacles"), "decoPreview" + num, "Prefab/Decoration/Decoration", item.getDecorationPosition(), Vector3.one, new Vector3(item.getXDegree(), item.getYDegree(), 0f));
						gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Decoration/" + item.getDecorationImage());
						gameObject.GetComponent<SpriteRenderer>().color = previewColor;
						gameObject.GetComponent<SpriteRenderer>().sortingOrder = item.getSortOrder() + 1;
						gameObject.GetComponent<SpriteRenderer>().sortingLayerName = item.getSortLayer();
						previewGameObject.Add(gameObject);
						num++;
					}
					break;
				}
			}
			case "buyenhance":
			case "enhance":
				createEnhancePreview(gameObjectName);
				break;
			}
			return;
		}
		isOverMenu = false;
		if (previewGameObject == null)
		{
			return;
		}
		foreach (GameObject item2 in previewGameObject)
		{
			commonScreenObject.destroyPrefabImmediate(item2);
		}
		previewGameObject.Clear();
	}

	private void Update()
	{
		handleInput();
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("closeButton");
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		decorationLabel.text = gameData.getTextByRefId("upgradeCategory03");
		enhancementLabel.text = gameData.getTextByRefId("upgradeCategory02");
		workshopLabel.text = gameData.getTextByRefId("upgradeCategory01");
		button_upgrade.SetActive(value: false);
		shoplevelLabel.text = gameData.getTextByRefId("upgradeMenu15") + " : " + shopLevel;
		selectCategory(UpgradeCategory.UpgradeCategoryDecoration);
	}

	private void selectCategory(UpgradeCategory aCategory)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int num = 0;
		switch (aCategory)
		{
		case UpgradeCategory.UpgradeCategoryDecoration:
		{
			selectedCategory = UpgradeCategory.UpgradeCategoryDecoration;
			button_decoration.spriteName = "bt_decoactive";
			Vector3 localPosition3 = decoOrigPos;
			localPosition3.y += 15f;
			button_decoration.transform.localPosition = localPosition3;
			decorationLabel.color = categoryColor;
			button_enhancement.spriteName = "bt_enhance";
			button_enhancement.transform.localPosition = enhanceOrigPos;
			enhancementLabel.color = categoryColorDisabled;
			button_workshop.spriteName = "bt_workshop";
			button_workshop.transform.localPosition = workshopOrigPos;
			workshopLabel.color = categoryColorDisabled;
			foreach (GameObject value in filterListObj.Values)
			{
				commonScreenObject.destroyPrefabImmediate(value);
			}
			filterListObj.Clear();
			List<RefDecorationType> decorationTypeList = gameData.getDecorationTypeList();
			foreach (string decoFilter in decoFilterList)
			{
				GameObject gameObject3 = commonScreenObject.createPrefab(filterGrid.gameObject, filterPrefix + num + "_" + decoFilter, "Prefab/ShopUpgrade/button_filter", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject3.GetComponentInChildren<UILabel>().text = decoFilter;
				filterListObj.Add(gameObject3.name, gameObject3);
				if (decoFilter == gameData.getTextByRefId("upgradeMenu04"))
				{
					gameObject3.GetComponent<UISprite>().spriteName = "bt_subcatselected";
					selectedFilter = decoFilter;
					selectedFilterIndex = num.ToString();
				}
				num++;
			}
			filterGrid.Reposition();
			spawnItem();
			break;
		}
		case UpgradeCategory.UpgradeCategoryWorkshop:
		{
			selectedCategory = UpgradeCategory.UpgradeCategoryWorkshop;
			button_workshop.spriteName = "bt_workshopactive";
			Vector3 localPosition2 = workshopOrigPos;
			localPosition2.y += 15f;
			button_workshop.transform.localPosition = localPosition2;
			workshopLabel.color = categoryColor;
			button_decoration.spriteName = "bt_deco";
			button_decoration.transform.localPosition = decoOrigPos;
			decorationLabel.color = categoryColorDisabled;
			button_enhancement.spriteName = "bt_enhance";
			button_enhancement.transform.localPosition = enhanceOrigPos;
			enhancementLabel.color = categoryColorDisabled;
			foreach (GameObject value2 in filterListObj.Values)
			{
				commonScreenObject.destroyPrefabImmediate(value2);
			}
			filterListObj.Clear();
			foreach (string workshopFilter in workshopFilterList)
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(filterGrid.gameObject, filterPrefix + num + "_" + workshopFilter, "Prefab/ShopUpgrade/button_filter", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject2.GetComponentInChildren<UILabel>().text = workshopFilter;
				filterListObj.Add(gameObject2.name, gameObject2);
				if (workshopFilter == gameData.getTextByRefId("upgradeMenu04"))
				{
					gameObject2.GetComponent<UISprite>().spriteName = "bt_subcatselected";
					selectedFilter = workshopFilter;
					selectedFilterIndex = num.ToString();
				}
				num++;
			}
			filterGrid.Reposition();
			spawnItem();
			break;
		}
		case UpgradeCategory.UpgradeCategoryEnhancement:
		{
			selectedCategory = UpgradeCategory.UpgradeCategoryEnhancement;
			button_enhancement.spriteName = "bt_enhanceactive";
			Vector3 localPosition = enhanceOrigPos;
			localPosition.y += 15f;
			button_enhancement.transform.localPosition = localPosition;
			enhancementLabel.color = categoryColor;
			button_workshop.spriteName = "bt_workshop";
			button_workshop.transform.localPosition = workshopOrigPos;
			workshopLabel.color = categoryColorDisabled;
			button_decoration.spriteName = "bt_deco";
			button_decoration.transform.localPosition = decoOrigPos;
			decorationLabel.color = categoryColorDisabled;
			foreach (GameObject value3 in filterListObj.Values)
			{
				commonScreenObject.destroyPrefabImmediate(value3);
			}
			filterListObj.Clear();
			foreach (string enhanceFilter in enhanceFilterList)
			{
				GameObject gameObject = commonScreenObject.createPrefab(filterGrid.gameObject, filterPrefix + num + "_" + enhanceFilter, "Prefab/ShopUpgrade/button_filter", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject.GetComponentInChildren<UILabel>().text = enhanceFilter;
				filterListObj.Add(gameObject.name, gameObject);
				if (enhanceFilter == gameData.getTextByRefId("upgradeMenu04"))
				{
					gameObject.GetComponent<UISprite>().spriteName = "bt_subcatselected";
					selectedFilter = enhanceFilter;
					selectedFilterIndex = num.ToString();
				}
				num++;
			}
			filterGrid.Reposition();
			spawnItem();
			break;
		}
		}
	}

	private void spawnItem()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		foreach (GameObject value in itemListObj.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		itemListObj.Clear();
		switch (selectedCategory)
		{
		case UpgradeCategory.UpgradeCategoryDecoration:
			if (selectedFilter == gameData.getTextByRefId("upgradeMenu04"))
			{
				decorationList = gameData.getDecorationList(isShopList: true, itemLockSet);
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu16"))
			{
				decorationList = gameData.getDecorationListByType("1", itemLockSet);
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu17"))
			{
				decorationList = gameData.getDecorationListByType("2", itemLockSet);
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu18"))
			{
				decorationList = gameData.getDecorationListNoWallCarpet(itemLockSet);
			}
			foreach (Decoration decoration in decorationList)
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(itemListGrid.gameObject, decoPrefix + decoration.getDecorationRefId(), "Prefab/ShopUpgrade/button_item", Vector3.zero, Vector3.one, Vector3.zero);
				GameObject aParent2 = commonScreenObject.findChild(gameObject2, "buttonPos").gameObject;
				if (decoration.getShopLevelReq() <= shopLevel)
				{
					commonScreenObject.findChild(gameObject2, "itemEffectLabel").GetComponent<UILabel>().text = generateDecoEffect(decoration);
					UITexture component3 = commonScreenObject.findChild(gameObject2, "itemImg").GetComponent<UITexture>();
					Texture texture4 = (component3.mainTexture = commonScreenObject.loadTexture("Image/Decoration/" + decoration.getDecorationImage()));
					if (texture4 != null)
					{
						float num3 = texture4.width;
						float num4 = texture4.height;
						CommonAPI.debug(gameObject2.name + ": " + num3 + " " + num4);
						CommonAPI.debug("scaledWidth: " + Mathf.RoundToInt(num3 / num4 * 70f));
						if (num4 > 70f)
						{
							component3.width = Mathf.RoundToInt(num3 / num4 * 70f);
							component3.height = 70;
							if (component3.width > 70)
							{
								component3.width = 70;
								component3.height = Mathf.RoundToInt(num4 / num3 * 70f);
							}
						}
					}
					if (decoration.checkIsPlayerOwned())
					{
						if (decoration.checkIsCurrentDisplay())
						{
							GameObject aObject4 = commonScreenObject.createPrefab(aParent2, equippedPrefix + decoPrefix + decoration.getDecorationRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
							commonScreenObject.findChild(aObject4, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
						}
						else
						{
							GameObject aObject5 = commonScreenObject.createPrefab(aParent2, equipPrefix + decoPrefix + decoration.getDecorationRefId(), "Prefab/ShopUpgrade/button_equip", Vector3.zero, Vector3.one, Vector3.zero);
							commonScreenObject.findChild(aObject5, "equipLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu02").ToUpper(CultureInfo.InvariantCulture);
						}
					}
					else
					{
						GameObject aObject6 = commonScreenObject.createPrefab(aParent2, buyPrefix + decoPrefix + decoration.getDecorationRefId(), "Prefab/ShopUpgrade/button_buy", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(aObject6, "buyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu01").ToUpper(CultureInfo.InvariantCulture);
						commonScreenObject.findChild(aObject6, "priceLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(decoration.getDecorationShopCost());
					}
				}
				else
				{
					commonScreenObject.findChild(gameObject2, "itemImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/ShopUpgrade/icon_lock");
					commonScreenObject.findChild(gameObject2, "unlockLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu11") + " " + decoration.getShopLevelReq();
					commonScreenObject.findChild(gameObject2, "greyOverlay").GetComponent<UISprite>().enabled = true;
				}
				itemListObj.Add(gameObject2.name, gameObject2);
			}
			break;
		case UpgradeCategory.UpgradeCategoryWorkshop:
			if (selectedFilter == gameData.getTextByRefId("upgradeMenu04"))
			{
				workshopList = gameData.getAllWorkstationsFurniture();
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu05"))
			{
				workshopList = gameData.getFurnitureListByType("601");
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu06"))
			{
				workshopList = gameData.getFurnitureListByType("701");
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu07"))
			{
				workshopList = gameData.getFurnitureListByType("801");
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu08"))
			{
				workshopList = gameData.getFurnitureListByType("901");
			}
			foreach (Furniture workshop in workshopList)
			{
				GameObject gameObject3 = commonScreenObject.createPrefab(itemListGrid.gameObject, workshopPrefix + workshop.getFurnitureRefId(), "Prefab/ShopUpgrade/button_item", Vector3.zero, Vector3.one, Vector3.zero);
				GameObject aParent3 = commonScreenObject.findChild(gameObject3, "buttonPos").gameObject;
				UIGrid component4 = commonScreenObject.findChild(gameObject3, "starGrid").GetComponent<UIGrid>();
				for (int j = 0; j < workshop.getFurnitureLevel(); j++)
				{
					commonScreenObject.createPrefab(component4.gameObject, "star" + workshop.getFurnitureRefId() + j, "Prefab/ShopUpgrade/starLevel", Vector3.zero, Vector3.one, Vector3.one);
				}
				component4.Reposition();
				if (workshop.getFurnitureLevel() == shopLevel)
				{
					commonScreenObject.findChild(gameObject3, "itemEffectLabel").GetComponent<UILabel>().text = workshop.getFurnitureCapacity() + " " + gameData.getTextByRefId("upgradeMenu09");
					UITexture component5 = commonScreenObject.findChild(gameObject3, "itemImg").GetComponent<UITexture>();
					Texture texture6 = (component5.mainTexture = commonScreenObject.loadTexture("Image/Obstacle/" + workshop.getImage()));
					if (texture6 != null)
					{
						float num5 = texture6.width;
						float num6 = texture6.height;
						CommonAPI.debug(gameObject3.name + ": " + num5 + " " + num6);
						CommonAPI.debug("scaledWidth: " + Mathf.RoundToInt(num5 / num6 * 70f));
						if (num6 > 70f)
						{
							component5.width = Mathf.RoundToInt(num5 / num6 * 70f);
							component5.height = 70;
						}
						if (component5.width > 70)
						{
							component5.width = 70;
							component5.height = Mathf.RoundToInt(num6 / num5 * 70f);
						}
					}
					if (game.getPlayer().checkOwnedFurnitureByRefID(workshop.getFurnitureRefId()))
					{
						GameObject aObject7 = commonScreenObject.createPrefab(aParent3, equippedPrefix + workshopPrefix + workshop.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(gameObject3, "greyOverlay").GetComponent<UISprite>().enabled = true;
						commonScreenObject.findChild(aObject7, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
					}
					else
					{
						GameObject aObject8 = commonScreenObject.createPrefab(aParent3, buyPrefix + workshopPrefix + workshop.getFurnitureRefId(), "Prefab/ShopUpgrade/button_buy", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(aObject8, "buyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu01").ToUpper(CultureInfo.InvariantCulture);
						commonScreenObject.findChild(aObject8, "priceLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(workshop.getFurnitureCost());
					}
				}
				else if (workshop.getFurnitureLevel() < shopLevel || workshop.getFurnitureLevel() == 1)
				{
					commonScreenObject.findChild(gameObject3, "itemEffectLabel").GetComponent<UILabel>().text = workshop.getFurnitureCapacity() + " " + gameData.getTextByRefId("upgradeMenu09");
					UITexture component6 = commonScreenObject.findChild(gameObject3, "itemImg").GetComponent<UITexture>();
					Texture texture8 = (component6.mainTexture = commonScreenObject.loadTexture("Image/Obstacle/" + workshop.getImage()));
					if (texture8 != null)
					{
						float num7 = texture8.width;
						float num8 = texture8.height;
						CommonAPI.debug(gameObject3.name + ": " + num7 + " " + num8);
						CommonAPI.debug("scaledWidth: " + Mathf.RoundToInt(num7 / num8 * 70f));
						if (num8 > 70f)
						{
							component6.width = Mathf.RoundToInt(num7 / num8 * 70f);
							component6.height = 70;
						}
						if (component6.width > 70)
						{
							component6.width = 70;
							component6.height = Mathf.RoundToInt(num8 / num7 * 70f);
						}
					}
					int furnitureLevel = player.getHighestPlayerFurnitureByType(workshop.getFurnitureType()).getFurnitureLevel();
					if (furnitureLevel == workshop.getFurnitureLevel())
					{
						GameObject aObject9 = commonScreenObject.createPrefab(aParent3, equippedPrefix + workshopPrefix + workshop.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(gameObject3, "greyOverlay").GetComponent<UISprite>().enabled = true;
						commonScreenObject.findChild(aObject9, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
					}
					else
					{
						GameObject aObject10 = commonScreenObject.createPrefab(aParent3, ownedPrefix + workshopPrefix + workshop.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(gameObject3, "greyOverlay").GetComponent<UISprite>().enabled = true;
						commonScreenObject.findChild(aObject10, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu10").ToUpper(CultureInfo.InvariantCulture);
					}
				}
				else if (workshop.getFurnitureLevel() > shopLevel)
				{
					commonScreenObject.findChild(gameObject3, "itemImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/ShopUpgrade/icon_lock");
					commonScreenObject.findChild(gameObject3, "unlockLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu11") + " " + workshop.getShopLevelRequirement();
					commonScreenObject.findChild(gameObject3, "greyOverlay").GetComponent<UISprite>().enabled = true;
				}
				itemListObj.Add(gameObject3.name, gameObject3);
			}
			break;
		case UpgradeCategory.UpgradeCategoryEnhancement:
			if (selectedFilter == gameData.getTextByRefId("upgradeMenu04"))
			{
				workshopList = gameData.getAllEnhancementsFurniture();
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu12"))
			{
				workshopList = gameData.getFurnitureListByType("501");
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu13"))
			{
				workshopList = gameData.getEnvironmentFurniture();
			}
			else if (selectedFilter == gameData.getTextByRefId("upgradeMenu14"))
			{
				workshopList = gameData.getFurnitureListByType("301");
			}
			foreach (Furniture workshop2 in workshopList)
			{
				GameObject gameObject = commonScreenObject.createPrefab(itemListGrid.gameObject, enhancePrefix + workshop2.getFurnitureRefId(), "Prefab/ShopUpgrade/button_item", Vector3.zero, Vector3.one, Vector3.zero);
				GameObject aParent = commonScreenObject.findChild(gameObject, "buttonPos").gameObject;
				UIGrid component = commonScreenObject.findChild(gameObject, "starGrid").GetComponent<UIGrid>();
				for (int i = 0; i < workshop2.getFurnitureLevel(); i++)
				{
					commonScreenObject.createPrefab(component.gameObject, "star" + workshop2.getFurnitureRefId() + i, "Prefab/ShopUpgrade/starLevel", Vector3.zero, Vector3.one, Vector3.one);
				}
				component.Reposition();
				if (workshop2.getFurnitureLevel() <= shopLevel || workshop2.getFurnitureLevel() == 1)
				{
					commonScreenObject.findChild(gameObject, "itemEffectLabel").GetComponent<UILabel>().text = workshop2.getFurnitureDesc();
					UITexture component2 = commonScreenObject.findChild(gameObject, "itemImg").GetComponent<UITexture>();
					Texture texture2 = (component2.mainTexture = commonScreenObject.loadTexture("Image/Obstacle/" + workshop2.getImage()));
					if (texture2 != null)
					{
						float num = texture2.width;
						float num2 = texture2.height;
						CommonAPI.debug(gameObject.name + ": " + num + " " + num2);
						CommonAPI.debug("scaledWidth: " + Mathf.RoundToInt(num / num2 * 70f));
						if (num2 > 70f)
						{
							component2.width = Mathf.RoundToInt(num / num2 * 70f);
							component2.height = 70;
						}
						if (component2.width > 70)
						{
							component2.width = 70;
							component2.height = Mathf.RoundToInt(num2 / num * 70f);
						}
					}
					Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType(workshop2.getFurnitureType());
					if (highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty && highestPlayerFurnitureByType.getFurnitureLevel() == workshop2.getFurnitureLevel())
					{
						GameObject aObject = commonScreenObject.createPrefab(aParent, equippedPrefix + enhancePrefix + workshop2.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(gameObject, "greyOverlay").GetComponent<UISprite>().enabled = true;
						commonScreenObject.findChild(aObject, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
					}
					else if (player.checkOwnedFurnitureByRefID(workshop2.getFurnitureRefId()))
					{
						GameObject aObject2 = commonScreenObject.createPrefab(aParent, ownedPrefix + enhancePrefix + workshop2.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(gameObject, "greyOverlay").GetComponent<UISprite>().enabled = true;
						commonScreenObject.findChild(aObject2, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu10").ToUpper(CultureInfo.InvariantCulture);
					}
					else
					{
						GameObject aObject3 = commonScreenObject.createPrefab(aParent, buyPrefix + enhancePrefix + workshop2.getFurnitureRefId(), "Prefab/ShopUpgrade/button_buy", Vector3.zero, Vector3.one, Vector3.zero);
						commonScreenObject.findChild(aObject3, "buyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu01").ToUpper(CultureInfo.InvariantCulture);
						commonScreenObject.findChild(aObject3, "priceLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(workshop2.getFurnitureCost());
					}
				}
				else if (workshop2.getFurnitureLevel() > shopLevel)
				{
					commonScreenObject.findChild(gameObject, "itemImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/ShopUpgrade/icon_lock");
					commonScreenObject.findChild(gameObject, "unlockLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu11") + " " + workshop2.getShopLevelRequirement();
					commonScreenObject.findChild(gameObject, "greyOverlay").GetComponent<UISprite>().enabled = true;
				}
				itemListObj.Add(gameObject.name, gameObject);
			}
			break;
		}
		itemListGrid.transform.parent.localPosition = Vector3.zero;
		itemListGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0f, 0f);
		itemListGrid.Reposition();
		itemListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		itemListGrid.enabled = true;
		itemList_scrollbar.value = 0f;
	}

	private void selectFilter(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		GameData gameData = game.getGameData();
		if (array[2] != selectedFilter)
		{
			filterListObj[filterPrefix + selectedFilterIndex + "_" + selectedFilter].GetComponent<UISprite>().spriteName = "bt_subcat";
			selectedFilterIndex = array[1];
			selectedFilter = array[2];
			filterListObj[gameObjectName].GetComponent<UISprite>().spriteName = "bt_subcatselected";
			spawnItem();
		}
	}

	private string generateDecoEffect(Decoration aDeco)
	{
		GameData gameData = game.getGameData();
		List<DecorationEffect> decorationEffectList = aDeco.getDecorationEffectList();
		string text = string.Empty;
		for (int i = 0; i < decorationEffectList.Count; i++)
		{
			string text2 = string.Empty;
			switch (decorationEffectList[i].getDecorationBoostType())
			{
			case "FORGE_EXP":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat01"));
				break;
			case "TRAIN_EXP":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat02"));
				break;
			case "MILESTONE_EXP":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat03"));
				break;
			case "SMITH_STA":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat04"));
				break;
			case "SMITH_POW":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat05"));
				break;
			case "SMITH_INT":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat06"));
				break;
			case "SMITH_TEC":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat07"));
				break;
			case "SMITH_LUC":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat08"));
				break;
			case "SMITH_ALL":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat09"));
				break;
			case "HERO_GOLD":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat10"));
				break;
			case "QUEST_GOLD":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat11"));
				break;
			case "FORGE_COST":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat12"));
				break;
			case "RECRUIT_COST":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat13"));
				break;
			case "SALARY":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat14"));
				break;
			case "HIRE_COST":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat15"));
				break;
			case "TRAIN_COST":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat16"));
				break;
			case "SMITH_ACTION_CHANCE":
			{
				SmithAction smithActionByRefId = gameData.getSmithActionByRefId(decorationEffectList[i].getDecorationBoostRefId());
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefIdWithDynText("decoEffectCat17", "[SmithAction]", smithActionByRefId.getText()));
				break;
			}
			case "DOG_LOVE":
				text2 = decorationEffectList[i].getDecorationStringBoost(gameData.getTextByRefId("decoEffectCat18"));
				break;
			}
			text += text2;
			if (i != decorationEffectList.Count - 1)
			{
				text += "\n";
			}
		}
		return text;
	}

	private void buyDeco(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Decoration decorationByRefId = gameData.getDecorationByRefId(array[1]);
		int decorationShopCost = decorationByRefId.getDecorationShopCost();
		if (shopMenuController.tryActionWithGold(decorationShopCost, allowNegative: false, useGold: true))
		{
			decorationByRefId.setIsPlayerOwned(aPlayerOwned: true);
			player.addOwnedDecoration(decorationByRefId);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseShopUpgrades, string.Empty, decorationShopCost * -1);
			GameObject aParent = GameObject.Find(gameObjectName).transform.parent.gameObject;
			commonScreenObject.destroyPrefabImmediate(GameObject.Find(gameObjectName));
			GameObject aObject = commonScreenObject.createPrefab(aParent, equipPrefix + decoPrefix + decorationByRefId.getDecorationRefId(), "Prefab/ShopUpgrade/button_equip", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "equipLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu02").ToUpper(CultureInfo.InvariantCulture);
		}
	}

	private void equipDeco(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameObject gameObject = null;
		Decoration decorationByRefId = gameData.getDecorationByRefId(array[1]);
		Decoration decorationCurrentDisplayByType = gameData.getDecorationCurrentDisplayByType(decorationByRefId.getDecorationType());
		if (decorationCurrentDisplayByType.getDecorationRefId() != string.Empty)
		{
			decorationCurrentDisplayByType.setIsCurrentDisplay(aDisplay: false);
			decoController.destroyDecoration(decorationCurrentDisplayByType);
			GameObject gameObject2 = GameObject.Find(equippedPrefix + decoPrefix + decorationCurrentDisplayByType.getDecorationRefId());
			gameObject = gameObject2.transform.parent.gameObject;
			commonScreenObject.destroyPrefabImmediate(gameObject2);
			GameObject aObject = commonScreenObject.createPrefab(gameObject, equipPrefix + decoPrefix + decorationCurrentDisplayByType.getDecorationRefId(), "Prefab/ShopUpgrade/button_equip", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "equipLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu02").ToUpper(CultureInfo.InvariantCulture);
			decorationByRefId.setIsCurrentDisplay(aDisplay: true);
			decoController.generateDecoration(decorationByRefId);
			GameObject gameObject3 = GameObject.Find(gameObjectName);
			gameObject = gameObject3.transform.parent.gameObject;
			GameObject aObject2 = commonScreenObject.createPrefab(gameObject, equippedPrefix + decoPrefix + decorationByRefId.getDecorationRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject2, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
			commonScreenObject.destroyPrefabImmediate(gameObject3);
		}
		else
		{
			decorationByRefId.setIsCurrentDisplay(aDisplay: true);
			decoController.generateDecoration(decorationByRefId);
			GameObject gameObject4 = GameObject.Find(gameObjectName);
			gameObject = gameObject4.transform.parent.gameObject;
			GameObject aObject3 = commonScreenObject.createPrefab(gameObject, equippedPrefix + decoPrefix + decorationByRefId.getDecorationRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject3, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
			commonScreenObject.destroyPrefabImmediate(gameObject4);
		}
		player.replaceDisplayDecoration(decorationByRefId);
	}

	private void buyWorkshop(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Furniture furnitureByRefId = gameData.getFurnitureByRefId(array[1]);
		if (shopMenuController.tryActionWithGold(furnitureByRefId.getFurnitureCost(), allowNegative: false, useGold: true))
		{
			Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType(furnitureByRefId.getFurnitureType());
			GameObject aObject = GameObject.Find(workshopPrefix + highestPlayerFurnitureByType.getFurnitureRefId());
			commonScreenObject.destroyPrefabImmediate(commonScreenObject.findChild(aObject, "buttonPos/" + equippedPrefix + workshopPrefix + highestPlayerFurnitureByType.getFurnitureRefId()).gameObject);
			GameObject aObject2 = commonScreenObject.createPrefab(commonScreenObject.findChild(aObject, "buttonPos").gameObject, ownedPrefix + workshopPrefix + furnitureByRefId.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject2, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu10").ToUpper(CultureInfo.InvariantCulture);
			player.unlockFurniture(furnitureByRefId);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseShopUpgrades, string.Empty, furnitureByRefId.getFurnitureCost() * -1);
			GameObject aObject3 = GameObject.Find(workshopPrefix + furnitureByRefId.getFurnitureRefId());
			commonScreenObject.destroyPrefabImmediate(GameObject.Find(gameObjectName));
			GameObject aObject4 = commonScreenObject.createPrefab(commonScreenObject.findChild(aObject3, "buttonPos").gameObject, equippedPrefix + workshopPrefix + furnitureByRefId.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject3, "greyOverlay").GetComponent<UISprite>().enabled = true;
			commonScreenObject.findChild(aObject4, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
			gridController.createWorld(refresh: true);
		}
	}

	private void buyEnhance(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Furniture furnitureByRefId = gameData.getFurnitureByRefId(array[1]);
		if (!shopMenuController.tryActionWithGold(furnitureByRefId.getFurnitureCost(), allowNegative: false, useGold: true))
		{
			return;
		}
		Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType(furnitureByRefId.getFurnitureType());
		player.unlockFurniture(furnitureByRefId);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseShopUpgrades, string.Empty, furnitureByRefId.getFurnitureCost() * -1);
		if (furnitureByRefId.getFurnitureLevel() > highestPlayerFurnitureByType.getFurnitureLevel())
		{
			if (highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty)
			{
				GameObject aObject = GameObject.Find(enhancePrefix + highestPlayerFurnitureByType.getFurnitureRefId());
				commonScreenObject.destroyPrefabImmediate(commonScreenObject.findChild(aObject, "buttonPos/" + equippedPrefix + enhancePrefix + highestPlayerFurnitureByType.getFurnitureRefId()).gameObject);
				GameObject aObject2 = commonScreenObject.createPrefab(commonScreenObject.findChild(aObject, "buttonPos").gameObject, ownedPrefix + enhancePrefix + furnitureByRefId.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(aObject2, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu10").ToUpper(CultureInfo.InvariantCulture);
			}
			GameObject aObject3 = GameObject.Find(enhancePrefix + furnitureByRefId.getFurnitureRefId());
			commonScreenObject.destroyPrefabImmediate(GameObject.Find(gameObjectName));
			GameObject aObject4 = commonScreenObject.createPrefab(commonScreenObject.findChild(aObject3, "buttonPos").gameObject, equippedPrefix + enhancePrefix + furnitureByRefId.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject3, "greyOverlay").GetComponent<UISprite>().enabled = true;
			commonScreenObject.findChild(aObject4, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu03").ToUpper(CultureInfo.InvariantCulture);
		}
		else
		{
			GameObject aObject5 = GameObject.Find(enhancePrefix + furnitureByRefId.getFurnitureRefId());
			commonScreenObject.destroyPrefabImmediate(GameObject.Find(gameObjectName));
			GameObject aObject6 = commonScreenObject.createPrefab(commonScreenObject.findChild(aObject5, "buttonPos").gameObject, ownedPrefix + enhancePrefix + furnitureByRefId.getFurnitureRefId(), "Prefab/ShopUpgrade/button_equipped", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject6, "equippedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("upgradeMenu10").ToUpper(CultureInfo.InvariantCulture);
		}
		if (furnitureByRefId.getFurnitureType() == "301")
		{
			player.feedDog();
			GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
			if (gameObject != null)
			{
				gameObject.GetComponent<GUITopMenuNewController>().updateDogBowl();
			}
		}
		gridController.createWorld(refresh: true);
	}

	private void createEnhancePreview(string gameObjectName)
	{
		GameData gameData = game.getGameData();
		string[] array = gameObjectName.Split('_');
		Furniture furnitureByRefId = gameData.getFurnitureByRefId(array[1]);
		if (furnitureByRefId.getShopLevelRequirement() > shopLevel)
		{
			return;
		}
		List<Obstacle> obstacleByFurnitureRefIDAndLevel = gameData.getObstacleByFurnitureRefIDAndLevel(furnitureByRefId.getFurnitureRefId(), furnitureByRefId.getFurnitureLevel(), shopLevel);
		foreach (Obstacle item in obstacleByFurnitureRefIDAndLevel)
		{
			Vector3 position = gridController.getPosition((int)item.getObstaclePoint().x, (int)item.getObstaclePoint().y);
			float num = position.x;
			float num2 = position.z;
			if (item.getWidth() > 1)
			{
				num += (float)(item.getWidth() - 1) / 2f * 0.73f;
			}
			if (item.getHeight() > 1)
			{
				num2 += (float)(item.getHeight() - 1) / 2f * 0.74f;
			}
			Vector3 aPosition = new Vector3(num, item.getYValue(), num2);
			GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Panel_Obstacles"), "obstaclePreview", "Prefab/Obstacle/Obstacle", aPosition, Vector3.one, new Vector3(item.getXDegree(), item.getYDegree(), 0f));
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + item.getImageUnlocked());
			gameObject.GetComponent<SpriteRenderer>().color = previewColor;
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = item.getSortOrder();
			previewGameObject.Add(gameObject);
		}
	}

	private void upgradeShop()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		ShopLevel shopLevel = player.getShopLevel();
		ShopLevel shopLevel2 = gameData.getShopLevel(shopLevel.getNextShopRefId());
		player.setShopLevel(shopLevel2);
		player.setAreaRegion(player.getAreaRegion() + 1);
		this.shopLevel = player.getShopLevelInt();
		shoplevelLabel.text = "Shop Level : " + this.shopLevel;
		spawnItem();
		gridController.createWorld(refresh: true);
	}

	public bool checkIsOverMenu()
	{
		return isOverMenu;
	}
}
