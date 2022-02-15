using System;

[Serializable]
public class ScenarioVariable
{
	private string scenarioVariableRefId;

	private string variableName;

	private string variableInitValue;

	private string variableValue;

	private string scenarioSet;

	public ScenarioVariable()
	{
		scenarioVariableRefId = string.Empty;
		variableName = string.Empty;
		variableInitValue = string.Empty;
		variableValue = string.Empty;
		scenarioSet = string.Empty;
	}

	public ScenarioVariable(string aRefId, string aName, string aValue, string aSet)
	{
		scenarioVariableRefId = aRefId;
		variableName = aName;
		variableInitValue = aValue;
		variableValue = aValue;
		scenarioSet = aSet;
	}

	public void resetVariable()
	{
		variableValue = variableInitValue;
	}

	public string getScenarioVariableRefId()
	{
		return scenarioVariableRefId;
	}

	public string getVariableName()
	{
		return variableName;
	}

	public string getVariableInitValue()
	{
		return variableInitValue;
	}

	public string getScenarioSet()
	{
		return scenarioSet;
	}

	public string getVariableValueString()
	{
		return variableValue;
	}

	public void setVariableValueString(string aValue)
	{
		variableValue = aValue;
	}

	public int getVariableValueInt()
	{
		return CommonAPI.parseInt(variableValue);
	}

	public void setVariableValueInt(int aValue)
	{
		variableValue = aValue.ToString();
	}

	public float getVariableValueFloat()
	{
		return CommonAPI.parseFloat(variableValue);
	}

	public void setVariableValueFloat(float aValue)
	{
		variableValue = aValue.ToString();
	}

	public bool getVariableValueBool()
	{
		return bool.Parse(variableValue);
	}

	public void setVariableValueBool(bool aValue)
	{
		variableValue = aValue.ToString();
	}
}
