using System;

[Serializable]
public class InitialValue
{
	private string initialValueRefId;

	private string initialValueType;

	private string initialValue;

	private int initialQty;

	private string initialConditionSet;

	public InitialValue()
	{
		initialValueRefId = string.Empty;
		initialValueType = string.Empty;
		initialValue = string.Empty;
		initialQty = 0;
		initialConditionSet = string.Empty;
	}

	public InitialValue(string aRefId, string aType, string aValue, int aQty, string aSet)
	{
		initialValueRefId = aRefId;
		initialValueType = aType;
		initialValue = aValue;
		initialQty = aQty;
		initialConditionSet = aSet;
	}

	public string getInitialValueRefId()
	{
		return initialValueRefId;
	}

	public string getInitialValueType()
	{
		return initialValueType;
	}

	public string getInitialValue()
	{
		return initialValue;
	}

	public int getInitialQty()
	{
		return initialQty;
	}

	public string getInitialConditionSet()
	{
		return initialConditionSet;
	}
}
