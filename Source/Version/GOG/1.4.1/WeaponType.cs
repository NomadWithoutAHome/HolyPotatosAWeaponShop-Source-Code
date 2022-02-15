using System;

[Serializable]
public class WeaponType
{
	private string weaponTypeRefId;

	private string weaponTypeName;

	private string skillName;

	private string firstWeaponRefId;

	private float scoreMultAtk;

	private float scoreMultSpd;

	private float scoreMultAcc;

	private float scoreMultMag;

	private bool isUnlocked;

	private string image;

	public WeaponType()
	{
		weaponTypeRefId = string.Empty;
		weaponTypeName = string.Empty;
		skillName = string.Empty;
		firstWeaponRefId = string.Empty;
		scoreMultAtk = 0f;
		scoreMultSpd = 0f;
		scoreMultAcc = 0f;
		scoreMultMag = 0f;
		image = string.Empty;
	}

	public WeaponType(string aRefId, string aName, string aSkill, string aFirstRefId, float aAtkMult, float aSpdMult, float aAccMult, float aMagMult, string aImage)
	{
		weaponTypeRefId = aRefId;
		weaponTypeName = aName;
		skillName = aSkill;
		firstWeaponRefId = aFirstRefId;
		scoreMultAtk = aAtkMult;
		scoreMultSpd = aSpdMult;
		scoreMultAcc = aAccMult;
		scoreMultMag = aMagMult;
		image = aImage;
	}

	public string getWeaponTypeRefId()
	{
		return weaponTypeRefId;
	}

	public string getWeaponTypeName(bool isDisplay = true)
	{
		return weaponTypeName;
	}

	public string getWeaponTypeSkill()
	{
		return skillName;
	}

	public int getWeaponScore(int atk, int spd, int acc, int mag)
	{
		int num = 0;
		num += (int)((float)atk * scoreMultAtk);
		num += (int)((float)spd * scoreMultSpd);
		num += (int)((float)acc * scoreMultAcc);
		return num + (int)((float)mag * scoreMultMag);
	}

	public void doUnlock()
	{
		isUnlocked = true;
	}

	public void setUnlock(bool aUnlock)
	{
		isUnlocked = aUnlock;
	}

	public bool checkUnlocked()
	{
		return isUnlocked;
	}

	public string getFirstWeaponRefId()
	{
		return firstWeaponRefId;
	}

	public string getImage()
	{
		return image;
	}
}
