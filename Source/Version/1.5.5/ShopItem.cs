using System;

[Serializable]
public class ShopItem
{
	private string areaShopItemRefID;

	private string areaRefID;

	private string itemRefID;

	private int cost;

	private int maxQty;

	public ShopItem()
	{
		areaShopItemRefID = string.Empty;
		areaRefID = string.Empty;
		itemRefID = string.Empty;
		cost = 0;
		maxQty = 0;
	}

	public ShopItem(string aRefId, string aAreaRefID, string aItemRefID, int aCost, int aMaxQty)
	{
		areaShopItemRefID = aRefId;
		areaRefID = aAreaRefID;
		itemRefID = aItemRefID;
		cost = aCost;
		maxQty = aMaxQty;
	}

	public string getAreaShopItemRefID()
	{
		return areaShopItemRefID;
	}

	public string getAreaRefID()
	{
		return areaRefID;
	}

	public string getItemRefID()
	{
		return itemRefID;
	}

	public int getCost()
	{
		return cost;
	}

	public int getMaxQty()
	{
		return maxQty;
	}
}
