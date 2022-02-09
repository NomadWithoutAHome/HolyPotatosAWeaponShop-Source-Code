using System;

[Serializable]
public class ShopMonthlyStarch
{
	private string shopMonthlyStarchId;

	private int month;

	private RecordType recordType;

	private string recordName;

	private int amount;

	public ShopMonthlyStarch()
	{
		shopMonthlyStarchId = string.Empty;
		month = 0;
		recordType = RecordType.RecordTypeBlank;
		recordName = string.Empty;
		amount = 0;
	}

	public ShopMonthlyStarch(string aShopMonthlyStarchId, int aMonth, RecordType aType, string aName, int aAmount)
	{
		shopMonthlyStarchId = aShopMonthlyStarchId;
		month = aMonth;
		recordType = aType;
		recordName = aName;
		amount = aAmount;
	}

	public string getShopMonthlyStarchId()
	{
		return shopMonthlyStarchId;
	}

	public int getMonth()
	{
		return month;
	}

	public RecordType getRecordType()
	{
		return recordType;
	}

	public string getRecordName()
	{
		return recordName;
	}

	public int getAmount()
	{
		return amount;
	}

	public void setAmount(int aAmount)
	{
		amount = aAmount;
	}

	public void addAmount(int aAmount)
	{
		amount += aAmount;
	}
}
