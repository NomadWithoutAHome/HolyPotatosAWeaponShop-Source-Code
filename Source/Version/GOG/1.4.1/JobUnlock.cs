using System;

[Serializable]
public class JobUnlock
{
	private string jobUnlockRefId;

	private string requiredJobClassRefId;

	private string requiredWeaponRefId;

	private string unlockJobClassRefId;

	public JobUnlock()
	{
		jobUnlockRefId = string.Empty;
		requiredJobClassRefId = string.Empty;
		requiredWeaponRefId = string.Empty;
		unlockJobClassRefId = string.Empty;
	}

	public JobUnlock(string aJobUnlockRefId, string aRequiredJobClassRefId, string aRequiredWeaponRefId, string aUnlockJobClassRefId)
	{
		jobUnlockRefId = aJobUnlockRefId;
		requiredJobClassRefId = aRequiredJobClassRefId;
		requiredWeaponRefId = aRequiredWeaponRefId;
		unlockJobClassRefId = aUnlockJobClassRefId;
	}

	public string getUnlockJobClassRefId()
	{
		return unlockJobClassRefId;
	}
}
