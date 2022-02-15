using System;

[Serializable]
public class Item
{
	private string itemId;

	private string itemRefId;

	private string itemName;

	private string itemDesc;

	private int itemCost;

	private int itemBuyExp;

	private int itemSellPrice;

	private ItemType itemType;

	private bool isSpecial;

	private WeaponStat effectStat;

	private Element element;

	private string scenarioLock;

	private int effectValue;

	private string effectString;

	private string image;

	private int itemNum;

	private int itemUsed;

	private int itemFromExplore;

	private int itemFromBuy;

	private int reqShopLevel;

	private int reqMonths;

	private int reqDogLove;

	private int chance;

	public Item()
	{
		itemId = string.Empty;
		itemRefId = string.Empty;
		itemName = string.Empty;
		itemDesc = string.Empty;
		itemCost = 0;
		itemBuyExp = 0;
		itemSellPrice = 0;
		itemType = ItemType.ItemTypeBlank;
		isSpecial = false;
		effectStat = WeaponStat.WeaponStatNone;
		element = Element.ElementNone;
		scenarioLock = string.Empty;
		effectValue = 0;
		effectString = string.Empty;
		image = string.Empty;
		itemNum = 0;
		itemUsed = 0;
		itemFromExplore = 0;
		itemFromBuy = 0;
		reqShopLevel = 0;
		reqMonths = 0;
		reqDogLove = 0;
		chance = 0;
	}

	public Item(string aId, string aRefId, string aName, string aDesc, int aCost, int aBuyExp, int aSellPrice, ItemType aType, bool aSpecial, WeaponStat aStat, Element aElement, string aScenarioLock, int aValue, string aString, string aImage, int aNum, int aReqShopLevel, int aReqMonths, int aReqDogLove, int aChance)
	{
		itemId = aId;
		itemRefId = aRefId;
		itemName = aName;
		itemDesc = aDesc;
		itemCost = aCost;
		itemBuyExp = aBuyExp;
		itemSellPrice = aSellPrice;
		itemType = aType;
		isSpecial = aSpecial;
		effectStat = aStat;
		element = aElement;
		scenarioLock = aScenarioLock;
		effectValue = aValue;
		effectString = aString;
		image = aImage;
		itemNum = aNum;
		itemUsed = 0;
		itemFromExplore = 0;
		itemFromBuy = 0;
		reqShopLevel = aReqShopLevel;
		reqMonths = aReqMonths;
		reqDogLove = aReqDogLove;
		chance = aChance;
	}

	public bool tryUseItem(int useNum, bool isUse)
	{
		if (itemNum >= useNum)
		{
			itemNum -= useNum;
			if (isUse)
			{
				itemUsed += useNum;
			}
			return true;
		}
		return false;
	}

	public void addItem(int addNum)
	{
		itemNum += addNum;
	}

	public string getItemId()
	{
		return itemId;
	}

	public string getItemRefId()
	{
		return itemRefId;
	}

	public string getItemName()
	{
		return itemName;
	}

	public string getItemDesc()
	{
		return itemDesc;
	}

	public int getItemCost()
	{
		return itemCost;
	}

	public int getItemBuyExp()
	{
		return itemBuyExp;
	}

	public int getItemSellPrice()
	{
		return itemSellPrice;
	}

	public ItemType getItemType()
	{
		return itemType;
	}

	public bool checkSpecial()
	{
		return isSpecial;
	}

	public int getItemNum()
	{
		return itemNum;
	}

	public int getItemUsed()
	{
		return itemUsed;
	}

	public int getTotalNum()
	{
		return itemNum + itemUsed;
	}

	public int getItemFromBuy()
	{
		return itemFromBuy;
	}

	public void setItemFromBuy(int aCount)
	{
		itemFromBuy = aCount;
	}

	public void addItemFromBuy(int aCount)
	{
		itemFromBuy += aCount;
	}

	public int getItemFromExplore()
	{
		return itemFromExplore;
	}

	public void setItemFromExplore(int aCount)
	{
		itemFromExplore = aCount;
	}

	public void addItemFromExplore(int aCount)
	{
		itemFromExplore += aCount;
	}

	public WeaponStat getItemEffectStat()
	{
		return effectStat;
	}

	public Element getItemElement()
	{
		return element;
	}

	public string getScenarioLock()
	{
		return scenarioLock;
	}

	public int getItemEffectValue()
	{
		return effectValue;
	}

	public string getItemEffectString()
	{
		return effectString;
	}

	public void setItemNum(int aNum)
	{
		itemNum = aNum;
	}

	public void setItemUsed(int aUsed)
	{
		itemUsed = aUsed;
	}

	public bool checkDogItemRequirements(int playerShopLevel, int playerMonths, int playerDogLove)
	{
		if (chance > 0 && playerShopLevel == reqShopLevel && playerMonths >= reqMonths && playerDogLove >= reqDogLove)
		{
			return true;
		}
		return false;
	}

	public int getDogItemChance()
	{
		return chance;
	}

	public string getImage()
	{
		return image;
	}

	public string getItemStandardTooltipString(Area aArea = null)
	{
		string empty = string.Empty;
		empty = empty + itemName + "\n";
		empty = empty + CommonAPI.getItemTypeName(itemType) + "\n";
		string text = "[";
		text = ((itemNum <= 0) ? (text + "FF4842") : (text + "56AE59"));
		string text2 = text;
		text = text2 + "]" + getItemNum() + "[-]";
		empty = empty + CommonAPI.getGameData().getTextByRefIdWithDynText("itemTooltip01", "[num]", text) + "\n";
		if (aArea != null && aArea.getExploreItemList().ContainsKey(itemRefId))
		{
			ExploreItem aItem = aArea.getExploreItemList()[itemRefId];
			string itemRarityText = CommonAPI.getItemRarityText(aItem, itemType);
			empty = empty + CommonAPI.getGameData().getTextByRefIdWithDynText("itemTooltip02", "[rarity]", itemRarityText) + "\n";
		}
		empty += "[s]                         [/s]\n";
		if (itemType == ItemType.ItemTypeEnhancement)
		{
			text2 = empty;
			empty = text2 + "[" + CommonAPI.getStatColorHex(effectStat) + "]" + effectString + " (+" + effectValue + ")[-]\n";
			empty += "[s]                         [/s]\n";
		}
		return empty + "[C0C0C0][i]" + itemDesc + "[/i][-]";
	}

	public bool checkScenarioAllow(string aScenario)
	{
		if (scenarioLock.Length > 0)
		{
			string[] array = scenarioLock.Split('@');
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text == aScenario)
				{
					return false;
				}
			}
		}
		return true;
	}
}
