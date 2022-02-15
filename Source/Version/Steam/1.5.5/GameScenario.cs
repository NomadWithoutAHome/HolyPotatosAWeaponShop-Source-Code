using System;

[Serializable]
public class GameScenario
{
	private string gameScenarioRefId;

	private string gameScenarioName;

	private string gameScenarioDescription;

	private int difficulty;

	private string bgImage;

	private string image;

	private string completeImg;

	private bool isScenario;

	private string initialConditionSet;

	private string gameLockSet;

	private string itemLockSet;

	private string objectiveSet;

	private string firstObjectiveRefId;

	private string formulaConstantsSet;

	private int dlc;

	public GameScenario()
	{
		gameScenarioRefId = string.Empty;
		gameScenarioName = string.Empty;
		gameScenarioDescription = string.Empty;
		difficulty = 0;
		bgImage = string.Empty;
		image = string.Empty;
		completeImg = string.Empty;
		isScenario = false;
		initialConditionSet = string.Empty;
		gameLockSet = string.Empty;
		itemLockSet = string.Empty;
		objectiveSet = string.Empty;
		firstObjectiveRefId = string.Empty;
		formulaConstantsSet = string.Empty;
		dlc = 0;
	}

	public GameScenario(string aRefId, string aName, string aDesc, int aDifficulty, string aBgImage, string aImage, string aCompleteImg, bool aScenario, string aCondition, string aLockSet, string aItemLockSet, string aObjectiveSet, string aObjective, string aConstants, int aDlc)
	{
		gameScenarioRefId = aRefId;
		gameScenarioName = aName;
		gameScenarioDescription = aDesc;
		difficulty = aDifficulty;
		bgImage = aBgImage;
		image = aImage;
		completeImg = aCompleteImg;
		isScenario = aScenario;
		initialConditionSet = aCondition;
		gameLockSet = aLockSet;
		itemLockSet = aItemLockSet;
		objectiveSet = aObjectiveSet;
		firstObjectiveRefId = aObjective;
		formulaConstantsSet = aConstants;
		dlc = aDlc;
	}

	public string getGameScenarioRefId()
	{
		return gameScenarioRefId;
	}

	public string getGameScenarioName()
	{
		return gameScenarioName;
	}

	public string getGameScenarioDescription()
	{
		return gameScenarioDescription;
	}

	public int getDifficultyStar()
	{
		return difficulty;
	}

	public string getBgImage()
	{
		return bgImage;
	}

	public string getImage()
	{
		return image;
	}

	public string getCompleteImg()
	{
		return completeImg;
	}

	public bool checkIsScenario()
	{
		return isScenario;
	}

	public string getInitialConditionSet()
	{
		return initialConditionSet;
	}

	public string getGameLockSet()
	{
		return gameLockSet;
	}

	public string getItemLockSet()
	{
		return itemLockSet;
	}

	public string getObjectiveSet()
	{
		return objectiveSet;
	}

	public string getFirstObjectiveRefId()
	{
		return firstObjectiveRefId;
	}

	public string getFormulaConstantsSet()
	{
		return formulaConstantsSet;
	}

	public int getDlc()
	{
		return dlc;
	}
}
