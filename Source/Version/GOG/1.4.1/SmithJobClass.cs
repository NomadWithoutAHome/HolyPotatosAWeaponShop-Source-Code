using System;
using System.Collections.Generic;

[Serializable]
public class SmithJobClass
{
	private string smithJobRefId;

	private string smithJobName;

	private string smithJobDesc;

	private int smithJobChangeCost;

	private int maxLevel;

	private int salaryMult;

	private float powMult;

	private float intMult;

	private float tecMult;

	private float lucMult;

	private bool canDesign;

	private bool canCraft;

	private bool canPolish;

	private bool canEnchant;

	private Dictionary<string, int> unlockRequirement;

	private int basePermPow;

	private float growthPermPow;

	private int basePermInt;

	private float growthPermInt;

	private int basePermTec;

	private float growthPermTec;

	private int basePermLuc;

	private float growthPermLuc;

	private int basePermSta;

	private float growthPermSta;

	private float expGrowthType;

	private int baseExp;

	private float growthExp;

	public SmithJobClass()
	{
		smithJobRefId = string.Empty;
		smithJobName = string.Empty;
		smithJobDesc = string.Empty;
		smithJobChangeCost = 0;
		maxLevel = 0;
		salaryMult = 0;
		powMult = 0f;
		intMult = 0f;
		tecMult = 0f;
		lucMult = 0f;
		canDesign = false;
		canCraft = false;
		canPolish = false;
		canEnchant = false;
		unlockRequirement = new Dictionary<string, int>();
		basePermPow = 0;
		growthPermPow = 0f;
		basePermInt = 0;
		growthPermInt = 0f;
		basePermTec = 0;
		growthPermTec = 0f;
		basePermLuc = 0;
		growthPermLuc = 0f;
		basePermSta = 0;
		growthPermSta = 0f;
		expGrowthType = 0f;
		baseExp = 0;
		growthExp = 0f;
	}

	public SmithJobClass(string aRefId, string aName, string aDesc, int aCost, int aMaxLevel, int aSalaryMult, float aPowMult, float aIntMult, float aTecMult, float aLucMult, bool aDesign, bool aCraft, bool aPolish, bool aEnchant, Dictionary<string, int> aReq, int aBasePermPow, float aGrowthPermPow, int aBasePermInt, float aGrowthPermInt, int aBasePermTec, float aGrowthPermTec, int aBasePermLuc, float aGrowthPermLuc, int aBasePermSta, float aGrowthPermSta, float aExpGrowthType, int aBaseExp, float aGrowthExp)
	{
		smithJobRefId = aRefId;
		smithJobName = aName;
		smithJobDesc = aDesc;
		smithJobChangeCost = aCost;
		maxLevel = aMaxLevel;
		salaryMult = aSalaryMult;
		powMult = aPowMult;
		intMult = aIntMult;
		tecMult = aTecMult;
		lucMult = aLucMult;
		canDesign = aDesign;
		canCraft = aCraft;
		canPolish = aPolish;
		canEnchant = aEnchant;
		unlockRequirement = aReq;
		basePermPow = aBasePermPow;
		growthPermPow = aGrowthPermPow;
		basePermInt = aBasePermInt;
		growthPermInt = aGrowthPermInt;
		basePermTec = aBasePermTec;
		growthPermTec = aGrowthPermTec;
		basePermLuc = aBasePermLuc;
		growthPermLuc = aGrowthPermLuc;
		basePermSta = aBasePermSta;
		growthPermSta = aGrowthPermSta;
		expGrowthType = aExpGrowthType;
		baseExp = aBaseExp;
		growthExp = aGrowthExp;
	}

	public string getSmithJobRefId()
	{
		return smithJobRefId;
	}

	public string getSmithJobName()
	{
		return smithJobName;
	}

	public string getSmithJobDesc()
	{
		return smithJobDesc;
	}

	public int getSmithJobChangeCost()
	{
		return smithJobChangeCost;
	}

	public int getMaxLevel()
	{
		return maxLevel;
	}

	public int getSalaryMult()
	{
		return salaryMult;
	}

	public bool checkLevelReq(List<SmithExperience> experienceList)
	{
		foreach (SmithExperience experience in experienceList)
		{
			if (unlockRequirement.ContainsKey(experience.getSmithJobClassRefId()) && unlockRequirement[experience.getSmithJobClassRefId()] > experience.getSmithJobClassLevel())
			{
				return false;
			}
		}
		return true;
	}

	public float getPowMult()
	{
		return powMult;
	}

	public float getIntMult()
	{
		return intMult;
	}

	public float getTecMult()
	{
		return tecMult;
	}

	public float getLucMult()
	{
		return lucMult;
	}

	public bool checkDesign()
	{
		return canDesign;
	}

	public bool checkCraft()
	{
		return canCraft;
	}

	public bool checkPolish()
	{
		return canPolish;
	}

	public bool checkEnchant()
	{
		return canEnchant;
	}

	public int getExpToLevelUp(int currentLevel)
	{
		return (int)((double)(float)baseExp + (double)growthExp * Math.Pow(currentLevel - 1, expGrowthType));
	}

	public List<int> getPermBuffForLevel(int reachedLevel)
	{
		List<int> list = new List<int>();
		list.Add((int)((float)basePermPow + growthPermPow * (float)(reachedLevel - 1)));
		list.Add((int)((float)basePermInt + growthPermInt * (float)(reachedLevel - 1)));
		list.Add((int)((float)basePermTec + growthPermTec * (float)(reachedLevel - 1)));
		list.Add((int)((float)basePermLuc + growthPermLuc * (float)(reachedLevel - 1)));
		list.Add((int)((float)basePermSta + growthPermSta * (float)(reachedLevel - 1)));
		return list;
	}
}
