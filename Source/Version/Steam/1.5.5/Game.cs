using UnityEngine;

public class Game : MonoBehaviour
{
	private GameData gameData;

	private Player player;

	public void resetPlayer()
	{
		player = new Player(string.Empty, string.Empty, string.Empty, string.Empty, "101", 0, 0, 0, 0);
	}

	public Player getPlayer()
	{
		if (player == null)
		{
			player = new Player(string.Empty, string.Empty, string.Empty, string.Empty, "101", 0, 0, 0, 0);
		}
		return player;
	}

	public void setPlayer(string aPlayerId, string aGameScenario, string aName, string aShopName, string aShopLevelRefId, int aAreaRegion, int aFame, int aTicket, int aUsedTicket, int aGold, int aLaw, int aChaos, int aUsedEmblems)
	{
		player = new Player(aPlayerId, aGameScenario, aName, aShopName, aShopLevelRefId, aGold, aLaw, aChaos, aUsedEmblems);
		player.setAreaRegion(aAreaRegion);
		player.setFame(aFame);
		player.setTickets(aTicket);
		player.setUsedTickets(aUsedTicket);
	}

	public GameData getGameData()
	{
		if (gameData == null)
		{
			gameData = new GameData();
		}
		return gameData;
	}

	private void processRefData()
	{
		JsonFileController component = GameObject.Find("JsonFileController").GetComponent<JsonFileController>();
		component.readRefContent("Resources/Data/");
	}

	public void clearRefData()
	{
		getGameData();
		gameData.clearForgeIncident();
		gameData.clearItem();
		gameData.clearHero();
		gameData.clearRecruitmentType();
		gameData.clearShopLevel();
		gameData.clearSmith();
		gameData.clearSmithAction();
		gameData.clearSmithJobClass();
		gameData.clearWeapon();
		gameData.clearWeaponType();
		gameData.clearWeather();
	}

	public void setGameData(GameData aValue)
	{
		gameData = aValue;
	}
}
