using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DayEndScenario
{
	private string scenarioRefId;

	private string scenarioText;

	private string scenarioChoice1;

	private string scenarioChoice2;

	private bool hasProject;

	private UnlockCondition unlockCondition;

	private int reqCount;

	private string checkString;

	private int checkNum;

	private int endCount;

	private int choice1Count;

	private int choice2Count;

	private string scenarioChoice1Success;

	private int choice1SuccessChance;

	private ScenarioEffect choice1SuccessEffect;

	private float choice1SuccessValue;

	private string scenarioChoice1Failure;

	private int choice1FailureChance;

	private ScenarioEffect choice1FailureEffect;

	private float choice1FailureValue;

	private string scenarioChoice2Success;

	private int choice2SuccessChance;

	private ScenarioEffect choice2SuccessEffect;

	private float choice2SuccessValue;

	private string scenarioChoice2Failure;

	private int choice2FailureChance;

	private ScenarioEffect choice2FailureEffect;

	private float choice2FailureValue;

	public DayEndScenario()
	{
		scenarioRefId = string.Empty;
		scenarioText = string.Empty;
		scenarioChoice1 = string.Empty;
		scenarioChoice2 = string.Empty;
		hasProject = false;
		unlockCondition = UnlockCondition.UnlockConditionNone;
		reqCount = 0;
		checkString = string.Empty;
		checkNum = 0;
		endCount = 0;
		choice1Count = 0;
		choice2Count = 0;
		scenarioChoice1Success = string.Empty;
		choice1SuccessChance = 0;
		choice1SuccessEffect = ScenarioEffect.ScenarioEffectNothing;
		choice1SuccessValue = 0f;
		scenarioChoice1Failure = string.Empty;
		choice1FailureChance = 0;
		choice1FailureEffect = ScenarioEffect.ScenarioEffectNothing;
		choice1FailureValue = 0f;
		scenarioChoice2Success = string.Empty;
		choice2SuccessChance = 0;
		choice2SuccessEffect = ScenarioEffect.ScenarioEffectNothing;
		choice2SuccessValue = 0f;
		scenarioChoice2Failure = string.Empty;
		choice2FailureChance = 0;
		choice2FailureEffect = ScenarioEffect.ScenarioEffectNothing;
		choice2FailureValue = 0f;
	}

	public DayEndScenario(string aID, string aText, string aChoice1, string aChoice2, bool aProject, UnlockCondition aCondition, int aReqCount, string aCheckString, int aCheckNum, int aEndCount, string a1Success, int a1SuccessChance, ScenarioEffect a1SuccessEffect, float a1SuccessValue, string a1Failure, int a1FailureChance, ScenarioEffect a1FailureEffect, float a1FailureValue, string a2Success, int a2SuccessChance, ScenarioEffect a2SuccessEffect, float a2SuccessValue, string a2Failure, int a2FailureChance, ScenarioEffect a2FailureEffect, float a2FailureValue)
	{
		scenarioRefId = aID;
		scenarioText = aText;
		scenarioChoice1 = aChoice1;
		scenarioChoice2 = aChoice2;
		hasProject = aProject;
		unlockCondition = aCondition;
		reqCount = aReqCount;
		checkString = aCheckString;
		checkNum = aCheckNum;
		endCount = aEndCount;
		choice1Count = 0;
		choice2Count = 0;
		scenarioChoice1Success = a1Success;
		choice1SuccessChance = a1SuccessChance;
		choice1SuccessEffect = a1SuccessEffect;
		choice1SuccessValue = a1SuccessValue;
		scenarioChoice1Failure = a1Failure;
		choice1FailureChance = a1FailureChance;
		choice1FailureEffect = a1FailureEffect;
		choice1FailureValue = a1FailureValue;
		scenarioChoice2Success = a2Success;
		choice2SuccessChance = a2SuccessChance;
		choice2SuccessEffect = a2SuccessEffect;
		choice2SuccessValue = a2SuccessValue;
		scenarioChoice2Failure = a2Failure;
		choice2FailureChance = a2FailureChance;
		choice2FailureEffect = a2FailureEffect;
		choice2FailureValue = a2FailureValue;
	}

	public void resetDynData()
	{
		choice1Count = 0;
		choice2Count = 0;
	}

	public string getScenarioRefId()
	{
		return scenarioRefId;
	}

	public bool checkNeedProject()
	{
		return hasProject;
	}

	public UnlockCondition getUnlockCondition()
	{
		return unlockCondition;
	}

	public int getReqCount()
	{
		return reqCount;
	}

	public string getCheckString()
	{
		return checkString;
	}

	public int getCheckNum()
	{
		return checkNum;
	}

	public int getEndCount()
	{
		return endCount;
	}

	public int getScenarioChoice1Count()
	{
		return choice1Count;
	}

	public int getScenarioChoice2Count()
	{
		return choice2Count;
	}

	public List<string> getScenarioStringList()
	{
		List<string> list = new List<string>();
		list.Add(scenarioText);
		list.Add(scenarioChoice1);
		list.Add(scenarioChoice2);
		return list;
	}

	public Hashtable makeChoice1()
	{
		choice1Count++;
		Hashtable hashtable = new Hashtable();
		List<int> list = new List<int>();
		list.Add(choice1SuccessChance);
		list.Add(choice1FailureChance);
		if (CommonAPI.getWeightedRandomIndex(list) == 0)
		{
			hashtable.Add("text", scenarioChoice1Success);
			hashtable.Add("effect", choice1SuccessEffect);
			hashtable.Add("value", choice1SuccessValue);
			hashtable.Add("result", "SUCCESS");
		}
		else
		{
			hashtable.Add("text", scenarioChoice1Failure);
			hashtable.Add("effect", choice1FailureEffect);
			hashtable.Add("value", choice1FailureValue);
			hashtable.Add("result", "FAIL");
		}
		return hashtable;
	}

	public Hashtable makeChoice2()
	{
		choice2Count++;
		Hashtable hashtable = new Hashtable();
		List<int> list = new List<int>();
		list.Add(choice2SuccessChance);
		list.Add(choice2FailureChance);
		if (CommonAPI.getWeightedRandomIndex(list) == 0)
		{
			hashtable.Add("text", scenarioChoice2Success);
			hashtable.Add("effect", choice2SuccessEffect);
			hashtable.Add("value", choice2SuccessValue);
			hashtable.Add("result", "SUCCESS");
		}
		else
		{
			hashtable.Add("text", scenarioChoice2Failure);
			hashtable.Add("effect", choice2FailureEffect);
			hashtable.Add("value", choice2FailureValue);
			hashtable.Add("result", "FAIL");
		}
		return hashtable;
	}
}
