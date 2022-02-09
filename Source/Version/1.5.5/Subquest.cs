using System;

[Serializable]
public class Subquest
{
	private string subquestRefId;

	private string subquestTitle;

	private string subquestDesc;

	private SubquestType subquestType;

	private string requirementType;

	private int requirementValue;

	private int subquestGold;

	private string subquestItemRefId;

	private int subquestItemQuantity;

	private int subquestAlignGood;

	private int subquestAlignEvil;

	private string subquestWarText;

	private string subquestCutscene;

	private bool subquestUnlocked;

	private bool subquestAttempted;

	public Subquest()
	{
		subquestRefId = string.Empty;
		subquestTitle = string.Empty;
		subquestDesc = string.Empty;
		subquestType = SubquestType.SubquestTypeBlank;
		requirementType = string.Empty;
		requirementValue = 0;
		subquestGold = 0;
		subquestItemRefId = string.Empty;
		subquestItemQuantity = 0;
		subquestAlignGood = 0;
		subquestAlignEvil = 0;
		subquestWarText = string.Empty;
		subquestCutscene = string.Empty;
		subquestUnlocked = false;
		subquestAttempted = false;
	}

	public Subquest(string aRefId, string aTitle, string aDesc, SubquestType aSubquestType, string aType, int aValue, int aGold, string aItemRef, int aItemQty, int aGood, int aEvil, string aWarText, string aCutscene)
	{
		subquestRefId = aRefId;
		subquestTitle = aTitle;
		subquestDesc = aDesc;
		subquestType = aSubquestType;
		requirementType = aType;
		requirementValue = aValue;
		subquestGold = aGold;
		subquestItemRefId = aItemRef;
		subquestItemQuantity = aItemQty;
		subquestAlignGood = aGood;
		subquestAlignEvil = aEvil;
		subquestWarText = aWarText;
		subquestCutscene = aCutscene;
		subquestUnlocked = false;
		subquestAttempted = false;
	}

	public bool checkSubquestExist()
	{
		if (subquestRefId == string.Empty || subquestRefId == "-1")
		{
			return false;
		}
		return true;
	}

	public string getRefId()
	{
		return subquestRefId;
	}

	public string getSubquestTitle()
	{
		return subquestTitle;
	}

	public string getSubquestDesc()
	{
		return subquestDesc;
	}

	public SubquestType getSubquestType()
	{
		return subquestType;
	}

	public string getSubquestRequirementType()
	{
		return requirementType;
	}

	public int getSubquestGold()
	{
		return subquestGold;
	}

	public int getSubquestGood()
	{
		return subquestAlignGood;
	}

	public int getSubquestEvil()
	{
		return subquestAlignEvil;
	}

	public string getSubquestItemRefId()
	{
		return subquestItemRefId;
	}

	public int getSubquestItemQuantity()
	{
		return subquestItemQuantity;
	}

	public bool checkSubquestAttempted()
	{
		return subquestAttempted;
	}

	public void setSubquestAttempted(bool aAttempted)
	{
		subquestAttempted = aAttempted;
	}

	public void attemptSubquest()
	{
		subquestAttempted = true;
	}

	public bool checkSubquestUnlocked()
	{
		return subquestUnlocked;
	}

	public void setSubquestUnlocked(bool aUnlocked)
	{
		subquestUnlocked = aUnlocked;
	}

	public void unlockSubquest()
	{
		subquestUnlocked = true;
	}

	public string getWarText()
	{
		return subquestWarText;
	}

	public string getCutsceneSetId()
	{
		return subquestCutscene;
	}

	public bool checkSubquestCondition(Project project)
	{
		switch (subquestType)
		{
		case SubquestType.SubquestTypeEnding:
			return true;
		case SubquestType.SubquestTypeWeapon:
			if (project.getProjectWeapon().getWeaponRefId() == requirementType)
			{
				return true;
			}
			break;
		case SubquestType.SubquestTypeElement:
			return true;
		case SubquestType.SubquestTypeStat:
			switch (requirementType)
			{
			case "ATK":
				if (project.getAtk() >= requirementValue)
				{
					return true;
				}
				break;
			case "SPD":
				if (project.getSpd() >= requirementValue)
				{
					return true;
				}
				break;
			case "ACC":
				if (project.getAcc() >= requirementValue)
				{
					return true;
				}
				break;
			case "MAG":
				if (project.getMag() >= requirementValue)
				{
					return true;
				}
				break;
			}
			break;
		}
		return false;
	}
}
