using System;
using UnityEngine;

[Serializable]
public class SmithAction
{
	private string smithActionRefId;

	private string smithActionText;

	private SmithActionState smithActionState;

	private bool isAllowedWhenWorking;

	private bool isAllowedWhenIdle;

	private string requiredWeather;

	private float hpBelow;

	private int actionChance;

	private int durationMin;

	private int durationMax;

	private StatEffect statEffect;

	private float effectMin;

	private float effectMax;

	private string itemRequired;

	private string itemPrevent;

	private int itemMinLevel;

	private string image;

	public SmithAction()
	{
		smithActionRefId = string.Empty;
		smithActionText = string.Empty;
		smithActionState = SmithActionState.SmithActionStateDefault;
		isAllowedWhenWorking = false;
		isAllowedWhenIdle = false;
		requiredWeather = string.Empty;
		hpBelow = 0f;
		actionChance = 0;
		durationMin = 0;
		durationMax = 0;
		statEffect = StatEffect.StatEffectNothing;
		effectMin = 0f;
		effectMax = 0f;
		itemRequired = string.Empty;
		itemPrevent = string.Empty;
		itemMinLevel = 0;
		image = string.Empty;
	}

	public SmithAction(string aRefId, string aText, SmithActionState aState, bool aWorkAllow, bool aIdleAllow, string aWeather, float aHpReq, int aChance, int aDurationMin, int aDurationMax, StatEffect aEffect, float aEffectMin, float aEffectMax, string aRequired, string aPrevent, int aMinLevel, string aImage)
	{
		smithActionRefId = aRefId;
		smithActionText = aText;
		smithActionState = aState;
		isAllowedWhenWorking = aWorkAllow;
		isAllowedWhenIdle = aIdleAllow;
		requiredWeather = aWeather;
		hpBelow = aHpReq;
		actionChance = aChance;
		durationMin = aDurationMin;
		durationMax = aDurationMax;
		statEffect = aEffect;
		effectMin = aEffectMin;
		effectMax = aEffectMax;
		itemRequired = aRequired;
		itemPrevent = aPrevent;
		itemMinLevel = aMinLevel;
		image = aImage;
	}

	public string getRefId()
	{
		return smithActionRefId;
	}

	public string getText()
	{
		return smithActionText;
	}

	public SmithActionState getActionState()
	{
		return smithActionState;
	}

	public int getChance()
	{
		return actionChance;
	}

	public int getDuration()
	{
		return UnityEngine.Random.Range(durationMin, durationMax + 1);
	}

	public float getEffectValue()
	{
		float num = UnityEngine.Random.Range(effectMin, effectMin);
		decimal num2 = Math.Round((decimal)num, 1);
		return (float)num2;
	}

	public StatEffect getStatEffect()
	{
		return statEffect;
	}

	public bool checkHpBelow(float hpRatio)
	{
		if (hpBelow < hpRatio)
		{
			return false;
		}
		return true;
	}

	public bool checkWeatherAllow(string aWeather)
	{
		if (requiredWeather == string.Empty || requiredWeather == "-1" || aWeather == requiredWeather)
		{
			return true;
		}
		return false;
	}

	public bool checkWorkAllow()
	{
		return isAllowedWhenWorking;
	}

	public bool checkIdleAllow()
	{
		return isAllowedWhenIdle;
	}

	public string getRequiredItem()
	{
		return itemRequired;
	}

	public string getPreventItem()
	{
		return itemPrevent;
	}

	public int getItemMinLevel()
	{
		return itemMinLevel;
	}

	public string getImage()
	{
		return image;
	}

	public bool checkWorking()
	{
		if (smithActionRefId == "102")
		{
			return true;
		}
		return false;
	}
}
