using System;

[Serializable]
public class SmithStatusEffect
{
	private string smithEffectRefId;

	private string effectName;

	private string effectComment;

	private string effectDescription;

	private StatEffect effect1Type;

	private float effect1Value;

	private StatEffect effect2Type;

	private float effect2Value;

	private int effectDuration;

	public SmithStatusEffect()
	{
		smithEffectRefId = string.Empty;
		effectName = string.Empty;
		effectComment = string.Empty;
		effectDescription = string.Empty;
		effect1Type = StatEffect.StatEffectNothing;
		effect1Value = 0f;
		effect2Type = StatEffect.StatEffectNothing;
		effect2Value = 0f;
		effectDuration = 0;
	}

	public SmithStatusEffect(string aRefId, string aName, string aComment, string aDesc, StatEffect aEffect1Type, float aEffect1Value, StatEffect aEffect2Type, float aEffect2Value, int aEffectDuration)
	{
		smithEffectRefId = aRefId;
		effectName = aName;
		effectComment = aComment;
		effectDescription = aDesc;
		effect1Type = aEffect1Type;
		effect1Value = aEffect1Value;
		effect2Type = aEffect2Type;
		effect2Value = aEffect2Value;
		effectDuration = aEffectDuration;
	}

	public string getEffectRefId()
	{
		return smithEffectRefId;
	}

	public string getEffectName()
	{
		return effectName;
	}

	public string getEffectComment()
	{
		return effectComment;
	}

	public string getEffectDesc()
	{
		return effectDescription;
	}

	public StatEffect getEffect1Type()
	{
		return effect1Type;
	}

	public float getEffect1Value()
	{
		return effect1Value;
	}

	public StatEffect getEffect2Type()
	{
		return effect2Type;
	}

	public float getEffect2Value()
	{
		return effect2Value;
	}

	public int getEffectDuration()
	{
		return effectDuration;
	}
}
