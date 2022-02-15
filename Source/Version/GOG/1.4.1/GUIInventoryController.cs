using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIInventoryController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private ItemType currentFilter;

	private UIButton allFilter;

	private UIButton enchantmentFilter;

	private UIButton materialFilter;

	private UIButton relicFilter;

	private UIGrid inventoryListGrid;

	private UIScrollBar inventoryScrollbar;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		currentFilter = ItemType.ItemTypeBlank;
		allFilter = commonScreenObject.findChild(base.gameObject, "InventoryFilter_bg/InventoryFilter_list/InventoryFilterAll_button").GetComponent<UIButton>();
		enchantmentFilter = commonScreenObject.findChild(base.gameObject, "InventoryFilter_bg/InventoryFilter_list/InventoryFilterEnchant_button").GetComponent<UIButton>();
		materialFilter = commonScreenObject.findChild(base.gameObject, "InventoryFilter_bg/InventoryFilter_list/InventoryFilterMaterial_button").GetComponent<UIButton>();
		relicFilter = commonScreenObject.findChild(base.gameObject, "InventoryFilter_bg/InventoryFilter_list/InventoryFilterRelic_button").GetComponent<UIButton>();
		inventoryListGrid = commonScreenObject.findChild(base.gameObject, "InventoryList_bg/InventoryList_clipPanel/InventoryList_grid").GetComponent<UIGrid>();
		inventoryScrollbar = commonScreenObject.findChild(base.gameObject, "InventoryList_bg/InventoryScroll_scroll").GetComponent<UIScrollBar>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			tooltipScript.setInactive();
			viewController.closeInventoryPopup(doResume: true);
			break;
		case "InventoryFilterAll_button":
			if (currentFilter != ItemType.ItemTypeBlank)
			{
				currentFilter = ItemType.ItemTypeBlank;
				updateFilterButtons();
				clearInventoryListGrid();
				filterInventory();
			}
			break;
		case "InventoryFilterEnchant_button":
			if (currentFilter != 0)
			{
				currentFilter = ItemType.ItemTypeEnhancement;
				updateFilterButtons();
				clearInventoryListGrid();
				filterInventory();
			}
			break;
		case "InventoryFilterMaterial_button":
			if (currentFilter != ItemType.ItemTypeMaterial)
			{
				currentFilter = ItemType.ItemTypeMaterial;
				updateFilterButtons();
				clearInventoryListGrid();
				filterInventory();
			}
			break;
		case "InventoryFilterRelic_button":
			if (currentFilter != ItemType.ItemTypeRelic)
			{
				currentFilter = ItemType.ItemTypeRelic;
				updateFilterButtons();
				clearInventoryListGrid();
				filterInventory();
			}
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null)
			{
			}
			string[] array = hoverName.Split('_');
			if (array[0] == "item")
			{
				GameData gameData = game.getGameData();
				Item itemByRefId = gameData.getItemByRefId(array[2]);
				string itemStandardTooltipString = itemByRefId.getItemStandardTooltipString();
				tooltipScript.showText(itemStandardTooltipString);
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void Update()
	{
		handleInput();
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	public void setReference()
	{
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		GameData gameData = game.getGameData();
		UILabel[] array = componentsInChildren;
		foreach (UILabel uILabel in array)
		{
			switch (uILabel.gameObject.name)
			{
			case "Inventory":
				uILabel.text = gameData.getTextByRefId("forgeBottomMenu04").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "InventoryFilterAll_label":
				uILabel.text = gameData.getTextByRefId("inventoryFilter01").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "InventoryFilterEnchant_label":
				uILabel.text = gameData.getTextByRefId("inventoryFilter02");
				break;
			case "InventoryFilterMaterial_label":
				uILabel.text = gameData.getTextByRefId("inventoryFilter03");
				break;
			case "InventoryFilterRelic_label":
				uILabel.text = gameData.getTextByRefId("inventoryFilter04");
				break;
			}
		}
		currentFilter = ItemType.ItemTypeBlank;
		updateFilterButtons();
		filterInventory();
	}

	public void updateFilterButtons()
	{
		switch (currentFilter)
		{
		case ItemType.ItemTypeBlank:
			allFilter.isEnabled = false;
			allFilter.transform.localPosition = new Vector3(-210f, -10f, 0f);
			enchantmentFilter.isEnabled = true;
			enchantmentFilter.transform.localPosition = new Vector3(-70f, 0f, 0f);
			materialFilter.isEnabled = true;
			materialFilter.transform.localPosition = new Vector3(70f, 0f, 0f);
			relicFilter.isEnabled = true;
			relicFilter.transform.localPosition = new Vector3(210f, 0f, 0f);
			break;
		case ItemType.ItemTypeEnhancement:
			allFilter.isEnabled = true;
			allFilter.transform.localPosition = new Vector3(-210f, 0f, 0f);
			enchantmentFilter.isEnabled = false;
			enchantmentFilter.transform.localPosition = new Vector3(-70f, -10f, 0f);
			materialFilter.isEnabled = true;
			materialFilter.transform.localPosition = new Vector3(70f, 0f, 0f);
			relicFilter.isEnabled = true;
			relicFilter.transform.localPosition = new Vector3(210f, 0f, 0f);
			break;
		case ItemType.ItemTypeMaterial:
			allFilter.isEnabled = true;
			allFilter.transform.localPosition = new Vector3(-210f, 0f, 0f);
			enchantmentFilter.isEnabled = true;
			enchantmentFilter.transform.localPosition = new Vector3(-70f, 0f, 0f);
			materialFilter.isEnabled = false;
			materialFilter.transform.localPosition = new Vector3(70f, -10f, 0f);
			relicFilter.isEnabled = true;
			relicFilter.transform.localPosition = new Vector3(210f, 0f, 0f);
			break;
		case ItemType.ItemTypeRelic:
			allFilter.isEnabled = true;
			allFilter.transform.localPosition = new Vector3(-210f, 0f, 0f);
			enchantmentFilter.isEnabled = true;
			enchantmentFilter.transform.localPosition = new Vector3(-70f, 0f, 0f);
			materialFilter.isEnabled = true;
			materialFilter.transform.localPosition = new Vector3(70f, 0f, 0f);
			relicFilter.isEnabled = false;
			relicFilter.transform.localPosition = new Vector3(210f, -10f, 0f);
			break;
		case ItemType.ItemTypeBoost:
		case ItemType.ItemTypeMemento:
		case ItemType.ItemTypeSpecial:
			break;
		}
	}

	public void filterInventory()
	{
		GameData gameData = game.getGameData();
		List<Item> list = new List<Item>();
		list = ((currentFilter != ItemType.ItemTypeBlank) ? gameData.getSortedItemList(ownedOnly: true, includeSpecial: true, currentFilter, string.Empty) : gameData.getSortedItemList(ownedOnly: true, includeSpecial: true, ItemType.ItemTypeBlank, string.Empty));
		int num = 10000;
		foreach (Item item in list)
		{
			GameObject gameObject = null;
			if (item.getItemType() == ItemType.ItemTypeEnhancement)
			{
				gameObject = commonScreenObject.createPrefab(inventoryListGrid.gameObject, "item_" + num + "_" + item.getItemRefId(), "Prefab/Inventory/InventoryListEnchantObj", Vector3.zero, Vector3.one, Vector3.zero);
				string spriteName = string.Empty;
				switch (item.getItemEffectStat())
				{
				case WeaponStat.WeaponStatAttack:
					spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					spriteName = "ico_enh";
					break;
				}
				commonScreenObject.findChild(gameObject, "ItemStat_bg/ItemStat_icon").GetComponent<UISprite>().spriteName = spriteName;
				commonScreenObject.findChild(gameObject, "ItemStat_bg/ItemStat_label").GetComponent<UILabel>().text = "+" + item.getItemEffectValue();
			}
			else
			{
				gameObject = commonScreenObject.createPrefab(inventoryListGrid.gameObject, "item_" + num + "_" + item.getItemRefId(), "Prefab/Inventory/InventoryListObj", Vector3.zero, Vector3.one, Vector3.zero);
			}
			commonScreenObject.findChild(gameObject, "ItemCount_bg/ItemCount_label").GetComponent<UILabel>().text = item.getItemNum().ToString();
			commonScreenObject.findChild(gameObject, "ItemName_bg/ItemName_label").GetComponent<UILabel>().text = item.getItemName();
			switch (item.getItemType())
			{
			case ItemType.ItemTypeMaterial:
				commonScreenObject.findChild(gameObject, "ItemImage_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/materials/" + item.getImage());
				break;
			case ItemType.ItemTypeRelic:
				commonScreenObject.findChild(gameObject, "ItemImage_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/relics/" + item.getImage());
				break;
			case ItemType.ItemTypeEnhancement:
				commonScreenObject.findChild(gameObject, "ItemImage_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Enchantment/" + item.getImage());
				break;
			}
			num++;
		}
		inventoryListGrid.Reposition();
		inventoryScrollbar.value = 0f;
		inventoryListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		inventoryListGrid.enabled = true;
	}

	private void clearInventoryListGrid()
	{
		while (inventoryListGrid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(inventoryListGrid.GetChild(0).gameObject);
		}
	}
}
