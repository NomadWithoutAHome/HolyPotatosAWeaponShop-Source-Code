using System;

[Serializable]
public class HeroLevel
{
	private string heroLevelRefId;

	private int heroLevel;

	private int levelExp;

	private int levelUpFame;

	public HeroLevel()
	{
		heroLevelRefId = string.Empty;
		heroLevel = 0;
		levelExp = 0;
		levelUpFame = 0;
	}

	public HeroLevel(string aHeroLevelRefId, int aHeroLevel, int aLevelExp, int aLevelUpFame)
	{
		heroLevelRefId = aHeroLevelRefId;
		heroLevel = aHeroLevel;
		levelExp = aLevelExp;
		levelUpFame = aLevelUpFame;
	}

	public int getHeroLevel()
	{
		return heroLevel;
	}

	public int getLevelExp()
	{
		return levelExp;
	}

	public int getLevelUpFame()
	{
		return levelUpFame;
	}
}
