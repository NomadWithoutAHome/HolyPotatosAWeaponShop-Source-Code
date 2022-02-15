using System.Runtime.InteropServices;
using UnityEngine;

public class SCiOS
{
	[DllImport("__Internal")]
	private static extern void _InitSCiOS();

	[DllImport("__Internal")]
	private static extern bool _deviceCracked();

	[DllImport("__Internal")]
	private static extern bool _haveCydia();

	[DllImport("__Internal")]
	private static extern bool _haveIAPCracker();

	[DllImport("__Internal")]
	private static extern bool _haveIAPFree();

	[DllImport("__Internal")]
	private static extern bool _haveAdBlocker();

	[DllImport("__Internal")]
	private static extern bool _haveAppCake();

	[DllImport("__Internal")]
	private static extern bool _haveEvasion();

	public static void InitSCiOS()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_InitSCiOS();
		}
	}

	public static bool DeviceCracked()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _deviceCracked();
		}
		return false;
	}

	public static bool HaveCydia()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _haveCydia();
		}
		return false;
	}

	public static bool HaveIAPCracker()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _haveIAPCracker();
		}
		return false;
	}

	public static bool HaveIAPFree()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _haveIAPFree();
		}
		return false;
	}

	public static bool HaveAdBlocker()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _haveAdBlocker();
		}
		return false;
	}

	public static bool HaveAppCake()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _haveAppCake();
		}
		return false;
	}

	public static bool HaveEvasion()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _haveEvasion();
		}
		return false;
	}

	public static bool MaliciousDevice()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (DeviceCracked())
			{
				return true;
			}
			if (HaveCydia())
			{
				return true;
			}
			if (HaveEvasion())
			{
				return true;
			}
			if (HaveAdBlocker())
			{
				return true;
			}
			if (HaveAppCake())
			{
				return true;
			}
			if (HaveIAPCracker())
			{
				return true;
			}
			if (HaveIAPFree())
			{
				return true;
			}
		}
		return false;
	}
}
