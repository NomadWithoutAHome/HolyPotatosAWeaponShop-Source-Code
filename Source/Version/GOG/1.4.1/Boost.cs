using System;

[Serializable]
public class Boost
{
	private int initialValueRefId;

	private int initialValueType;

	private int initialValue;

	private int initialQty;

	public Boost()
	{
		initialValueRefId = 0;
		initialValueType = 0;
		initialValue = 0;
		initialQty = 0;
	}

	public Boost(int aBoostAtk, int aBoostAcc, int aBoostSpd, int aBoostMag)
	{
		initialValueRefId = aBoostAtk;
		initialValueType = aBoostAcc;
		initialValue = aBoostSpd;
		initialQty = aBoostMag;
	}

	public int getBoostAtk()
	{
		return initialValueRefId;
	}

	public int getBoostAcc()
	{
		return initialValueType;
	}

	public int getBoostSpd()
	{
		return initialValue;
	}

	public int getBoostMag()
	{
		return initialQty;
	}
}
