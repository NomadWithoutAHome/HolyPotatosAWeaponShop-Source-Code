using System;

[Serializable]
public class Avatar
{
	private string avatarRefId;

	private string avatarName;

	private string avatarDescription;

	private string avatarImage;

	private bool isUnlock;

	private int unlockPlayRequirement;

	public Avatar()
	{
		avatarRefId = string.Empty;
		avatarName = string.Empty;
		avatarDescription = string.Empty;
		avatarImage = string.Empty;
		isUnlock = false;
		unlockPlayRequirement = 0;
	}

	public Avatar(string aRefId, string aName, string aDesc, string aImage, bool initUnlock, int aPlayReq)
	{
		avatarRefId = aRefId;
		avatarName = aName;
		avatarDescription = aDesc;
		avatarImage = aImage;
		isUnlock = initUnlock;
		unlockPlayRequirement = aPlayReq;
	}

	public string getAvatarRefId()
	{
		return avatarRefId;
	}

	public string getAvatarName()
	{
		return avatarName;
	}

	public string getAvatarDesc()
	{
		return avatarDescription;
	}

	public string getAvatarImage()
	{
		return avatarImage;
	}

	public bool checkIsUnlock()
	{
		return isUnlock;
	}

	public void setUnlock(bool aUnlock)
	{
		isUnlock = aUnlock;
	}
}
