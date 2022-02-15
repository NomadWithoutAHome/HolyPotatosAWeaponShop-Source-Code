using System;

[Serializable]
public class RefObjective
{
	public string objectiveRefId;

	public string objectiveName;

	public string objectiveDesc;

	public string timeLimit;

	public string startDialogueRefId;

	public string successDialogueRefId;

	public string successNextObjective;

	public string failDialogueRefId;

	public string failNextObjective;

	public string successCondition;

	public string reqCount;

	public string checkString;

	public string checkNum;

	public string countFromObjectiveStart;

	public string countAsObjective;

	public string objectiveSet;
}
