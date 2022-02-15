using System;

[Serializable]
public class ServerDynResponse
{
	public int result;

	public int errorCode;

	public string errorMsg;

	public ServerDynValue value;

	public string requestID;
}
