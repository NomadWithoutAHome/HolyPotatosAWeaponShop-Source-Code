using System.Collections.Generic;
using UnityEngine;

public class GUIShopPopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private UILabel titleLabel;

	private UILabel descriptionLabel;

	private UILabel itemNameLabel;

	private GameObject processButton;

	private GameObject shop_Grid;

	private PopupType currPopupType;

	private List<Item> itemList;

	private List<Furniture> furnitureShopList;

	private int currSelectedIndex;

	private string horizontalGridPrefix;

	private string itemPrefix;

	private bool firstFurniture;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		titleLabel = GameObject.Find("TitleLabel").GetComponent<UILabel>();
		titleLabel.text = game.getGameData().getTextByRefId("menuMain04");
		descriptionLabel = GameObject.Find("DescriptionLabel").GetComponent<UILabel>();
		itemNameLabel = GameObject.Find("ItemNameLabel").GetComponent<UILabel>();
		processButton = GameObject.Find("ProcessButton");
		shop_Grid = GameObject.Find("Shop_Grid");
		currSelectedIndex = -1;
		horizontalGridPrefix = "HoriGrid_";
		itemPrefix = "Item_";
		firstFurniture = true;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "ProcessButton")
		{
			switch (currPopupType)
			{
			case PopupType.PopupTypeBuyEnchantment:
				shopMenuController.insertStoredInput("enchantment", currSelectedIndex + 1);
				shopMenuController.buyEnchantment();
				break;
			case PopupType.PopupTypeBuyFurniture:
				shopMenuController.insertStoredInput("furniture", currSelectedIndex + 1);
				shopMenuController.buyFurniture();
				break;
			case PopupType.PopupTypeSellItem:
				shopMenuController.insertStoredInput("sell", currSelectedIndex + 1);
				shopMenuController.sellItem();
				break;
			}
		}
		else if (gameObjectName == "CloseButton")
		{
			viewController.closeShopPopup(hide: false);
			switch (currPopupType)
			{
			case PopupType.PopupTypeBuyFurniture:
			case PopupType.PopupTypeBuyEnchantment:
				viewController.showMainMenu(MenuState.MenuStateInventory);
				viewController.showTier2Menu(MenuState.MenuStateShopBuyMenu);
				break;
			case PopupType.PopupTypeSellItem:
				viewController.showMainMenu(MenuState.MenuStateInventory);
				break;
			}
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			int selectedIndex = CommonAPI.parseInt(array[1]);
			switch (currPopupType)
			{
			case PopupType.PopupTypeBuyEnchantment:
			case PopupType.PopupTypeSellItem:
				selectEnchantment(selectedIndex);
				break;
			case PopupType.PopupTypeBuyFurniture:
				selectFurniture(selectedIndex);
				break;
			}
		}
	}

	public void setReference(PopupType aPopupType, Item prevItem = null)
	{
		GameData gameData = game.getGameData();
		currPopupType = aPopupType;
		processButton.GetComponent<UIButton>().isEnabled = false;
		int num = 0;
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (currPopupType)
		{
		case PopupType.PopupTypeBuyEnchantment:
		{
			processButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuShop17");
			itemList = gameData.getShopItemListByType(ItemType.ItemTypeEnhancement);
			gameObject = commonScreenObject.createPrefab(shop_Grid, horizontalGridPrefix + num, "Prefab/Shop/ShopGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
			for (int k = 0; k < itemList.Count; k++)
			{
				gameObject2 = commonScreenObject.createPrefab(gameObject, itemPrefix + k, "Prefab/Shop/ShopObject", Vector3.zero, Vector3.one, Vector3.zero);
				GameObject gameObject7 = commonScreenObject.findChild(gameObject2, "CostFrame").gameObject;
				GameObject gameObject8 = commonScreenObject.findChild(gameObject2, "OwnedFrame").gameObject;
				gameObject8.GetComponent<UISprite>().enabled = false;
				gameObject8.GetComponentInChildren<UILabel>().text = string.Empty;
				gameObject7.GetComponentInChildren<UILabel>().text = CommonAPI.formatNumber(itemList[k].getItemCost());
				commonScreenObject.findChild(gameObject2, "ObjectFrame/ObjectIcon").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Enchantment/amulet of strength");
				if (itemList[k].getItemNum() > 0)
				{
					commonScreenObject.findChild(gameObject2, "AmountFrame").GetComponent<UISprite>().enabled = true;
					commonScreenObject.findChild(gameObject2, "AmountFrame").GetComponentInChildren<UILabel>().text = CommonAPI.formatNumber(itemList[k].getItemNum());
				}
				if (k == 0)
				{
					selectEnchantment(k);
				}
				if ((k + 1) % 4 == 0 && k + 1 != itemList.Count)
				{
					gameObject.GetComponent<UIGrid>().Reposition();
					num++;
					gameObject = commonScreenObject.createPrefab(shop_Grid, horizontalGridPrefix + num, "Prefab/Shop/ShopGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
				}
			}
			gameObject.GetComponent<UIGrid>().Reposition();
			break;
		}
		case PopupType.PopupTypeBuyFurniture:
		{
			processButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuShop17");
			furnitureShopList = gameData.getFurnitureListFiltered(onlyNotOwned: true, onlyLowestLevel: true, -1, game.getPlayer().checkHasDog(), isShopList: true);
			gameObject = commonScreenObject.createPrefab(shop_Grid, horizontalGridPrefix + num, "Prefab/Shop/ShopGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
			for (int j = 0; j < furnitureShopList.Count; j++)
			{
				gameObject2 = commonScreenObject.createPrefab(gameObject, itemPrefix + j, "Prefab/Shop/ShopObject", Vector3.zero, Vector3.one, Vector3.zero);
				GameObject gameObject5 = commonScreenObject.findChild(gameObject2, "CostFrame").gameObject;
				GameObject gameObject6 = commonScreenObject.findChild(gameObject2, "OwnedFrame").gameObject;
				commonScreenObject.findChild(gameObject2, "ObjectFrame/ObjectIcon").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Shopping/" + furnitureShopList[j].getImage());
				if (furnitureShopList[j].checkPlayerOwned())
				{
					gameObject5.GetComponent<UISprite>().enabled = false;
					gameObject5.GetComponentInChildren<UILabel>().text = string.Empty;
					gameObject6.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuShop16");
				}
				else
				{
					gameObject6.GetComponent<UISprite>().enabled = false;
					gameObject6.GetComponentInChildren<UILabel>().text = string.Empty;
					gameObject5.GetComponentInChildren<UILabel>().text = CommonAPI.formatNumber(furnitureShopList[j].getFurnitureCost());
					if (firstFurniture)
					{
						firstFurniture = false;
						selectFurniture(j);
					}
				}
				if ((j + 1) % 4 == 0)
				{
					gameObject.GetComponent<UIGrid>().Reposition();
					num++;
					gameObject = commonScreenObject.createPrefab(shop_Grid, horizontalGridPrefix + num, "Prefab/Shop/ShopGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
				}
			}
			gameObject.GetComponent<UIGrid>().Reposition();
			break;
		}
		case PopupType.PopupTypeSellItem:
		{
			processButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuShop18");
			itemList = gameData.getSellItemList();
			gameObject = commonScreenObject.createPrefab(shop_Grid, horizontalGridPrefix + num, "Prefab/Shop/ShopGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
			for (int i = 0; i < itemList.Count; i++)
			{
				gameObject2 = commonScreenObject.createPrefab(gameObject, itemPrefix + i, "Prefab/Shop/ShopObject", Vector3.zero, Vector3.one, Vector3.zero);
				GameObject gameObject3 = commonScreenObject.findChild(gameObject2, "CostFrame").gameObject;
				GameObject gameObject4 = commonScreenObject.findChild(gameObject2, "OwnedFrame").gameObject;
				gameObject4.GetComponent<UISprite>().enabled = false;
				gameObject4.GetComponentInChildren<UILabel>().text = string.Empty;
				gameObject3.GetComponentInChildren<UILabel>().text = CommonAPI.formatNumber(itemList[i].getItemSellPrice());
				commonScreenObject.findChild(gameObject2, "AmountFrame").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(gameObject2, "AmountFrame").GetComponentInChildren<UILabel>().text = CommonAPI.formatNumber(itemList[i].getItemNum());
				if (i == 0)
				{
					selectEnchantment(i);
				}
				if (prevItem != null && prevItem.getItemRefId() == itemList[i].getItemRefId())
				{
					selectEnchantment(i);
				}
				if ((i + 1) % 4 == 0 && i + 1 != itemList.Count)
				{
					gameObject.GetComponent<UIGrid>().Reposition();
					num++;
					gameObject = commonScreenObject.createPrefab(shop_Grid, horizontalGridPrefix + num, "Prefab/Shop/ShopGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
				}
			}
			gameObject.GetComponent<UIGrid>().Reposition();
			break;
		}
		}
		shop_Grid.GetComponent<UIGrid>().Reposition();
		shop_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}

	private void selectEnchantment(int selectedIndex)
	{
		if (currSelectedIndex != selectedIndex)
		{
			if (currSelectedIndex != -1)
			{
				commonScreenObject.findChild(GameObject.Find(itemPrefix + currSelectedIndex), "ShopSelectFrame").GetComponent<UISprite>().enabled = false;
			}
			currSelectedIndex = selectedIndex;
			commonScreenObject.findChild(GameObject.Find(itemPrefix + currSelectedIndex), "ShopSelectFrame").GetComponent<UISprite>().enabled = true;
			itemNameLabel.text = itemList[selectedIndex].getItemName() + " (" + shopMenuController.generateItemEffect(itemList[selectedIndex]) + ")";
			descriptionLabel.text = itemList[selectedIndex].getItemDesc();
			processButton.GetComponent<UIButton>().isEnabled = true;
		}
	}

	private void selectFurniture(int selectedIndex)
	{
		if (!furnitureShopList[selectedIndex].checkPlayerOwned() && currSelectedIndex != selectedIndex)
		{
			if (currSelectedIndex != -1)
			{
				commonScreenObject.findChild(GameObject.Find(itemPrefix + currSelectedIndex), "ShopSelectFrame").GetComponent<UISprite>().enabled = false;
			}
			currSelectedIndex = selectedIndex;
			commonScreenObject.findChild(GameObject.Find(itemPrefix + currSelectedIndex), "ShopSelectFrame").GetComponent<UISprite>().enabled = true;
			itemNameLabel.text = furnitureShopList[selectedIndex].getFurnitureName();
			descriptionLabel.text = furnitureShopList[selectedIndex].getFurnitureDesc();
			processButton.GetComponent<UIButton>().isEnabled = true;
		}
	}

	public void buyItem()
	{
		GameObject aObject = GameObject.Find(itemPrefix + currSelectedIndex);
		PopupType popupType = currPopupType;
		if (popupType == PopupType.PopupTypeBuyEnchantment)
		{
			commonScreenObject.findChild(aObject, "ShopSelectFrame").GetComponent<UISprite>().enabled = false;
			commonScreenObject.findChild(aObject, "AmountFrame").GetComponent<UISprite>().enabled = true;
			commonScreenObject.findChild(aObject, "AmountFrame").GetComponentInChildren<UILabel>().text = CommonAPI.formatNumber(itemList[currSelectedIndex].getItemNum());
			processButton.GetComponent<UIButton>().isEnabled = false;
		}
		currSelectedIndex = -1;
	}
}
