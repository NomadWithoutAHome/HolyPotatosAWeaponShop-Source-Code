using System;

[Serializable]
public class Code
{
	private string codeRefID;

	private string code;

	private UnlockType unlockType;

	private string refID;

	private bool isUsed;

	public Code()
	{
		codeRefID = string.Empty;
		code = string.Empty;
		unlockType = UnlockType.UnlockTypeBlank;
		refID = string.Empty;
		isUsed = false;
	}

	public Code(string aCodeRefID, string aCode, string aUnlockType, string aRefID)
	{
		codeRefID = aCodeRefID;
		code = aCode;
		switch (aUnlockType)
		{
		case "SMITH":
			unlockType = UnlockType.UnlockTypeSmith;
			break;
		case "WEAPON":
			unlockType = UnlockType.UnlockTypeWeapon;
			break;
		}
		refID = aRefID;
		isUsed = false;
	}

	public string getCodeRefID()
	{
		return codeRefID;
	}

	public string getCode()
	{
		return code;
	}

	public UnlockType getUnlockType()
	{
		return unlockType;
	}

	public string getRefID()
	{
		return refID;
	}

	public bool checkUsed()
	{
		return isUsed;
	}

	public void setUsed(bool aUsed)
	{
		isUsed = aUsed;
	}
}
