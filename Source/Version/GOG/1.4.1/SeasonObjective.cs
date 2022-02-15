using System;

[Serializable]
public class SeasonObjective
{
	private string objectiveRefId;

	private string objectiveTitle;

	private string objectiveDescription;

	private int objectiveSeasonIndex;

	private string alignment;

	private int range;

	private int targetPoints;

	private string startDialogueRefId;

	private string endDialogueRefId;

	private bool isCompleted;

	private int points;

	public SeasonObjective()
	{
		objectiveRefId = string.Empty;
		objectiveTitle = string.Empty;
		objectiveDescription = string.Empty;
		objectiveSeasonIndex = 0;
		alignment = string.Empty;
		range = 0;
		targetPoints = 0;
		startDialogueRefId = string.Empty;
		endDialogueRefId = string.Empty;
		isCompleted = false;
		points = 0;
	}

	public SeasonObjective(string aRefId, string aTitle, string aDesc, int aSeason, string aAlign, int aRange, int aTargetPoints, string aStartDialogueRefId, string aEndDialogueRefId)
	{
		objectiveRefId = aRefId;
		objectiveTitle = aTitle;
		objectiveDescription = aDesc;
		objectiveSeasonIndex = aSeason;
		alignment = aAlign;
		range = aRange;
		targetPoints = aTargetPoints;
		startDialogueRefId = aStartDialogueRefId;
		endDialogueRefId = aEndDialogueRefId;
		isCompleted = false;
		points = 0;
	}

	public string getObjectiveRefId()
	{
		return objectiveRefId;
	}

	public string getObjectiveTitle()
	{
		return objectiveTitle;
	}

	public string getObjectiveDesc()
	{
		return objectiveDescription;
	}

	public int getSeasonIndex()
	{
		return objectiveSeasonIndex;
	}

	public bool checkAlignmentCondition(int playerLaw, int playerChaos)
	{
		switch (alignment)
		{
		case "LAW":
			if (playerLaw - playerChaos > range)
			{
				return true;
			}
			break;
		case "CHAOS":
			if (playerChaos - playerLaw > range)
			{
				return true;
			}
			break;
		case "NEUTRAL":
			return true;
		}
		return false;
	}

	public int getPoints()
	{
		return points;
	}

	public void setPoints(int aPoint)
	{
		points = aPoint;
	}

	public void addPoints(int aPoint)
	{
		points += aPoint;
	}

	public int getTargetPoints()
	{
		return targetPoints;
	}

	public int getCompletionPercent()
	{
		float num = (float)points / (float)targetPoints * 100f;
		return (int)num;
	}

	public string getStartDialogue()
	{
		return startDialogueRefId;
	}

	public string getEndDialogue()
	{
		return endDialogueRefId;
	}

	public bool checkPoints()
	{
		if (points >= targetPoints)
		{
			return true;
		}
		return false;
	}

	public bool checkIsCompleted()
	{
		return isCompleted;
	}

	public void setCompletion(bool aComplete)
	{
		isCompleted = aComplete;
	}
}
