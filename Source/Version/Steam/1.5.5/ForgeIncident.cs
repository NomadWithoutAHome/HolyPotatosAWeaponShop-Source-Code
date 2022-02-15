using System;

[Serializable]
public class ForgeIncident
{
	private string incidentRefId;

	private string incidentName;

	private string incidentDesc;

	private IncidentType incidentType;

	private float incidentMagnitude;

	private string incidentImage;

	private int monthReq;

	public ForgeIncident()
	{
		incidentRefId = string.Empty;
		incidentName = string.Empty;
		incidentDesc = string.Empty;
		incidentType = IncidentType.IncidentTypeNothing;
		incidentMagnitude = 0f;
		incidentImage = string.Empty;
		monthReq = 0;
	}

	public ForgeIncident(string aRefId, string aName, string aDesc, IncidentType aType, float aMagnitude, string aImage, int aMonth)
	{
		incidentRefId = aRefId;
		incidentName = aName;
		incidentDesc = aDesc;
		incidentType = aType;
		incidentMagnitude = aMagnitude;
		incidentImage = aImage;
		monthReq = aMonth;
	}

	public string getIncidentRefId()
	{
		return incidentRefId;
	}

	public string getIncidentName()
	{
		return incidentName;
	}

	public string getIncidentDesc()
	{
		return incidentDesc;
	}

	public IncidentType getIncidentType()
	{
		return incidentType;
	}

	public float getIncidentMagnitude()
	{
		return incidentMagnitude;
	}

	public string getImage()
	{
		return incidentImage;
	}

	public bool checkMonthReq(int month)
	{
		if (month < monthReq)
		{
			return false;
		}
		return true;
	}
}
