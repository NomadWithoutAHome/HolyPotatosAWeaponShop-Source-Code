using System;

[Serializable]
public class Enemy
{
	private string enemyRefId;

	private string terrainRefId;

	private string enemyName;

	private string enemyImage;

	private int enemyGoldMin;

	public Enemy()
	{
		enemyRefId = string.Empty;
		terrainRefId = string.Empty;
		enemyName = string.Empty;
		enemyImage = string.Empty;
		enemyGoldMin = 0;
	}

	public Enemy(string aEnemyRefId, string aTerrainRefId, string aEnemyName, string aEnemyImage, int aEnemyGoldMin)
	{
		enemyRefId = aEnemyRefId;
		terrainRefId = aTerrainRefId;
		enemyName = aEnemyName;
		enemyImage = aEnemyImage;
		enemyGoldMin = aEnemyGoldMin;
	}

	public bool checkEncounter(string checkTerrain, int aGold)
	{
		if (checkTerrain == terrainRefId && aGold >= enemyGoldMin)
		{
			return true;
		}
		return false;
	}

	public string getTerrainRefId()
	{
		return terrainRefId;
	}

	public string getEnemyName()
	{
		return enemyName;
	}

	public string getEnemyImage()
	{
		return enemyImage;
	}
}
