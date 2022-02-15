using System;

[Serializable]
public class Furniture
{
	private string furnitureRefId;

	private string furnitureName;

	private string furnitureDesc;

	private bool showInShop;

	private int furnitureCost;

	private string furnitureType;

	private int furnitureLevel;

	private int furnitureCapacity;

	private bool isPlayerOwned;

	private int shopLevelReq;

	private int dogLove;

	private string image;

	public Furniture()
	{
		furnitureRefId = string.Empty;
		furnitureName = string.Empty;
		furnitureDesc = string.Empty;
		showInShop = false;
		furnitureCost = 0;
		furnitureType = string.Empty;
		furnitureLevel = 0;
		furnitureCapacity = 0;
		isPlayerOwned = false;
		shopLevelReq = 0;
		dogLove = 0;
		image = string.Empty;
	}

	public Furniture(string aRefId, string aName, string aDesc, bool aShowInShop, int aCost, string aType, int aFurnitureLevel, int aFurnitureCapacity, int aLevelReq, int aDogLove, string aImage)
	{
		furnitureRefId = aRefId;
		furnitureName = aName;
		furnitureDesc = aDesc;
		showInShop = aShowInShop;
		furnitureCost = aCost;
		furnitureType = aType;
		furnitureLevel = aFurnitureLevel;
		furnitureCapacity = aFurnitureCapacity;
		isPlayerOwned = false;
		shopLevelReq = aLevelReq;
		dogLove = aDogLove;
		image = aImage;
	}

	public void resetDynData()
	{
		isPlayerOwned = false;
	}

	public string getFurnitureRefId()
	{
		return furnitureRefId;
	}

	public string getFurnitureName()
	{
		return furnitureName;
	}

	public string getFurnitureDesc()
	{
		return furnitureDesc;
	}

	public bool checkShowInShop()
	{
		return showInShop;
	}

	public int getFurnitureCost()
	{
		return furnitureCost;
	}

	public string getFurnitureType()
	{
		return furnitureType;
	}

	public int getFurnitureLevel()
	{
		return furnitureLevel;
	}

	public int getFurnitureCapacity()
	{
		return furnitureCapacity;
	}

	public bool checkShopLevelReq(int currentLevel)
	{
		if (currentLevel < shopLevelReq)
		{
			return false;
		}
		return true;
	}

	public int getShopLevelRequirement()
	{
		return shopLevelReq;
	}

	public void setPlayerOwned(bool aOwned)
	{
		isPlayerOwned = aOwned;
	}

	public bool checkPlayerOwned()
	{
		return isPlayerOwned;
	}

	public int getDogLove()
	{
		return dogLove;
	}

	public string getImage()
	{
		return image;
	}
}
