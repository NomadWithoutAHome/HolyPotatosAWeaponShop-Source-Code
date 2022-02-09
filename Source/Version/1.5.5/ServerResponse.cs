using System;

[Serializable]
public class ServerResponse
{
	public int result;

	public int errorCode;

	public string errorMsg;

	public ServerValue value;

	public string requestID;
}
