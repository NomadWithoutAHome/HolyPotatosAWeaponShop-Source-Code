using System;

[Serializable]
public class Ending
{
	private string endingRefId;

	private string endingName;

	private int endingReqGoodAmt;

	private int endingReqEvilAmt;

	public Ending()
	{
		endingRefId = string.Empty;
		endingName = string.Empty;
		endingReqGoodAmt = 0;
		endingReqEvilAmt = 0;
	}

	public Ending(string aEndingRefId, string aName, int aGood, int aEvil)
	{
		endingRefId = aEndingRefId;
		endingName = aName;
		endingReqGoodAmt = aGood;
		endingReqEvilAmt = aEvil;
	}
}
