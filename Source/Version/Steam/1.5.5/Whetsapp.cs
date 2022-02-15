using System;

[Serializable]
public class Whetsapp
{
	private string whetsappId;

	private string senderName;

	private string messageTextRefId;

	private string imagePathEncode;

	private string imagePath;

	private long time;

	private WhetsappFilterType filterType;

	private bool isRead;

	public Whetsapp()
	{
		whetsappId = string.Empty;
		senderName = string.Empty;
		messageTextRefId = string.Empty;
		imagePath = string.Empty;
		time = 0L;
		filterType = WhetsappFilterType.WhetsappFilterTypeBlank;
		isRead = false;
	}

	public Whetsapp(string aId, string aName, string aText, string aImage, long aTime, WhetsappFilterType aFilter)
	{
		whetsappId = aId;
		senderName = aName;
		messageTextRefId = aText;
		imagePath = aImage;
		time = aTime;
		filterType = aFilter;
		isRead = false;
	}

	public string getWhetsappId()
	{
		return whetsappId;
	}

	public string getSenderName()
	{
		return senderName;
	}

	public string getMessageText()
	{
		return messageTextRefId;
	}

	public string getImage()
	{
		return imagePath;
	}

	public long getTime()
	{
		return time;
	}

	public WhetsappFilterType getFilterType()
	{
		return filterType;
	}

	public bool checkRead()
	{
		return isRead;
	}

	public void setRead(bool aRead)
	{
		isRead = aRead;
	}
}
