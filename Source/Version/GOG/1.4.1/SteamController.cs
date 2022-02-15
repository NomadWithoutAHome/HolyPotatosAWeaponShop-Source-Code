using System.Text;
using Steamworks;
using UnityEngine;

internal class SteamController : MonoBehaviour
{
	private GameData gameData;

	private static SteamController s_instance;

	private bool m_bInitialized;

	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	private static SteamController Instance => s_instance ?? new GameObject("SteamController").AddComponent<SteamController>();

	public static bool Initialized => Instance.m_bInitialized;

	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		CommonAPI.debugError(pchDebugText);
	}

	private void Awake()
	{
		gameData = GameObject.Find("Game").GetComponent<Game>().getGameData();
	}

	private void OnEnable()
	{
		if (s_instance == null)
		{
			s_instance = this;
		}
		if (m_bInitialized && m_SteamAPIWarningMessageHook != null)
		{
		}
	}

	private void OnDestroy()
	{
		if (!(s_instance != this))
		{
			s_instance = null;
			if (m_bInitialized)
			{
			}
		}
	}

	private void Update()
	{
		if (m_bInitialized)
		{
		}
	}
}
