using System;

[Serializable]
public class GameLock
{
	private string gameLockRefId;

	private string lockFeature;

	private string unlockType;

	private string unlockRefId;

	private string gameLockSet;

	public GameLock()
	{
		gameLockRefId = string.Empty;
		lockFeature = string.Empty;
		unlockType = string.Empty;
		unlockRefId = string.Empty;
		gameLockSet = string.Empty;
	}

	public GameLock(string aRefId, string aFeature, string aUnlock, string aUnlockRefId, string aSet)
	{
		gameLockRefId = aRefId;
		lockFeature = aFeature;
		unlockType = aUnlock;
		unlockRefId = aUnlockRefId;
		gameLockSet = aSet;
	}

	public string getGameLockRefId()
	{
		return gameLockRefId;
	}

	public string getLockFeature()
	{
		return lockFeature;
	}

	public string getUnlockType()
	{
		return unlockType;
	}

	public string getUnlockRefId()
	{
		return unlockRefId;
	}

	public string getGameLockSet()
	{
		return gameLockSet;
	}
}
