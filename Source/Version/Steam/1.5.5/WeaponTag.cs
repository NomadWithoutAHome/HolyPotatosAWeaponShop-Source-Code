using System;

[Serializable]
public class WeaponTag
{
	private string weaponTagRefId;

	private string tagRefId;

	private string weaponRefId;

	public WeaponTag()
	{
		weaponTagRefId = string.Empty;
		tagRefId = string.Empty;
		weaponRefId = string.Empty;
	}

	public WeaponTag(string aWeaponTagRefId, string aTagRefId, string aWeaponRefId)
	{
		weaponTagRefId = aWeaponTagRefId;
		tagRefId = aTagRefId;
		weaponRefId = aWeaponRefId;
	}

	public string getWeaponTagRefId()
	{
		return weaponTagRefId;
	}

	public string getTagRefId()
	{
		return tagRefId;
	}

	public string getWeaponRefId()
	{
		return weaponRefId;
	}
}
