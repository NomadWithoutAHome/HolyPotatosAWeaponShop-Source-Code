using System;
using System.Collections.Generic;

[Serializable]
public class Decoration
{
	private string decorationRefId;

	private string decorationName;

	private string decorationDesc;

	private string decorationImage;

	private int shopLevelReq;

	private string decorationTypeRefId;

	private string decorationTypeName;

	private int decorationShopCost;

	private UnlockCondition decorationUnlockCondition;

	private int decoReqCount;

	private string decoCheckString;

	private int decoCheckNum;

	private List<DecorationEffect> effectList;

	private bool isSpecial;

	private string scenarioLock;

	private int dlc;

	private bool isVisibleInShop;

	private bool isPlayerOwned;

	private bool isCurrentDisplay;

	public Decoration()
	{
		decorationRefId = string.Empty;
		decorationName = string.Empty;
		decorationDesc = string.Empty;
		decorationImage = string.Empty;
		shopLevelReq = 0;
		decorationTypeRefId = string.Empty;
		decorationTypeName = string.Empty;
		decorationShopCost = 0;
		decorationUnlockCondition = UnlockCondition.UnlockConditionNone;
		decoReqCount = 0;
		decoCheckString = string.Empty;
		decoCheckNum = 0;
		isSpecial = false;
		scenarioLock = string.Empty;
		dlc = 0;
		effectList = new List<DecorationEffect>();
		isVisibleInShop = false;
		isPlayerOwned = false;
		isCurrentDisplay = false;
	}

	public Decoration(string aRefId, string aName, string aDesc, string aImage, int aShopLevelReq, string aType, string aTypeName, int aShopCost, UnlockCondition aCondition, int aValue, string aCheckString, int aCheckNum, bool aSpecial, string aScenarioLock, int aDlc)
	{
		decorationRefId = aRefId;
		decorationName = aName;
		decorationDesc = aDesc;
		decorationImage = aImage;
		shopLevelReq = aShopLevelReq;
		decorationTypeRefId = aType;
		decorationTypeName = aTypeName;
		decorationShopCost = aShopCost;
		decorationUnlockCondition = aCondition;
		decoReqCount = aValue;
		decoCheckString = aCheckString;
		decoCheckNum = aCheckNum;
		isSpecial = aSpecial;
		scenarioLock = aScenarioLock;
		dlc = aDlc;
		effectList = new List<DecorationEffect>();
		isVisibleInShop = false;
		isPlayerOwned = false;
		isCurrentDisplay = false;
	}

	public string getDecorationRefId()
	{
		return decorationRefId;
	}

	public string getDecorationName()
	{
		return decorationName;
	}

	public string getDecorationDesc()
	{
		return decorationDesc;
	}

	public string getDecorationImage()
	{
		return decorationImage;
	}

	public int getShopLevelReq()
	{
		return shopLevelReq;
	}

	public string getDecorationType()
	{
		return decorationTypeRefId;
	}

	public string getDecorationTypeName()
	{
		return decorationTypeName;
	}

	public int getDecorationShopCost()
	{
		return decorationShopCost;
	}

	public UnlockCondition getDecorationShopUnlockCondition()
	{
		return decorationUnlockCondition;
	}

	public int getDecorationShopUnlockValue()
	{
		return decoReqCount;
	}

	public string getDecorationCheckString()
	{
		return decoCheckString;
	}

	public int getDecorationCheckNum()
	{
		return decoCheckNum;
	}

	public bool checkSpecial()
	{
		return isSpecial;
	}

	public bool checkIsVisibleInShop()
	{
		return isVisibleInShop;
	}

	public void setIsVisibleInShop(bool aVisible)
	{
		isVisibleInShop = aVisible;
	}

	public bool checkIsPlayerOwned()
	{
		return isPlayerOwned;
	}

	public void setIsPlayerOwned(bool aPlayerOwned)
	{
		isPlayerOwned = aPlayerOwned;
	}

	public bool checkIsCurrentDisplay()
	{
		return isCurrentDisplay;
	}

	public void setIsCurrentDisplay(bool aDisplay)
	{
		isCurrentDisplay = aDisplay;
	}

	public int getDlc()
	{
		return dlc;
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

	public List<DecorationEffect> getDecorationEffectList()
	{
		return effectList;
	}

	public void addDecorationEffect(DecorationEffect aEffect)
	{
		effectList.Add(aEffect);
	}

	public void setDecorationEffectList(List<DecorationEffect> aEffectList)
	{
		effectList = aEffectList;
	}
}
