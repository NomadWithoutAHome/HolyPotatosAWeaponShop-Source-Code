using System.Collections.Generic;
using UnityEngine;

internal class SteamStatsAndAchievements : MonoBehaviour
{
	public enum AchievementType
	{
		ACH_PLAY,
		ACH_STARTER
	}

	private class Achievement_t
	{
		public AchievementType m_eAchievementID;

		public string m_strName;

		public string m_strDescription;

		public bool m_bAchieved;

		public Achievement_t(AchievementType achievementID, string name, string desc)
		{
			m_eAchievementID = achievementID;
			m_strName = name;
			m_strDescription = desc;
			m_bAchieved = false;
		}
	}

	private Achievement_t[] m_Achievements = new Achievement_t[2]
	{
		new Achievement_t(AchievementType.ACH_PLAY, "Player", string.Empty),
		new Achievement_t(AchievementType.ACH_STARTER, "Starter", string.Empty)
	};

	private Game game;

	private ShopMenuController shopMenuController;

	private bool m_bRequestedStats;

	private bool m_bStatsValid;

	private bool m_bStoreStats;

	private void OnEnable()
	{
		game = (game = GameObject.Find("Game").GetComponent<Game>());
	}

	private void Update()
	{
		if (!SteamController.Initialized)
		{
			return;
		}
		if (!m_bRequestedStats)
		{
			if (!SteamController.Initialized)
			{
				m_bRequestedStats = true;
				return;
			}
			bool flag = (m_bRequestedStats = true);
		}
		if (m_bStatsValid && m_bStoreStats)
		{
			bool flag2 = true;
			m_bStoreStats = !flag2;
		}
	}

	public void requestStats()
	{
		m_bRequestedStats = false;
	}

	public bool checkAchievementStatus()
	{
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		GameData gameData = game.getGameData();
		List<Achievement> achievementList = gameData.getAchievementList();
		foreach (Achievement item in achievementList)
		{
			if (shopMenuController.checkUnlockCondition(item.getSuccessCondition(), item.getReqCount(), item.getCheckString(), item.getCheckNum(), 0))
			{
				unlockAchievement(item.getSteamID());
			}
		}
		return false;
	}

	private void unlockAchievement(Achievement_t achievement)
	{
		achievement.m_bAchieved = true;
		m_bStoreStats = true;
	}

	public void unlockAchievement(string achievementSteamID)
	{
		if (SteamController.Initialized && !game.getGameData().getAchievementBySteamID(achievementSteamID).getAchieved())
		{
			game.getGameData().getAchievementBySteamID(achievementSteamID).setAchieved(aBool: true);
			m_bStoreStats = true;
		}
	}

	public void Render()
	{
		if (!SteamController.Initialized)
		{
			GUILayout.Label("Steamworks not Initialized");
			return;
		}
		GUILayout.BeginArea(new Rect(Screen.width - 300, 0f, 300f, 800f));
		Achievement_t[] achievements = m_Achievements;
		foreach (Achievement_t achievement_t in achievements)
		{
			GUILayout.Label(achievement_t.m_eAchievementID.ToString());
			GUILayout.Label(achievement_t.m_strName + " - " + achievement_t.m_strDescription);
			GUILayout.Label("Achieved: " + achievement_t.m_bAchieved);
			GUILayout.Space(20f);
		}
		if (GUILayout.Button("RESET STATS AND ACHIEVEMENTS"))
		{
		}
		GUILayout.EndArea();
	}
}
