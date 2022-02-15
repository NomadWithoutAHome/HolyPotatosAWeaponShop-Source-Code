using System;

[Serializable]
public class GameConstant
{
	private string lookUpKey;

	private string value;

	public GameConstant()
	{
		lookUpKey = string.Empty;
		value = string.Empty;
	}

	public GameConstant(string aLookUpKey, string aValue)
	{
		lookUpKey = aLookUpKey;
		value = aValue;
	}

	public string getLookUpKey()
	{
		return lookUpKey;
	}

	public string getValue()
	{
		return value;
	}
}
