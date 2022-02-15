using System.Collections.Generic;
using Steamworks;
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

	private CGameID m_GameID;

	private bool m_bRequestedStats;

	private bool m_bStatsValid;

	private bool m_bStoreStats;

	protected Callback<UserStatsReceived_t> m_UserStatsReceived;

	protected Callback<UserStatsStored_t> m_UserStatsStored;

	protected Callback<UserAchievementStored_t> m_UserAchievementStored;

	protected Callback<DlcInstalled_t> m_DlcInstalled;

	private void OnEnable()
	{
		game = (game = GameObject.Find("Game").GetComponent<Game>());
		if (SteamController.Initialized)
		{
			m_GameID = new CGameID(SteamUtils.GetAppID());
			m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
			m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
			m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
			m_DlcInstalled = Callback<DlcInstalled_t>.Create(OnDlcInstalled);
			m_bRequestedStats = false;
			m_bStatsValid = false;
		}
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
			bool flag = true;
			flag = (m_bRequestedStats = SteamUserStats.RequestCurrentStats());
		}
		if (m_bStatsValid && m_bStoreStats)
		{
			bool flag2 = true;
			flag2 = SteamUserStats.StoreStats();
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
		SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());
		m_bStoreStats = true;
	}

	public void unlockAchievement(string achievementSteamID)
	{
		if (SteamController.Initialized && !game.getGameData().getAchievementBySteamID(achievementSteamID).getAchieved())
		{
			SteamUserStats.SetAchievement(achievementSteamID);
			game.getGameData().getAchievementBySteamID(achievementSteamID).setAchieved(aBool: true);
			m_bStoreStats = true;
		}
	}

	private void OnUserStatsReceived(UserStatsReceived_t pCallback)
	{
		CommonAPI.debug(string.Concat("gameID: ", m_GameID, " callback gameID: ", pCallback.m_nGameID));
		if (!SteamController.Initialized || (ulong)m_GameID != pCallback.m_nGameID)
		{
			return;
		}
		if (pCallback.m_eResult == EResult.k_EResultOK)
		{
			CommonAPI.debug("Received stats and achievements from Steam\n");
			m_bStatsValid = true;
			List<Achievement> achievementList = game.getGameData().getAchievementList();
			bool pbAchieved = false;
			{
				foreach (Achievement item in achievementList)
				{
					if (!SteamUserStats.GetAchievement(item.getSteamID(), out pbAchieved))
					{
						CommonAPI.debugError("SteamUserStats.GetAchievement failed for Achievement " + item.getSteamID() + "\nIs it registered in the Steam Partner site?");
					}
				}
				return;
			}
		}
		CommonAPI.debug("RequestStats - failed, " + pCallback.m_eResult);
	}

	private void OnUserStatsStored(UserStatsStored_t pCallback)
	{
		CommonAPI.debug(string.Concat("gameID: ", m_GameID, " callback gameID: ", pCallback.m_nGameID));
		if ((ulong)m_GameID == pCallback.m_nGameID)
		{
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				CommonAPI.debug("StoreStats - success");
			}
			else if (pCallback.m_eResult == EResult.k_EResultInvalidParam)
			{
				CommonAPI.debug("StoreStats - some failed to validate");
				UserStatsReceived_t pCallback2 = default(UserStatsReceived_t);
				pCallback2.m_eResult = EResult.k_EResultOK;
				pCallback2.m_nGameID = (ulong)m_GameID;
				OnUserStatsReceived(pCallback2);
			}
			else
			{
				CommonAPI.debug("StoreStats - failed, " + pCallback.m_eResult);
			}
		}
	}

	private void OnAchievementStored(UserAchievementStored_t pCallback)
	{
		CommonAPI.debug(string.Concat("gameID: ", m_GameID, " callback gameID: ", pCallback.m_nGameID));
		if ((ulong)m_GameID == pCallback.m_nGameID)
		{
			if (pCallback.m_nMaxProgress == 0)
			{
				CommonAPI.debug("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
				return;
			}
			CommonAPI.debug("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
		}
	}

	private void OnDlcInstalled(DlcInstalled_t pCallback)
	{
		CommonAPI.debug("dlc is installed");
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
			SteamUserStats.ResetAllStats(bAchievementsToo: true);
			SteamUserStats.RequestCurrentStats();
		}
		GUILayout.EndArea();
	}
}
