using System;
using System.Collections.Generic;

[Serializable]
public class Weapon
{
	private string weaponRefId;

	private string weaponName;

	private string weaponDesc;

	private string image;

	private string weaponTypeRefId;

	private WeaponType weaponType;

	private float atkMult;

	private float spdMult;

	private float accMult;

	private float magMult;

	private Dictionary<string, int> materialList;

	private List<string> relicList;

	private WeaponStat researchType;

	private int researchDuration;

	private int researchCost;

	private float researchMood;

	private bool isUnlocked;

	private int timesUsed;

	private int dlc;

	private string scenarioLock;

	public Weapon()
	{
		weaponRefId = string.Empty;
		weaponName = string.Empty;
		weaponDesc = string.Empty;
		image = string.Empty;
		weaponTypeRefId = string.Empty;
		weaponType = new WeaponType();
		atkMult = 0f;
		spdMult = 0f;
		accMult = 0f;
		magMult = 0f;
		materialList = new Dictionary<string, int>();
		relicList = new List<string>();
		researchType = WeaponStat.WeaponStatNone;
		researchDuration = 0;
		researchCost = 0;
		researchMood = 0f;
		isUnlocked = false;
		timesUsed = 0;
		dlc = 0;
		scenarioLock = string.Empty;
	}

	public Weapon(string aRefID, string aName, string aDesc, string aImage, string aTypeRefId, float aAtkMult, float aSpdMult, float aAccMult, float aMagMult, Dictionary<string, int> aMaterialList, List<string> aRelicList, WeaponStat aResearchType, int aResearchDuration, int aResearchCost, float aResearchMood, int aDlc, string aScenarioLock)
	{
		weaponRefId = aRefID;
		weaponName = aName;
		weaponDesc = aDesc;
		image = aImage;
		weaponTypeRefId = aTypeRefId;
		weaponType = new WeaponType();
		atkMult = aAtkMult;
		spdMult = aSpdMult;
		accMult = aAccMult;
		magMult = aMagMult;
		materialList = aMaterialList;
		relicList = aRelicList;
		researchType = aResearchType;
		researchDuration = aResearchDuration;
		researchCost = aResearchCost;
		researchMood = aResearchMood;
		isUnlocked = false;
		timesUsed = 0;
		dlc = aDlc;
		scenarioLock = aScenarioLock;
	}

	public string getWeaponRefId()
	{
		return weaponRefId;
	}

	public string getWeaponName(bool isDisplay = true)
	{
		return weaponName;
	}

	public void setWeaponName(string aName)
	{
		weaponName = aName;
	}

	public string getWeaponDesc()
	{
		return weaponDesc;
	}

	public void setWeaponDesc(string aDesc)
	{
		weaponDesc = aDesc;
	}

	public void setWeaponType(WeaponType aType)
	{
		weaponType = aType;
	}

	public WeaponType getWeaponType()
	{
		return weaponType;
	}

	public string getWeaponTypeRefId()
	{
		return weaponTypeRefId;
	}

	public float getAtkMult()
	{
		return atkMult;
	}

	public float getSpdMult()
	{
		return spdMult;
	}

	public float getAccMult()
	{
		return accMult;
	}

	public float getMagMult()
	{
		return magMult;
	}

	public bool getWeaponUnlocked()
	{
		return isUnlocked;
	}

	public void setWeaponUnlocked(bool aUnlocked)
	{
		isUnlocked = aUnlocked;
	}

	public int getTimesUsed()
	{
		return timesUsed;
	}

	public void addTimesUsed(int aUsed)
	{
		timesUsed += aUsed;
	}

	public void setTimesUsed(int aUsed)
	{
		timesUsed = aUsed;
	}

	public bool checkWeaponValid()
	{
		if (weaponRefId == string.Empty)
		{
			return false;
		}
		return true;
	}

	public string getImage()
	{
		return image;
	}

	public string getScenarioLock()
	{
		return scenarioLock;
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

	public void addMaterial(string aMatRefId, int aMatCount)
	{
		materialList.Add(aMatRefId, aMatCount);
	}

	public Dictionary<string, int> getMaterialList()
	{
		return materialList;
	}

	public void addRelic(string aRelicRefId)
	{
		relicList.Add(aRelicRefId);
	}

	public List<string> getRelicList()
	{
		return relicList;
	}

	public WeaponStat getResearchType()
	{
		return researchType;
	}

	public int getResearchDuration()
	{
		return researchDuration;
	}

	public int getResearchCost()
	{
		return researchCost;
	}

	public float getResearchMood()
	{
		return researchMood;
	}

	public int getDlc()
	{
		return dlc;
	}
}
