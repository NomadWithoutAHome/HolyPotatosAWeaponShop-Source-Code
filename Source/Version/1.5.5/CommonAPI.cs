using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Steamworks;
using UnityEngine;

public static class CommonAPI
{
	public static List<string> newsFeedTextOnGui;

	public static List<int> newsFeedTextOnGuiTime;

	public static bool showFeed;

	public static void setScreenResolution()
	{
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullscreen: true);
		Screen.sleepTimeout = -1;
		Application.targetFrameRate = 30;
	}

	public static int getScaleWidth(int aWidth)
	{
		return (int)((float)aWidth * Constants.SCREEN_SCALE);
	}

	public static int getScaleHeight(int aHeight)
	{
		return (int)((float)aHeight * Constants.SCREEN_SCALE);
	}

	public static GameData getGameData()
	{
		GameObject gameObject = GameObject.Find("Game");
		if (gameObject != null)
		{
			return gameObject.GetComponent<Game>().getGameData();
		}
		return new GameData();
	}

	public static void debug(object objectToDisplay)
	{
		if (objectToDisplay != null)
		{
			Debug.Log(objectToDisplay);
		}
	}

	public static void debugError(object objectToDisplay, UnityEngine.Object context = null)
	{
		if (objectToDisplay != null)
		{
			Debug.LogError(objectToDisplay, context);
		}
	}

	public static void debugMethod(string methodName, string value1)
	{
		Debug.Log($"Method: {methodName}, Value1: {value1}");
	}

	public static string getNewsFeedTextOnGui()
	{
		string text = string.Empty;
		if (newsFeedTextOnGui == null || newsFeedTextOnGuiTime == null)
		{
			newsFeedTextOnGui = new List<string>();
			newsFeedTextOnGuiTime = new List<int>();
		}
		if (!showFeed)
		{
			return text;
		}
		for (int i = 0; i < newsFeedTextOnGui.Count; i++)
		{
			if (newsFeedTextOnGuiTime[i] > 0)
			{
				text = text + newsFeedTextOnGui[i] + "\n";
			}
		}
		return text;
	}

	public static void setShowFeed(bool aShow)
	{
		showFeed = aShow;
	}

	public static void passTimeOnNewsFeed(int elapsed)
	{
		if (!showFeed)
		{
			return;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < newsFeedTextOnGui.Count; i++)
		{
			newsFeedTextOnGuiTime[i] -= elapsed;
			if (newsFeedTextOnGuiTime[i] <= 0)
			{
				list.Add(i);
			}
		}
		list.Reverse();
		foreach (int item in list)
		{
			newsFeedTextOnGui.RemoveAt(item);
			newsFeedTextOnGuiTime.RemoveAt(item);
		}
	}

	public static void showNewsFeedTextGUI(string text, int duration)
	{
	}

	public static void clearNewsFeedGUIItems()
	{
		newsFeedTextOnGui = new List<string>();
		newsFeedTextOnGuiTime = new List<int>();
	}

	public static bool checkReversePrefixFormat()
	{
		if (Constants.LANGUAGE == LanguageType.kLanguageTypeGermany || Constants.LANGUAGE == LanguageType.kLanguageTypeFrench || Constants.LANGUAGE == LanguageType.kLanguageTypeSpanish)
		{
			return true;
		}
		return false;
	}

	public static int parseInt(string aText)
	{
		return int.Parse(aText, CultureInfo.InvariantCulture);
	}

	public static float parseFloat(string aText)
	{
		return float.Parse(aText, CultureInfo.InvariantCulture);
	}

	public static int getTimeInSeconds(string timeFormat)
	{
		string[] array = timeFormat.Split(':');
		int num = parseInt(array[0]);
		int num2 = parseInt(array[1]);
		int num3 = parseInt(array[2]);
		return num * 3600 + num2 * 60 + num3;
	}

	public static string getTimeFormat(int timeSeconds)
	{
		int num = timeSeconds / 86400;
		int num2 = timeSeconds / 3600 - num * 24;
		int num3 = timeSeconds / 60 - num * 24 * 60 - num2 * 60;
		int num4 = timeSeconds - num * 24 * 3600 - num2 * 3600 - num3 * 60;
		string text = null;
		if (num > 0)
		{
			return num + "D " + num2 + ":" + num3 + ":" + num4;
		}
		if (num4 < 10)
		{
			return num3 + ":0" + num4;
		}
		return num3 + ":" + num4;
	}

	public static long convertStringToTimestamp(string aTime)
	{
		DateTime dateTime = DateTime.ParseExact(aTime, "yyyy-MM-dd hh:mm:ss", CultureInfo.CurrentCulture);
		return (long)(dateTime - new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc)).TotalSeconds;
	}

	public static int getTimeStamp()
	{
		return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc)).TotalSeconds;
	}

	public static string generateWhetsappDisplayDate(long messageTime, long playerTime)
	{
		GameData gameData = getGameData();
		List<int> list = convertHalfHoursToIntList(messageTime);
		List<int> list2 = convertHalfHoursToIntList(playerTime);
		string empty = string.Empty;
		if (list[0] == list2[0] && list[1] == list2[1] && list[2] == list2[2] && list[3] == list2[3])
		{
			return gameData.getTextByRefId("whetsappToday");
		}
		if (list[0] == list2[0] && list[1] == list2[1] && list[2] == list2[2] && list[3] == list2[3] - 1)
		{
			return gameData.getTextByRefId("whetsappYesterday");
		}
		return formatDateString(list);
	}

	public static long convertIntListToHalfHours(List<int> timeList)
	{
		long num = 0L;
		num += timeList[5];
		num += (long)timeList[4] * 2L;
		num += (long)timeList[3] * 24L * 2;
		num += (long)timeList[2] * 7L * 24 * 2;
		num += (long)timeList[1] * 4L * 7 * 24 * 2;
		return num + (long)timeList[0] * 4L * 4 * 7 * 24 * 2;
	}

	public static List<int> convertHalfHoursToPlayerTimeIntList(long halfHours)
	{
		List<int> list = convertHalfHoursToIntList(halfHours);
		list[0]++;
		list[1]++;
		list[2]++;
		list[3]++;
		list[5] *= 30;
		return list;
	}

	public static List<int> convertHalfHoursToIntList(long halfHours)
	{
		List<int> list = new List<int>();
		long num = halfHours;
		int item = (int)(num % 2);
		num /= 2;
		int item2 = (int)(num % 24);
		num /= 24;
		int item3 = (int)(num % 7);
		num /= 7;
		int item4 = (int)(num % 4);
		num /= 4;
		int item5 = (int)(num % 4);
		num /= 4;
		int item6 = (int)num;
		list.Add(item6);
		list.Add(item5);
		list.Add(item4);
		list.Add(item3);
		list.Add(item2);
		list.Add(item);
		return list;
	}

	public static string convertGameTimeToTimeLeftString(List<int> gameTime, bool showHalfHours = true)
	{
		GameData gameData = getGameData();
		string text = string.Empty;
		if (gameTime[0] > 0)
		{
			text = text + gameData.getTextByRefIdWithDynText("YEAR", "[year]", gameTime[0].ToString()) + " ";
		}
		if (gameTime[1] > 0)
		{
			text = text + gameData.getTextByRefIdWithDynText("MONTH", "[month]", gameTime[1].ToString()) + " ";
		}
		if (gameTime[3] > 0 || gameTime[2] > 0)
		{
			text = text + gameData.getTextByRefIdWithDynText("DATE", "[day]", (gameTime[3] + gameTime[2] * 7).ToString()) + " ";
		}
		if (gameTime[4] > 0 || gameTime[5] > 0)
		{
			float num = gameTime[4];
			num = ((!showHalfHours) ? (num + (float)gameTime[5] * 1f) : (num + (float)gameTime[5] * 0.5f));
			text = text + num + gameData.getTextByRefId("HOUR");
		}
		return text;
	}

	public static string convertHalfHoursToTimeString(long halfHours, bool showHalfHours = true)
	{
		string empty = string.Empty;
		return convertGameTimeToTimeLeftString(convertHalfHoursToIntList(halfHours), showHalfHours);
	}

	public static string formatDateString(List<int> gameTime)
	{
		GameData gameData = getGameData();
		string empty = string.Empty;
		empty = empty + gameData.getTextByRefIdWithDynText("YEAR", "[year]", (gameTime[0] + 1).ToString()) + " ";
		empty = empty + gameData.getTextByRefIdWithDynText("MONTH", "[month]", (gameTime[1] + 1).ToString()) + " ";
		return empty + gameData.getTextByRefIdWithDynText("DATE", "[day]", (gameTime[3] + gameTime[2] * 7 + 1).ToString()) + " ";
	}

	public static string formatDateDay(int day)
	{
		GameData gameData = getGameData();
		string empty = string.Empty;
		switch (Constants.LANGUAGE)
		{
		case LanguageType.kLanguageTypeEnglish:
			empty = day.ToString();
			switch (day % 10)
			{
			case 1:
				if (day % 100 == 11)
				{
					return empty + gameData.getTextByRefId("dateSuffix0");
				}
				return empty + gameData.getTextByRefId("dateSuffix1");
			case 2:
				if (day % 100 == 12)
				{
					return empty + gameData.getTextByRefId("dateSuffix0");
				}
				return empty + gameData.getTextByRefId("dateSuffix2");
			case 3:
				if (day % 100 == 13)
				{
					return empty + gameData.getTextByRefId("dateSuffix0");
				}
				return empty + gameData.getTextByRefId("dateSuffix3");
			default:
				return empty + gameData.getTextByRefId("dateSuffix0");
			}
		case LanguageType.kLanguageTypeRussia:
			empty = day.ToString();
			switch (day % 10)
			{
			case 1:
				if (day % 100 == 11)
				{
					return empty + gameData.getTextByRefId("dateSuffix0");
				}
				return empty + gameData.getTextByRefId("dateSuffix1");
			case 2:
				if (day % 100 == 12)
				{
					return empty + gameData.getTextByRefId("dateSuffix0");
				}
				return empty + gameData.getTextByRefId("dateSuffix2");
			case 3:
				if (day % 100 == 13)
				{
					return empty + gameData.getTextByRefId("dateSuffix0");
				}
				return empty + gameData.getTextByRefId("dateSuffix3");
			default:
				return empty + gameData.getTextByRefId("dateSuffix0");
			}
		default:
			return empty + gameData.getTextByRefIdWithDynText("dayLong", "[day]", day.ToString());
		}
	}

	public static string formatTextToWidth(string text, int lineChars)
	{
		string[] array = text.Split(' ');
		string text2 = string.Empty;
		int num = 0;
		string[] array2 = array;
		foreach (string text3 in array2)
		{
			if (num != 0)
			{
				text2 += " ";
				num++;
			}
			text2 += text3;
			num += text3.Length;
			if (num > lineChars)
			{
				text2 += "\n";
				num = 0;
			}
		}
		return text2;
	}

	public static string formatIntListString(List<int> list)
	{
		string text = string.Empty;
		foreach (int item in list)
		{
			text = text + item + " ";
		}
		return text;
	}

	public static string formatTimeString(float input, bool withMs)
	{
		string empty = string.Empty;
		int num = (int)(input / 60f);
		int num2 = (int)(input - (float)(num * 60));
		int num3 = (int)(100f * (input - (float)(num * 60) - (float)num2));
		if (num > 0)
		{
			empty += num;
			empty += ":";
			empty += ((num2 >= 10) ? num2.ToString() : ("0" + num2));
		}
		else
		{
			empty += num2;
		}
		if (withMs)
		{
			empty += ".";
			empty += ((num3 >= 10) ? num3.ToString() : ("0" + num3));
		}
		return empty;
	}

	public static string formatNumber(float aNumber)
	{
		if (aNumber == 0f)
		{
			return "0";
		}
		return aNumber.ToString("#,#");
	}

	public static string formatNumber(int aNumber)
	{
		if (aNumber == 0)
		{
			return "0";
		}
		return aNumber.ToString("#,#");
	}

	public static float convertRatioToPercent2DP(float aRatio)
	{
		return (float)(int)(aRatio * 10000f) / 100f;
	}

	public static string formatKeycodeName(string aName)
	{
		if (aName.Contains("Alpha"))
		{
			string[] array = Regex.Split(aName, "Alpha");
			return array[1];
		}
		if (aName.Contains("Keypad"))
		{
			string[] array2 = Regex.Split(aName, "Keypad");
			return "Numpad " + array2[1];
		}
		return aName;
	}

	public static List<int> getRandomIntList(int listCount, int selectCount)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		selectCount = Math.Min(listCount, selectCount);
		for (int i = 0; i < listCount; i++)
		{
			list2.Add(i);
		}
		for (int j = 0; j < list2.Count; j++)
		{
			int value = list2[j];
			int index = UnityEngine.Random.Range(j, list2.Count);
			list2[j] = list2[index];
			list2[index] = value;
		}
		for (int k = 0; k < selectCount; k++)
		{
			list.Add(list2[k]);
		}
		return list;
	}

	public static int getWeightedRandomIndex(List<int> weightList)
	{
		string text = string.Empty;
		foreach (int weight in weightList)
		{
			text = text + weight + " ";
		}
		text += "\n";
		int num = weightList.Sum();
		if (num > 0)
		{
			int num2 = UnityEngine.Random.Range(0, num);
			text = text + num2 + "\n";
			int num3 = 0;
			foreach (int weight2 in weightList)
			{
				num2 -= weight2;
				if (num2 < 0)
				{
					break;
				}
				num3++;
			}
			text = text + num3 + "\n";
			return num3;
		}
		return -1;
	}

	public static int getRandomInt(int aRange)
	{
		return UnityEngine.Random.Range(0, aRange);
	}

	public static int getRandomInt(int minInclusive, int maxInclusive)
	{
		return UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
	}

	public static string getRandomWeaponTypeForAward(ProjectAchievement award)
	{
		List<string> list = new List<string>();
		switch (award)
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			list.Add("103");
			list.Add("201");
			list.Add("202");
			list.Add("203");
			list.Add("302");
			list.Add("303");
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			list.Add("101");
			list.Add("205");
			list.Add("303");
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			list.Add("102");
			list.Add("202");
			list.Add("205");
			list.Add("301");
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			list.Add("104");
			list.Add("204");
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			list.Add("101");
			list.Add("102");
			list.Add("103");
			list.Add("104");
			list.Add("201");
			list.Add("202");
			list.Add("203");
			list.Add("204");
			list.Add("205");
			list.Add("301");
			list.Add("302");
			list.Add("303");
			break;
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	public static int getHeroMaxLevel(int tier)
	{
		return tier switch
		{
			1 => 10, 
			2 => 25, 
			3 => 50, 
			4 => 75, 
			5 => 100, 
			6 => 125, 
			7 => 150, 
			_ => 100, 
		};
	}

	public static int getHeroLevel(int points)
	{
		return getGameData().getHeroLevelByExp(points);
	}

	public static int getHeroLevelExp(int level)
	{
		return getGameData().getTotalExpByHeroLevel(level);
	}

	public static int getExploreMerchantMaxExp(int level)
	{
		return level switch
		{
			1 => 50, 
			2 => 150, 
			3 => 250, 
			4 => 375, 
			5 => 500, 
			6 => 675, 
			7 => 825, 
			8 => 1000, 
			9 => 1200, 
			10 => 1400, 
			11 => 1600, 
			12 => 1825, 
			13 => 2050, 
			14 => 2275, 
			_ => -1, 
		};
	}

	public static int getExploreLevel(int points)
	{
		if (points >= 2275)
		{
			return 15;
		}
		if (points >= 2050)
		{
			return 14;
		}
		if (points >= 1825)
		{
			return 13;
		}
		if (points >= 1600)
		{
			return 12;
		}
		if (points >= 1400)
		{
			return 11;
		}
		if (points >= 1200)
		{
			return 10;
		}
		if (points >= 1000)
		{
			return 9;
		}
		if (points >= 825)
		{
			return 8;
		}
		if (points >= 675)
		{
			return 7;
		}
		if (points >= 500)
		{
			return 6;
		}
		if (points >= 375)
		{
			return 5;
		}
		if (points >= 250)
		{
			return 4;
		}
		if (points >= 150)
		{
			return 3;
		}
		if (points >= 50)
		{
			return 2;
		}
		return 1;
	}

	public static int getMerchantLevel(int points)
	{
		if (points >= 2275)
		{
			return 15;
		}
		if (points >= 2050)
		{
			return 14;
		}
		if (points >= 1825)
		{
			return 13;
		}
		if (points >= 1600)
		{
			return 12;
		}
		if (points >= 1400)
		{
			return 11;
		}
		if (points >= 1200)
		{
			return 10;
		}
		if (points >= 1000)
		{
			return 9;
		}
		if (points >= 825)
		{
			return 8;
		}
		if (points >= 675)
		{
			return 7;
		}
		if (points >= 500)
		{
			return 6;
		}
		if (points >= 375)
		{
			return 5;
		}
		if (points >= 250)
		{
			return 4;
		}
		if (points >= 150)
		{
			return 3;
		}
		if (points >= 50)
		{
			return 2;
		}
		return 1;
	}

	public static int getSmithJobClassTier(string aRefId)
	{
		switch (aRefId)
		{
		case "10001":
		case "10002":
		case "10003":
		case "10004":
			return 1;
		case "10005":
		case "10006":
		case "10007":
			return 2;
		case "10008":
		case "10009":
			return 3;
		case "10010":
		case "10011":
			return 4;
		default:
			return 0;
		}
	}

	public static List<int> sortIndices(List<int> origList, bool isAscending)
	{
		List<int> list = new List<int>();
		list.AddRange(origList);
		int num = list.Count;
		List<int> list2 = new List<int>();
		for (int i = 0; i < num; i++)
		{
			list2.Add(i);
		}
		if (isAscending)
		{
			while (num > 0)
			{
				for (int j = 0; j < num - 1; j++)
				{
					if (list[j] > list[j + 1])
					{
						int value = list[j + 1];
						list[j + 1] = list[j];
						list[j] = value;
						int value2 = list2[j + 1];
						list2[j + 1] = list2[j];
						list2[j] = value2;
					}
				}
				num--;
			}
		}
		else
		{
			while (num > 0)
			{
				for (int k = 0; k < num - 1; k++)
				{
					if (list[k] < list[k + 1])
					{
						int value3 = list[k + 1];
						list[k + 1] = list[k];
						list[k] = value3;
						int value4 = list2[k + 1];
						list2[k + 1] = list2[k];
						list2[k] = value4;
					}
				}
				num--;
			}
		}
		return list2;
	}

	public static string convertShopStarchRecordTypeToString(RecordType aType)
	{
		string result = string.Empty;
		GameData gameData = getGameData();
		switch (aType)
		{
		case RecordType.RecordTypeEarningStartingCapital:
			result = gameData.getTextByRefId("recordTypeName19");
			break;
		case RecordType.RecordTypeEarningWeapon:
			result = gameData.getTextByRefId("recordTypeName01");
			break;
		case RecordType.RecordTypeEarningContract:
			result = gameData.getTextByRefId("recordTypeName02");
			break;
		case RecordType.RecordTypeEarningRequest:
			result = gameData.getTextByRefId("recordTypeName03");
			break;
		case RecordType.RecordTypeEarningLegendary:
			result = gameData.getTextByRefId("recordTypeName04");
			break;
		case RecordType.RecordTypeEarningBuyDiscount:
			result = gameData.getTextByRefId("recordTypeName06");
			break;
		case RecordType.RecordTypeEarningMisc:
			result = gameData.getTextByRefId("recordTypeName07");
			break;
		case RecordType.RecordTypeEarningLoan:
			result = gameData.getTextByRefId("recordTypeName05");
			break;
		case RecordType.RecordTypeExpenseSalary:
			result = gameData.getTextByRefId("recordTypeName16");
			break;
		case RecordType.RecordTypeExpenseOutsource:
			result = gameData.getTextByRefId("recordTypeName08");
			break;
		case RecordType.RecordTypeExpenseBuyItem:
			result = gameData.getTextByRefId("recordTypeName09");
			break;
		case RecordType.RecordTypeExpenseTraining:
			result = gameData.getTextByRefId("recordTypeName11");
			break;
		case RecordType.RecordTypeExpenseVacation:
			result = gameData.getTextByRefId("recordTypeName12");
			break;
		case RecordType.RecordTypeExpenseResearch:
			result = gameData.getTextByRefId("recordTypeName13");
			break;
		case RecordType.RecordTypeExpenseJobChange:
			result = gameData.getTextByRefId("recordTypeName14");
			break;
		case RecordType.RecordTypeExpenseRecruitHire:
			result = gameData.getTextByRefId("recordTypeName15");
			break;
		case RecordType.RecordTypeExpenseShopUpgrades:
			result = gameData.getTextByRefId("recordTypeName10");
			break;
		case RecordType.RecordTypeExpenseMisc:
			result = gameData.getTextByRefId("recordTypeName17");
			break;
		case RecordType.RecordTypeSpecial:
			result = gameData.getTextByRefId("recordTypeName18");
			break;
		}
		return result;
	}

	public static RecordType convertDataStringToShopStarchRecordType(string aTypeString)
	{
		RecordType result = RecordType.RecordTypeBlank;
		GameData gameData = getGameData();
		switch (aTypeString)
		{
		case "RecordTypeEarningStartingCapital":
			result = RecordType.RecordTypeEarningStartingCapital;
			break;
		case "RecordTypeEarningWeapon":
			result = RecordType.RecordTypeEarningWeapon;
			break;
		case "RecordTypeEarningContract":
			result = RecordType.RecordTypeEarningContract;
			break;
		case "RecordTypeEarningRequest":
			result = RecordType.RecordTypeEarningRequest;
			break;
		case "RecordTypeEarningLegendary":
			result = RecordType.RecordTypeEarningLegendary;
			break;
		case "RecordTypeEarningBuyDiscount":
			result = RecordType.RecordTypeEarningBuyDiscount;
			break;
		case "RecordTypeEarningMisc":
			result = RecordType.RecordTypeEarningMisc;
			break;
		case "RecordTypeEarningLoan":
			result = RecordType.RecordTypeEarningLoan;
			break;
		case "RecordTypeExpenseSalary":
			result = RecordType.RecordTypeExpenseSalary;
			break;
		case "RecordTypeExpenseOutsource":
			result = RecordType.RecordTypeExpenseOutsource;
			break;
		case "RecordTypeExpenseBuyItem":
			result = RecordType.RecordTypeExpenseBuyItem;
			break;
		case "RecordTypeExpenseTraining":
			result = RecordType.RecordTypeExpenseTraining;
			break;
		case "RecordTypeExpenseVacation":
			result = RecordType.RecordTypeExpenseVacation;
			break;
		case "RecordTypeExpenseResearch":
			result = RecordType.RecordTypeExpenseResearch;
			break;
		case "RecordTypeExpenseJobChange":
			result = RecordType.RecordTypeExpenseJobChange;
			break;
		case "RecordTypeExpenseRecruitHire":
			result = RecordType.RecordTypeExpenseRecruitHire;
			break;
		case "RecordTypeExpenseShopUpgrades":
			result = RecordType.RecordTypeExpenseShopUpgrades;
			break;
		case "RecordTypeExpenseMisc":
			result = RecordType.RecordTypeExpenseMisc;
			break;
		case "RecordTypeSpecial":
			result = RecordType.RecordTypeSpecial;
			break;
		}
		return result;
	}

	public static string convertSmithExploreStateToDisplayString(SmithExploreState aState)
	{
		string result = string.Empty;
		GameData gameData = getGameData();
		switch (aState)
		{
		case SmithExploreState.SmithExploreStateTravelToExplore:
			result = gameData.getRandomTextBySetRefId("travelingText");
			break;
		case SmithExploreState.SmithExploreStateExplore:
			result = gameData.getRandomTextBySetRefId("exploringText");
			break;
		case SmithExploreState.SmithExploreStateExploreTravelHome:
			result = gameData.getRandomTextBySetRefId("returningText");
			break;
		case SmithExploreState.SmithExploreStateExploreReturned:
			result = gameData.getRandomTextBySetRefId("returnedText");
			break;
		case SmithExploreState.SmithExploreStateTravelToBuyMaterial:
			result = gameData.getRandomTextBySetRefId("travelingText");
			break;
		case SmithExploreState.SmithExploreStateBuyMaterial:
			result = gameData.getRandomTextBySetRefId("buyingText");
			break;
		case SmithExploreState.SmithExploreStateBuyMaterialTravelHome:
			result = gameData.getRandomTextBySetRefId("returningText");
			break;
		case SmithExploreState.SmithExploreStateBuyMaterialReturned:
			result = gameData.getRandomTextBySetRefId("returnedText");
			break;
		case SmithExploreState.SmithExploreStateTravelToSellWeapon:
			result = gameData.getRandomTextBySetRefId("travelingText");
			break;
		case SmithExploreState.SmithExploreStateSellWeapon:
			result = gameData.getRandomTextBySetRefId("sellingText");
			break;
		case SmithExploreState.SmithExploreStateOffersWaiting:
			result = gameData.getRandomTextBySetRefId("waitingText");
			break;
		case SmithExploreState.SmithExploreStateSellWeaponTravelHome:
			result = gameData.getRandomTextBySetRefId("returningText");
			break;
		case SmithExploreState.SmithExploreStateSellWeaponReturned:
			result = gameData.getRandomTextBySetRefId("returnedText");
			break;
		case SmithExploreState.SmithExploreStateTravelToVacation:
			result = gameData.getRandomTextBySetRefId("travelingText");
			break;
		case SmithExploreState.SmithExploreStateVacation:
			result = gameData.getRandomTextBySetRefId("vacationText");
			break;
		case SmithExploreState.SmithExploreStateVacationTravelHome:
			result = gameData.getRandomTextBySetRefId("returningText");
			break;
		case SmithExploreState.SmithExploreStateVacationReturned:
			result = gameData.getRandomTextBySetRefId("returnedText");
			break;
		case SmithExploreState.SmithExploreStateTravelToTraining:
			result = gameData.getRandomTextBySetRefId("travelingText");
			break;
		case SmithExploreState.SmithExploreStateTraining:
			result = gameData.getRandomTextBySetRefId("trainingText");
			break;
		case SmithExploreState.SmithExploreStateTrainingTravelHome:
			result = gameData.getRandomTextBySetRefId("returningText");
			break;
		case SmithExploreState.SmithExploreStateTrainingReturned:
			result = gameData.getRandomTextBySetRefId("returnedText");
			break;
		}
		return result;
	}

	public static Season getSeasonByMonth(int monthIndex)
	{
		Season result = Season.SeasonBlank;
		switch (monthIndex)
		{
		case 0:
			result = Season.SeasonSpring;
			break;
		case 1:
			result = Season.SeasonSummer;
			break;
		case 2:
			result = Season.SeasonAutumn;
			break;
		case 3:
			result = Season.SeasonWinter;
			break;
		}
		return result;
	}

	public static string convertSeasonToString(Season season)
	{
		string result = string.Empty;
		switch (season)
		{
		case Season.SeasonSpring:
			result = getGameData().getTextByRefId("SPRING");
			break;
		case Season.SeasonSummer:
			result = getGameData().getTextByRefId("SUMMER");
			break;
		case Season.SeasonAutumn:
			result = getGameData().getTextByRefId("AUTUMN");
			break;
		case Season.SeasonWinter:
			result = getGameData().getTextByRefId("WINTER");
			break;
		}
		return result;
	}

	public static string getSeasonDesc(Season season)
	{
		string result = string.Empty;
		switch (season)
		{
		case Season.SeasonSpring:
			result = getGameData().getTextByRefId("seasonEvent02");
			break;
		case Season.SeasonSummer:
			result = getGameData().getTextByRefId("seasonEvent04");
			break;
		case Season.SeasonAutumn:
			result = getGameData().getTextByRefId("seasonEvent06");
			break;
		case Season.SeasonWinter:
			result = getGameData().getTextByRefId("seasonEvent08");
			break;
		}
		return result;
	}

	public static string getSeasonIconName(Season season)
	{
		string result = string.Empty;
		switch (season)
		{
		case Season.SeasonSpring:
			result = "spring";
			break;
		case Season.SeasonSummer:
			result = "summer";
			break;
		case Season.SeasonAutumn:
			result = "autumn";
			break;
		case Season.SeasonWinter:
			result = "winter";
			break;
		}
		return result;
	}

	public static string getSeasonBGM(Season season)
	{
		string result = string.Empty;
		switch (season)
		{
		case Season.SeasonSpring:
			result = "season_spring";
			break;
		case Season.SeasonSummer:
			result = "season_summer";
			break;
		case Season.SeasonAutumn:
			result = "season_autumn";
			break;
		case Season.SeasonWinter:
			result = "season_winter";
			break;
		}
		return result;
	}

	public static AreaType convertStringToAreaType(string typeString)
	{
		AreaType result = AreaType.AreaTypeBlank;
		switch (typeString)
		{
		case "HOME":
			result = AreaType.AreaTypeHome;
			break;
		case "WILD":
			result = AreaType.AreaTypeWild;
			break;
		case "TOWN":
			result = AreaType.AreaTypeTown;
			break;
		}
		return result;
	}

	public static ProjectTagType convertStringToProjectTagType(string typeString)
	{
		ProjectTagType result = ProjectTagType.ProjectTagTypeBlank;
		switch (typeString)
		{
		case "ProjectTagTypeItem":
			result = ProjectTagType.ProjectTagTypeItem;
			break;
		case "ProjectTagTypeJobClass":
			result = ProjectTagType.ProjectTagTypeJobClass;
			break;
		case "ProjectTagTypeWeapon":
			result = ProjectTagType.ProjectTagTypeWeapon;
			break;
		case "ProjectTagTypeSmith":
			result = ProjectTagType.ProjectTagTypeSmith;
			break;
		}
		return result;
	}

	public static ProjectAchievement convertStringToProjectAchievement(string typeString)
	{
		ProjectAchievement result = ProjectAchievement.ProjectAchievementBlank;
		switch (typeString)
		{
		case "ProjectAchievementGoldenHammerOverall":
			result = ProjectAchievement.ProjectAchievementGoldenHammerOverall;
			break;
		case "ProjectAchievementGoldenHammerAttack":
			result = ProjectAchievement.ProjectAchievementGoldenHammerAttack;
			break;
		case "ProjectAchievementGoldenHammerSpeed":
			result = ProjectAchievement.ProjectAchievementGoldenHammerSpeed;
			break;
		case "ProjectAchievementGoldenHammerAccuracy":
			result = ProjectAchievement.ProjectAchievementGoldenHammerAccuracy;
			break;
		case "ProjectAchievementGoldenHammerMagic":
			result = ProjectAchievement.ProjectAchievementGoldenHammerMagic;
			break;
		}
		return result;
	}

	public static Season convertStringToSeason(string seasonString)
	{
		Season result = Season.SeasonBlank;
		switch (seasonString)
		{
		case "SPRING":
			result = Season.SeasonSpring;
			break;
		case "SUMMER":
			result = Season.SeasonSummer;
			break;
		case "AUTUMN":
			result = Season.SeasonAutumn;
			break;
		case "WINTER":
			result = Season.SeasonWinter;
			break;
		}
		return result;
	}

	public static SmithStat convertStringToSmithStat(string statString)
	{
		SmithStat result = SmithStat.SmithStatBlank;
		switch (statString)
		{
		case "POWER":
			result = SmithStat.SmithStatPower;
			break;
		case "POW":
			result = SmithStat.SmithStatPower;
			break;
		case "INTELLIGENCE":
			result = SmithStat.SmithStatIntelligence;
			break;
		case "INT":
			result = SmithStat.SmithStatIntelligence;
			break;
		case "TECHNIQUE":
			result = SmithStat.SmithStatTechnique;
			break;
		case "TEC":
			result = SmithStat.SmithStatTechnique;
			break;
		case "LUCK":
			result = SmithStat.SmithStatLuck;
			break;
		case "LUC":
			result = SmithStat.SmithStatLuck;
			break;
		case "STAMINA":
			result = SmithStat.SmithStatStamina;
			break;
		case "STA":
			result = SmithStat.SmithStatStamina;
			break;
		case "ALL":
			result = SmithStat.SmithStatAll;
			break;
		}
		return result;
	}

	public static WeekendActivityType convertStringToWeekendActivity(string weekendString)
	{
		WeekendActivityType result = WeekendActivityType.WeekendActivityTypeBlank;
		switch (weekendString)
		{
		case "NORMAL":
			result = WeekendActivityType.WeekendActivityTypeNormal;
			break;
		case "DOG":
			result = WeekendActivityType.WeekendActivityTypeDog;
			break;
		case "ADVENTURE":
			result = WeekendActivityType.WeekendActivityTypeAdventure;
			break;
		}
		return result;
	}

	public static ItemType convertStringToItemType(string itemTypeString)
	{
		ItemType result = ItemType.ItemTypeBlank;
		switch (itemTypeString)
		{
		case "BOOST":
			result = ItemType.ItemTypeBoost;
			break;
		case "ENCHANTMENT":
			result = ItemType.ItemTypeEnhancement;
			break;
		case "MATERIAL":
			result = ItemType.ItemTypeMaterial;
			break;
		case "RELIC":
			result = ItemType.ItemTypeRelic;
			break;
		case "MEMENTO":
			result = ItemType.ItemTypeMemento;
			break;
		case "SPECIAL":
			result = ItemType.ItemTypeSpecial;
			break;
		}
		return result;
	}

	public static string getItemTypeName(ItemType aType)
	{
		return aType switch
		{
			ItemType.ItemTypeMaterial => getGameData().getTextByRefId("material"), 
			ItemType.ItemTypeEnhancement => getGameData().getTextByRefId("enchantment"), 
			ItemType.ItemTypeRelic => getGameData().getTextByRefId("relic"), 
			_ => string.Empty, 
		};
	}

	public static ForgeSeasonalEffect convertDataStringToForgeSeasonalEffect(string effectString)
	{
		ForgeSeasonalEffect result = ForgeSeasonalEffect.ForgeSeasonalEffectNothing;
		switch (effectString)
		{
		case "ForgeSeasonalEffectSpringHayFever":
			result = ForgeSeasonalEffect.ForgeSeasonalEffectSpringHayFever;
			break;
		case "ForgeSeasonalEffectSummerHeatwave":
			result = ForgeSeasonalEffect.ForgeSeasonalEffectSummerHeatwave;
			break;
		case "ForgeSeasonalEffectAutumnFlu":
			result = ForgeSeasonalEffect.ForgeSeasonalEffectAutumnFlu;
			break;
		case "ForgeSeasonalEffectWinterChill":
			result = ForgeSeasonalEffect.ForgeSeasonalEffectWinterChill;
			break;
		}
		return result;
	}

	public static TutorialState convertDataStringToTutorialState(string stateString)
	{
		TutorialState result = TutorialState.TutorialStateBlank;
		switch (stateString)
		{
		case "TutorialStateStart":
			result = TutorialState.TutorialStateStart;
			break;
		case "TutorialStateBeforeFirstForging":
			result = TutorialState.TutorialStateBeforeFirstForging;
			break;
		case "TutorialStateAfterFirstForging":
			result = TutorialState.TutorialStateAfterFirstForging;
			break;
		case "TutorialStateBeforeSellWeapon":
			result = TutorialState.TutorialStateBeforeSellWeapon;
			break;
		case "TutorialStateAfterSellWeapon":
			result = TutorialState.TutorialStateAfterSellWeapon;
			break;
		case "TutorialStateEnd":
			result = TutorialState.TutorialStateEnd;
			break;
		}
		return result;
	}

	public static ActivityType convertDataStringToActivityType(string typeString)
	{
		ActivityType result = ActivityType.ActivityTypeBlank;
		switch (typeString)
		{
		case "ActivityTypeExplore":
			result = ActivityType.ActivityTypeExplore;
			break;
		case "ActivityTypeBuyMats":
			result = ActivityType.ActivityTypeBuyMats;
			break;
		case "ActivityTypeSellWeapon":
			result = ActivityType.ActivityTypeSellWeapon;
			break;
		case "ActivityTypeResearch":
			result = ActivityType.ActivityTypeResearch;
			break;
		case "ActivityTypeVacation":
			result = ActivityType.ActivityTypeVacation;
			break;
		case "ActivityTypeTraining":
			result = ActivityType.ActivityTypeTraining;
			break;
		case "ActivityTypeUnlock":
			result = ActivityType.ActivityTypeUnlock;
			break;
		}
		return result;
	}

	public static ActivityState convertDataStringToActivityState(string stateString)
	{
		ActivityState result = ActivityState.ActivityStateBlank;
		switch (stateString)
		{
		case "ActivityStateComingBack":
			result = ActivityState.ActivityStateComingBack;
			break;
		case "ActivityStateGoing":
			result = ActivityState.ActivityStateGoing;
			break;
		}
		return result;
	}

	public static RequestState convertDataStringToRequestState(string stateString)
	{
		RequestState result = RequestState.RequestStateBlank;
		switch (stateString)
		{
		case "RequestStateAccepted":
			result = RequestState.RequestStateAccepted;
			break;
		case "RequestStateCancelled":
			result = RequestState.RequestStateCancelled;
			break;
		case "RequestStateCompleted":
			result = RequestState.RequestStateCompleted;
			break;
		case "RequestStateExpired":
			result = RequestState.RequestStateExpired;
			break;
		case "RequestStatePending":
			result = RequestState.RequestStatePending;
			break;
		case "RequestStateRejected":
			result = RequestState.RequestStateRejected;
			break;
		}
		return result;
	}

	public static QuestNEWType convertDataStringToQuestNEWType(string typeString)
	{
		QuestNEWType result = QuestNEWType.QuestNEWTypeBlank;
		switch (typeString)
		{
		case "BLUEPRINT":
			result = QuestNEWType.QuestNEWTypeBlueprint;
			break;
		case "CHALLENGE":
			result = QuestNEWType.QuestNEWTypeChallenge;
			break;
		case "CONTRACT":
			result = QuestNEWType.QuestNEWTypeContract;
			break;
		case "NORMAL":
			result = QuestNEWType.QuestNEWTypeNormal;
			break;
		case "RESEARCH":
			result = QuestNEWType.QuestNEWTypeResearch;
			break;
		case "SEASON":
			result = QuestNEWType.QuestNEWTypeSeason;
			break;
		}
		return result;
	}

	public static ProjectType convertDataStringToProjectType(string typeString)
	{
		ProjectType result = ProjectType.ProjectTypeNothing;
		switch (typeString)
		{
		case "ProjectTypeContract":
			result = ProjectType.ProjectTypeContract;
			break;
		case "ProjectTypeUnique":
			result = ProjectType.ProjectTypeUnique;
			break;
		case "ProjectTypeWeapon":
			result = ProjectType.ProjectTypeWeapon;
			break;
		}
		return result;
	}

	public static ProjectState convertDataStringToProjectState(string stateString)
	{
		ProjectState result = ProjectState.ProjectStateBlank;
		switch (stateString)
		{
		case "ProjectStateAbandoned":
			result = ProjectState.ProjectStateAbandoned;
			break;
		case "ProjectStateCompleted":
			result = ProjectState.ProjectStateCompleted;
			break;
		case "ProjectStateCurrent":
			result = ProjectState.ProjectStateCurrent;
			break;
		case "ProjectStateResult":
			result = ProjectState.ProjectStateResult;
			break;
		case "ProjectStateTimedOut":
			result = ProjectState.ProjectStateTimedOut;
			break;
		}
		return result;
	}

	public static ProjectSaleState convertDataStringToProjectSellState(string stateString)
	{
		ProjectSaleState result = ProjectSaleState.ProjectSaleStateBlank;
		switch (stateString)
		{
		case "ProjectSaleStateDelivered":
			result = ProjectSaleState.ProjectSaleStateDelivered;
			break;
		case "ProjectSaleStateInStock":
			result = ProjectSaleState.ProjectSaleStateInStock;
			break;
		case "ProjectSaleStateRejected":
			result = ProjectSaleState.ProjectSaleStateRejected;
			break;
		case "ProjectSaleStateSelling":
			result = ProjectSaleState.ProjectSaleStateSelling;
			break;
		case "ProjectSaleStateSold":
			result = ProjectSaleState.ProjectSaleStateSold;
			break;
		}
		return result;
	}

	public static WhetsappFilterType convertDataStringToWhetsappFilterType(string typeString)
	{
		WhetsappFilterType result = WhetsappFilterType.WhetsappFilterTypeBlank;
		switch (typeString)
		{
		case "WhetsappFilterTypeAll":
			result = WhetsappFilterType.WhetsappFilterTypeAll;
			break;
		case "WhetsappFilterTypeHero":
			result = WhetsappFilterType.WhetsappFilterTypeHero;
			break;
		case "WhetsappFilterTypeNotice":
			result = WhetsappFilterType.WhetsappFilterTypeNotice;
			break;
		case "WhetsappFilterTypeSmith":
			result = WhetsappFilterType.WhetsappFilterTypeSmith;
			break;
		}
		return result;
	}

	public static ProjectPhase convertDataStringToProjectPhase(string phaseString)
	{
		ProjectPhase result = ProjectPhase.ProjectPhaseBlank;
		switch (phaseString)
		{
		case "ProjectPhaseStart":
			result = ProjectPhase.ProjectPhaseStart;
			break;
		case "ProjectPhaseDesignDone":
			result = ProjectPhase.ProjectPhaseDesignDone;
			break;
		case "ProjectPhaseCraftDone":
			result = ProjectPhase.ProjectPhaseCraftDone;
			break;
		case "ProjectPhasePolishDone":
			result = ProjectPhase.ProjectPhasePolishDone;
			break;
		case "ProjectPhaseEnchantDone":
			result = ProjectPhase.ProjectPhaseEnchantDone;
			break;
		}
		return result;
	}

	public static TimedAction convertDataStringToTimedAction(string actionString)
	{
		TimedAction result = TimedAction.TimedActionBlank;
		switch (actionString)
		{
		case "TimedActionForgeIncident":
			result = TimedAction.TimedActionForgeIncident;
			break;
		case "TimedActionRecruit":
			result = TimedAction.TimedActionRecruit;
			break;
		case "TimedActionDogAction":
			result = TimedAction.TimedActionDogAction;
			break;
		case "TimedActionRandomScenario":
			result = TimedAction.TimedActionRandomScenario;
			break;
		}
		return result;
	}

	public static string convertWeaponStatToString(WeaponStat stat)
	{
		string result = string.Empty;
		switch (stat)
		{
		case WeaponStat.WeaponStatAttack:
			result = getGameData().getTextByRefId("weaponStats06");
			break;
		case WeaponStat.WeaponStatSpeed:
			result = getGameData().getTextByRefId("weaponStats07");
			break;
		case WeaponStat.WeaponStatAccuracy:
			result = getGameData().getTextByRefId("weaponStats08");
			break;
		case WeaponStat.WeaponStatMagic:
			result = getGameData().getTextByRefId("weaponStats09");
			break;
		case WeaponStat.WeaponStatElement:
			result = getGameData().getTextByRefId("weaponStats10");
			break;
		}
		return result;
	}

	public static string convertWeaponStatToDataString(WeaponStat stat)
	{
		string result = string.Empty;
		switch (stat)
		{
		case WeaponStat.WeaponStatAttack:
			result = "ATTACK";
			break;
		case WeaponStat.WeaponStatSpeed:
			result = "SPEED";
			break;
		case WeaponStat.WeaponStatAccuracy:
			result = "ACCURACY";
			break;
		case WeaponStat.WeaponStatMagic:
			result = "MAGIC";
			break;
		}
		return result;
	}

	public static WeaponStat convertStringToWeaponStat(string statString)
	{
		WeaponStat result = WeaponStat.WeaponStatNone;
		switch (statString)
		{
		case "DESIGN":
			result = WeaponStat.WeaponStatAttack;
			break;
		case "ATTACK":
			result = WeaponStat.WeaponStatAttack;
			break;
		case "ATK":
			result = WeaponStat.WeaponStatAttack;
			break;
		case "CRAFT":
			result = WeaponStat.WeaponStatSpeed;
			break;
		case "SPEED":
			result = WeaponStat.WeaponStatSpeed;
			break;
		case "SPD":
			result = WeaponStat.WeaponStatSpeed;
			break;
		case "POLISH":
			result = WeaponStat.WeaponStatAccuracy;
			break;
		case "ACCURACY":
			result = WeaponStat.WeaponStatAccuracy;
			break;
		case "ACC":
			result = WeaponStat.WeaponStatAccuracy;
			break;
		case "ENCHANT":
			result = WeaponStat.WeaponStatAccuracy;
			break;
		case "MAGIC":
			result = WeaponStat.WeaponStatMagic;
			break;
		case "MAG":
			result = WeaponStat.WeaponStatMagic;
			break;
		case "ELEMENT":
			result = WeaponStat.WeaponStatElement;
			break;
		case "ELEM":
			result = WeaponStat.WeaponStatElement;
			break;
		}
		return result;
	}

	public static Element convertStringToElement(string elemString)
	{
		Element result = Element.ElementNone;
		switch (elemString)
		{
		case "FIRE":
			result = Element.ElementFire;
			break;
		case "WATER":
			result = Element.ElementWater;
			break;
		case "EARTH":
			result = Element.ElementEarth;
			break;
		case "WIND":
			result = Element.ElementWind;
			break;
		}
		return result;
	}

	public static IncidentType convertStringToIncidentType(string incidentString)
	{
		IncidentType result = IncidentType.IncidentTypeNothing;
		switch (incidentString)
		{
		case "GOLD":
			result = IncidentType.IncidentTypeGold;
			break;
		case "BUFF":
			result = IncidentType.IncidentTypeBuff;
			break;
		case "DEBUFF":
			result = IncidentType.IncidentTypeDebuff;
			break;
		case "BOOST":
			result = IncidentType.IncidentTypeBoost;
			break;
		}
		return result;
	}

	public static SubquestType convertStringToSubquestType(string subquestString)
	{
		SubquestType result = SubquestType.SubquestTypeBlank;
		switch (subquestString)
		{
		case "ELEMENT":
			result = SubquestType.SubquestTypeElement;
			break;
		case "STAT":
			result = SubquestType.SubquestTypeStat;
			break;
		case "WEAPON":
			result = SubquestType.SubquestTypeWeapon;
			break;
		case "ENDING":
			result = SubquestType.SubquestTypeEnding;
			break;
		}
		return result;
	}

	public static SmithActionState convertStringToSmithActionState(string smithActionString)
	{
		SmithActionState result = SmithActionState.SmithActionStateDefault;
		switch (smithActionString)
		{
		case "DEFAULT":
			result = SmithActionState.SmithActionStateDefault;
			break;
		case "AWAY":
			result = SmithActionState.SmithActionStateAway;
			break;
		case "STANDBY":
			result = SmithActionState.SmithActionStateStandby;
			break;
		case "DISTRACTION":
			result = SmithActionState.SmithActionStateDistraction;
			break;
		}
		return result;
	}

	public static QuestType convertStringToQuestType(string questTypeString)
	{
		QuestType result = QuestType.QuestTypeBlank;
		switch (questTypeString)
		{
		case "INSTANT":
			result = QuestType.QuestTypeInstant;
			break;
		case "NORMAL":
			result = QuestType.QuestTypeNormal;
			break;
		}
		return result;
	}

	public static SmithGender convertStringToGender(string genderString)
	{
		SmithGender result = SmithGender.SmithGenderNil;
		switch (genderString)
		{
		case "FEMALE":
			result = SmithGender.SmithGenderFemale;
			break;
		case "MALE":
			result = SmithGender.SmithGenderMale;
			break;
		}
		return result;
	}

	public static ScenarioCondition convertStringToScenarioCondition(string conditionString)
	{
		ScenarioCondition result = ScenarioCondition.ScenarioConditionNothing;
		switch (conditionString)
		{
		case "HAS_PROJECT":
			result = ScenarioCondition.ScenarioConditionHasProject;
			break;
		case "SMITH_NOT_UNLOCKED":
			result = ScenarioCondition.ScenarioConditionSmithNotUnlocked;
			break;
		}
		return result;
	}

	public static ScenarioEffect convertStringToScenarioEffect(string scenarioEffectString)
	{
		ScenarioEffect result = ScenarioEffect.ScenarioEffectNothing;
		switch (scenarioEffectString)
		{
		case "GOLD":
			result = ScenarioEffect.ScenarioEffectGold;
			break;
		case "UNLOCK_SMITH":
			result = ScenarioEffect.ScenarioEffectUnlockSmith;
			break;
		case "WEAPON_ATK_ABS":
			result = ScenarioEffect.ScenarioEffectAtkAbs;
			break;
		case "WEAPON_SPD_ABS":
			result = ScenarioEffect.ScenarioEffectSpdAbs;
			break;
		case "WEAPON_ACC_ABS":
			result = ScenarioEffect.ScenarioEffectAccAbs;
			break;
		case "WEAPON_MAG_ABS":
			result = ScenarioEffect.ScenarioEffectMagAbs;
			break;
		case "WEAPON_STATS_MULT":
			result = ScenarioEffect.ScenarioEffectWeaponStats;
			break;
		case "SMITH_POW_TEMP":
			result = ScenarioEffect.ScenarioEffectPowUpTempMult;
			break;
		case "SMITH_INT_TEMP":
			result = ScenarioEffect.ScenarioEffectIntUpTempMult;
			break;
		case "SMITH_TEC_TEMP":
			result = ScenarioEffect.ScenarioEffectTecUpTempMult;
			break;
		case "SMITH_LUC_TEMP":
			result = ScenarioEffect.ScenarioEffectLucUpTempMult;
			break;
		case "SMITH_STA_TEMP":
			result = ScenarioEffect.ScenarioEffectStaUpTempMult;
			break;
		case "SMITH_POW_PERM":
			result = ScenarioEffect.ScenarioEffectPowUpPerm;
			break;
		case "SMITH_INT_PERM":
			result = ScenarioEffect.ScenarioEffectIntUpPerm;
			break;
		case "SMITH_TEC_PERM":
			result = ScenarioEffect.ScenarioEffectTecUpPerm;
			break;
		case "SMITH_LUC_PERM":
			result = ScenarioEffect.ScenarioEffectLucUpPerm;
			break;
		case "SMITH_STA_PERM":
			result = ScenarioEffect.ScenarioEffectLucUpPerm;
			break;
		}
		return result;
	}

	public static string convertStatEffectToString(StatEffect statEffect)
	{
		string result = string.Empty;
		switch (statEffect)
		{
		case StatEffect.StatEffectAbsPower:
			result = "STAT_ABS_POWER";
			break;
		case StatEffect.StatEffectAbsIntelligence:
			result = "STAT_ABS_INTELLIGENCE";
			break;
		case StatEffect.StatEffectAbsTechnique:
			result = "STAT_ABS_TECHNIQUE";
			break;
		case StatEffect.StatEffectAbsLuck:
			result = "STAT_ABS_LUCK";
			break;
		case StatEffect.StatEffectAbsStamina:
			result = "STAT_ABS_STAMINA";
			break;
		case StatEffect.StatEffectMultPower:
			result = "STAT_MULT_POWER";
			break;
		case StatEffect.StatEffectMultIntelligence:
			result = "STAT_MULT_INTELLIGENCE";
			break;
		case StatEffect.StatEffectMultTechnique:
			result = "STAT_MULT_TECHNIQUE";
			break;
		case StatEffect.StatEffectMultLuck:
			result = "STAT_MULT_LUCK";
			break;
		case StatEffect.StatEffectMultStamina:
			result = "STAT_MULT_STAMINA";
			break;
		case StatEffect.StatEffectWorkProgressMax:
			result = "WORK_PROGRESS_MAX";
			break;
		case StatEffect.StatEffectForgeBoostNext:
			result = "FORGE_BOOST_NEXT";
			break;
		}
		return result;
	}

	public static StatEffect convertStringToStatEffect(string statEffectString)
	{
		StatEffect result = StatEffect.StatEffectNothing;
		switch (statEffectString)
		{
		case "STAT_ABS_POWER":
			result = StatEffect.StatEffectAbsPower;
			break;
		case "STAT_ABS_INTELLIGENCE":
			result = StatEffect.StatEffectAbsIntelligence;
			break;
		case "STAT_ABS_TECHNIQUE":
			result = StatEffect.StatEffectAbsTechnique;
			break;
		case "STAT_ABS_LUCK":
			result = StatEffect.StatEffectAbsLuck;
			break;
		case "STAT_ABS_STAMINA":
			result = StatEffect.StatEffectAbsStamina;
			break;
		case "STAT_MULT_POWER":
			result = StatEffect.StatEffectMultPower;
			break;
		case "STAT_MULT_INTELLIGENCE":
			result = StatEffect.StatEffectMultIntelligence;
			break;
		case "STAT_MULT_TECHNIQUE":
			result = StatEffect.StatEffectMultTechnique;
			break;
		case "STAT_MULT_LUCK":
			result = StatEffect.StatEffectMultLuck;
			break;
		case "STAT_MULT_STAMINA":
			result = StatEffect.StatEffectMultStamina;
			break;
		case "WORK_PROGRESS_MAX":
			result = StatEffect.StatEffectWorkProgressMax;
			break;
		case "FORGE_BOOST_NEXT":
			result = StatEffect.StatEffectForgeBoostNext;
			break;
		}
		return result;
	}

	public static SpecialEventType convertStringToSpecialEventType(string eventString)
	{
		SpecialEventType result = SpecialEventType.SpecialEventTypeBlank;
		switch (eventString)
		{
		case "DOG":
			result = SpecialEventType.SpecialEventTypeDog;
			break;
		case "FESTIVAL":
			result = SpecialEventType.SpecialEventTypeFestival;
			break;
		case "SEASON":
			result = SpecialEventType.SpecialEventTypeSeason;
			break;
		case "STORY":
			result = SpecialEventType.SpecialEventTypeStory;
			break;
		case "WARNING":
			result = SpecialEventType.SpecialEventTypeWarning;
			break;
		case "AVATAR":
			result = SpecialEventType.specialEventTypeAvatar;
			break;
		}
		return result;
	}

	public static SmithStation convertStringToSmithStation(string stationString)
	{
		SmithStation result = SmithStation.SmithStationBlank;
		switch (stationString)
		{
		case "SmithStationAuto":
			result = SmithStation.SmithStationAuto;
			break;
		case "SmithStationCraft":
			result = SmithStation.SmithStationCraft;
			break;
		case "SmithStationDesign":
			result = SmithStation.SmithStationDesign;
			break;
		case "SmithStationEnchant":
			result = SmithStation.SmithStationEnchant;
			break;
		case "SmithStationPolish":
			result = SmithStation.SmithStationPolish;
			break;
		case "SmithStationDogHome":
			result = SmithStation.SmithStationDogHome;
			break;
		}
		return result;
	}

	public static UnlockCondition convertStringToUnlockCondition(string conditionString)
	{
		UnlockCondition result = UnlockCondition.UnlockConditionNone;
		switch (conditionString)
		{
		case "TIME":
			result = UnlockCondition.UnlockConditionTime;
			break;
		case "SHOP_LEVEL":
			result = UnlockCondition.UnlockConditionShopLevel;
			break;
		case "FAME":
			result = UnlockCondition.UnlockConditionFame;
			break;
		case "STARCH":
			result = UnlockCondition.UnlockConditionStarch;
			break;
		case "WEAPON_NUM":
			result = UnlockCondition.UnlockConditionForgeCount;
			break;
		case "WEAPON_TYPE_NUM":
			result = UnlockCondition.UnlockConditionForgeTypeCount;
			break;
		case "CONTRACT":
			result = UnlockCondition.UnlockConditionContractCount;
			break;
		case "HERO_EXP":
			result = UnlockCondition.UnlockConditionHeroExp;
			break;
		case "HERO_MAX":
			result = UnlockCondition.UnlockConditionHeroMax;
			break;
		case "HERO_LEVEL":
			result = UnlockCondition.UnlockConditionHeroLevel;
			break;
		case "HERO_LEVEL_NUM":
			result = UnlockCondition.UnlockConditionHeroLevelCount;
			break;
		case "WEAPON":
			result = UnlockCondition.UnlockConditionResearchWeapon;
			break;
		case "WEAPON_TYPE":
			result = UnlockCondition.UnlockConditionResearchType;
			break;
		case "WORKSTATION_UPGRADE":
			result = UnlockCondition.UnlockConditionUpgradeTotal;
			break;
		case "WORKSTATION_LEVEL":
			result = UnlockCondition.UnlockConditionWorkstationLevel;
			break;
		case "DECO_COUNT":
			result = UnlockCondition.UnlockConditionDecoCount;
			break;
		case "DECO_EQUIP":
			result = UnlockCondition.UnlockConditionDecoEquip;
			break;
		case "FURNITURE_EQUIP":
			result = UnlockCondition.UnlockConditionFurnitureEquip;
			break;
		case "REQUEST_COMPLETE":
			result = UnlockCondition.UnlockConditionRequestComplete;
			break;
		case "LEGENDARY_COMPLETE":
			result = UnlockCondition.UnlockConditionLegendaryComplete;
			break;
		case "AREA":
			result = UnlockCondition.UnlockConditionArea;
			break;
		case "REGION":
			result = UnlockCondition.UnlockConditionRegion;
			break;
		case "ITEM_EXPLORE":
			result = UnlockCondition.UnlockConditionExploreItem;
			break;
		case "ITEM_BUY":
			result = UnlockCondition.UnlockConditionBuyItem;
			break;
		case "ITEM_OWN":
			result = UnlockCondition.UnlockConditionOwnItem;
			break;
		case "WEAPON_SELL":
			result = UnlockCondition.UnlockConditionWeaponsSold;
			break;
		case "EXPLORE":
			result = UnlockCondition.UnlockConditionExplore;
			break;
		case "BUY":
			result = UnlockCondition.UnlockConditionBuy;
			break;
		case "SELL":
			result = UnlockCondition.UnlockConditionSell;
			break;
		case "TRAINING":
			result = UnlockCondition.UnlockConditionTraining;
			break;
		case "VACATION":
			result = UnlockCondition.UnlockConditionVacation;
			break;
		case "SMITH":
			result = UnlockCondition.UnlockConditionSmith;
			break;
		case "SMITH_HIRE":
			result = UnlockCondition.UnlockConditionSmithHire;
			break;
		case "SMITH_STAT":
			result = UnlockCondition.UnlockConditionSmithStat;
			break;
		case "SMITH_JOBCLASS":
			result = UnlockCondition.UnlockConditionSmithJobClass;
			break;
		case "OBJECTIVE":
			result = UnlockCondition.UnlockConditionObjective;
			break;
		case "CODE":
			result = UnlockCondition.UnlockConditionCode;
			break;
		}
		return result;
	}

	public static SmithExploreState convertStringToSmithExploreState(string stateString)
	{
		SmithExploreState result = SmithExploreState.SmithExploreStateBlank;
		switch (stateString)
		{
		case "SmithExploreStateBuyMaterial":
			result = SmithExploreState.SmithExploreStateBuyMaterial;
			break;
		case "SmithExploreStateBuyMaterialReturned":
			result = SmithExploreState.SmithExploreStateBuyMaterialReturned;
			break;
		case "SmithExploreStateBuyMaterialTravelHome":
			result = SmithExploreState.SmithExploreStateBuyMaterialTravelHome;
			break;
		case "SmithExploreStateExplore":
			result = SmithExploreState.SmithExploreStateExplore;
			break;
		case "SmithExploreStateExploreReturned":
			result = SmithExploreState.SmithExploreStateExploreReturned;
			break;
		case "SmithExploreStateExploreTravelHome":
			result = SmithExploreState.SmithExploreStateExploreTravelHome;
			break;
		case "SmithExploreStateOffersWaiting":
			result = SmithExploreState.SmithExploreStateOffersWaiting;
			break;
		case "SmithExploreStateSellWeapon":
			result = SmithExploreState.SmithExploreStateSellWeapon;
			break;
		case "SmithExploreStateSellWeaponReturned":
			result = SmithExploreState.SmithExploreStateSellWeaponReturned;
			break;
		case "SmithExploreStateSellWeaponTravelHome":
			result = SmithExploreState.SmithExploreStateSellWeaponTravelHome;
			break;
		case "SmithExploreStateTraining":
			result = SmithExploreState.SmithExploreStateTraining;
			break;
		case "SmithExploreStateTrainingReturned":
			result = SmithExploreState.SmithExploreStateTrainingReturned;
			break;
		case "SmithExploreStateTrainingTravelHome":
			result = SmithExploreState.SmithExploreStateTrainingTravelHome;
			break;
		case "SmithExploreStateTravelToBuyMaterial":
			result = SmithExploreState.SmithExploreStateTravelToBuyMaterial;
			break;
		case "SmithExploreStateTravelToExplore":
			result = SmithExploreState.SmithExploreStateTravelToExplore;
			break;
		case "SmithExploreStateTravelToSellWeapon":
			result = SmithExploreState.SmithExploreStateTravelToSellWeapon;
			break;
		case "SmithExploreStateTravelToTraining":
			result = SmithExploreState.SmithExploreStateTravelToTraining;
			break;
		case "SmithExploreStateTravelToVacation":
			result = SmithExploreState.SmithExploreStateTravelToVacation;
			break;
		case "SmithExploreStateVacation":
			result = SmithExploreState.SmithExploreStateVacation;
			break;
		case "SmithExploreStateVacationReturned":
			result = SmithExploreState.SmithExploreStateVacationReturned;
			break;
		case "SmithExploreStateVacationTravelHome":
			result = SmithExploreState.SmithExploreStateVacationTravelHome;
			break;
		}
		return result;
	}

	public static LanguageType convertStringToLanguageType(string langString)
	{
		LanguageType result = LanguageType.kLanguageTypeBlank;
		switch (langString)
		{
		case "kLanguageTypeEnglish":
			result = LanguageType.kLanguageTypeEnglish;
			break;
		case "kLanguageTypeJap":
			result = LanguageType.kLanguageTypeJap;
			break;
		case "kLanguageTypeChinese":
			result = LanguageType.kLanguageTypeChinese;
			break;
		case "kLanguageTypeArabic":
			result = LanguageType.kLanguageTypeArabic;
			break;
		case "kLanguageTypeThai":
			result = LanguageType.kLanguageTypeThai;
			break;
		case "kLanguageTypeBahasa":
			result = LanguageType.kLanguageTypeBahasa;
			break;
		case "kLanguageTypeGermany":
			result = LanguageType.kLanguageTypeGermany;
			break;
		case "kLanguageTypeRussia":
			result = LanguageType.kLanguageTypeRussia;
			break;
		case "kLanguageTypeFrench":
			result = LanguageType.kLanguageTypeFrench;
			break;
		case "kLanguageTypeItalian":
			result = LanguageType.kLanguageTypeItalian;
			break;
		case "kLanguageTypeSpanish":
			result = LanguageType.kLanguageTypeSpanish;
			break;
		}
		return result;
	}

	public static string convertLanguageTypeToLanguageString(LanguageType aLang)
	{
		string result = string.Empty;
		switch (aLang)
		{
		case LanguageType.kLanguageTypeEnglish:
			result = "ENGLISH";
			break;
		case LanguageType.kLanguageTypeJap:
			result = "JAPANESE";
			break;
		case LanguageType.kLanguageTypeChinese:
			result = "CHINESE";
			break;
		case LanguageType.kLanguageTypeArabic:
			result = "ARABIC";
			break;
		case LanguageType.kLanguageTypeThai:
			result = "THAI";
			break;
		case LanguageType.kLanguageTypeBahasa:
			result = "BAHASA";
			break;
		case LanguageType.kLanguageTypeGermany:
			result = "GERMAN";
			break;
		case LanguageType.kLanguageTypeRussia:
			result = "RUSSIAN";
			break;
		case LanguageType.kLanguageTypeFrench:
			result = "FRENCH";
			break;
		case LanguageType.kLanguageTypeItalian:
			result = "ITALIAN";
			break;
		case LanguageType.kLanguageTypeSpanish:
			result = "SPANISH";
			break;
		}
		return result;
	}

	public static List<WeekendActivityType> getWeekendActivityTypeListByShopLevel(int shopLevel)
	{
		List<WeekendActivityType> list = new List<WeekendActivityType>();
		switch (shopLevel)
		{
		case 1:
			list.Add(WeekendActivityType.WeekendActivityTypeAdventure);
			list.Add(WeekendActivityType.WeekendActivityTypeDog);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			break;
		case 2:
			list.Add(WeekendActivityType.WeekendActivityTypeAdventure);
			list.Add(WeekendActivityType.WeekendActivityTypeDog);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			break;
		case 3:
			list.Add(WeekendActivityType.WeekendActivityTypeAdventure);
			list.Add(WeekendActivityType.WeekendActivityTypeDog);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			break;
		case 4:
			list.Add(WeekendActivityType.WeekendActivityTypeAdventure);
			list.Add(WeekendActivityType.WeekendActivityTypeDog);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			break;
		case 5:
			list.Add(WeekendActivityType.WeekendActivityTypeAdventure);
			list.Add(WeekendActivityType.WeekendActivityTypeAdventure);
			list.Add(WeekendActivityType.WeekendActivityTypeDog);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			list.Add(WeekendActivityType.WeekendActivityTypeNormal);
			break;
		}
		return list;
	}

	public static string convertHalfHoursToTimeDisplay(long halfHours, bool isMenu)
	{
		List<int> list = convertHalfHoursToIntList(halfHours);
		string text = "\n";
		if (!isMenu)
		{
			text = " | ";
		}
		Season seasonByMonth = getSeasonByMonth(list[1]);
		string text2 = convertSeasonToString(seasonByMonth);
		string text3 = "00";
		if (list[5] == 1)
		{
			text3 = "30";
		}
		string text4 = getGameData().getTextByRefId("YEAR") + " " + (list[0] + 1) + " " + getGameData().getTextByRefId("MONTH") + " " + (list[1] + 1) + " " + getGameData().getTextByRefId("WEEK") + " " + (list[2] + 1) + " " + getGameData().getTextByRefId("DATE") + " " + (list[3] + 1);
		string text5 = text4;
		return text5 + text + text2 + text + getGameData().getTextByRefId("time") + " " + list[4] + ":" + text3;
	}

	public static string convertElementToString(Element elem)
	{
		string result = string.Empty;
		switch (elem)
		{
		case Element.ElementFire:
			result = getGameData().getTextByRefId("FIRE");
			break;
		case Element.ElementWater:
			result = getGameData().getTextByRefId("WATER");
			break;
		case Element.ElementEarth:
			result = getGameData().getTextByRefId("EARTH");
			break;
		case Element.ElementWind:
			result = getGameData().getTextByRefId("WIND");
			break;
		}
		return result;
	}

	public static string convertClassWeaponAffinityToAppraisal1(int affinity, Weapon weapon, Hero jobclass)
	{
		string aRefId = string.Empty;
		List<string> list = new List<string>();
		list.Add("[weapon]");
		list.Add("[jobClass]");
		List<string> list2 = new List<string>();
		list2.Add(weapon.getWeaponName());
		list2.Add(jobclass.getJobClassName());
		switch (affinity)
		{
		case 1:
			aRefId = "appraisalAffinity01";
			break;
		case 2:
			aRefId = "appraisalAffinity02";
			break;
		case 3:
			aRefId = "appraisalAffinity03";
			break;
		case 4:
			aRefId = "appraisalAffinity04";
			break;
		case 5:
			aRefId = "appraisalAffinity05";
			break;
		case 6:
			aRefId = "appraisalAffinity06";
			break;
		case 7:
			aRefId = "appraisalAffinity07";
			break;
		case 8:
			aRefId = "appraisalAffinity08";
			break;
		case 9:
			aRefId = "appraisalAffinity09";
			break;
		case 10:
			aRefId = "appraisalAffinity10";
			break;
		}
		return getGameData().getTextByRefIdWithDynTextList(aRefId, list, list2);
	}

	public static string convertStatAppraisal(int appraiseScore, WeaponStat stat)
	{
		string aRefId = string.Empty;
		switch (appraiseScore)
		{
		case 1:
			aRefId = "appraisalStat01";
			break;
		case 2:
			aRefId = "appraisalStat02";
			break;
		case 3:
			aRefId = "appraisalStat03";
			break;
		case 4:
			aRefId = "appraisalStat04";
			break;
		case 5:
			aRefId = "appraisalStat05";
			break;
		case 6:
			aRefId = "appraisalStat06";
			break;
		case 7:
			aRefId = "appraisalStat07";
			break;
		case 8:
			aRefId = "appraisalStat08";
			break;
		case 9:
			aRefId = "appraisalStat09";
			break;
		case 10:
			aRefId = "appraisalStat10";
			break;
		}
		return getGameData().getTextByRefIdWithDynText(aRefId, "[stat]", convertWeaponStatToString(stat));
	}

	public static string convertClassWeaponAffinityToRank(int affinity)
	{
		string result = string.Empty;
		switch (affinity)
		{
		case 1:
			result = "D";
			break;
		case 2:
			result = "C";
			break;
		case 3:
			result = "B";
			break;
		case 4:
			result = "A";
			break;
		case 5:
			result = "S";
			break;
		case 7:
			result = "X";
			break;
		}
		return result;
	}

	public static string getLoyaltyIcon(int level)
	{
		string result = string.Empty;
		switch (level)
		{
		case 1:
			result = "heart_1";
			break;
		case 2:
			result = "heart_2";
			break;
		case 3:
			result = "heart_3";
			break;
		case 4:
			result = "heart_4";
			break;
		case 5:
			result = "heart_5";
			break;
		}
		return result;
	}

	public static List<Hashtable> getRandomWeaponList(int num)
	{
		List<Hashtable> list = new List<Hashtable>();
		Hashtable hashtable = new Hashtable();
		hashtable["name"] = "Narsil";
		hashtable["image"] = "Sword";
		list.Add(hashtable);
		Hashtable hashtable2 = new Hashtable();
		hashtable2["name"] = "Sting";
		hashtable2["image"] = "Sword";
		list.Add(hashtable2);
		Hashtable hashtable3 = new Hashtable();
		hashtable3["name"] = "Butterfly Cutter";
		hashtable3["image"] = "Spear";
		list.Add(hashtable3);
		Hashtable hashtable4 = new Hashtable();
		hashtable4["name"] = "Golden Rod";
		hashtable4["image"] = "Staff";
		list.Add(hashtable4);
		Hashtable hashtable5 = new Hashtable();
		hashtable5["name"] = "Hawk's Talon";
		hashtable5["image"] = "Sword";
		list.Add(hashtable5);
		Hashtable hashtable6 = new Hashtable();
		hashtable6["name"] = "Sylph Blade";
		hashtable6["image"] = "Sword";
		list.Add(hashtable6);
		Hashtable hashtable7 = new Hashtable();
		hashtable7["name"] = "Repeater";
		hashtable7["image"] = "Bow";
		list.Add(hashtable7);
		Hashtable hashtable8 = new Hashtable();
		hashtable8["name"] = "Dragonslayer";
		hashtable8["image"] = "Sword";
		list.Add(hashtable8);
		Hashtable hashtable9 = new Hashtable();
		hashtable9["name"] = "Salamander";
		hashtable9["image"] = "Dagger";
		list.Add(hashtable9);
		Hashtable hashtable10 = new Hashtable();
		hashtable10["name"] = "Arm Cannon";
		hashtable10["image"] = "Cannon";
		list.Add(hashtable10);
		List<int> randomIntList = getRandomIntList(list.Count, num);
		List<Hashtable> list2 = new List<Hashtable>();
		foreach (int item in randomIntList)
		{
			list2.Add(list[item]);
		}
		return list2;
	}

	public static string convertSmithStationToDisplayString(SmithStation station)
	{
		string result = string.Empty;
		switch (station)
		{
		case SmithStation.SmithStationAuto:
			result = "smithAssignment01";
			break;
		case SmithStation.SmithStationDesign:
			result = "upgradeMenu05";
			break;
		case SmithStation.SmithStationCraft:
			result = "upgradeMenu06";
			break;
		case SmithStation.SmithStationPolish:
			result = "upgradeMenu07";
			break;
		case SmithStation.SmithStationEnchant:
			result = "upgradeMenu08";
			break;
		}
		return result;
	}

	public static string getHeroRarityText(bool isRare, float percent)
	{
		string empty = string.Empty;
		string empty2 = string.Empty;
		string empty3 = string.Empty;
		if (isRare)
		{
			empty = "heroRarityText1";
			empty3 = "FFD84A";
		}
		else if (percent < 0.12f)
		{
			empty = "heroRarityText2";
			empty3 = "D484F5";
		}
		else if (percent < 0.16f)
		{
			empty = "heroRarityText3";
			empty3 = "00AAC7";
		}
		else if ((double)percent < 0.2)
		{
			empty = "heroRarityText4";
			empty3 = "56AE59";
		}
		else
		{
			empty = "heroRarityText5";
			empty3 = "FFFFFF";
		}
		string text = empty2;
		return text + "[" + empty3 + "][i]" + getGameData().getTextByRefId(empty) + "[/i][-]";
	}

	public static string getSmithExploreMoodText(SmithMood mood, string action)
	{
		GameData gameData = getGameData();
		string empty = string.Empty;
		string text = string.Empty;
		switch (action)
		{
		case "EXPLORE":
			text = "smithExploreMood";
			break;
		case "BUY":
			text = "smithBuyMood";
			break;
		case "SELL":
			text = "smithSellMood";
			break;
		}
		switch (mood)
		{
		case SmithMood.SmithMoodVeryHappy:
			text += "VeryHappy";
			break;
		case SmithMood.SmithMoodHappy:
			text += "Happy";
			break;
		case SmithMood.SmithMoodNormal:
			text += "Normal";
			break;
		case SmithMood.SmithMoodSad:
			text += "Sad";
			break;
		case SmithMood.SmithMoodVerySad:
			text += "VerySad";
			break;
		}
		return gameData.getRandomTextBySetRefId(text);
	}

	public static string convertWeaponScoreToRating(int score)
	{
		if (score < 2)
		{
			return "F";
		}
		if (score < 4)
		{
			return "D";
		}
		if (score < 6)
		{
			return "C";
		}
		if (score < 8)
		{
			return "B";
		}
		if (score < 10)
		{
			return "A";
		}
		return "S";
	}

	public static string getHeroRatingText(int score)
	{
		GameData gameData = getGameData();
		if (score < 2)
		{
			return gameData.getRandomTextBySetRefId("heroRatingF");
		}
		if (score < 4)
		{
			return gameData.getRandomTextBySetRefId("heroRatingD");
		}
		if (score < 6)
		{
			return gameData.getRandomTextBySetRefId("heroRatingC");
		}
		if (score < 8)
		{
			return gameData.getRandomTextBySetRefId("heroRatingB");
		}
		if (score < 10)
		{
			return gameData.getRandomTextBySetRefId("heroRatingA");
		}
		return gameData.getRandomTextBySetRefId("heroRatingS");
	}

	public static string generateBeforeBoostText(int score)
	{
		string empty = string.Empty;
		return getGameData().getTextByRefId(score switch
		{
			1 => "preBoostMessage01", 
			2 => "preBoostMessage02", 
			4 => "preBoostMessage04", 
			5 => "preBoostMessage05", 
			6 => "preBoostMessage06", 
			7 => "preBoostMessage07", 
			_ => "preBoostMessage03", 
		});
	}

	public static string generateAfterBoostText(int score)
	{
		string empty = string.Empty;
		return getGameData().getTextByRefId(score switch
		{
			1 => "postBoostMessage01", 
			2 => "postBoostMessage02", 
			3 => "postBoostMessage03", 
			4 => "postBoostMessage04", 
			5 => "postBoostMessage05", 
			6 => "postBoostMessage06", 
			7 => "postBoostMessage07", 
			_ => "postBoostMessage03", 
		});
	}

	public static int convertMoodStateToInt(SmithMood smithMood)
	{
		return smithMood switch
		{
			SmithMood.SmithMoodVeryHappy => 5, 
			SmithMood.SmithMoodHappy => 4, 
			SmithMood.SmithMoodNormal => 3, 
			SmithMood.SmithMoodSad => 2, 
			SmithMood.SmithMoodVerySad => 1, 
			_ => 3, 
		};
	}

	public static string generateBoostString(int atk, int spd, int acc, int mag)
	{
		string empty = string.Empty;
		if (atk >= 0)
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats06") + " +" + atk + " | ";
		}
		else
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats06") + " " + atk + " | ";
		}
		if (spd >= 0)
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats07") + " +" + spd + " | ";
		}
		else
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats07") + " " + spd + " | ";
		}
		if (acc >= 0)
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats08") + " +" + acc + " | ";
		}
		else
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats08") + " " + acc + " | ";
		}
		if (mag >= 0)
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats09") + " +" + mag;
		}
		else
		{
			string text = empty;
			empty = text + getGameData().getTextByRefId("weaponStats09") + " " + mag;
		}
		if (empty == string.Empty)
		{
			empty += getGameData().getTextByRefId("menuGeneral06");
		}
		return empty;
	}

	public static string getSubquestIconName(SubquestType subquestType, bool isOn)
	{
		switch (subquestType)
		{
		case SubquestType.SubquestTypeElement:
			if (isOn)
			{
				return "ele-on";
			}
			return "ele-off";
		case SubquestType.SubquestTypeWeapon:
			if (isOn)
			{
				return "weapon-on";
			}
			return "weapon-off";
		case SubquestType.SubquestTypeStat:
			if (isOn)
			{
				return "stat-on";
			}
			return "stat-off";
		default:
			return string.Empty;
		}
	}

	public static int getMysteryBoostVariation()
	{
		List<int> list = new List<int>();
		list.Add(-2);
		list.Add(-1);
		list.Add(0);
		list.Add(1);
		list.Add(2);
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	public static int getBoostVariation()
	{
		List<int> list = new List<int>();
		list.Add(-1);
		list.Add(0);
		list.Add(0);
		list.Add(1);
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	public static string makeTextRefNumString(int num)
	{
		string empty = string.Empty;
		if (num >= 0 && num < 10)
		{
			return "0" + num;
		}
		return num.ToString();
	}

	public static string getQuestRandomText()
	{
		int num = UnityEngine.Random.Range(1, 10);
		return getGameData().getTextByRefId("questRandomText" + makeTextRefNumString(num));
	}

	public static string getRandomQuestEnemy()
	{
		int num = UnityEngine.Random.Range(1, 10);
		return getGameData().getTextByRefId("questRandomEnemy" + makeTextRefNumString(num));
	}

	public static string getMoodIcon(SmithMood moodState)
	{
		return moodState switch
		{
			SmithMood.SmithMoodVeryHappy => "Mood-elated", 
			SmithMood.SmithMoodHappy => "Mood-happy", 
			SmithMood.SmithMoodSad => "Mood-stressed", 
			SmithMood.SmithMoodVerySad => "Mood-depressed", 
			_ => "Mood-neutral", 
		};
	}

	public static SmithMood getMoodState(float moodPercent)
	{
		if (moodPercent >= 0.9f)
		{
			return SmithMood.SmithMoodVeryHappy;
		}
		if (moodPercent >= 0.75f)
		{
			return SmithMood.SmithMoodHappy;
		}
		if (moodPercent >= 0.25f)
		{
			return SmithMood.SmithMoodNormal;
		}
		if (moodPercent >= 0.1f)
		{
			return SmithMood.SmithMoodSad;
		}
		return SmithMood.SmithMoodVerySad;
	}

	public static float getMoodEffect(SmithMood mood, float baseNum, float powerNum, float multNum)
	{
		float f = 0f;
		switch (mood)
		{
		case SmithMood.SmithMoodVeryHappy:
			f = 5f;
			break;
		case SmithMood.SmithMoodHappy:
			f = 4f;
			break;
		case SmithMood.SmithMoodNormal:
			f = 3f;
			break;
		case SmithMood.SmithMoodSad:
			f = 2f;
			break;
		case SmithMood.SmithMoodVerySad:
			f = 1f;
			break;
		}
		return baseNum + multNum * Mathf.Pow(f, powerNum);
	}

	public static string getWhetsappMoodString(SmithMood mood)
	{
		GameData gameData = getGameData();
		string result = string.Empty;
		switch (mood)
		{
		case SmithMood.SmithMoodVeryHappy:
			result = gameData.getRandomTextBySetRefId("whetsappMoodElated");
			break;
		case SmithMood.SmithMoodHappy:
			result = gameData.getRandomTextBySetRefId("whetsappMoodHappy");
			break;
		case SmithMood.SmithMoodNormal:
			result = gameData.getRandomTextBySetRefId("whetsappMoodNormal");
			break;
		case SmithMood.SmithMoodSad:
			result = gameData.getRandomTextBySetRefId("whetsappMoodStressed");
			break;
		case SmithMood.SmithMoodVerySad:
			result = gameData.getRandomTextBySetRefId("whetsappMoodDepressed");
			break;
		}
		return result;
	}

	public static string getMoodString(SmithMood mood, bool showDesc)
	{
		GameData gameData = getGameData();
		string text = "moodName";
		if (showDesc)
		{
			text = "moodDesc";
		}
		string result = string.Empty;
		switch (mood)
		{
		case SmithMood.SmithMoodVeryHappy:
			result = "[00AAC7]" + gameData.getTextByRefId(text + "01") + "[-]";
			break;
		case SmithMood.SmithMoodHappy:
			result = "[56AE59]" + gameData.getTextByRefId(text + "02") + "[-]";
			break;
		case SmithMood.SmithMoodNormal:
			result = "[FFD84A]" + gameData.getTextByRefId(text + "03") + "[-]";
			break;
		case SmithMood.SmithMoodSad:
			result = "[FF9000]" + gameData.getTextByRefId(text + "04") + "[-]";
			break;
		case SmithMood.SmithMoodVerySad:
			result = "[FF4842]" + gameData.getTextByRefId(text + "05") + "[-]";
			break;
		}
		return result;
	}

	public static int getBubblePriority(string actionText)
	{
		return actionText switch
		{
			"HIRE" => 1, 
			"FIRE" => 1, 
			"CHANGE_STATION" => 2, 
			"EXPLORE" => 2, 
			"BUY" => 2, 
			"SELL" => 2, 
			"TRAINING" => 2, 
			"VACATION" => 2, 
			"RESEARCH" => 2, 
			"STANDBY" => 2, 
			"START_FORGE" => 2, 
			"LEVEL_UP" => 3, 
			"MOOD_CHANGE" => 4, 
			"STATUS" => 5, 
			"RANDOM" => 6, 
			_ => 99, 
		};
	}

	public static int getProjectGrowthNum(int smithNum)
	{
		int result = 100;
		switch (smithNum)
		{
		case 1:
			result = 50;
			break;
		case 2:
			result = 50;
			break;
		case 3:
			result = 50;
			break;
		case 4:
			result = 64;
			break;
		case 5:
			result = 80;
			break;
		case 6:
			result = 96;
			break;
		case 7:
			result = 112;
			break;
		case 8:
			result = 128;
			break;
		case 9:
			result = 144;
			break;
		case 10:
			result = 160;
			break;
		}
		return result;
	}

	public static string getItemImagePath(ItemType aType)
	{
		string result = string.Empty;
		switch (aType)
		{
		case ItemType.ItemTypeEnhancement:
			result = "Image/Enchantment/";
			break;
		case ItemType.ItemTypeRelic:
			result = "Image/relics/";
			break;
		case ItemType.ItemTypeMaterial:
			result = "Image/materials/";
			break;
		}
		return result;
	}

	public static string generateSmithStatsTextColorDisplay(string textOrigColor, int addedAmt, int showAmt, bool isPositiveGreen)
	{
		if (!isPositiveGreen)
		{
			addedAmt *= -1;
		}
		if (addedAmt > 0)
		{
			return "[56AE59]" + showAmt + "[-]";
		}
		if (addedAmt < 0)
		{
			return "[FF4842]" + showAmt + "[-]";
		}
		return "[" + textOrigColor + "]" + showAmt + "[-]";
	}

	public static string generateStatModifyDisplay(int statBefore, int statAfter, bool isNGUI = false)
	{
		int num = statAfter - statBefore;
		if (!isNGUI)
		{
			if (num > 0)
			{
				return "<color=green>(+" + num + ")</color>";
			}
			if (num < 0)
			{
				return "<color=red>(" + num + ")</color>";
			}
		}
		else
		{
			if (num > 0)
			{
				return "[56AE59](+" + num + ")[-]";
			}
			if (num < 0)
			{
				return "[FF4842](" + num + ")[-]";
			}
		}
		return string.Empty;
	}

	public static Color32 getRarityColor(ExploreItem aItem, ItemType aType)
	{
		int probability = aItem.getProbability();
		Color32 result = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		int num = 30;
		int num2 = 75;
		int num3 = 100;
		int num4 = 150;
		switch (aType)
		{
		case ItemType.ItemTypeEnhancement:
			num = 5;
			num2 = 10;
			num3 = 15;
			num4 = 20;
			break;
		case ItemType.ItemTypeRelic:
			num = 10;
			num2 = 15;
			num3 = 20;
			num4 = 25;
			break;
		}
		if (probability <= num)
		{
			result = new Color32(byte.MaxValue, 97, 0, byte.MaxValue);
		}
		else if (probability > num && probability <= num2)
		{
			result = new Color32(155, 89, 182, byte.MaxValue);
		}
		else if (probability > num2 && probability <= num3)
		{
			result = new Color32(52, 152, 219, byte.MaxValue);
		}
		else if (probability > num3 && probability <= num4)
		{
			result = new Color32(26, 188, 156, byte.MaxValue);
		}
		else if (probability > num4)
		{
			result = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
		return result;
	}

	public static string getRarityColorHex(ExploreItem aItem, ItemType aType)
	{
		int probability = aItem.getProbability();
		string result = "FFFFFF";
		int num = 30;
		int num2 = 75;
		int num3 = 100;
		int num4 = 150;
		switch (aType)
		{
		case ItemType.ItemTypeEnhancement:
			num = 5;
			num2 = 10;
			num3 = 15;
			num4 = 20;
			break;
		case ItemType.ItemTypeRelic:
			num = 10;
			num2 = 15;
			num3 = 20;
			num4 = 25;
			break;
		}
		if (probability <= num)
		{
			result = "FF9000";
		}
		else if (probability > num && probability <= num2)
		{
			result = "D484F5";
		}
		else if (probability > num2 && probability <= num3)
		{
			result = "00AAC7";
		}
		else if (probability > num3 && probability <= num4)
		{
			result = "56AE59";
		}
		else if (probability > num4)
		{
			result = "FFFFFF";
		}
		return result;
	}

	public static string getItemRarityText(ExploreItem aItem, ItemType aType)
	{
		int probability = aItem.getProbability();
		string text = "FFFFFF";
		string text2 = string.Empty;
		int num = 30;
		int num2 = 75;
		int num3 = 100;
		int num4 = 150;
		switch (aType)
		{
		case ItemType.ItemTypeEnhancement:
			num = 5;
			num2 = 10;
			num3 = 15;
			num4 = 20;
			break;
		case ItemType.ItemTypeRelic:
			num = 10;
			num2 = 15;
			num3 = 20;
			num4 = 25;
			break;
		}
		if (probability <= num)
		{
			text = "FF9000";
			text2 = getGameData().getTextByRefId("itemRarity01");
		}
		else if (probability > num && probability <= num2)
		{
			text = "D484F5";
			text2 = getGameData().getTextByRefId("itemRarity02");
		}
		else if (probability > num2 && probability <= num3)
		{
			text = "00AAC7";
			text2 = getGameData().getTextByRefId("itemRarity03");
		}
		else if (probability > num3 && probability <= num4)
		{
			text = "56AE59";
			text2 = getGameData().getTextByRefId("itemRarity04");
		}
		else if (probability > num4)
		{
			text = "FFFFFF";
			text2 = getGameData().getTextByRefId("itemRarity05");
		}
		return "[" + text + "]" + text2 + "[-]";
	}

	public static string getStatColorHex(WeaponStat stat)
	{
		string result = "FFFFFF";
		switch (stat)
		{
		case WeaponStat.WeaponStatAttack:
			result = "FF4842";
			break;
		case WeaponStat.WeaponStatSpeed:
			result = "56AE59";
			break;
		case WeaponStat.WeaponStatAccuracy:
			result = "00AAC7";
			break;
		case WeaponStat.WeaponStatMagic:
			result = "FFD84A";
			break;
		}
		return result;
	}

	public static int getVacationPackageRating(int packageMoodValue, int playerRegion)
	{
		int num = (int)(59f + 8f * Mathf.Pow(playerRegion - 1, 1.85f) + 0.5f);
		int num2 = (int)(85f + 19f * Mathf.Pow(playerRegion - 1, 1.15f) + 0.5f);
		int b = (packageMoodValue - num2) * 4 / num + 1;
		b = Mathf.Min(5, b);
		return Mathf.Max(1, b);
	}

	public static string ConvertDictToString(Dictionary<string, int> d)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, int> item in d)
		{
			stringBuilder.Append(item.Key).Append(":").Append(item.Value)
				.Append(',');
		}
		string text = stringBuilder.ToString();
		return text.TrimEnd(',');
	}

	public static Dictionary<string, int> ConvertStringToDict(string aString)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		string[] array = aString.Split(new char[2] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i += 2)
		{
			string key = array[i];
			string aText = array[i + 1];
			int num = parseInt(aText);
			if (dictionary.ContainsKey(key))
			{
				dictionary[key] += num;
			}
			else
			{
				dictionary.Add(key, num);
			}
		}
		return dictionary;
	}

	public static string ConvertWeaponStatListToString(List<WeaponStat> statList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (statList.Count > 0)
		{
			for (int i = 0; i < statList.Count; i++)
			{
				stringBuilder.Append(convertWeaponStatToDataString(statList[i]));
				if (i != statList.Count - 1)
				{
					stringBuilder.Append("|");
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<WeaponStat> ConvertStringToWeaponStatList(string statString)
	{
		List<WeaponStat> list = new List<WeaponStat>();
		if (statString != string.Empty)
		{
			string[] array = statString.Split('|');
			string[] array2 = array;
			foreach (string statString2 in array2)
			{
				list.Add(convertStringToWeaponStat(statString2));
			}
		}
		return list;
	}

	public static string ConvertStatEffectListToString(List<StatEffect> statList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (statList.Count > 0)
		{
			for (int i = 0; i < statList.Count; i++)
			{
				stringBuilder.Append(convertStatEffectToString(statList[i]));
				if (i != statList.Count - 1)
				{
					stringBuilder.Append("|");
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<StatEffect> ConvertStringToStatEffectList(string statString)
	{
		List<StatEffect> list = new List<StatEffect>();
		if (statString != string.Empty)
		{
			string[] array = statString.Split('|');
			string[] array2 = array;
			foreach (string statEffectString in array2)
			{
				list.Add(convertStringToStatEffect(statEffectString));
			}
		}
		return list;
	}

	public static string ConvertSmithListToString(List<Smith> smithList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (smithList.Count > 0)
		{
			for (int i = 0; i < smithList.Count; i++)
			{
				if (smithList[i] != null)
				{
					stringBuilder.Append(smithList[i].getSmithRefId());
					if (i != smithList.Count - 1)
					{
						stringBuilder.Append("|");
					}
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<Smith> ConvertStringToSmithList(string smithString)
	{
		List<Smith> list = new List<Smith>();
		if (smithString != string.Empty)
		{
			string[] array = smithString.Split('|');
			string[] array2 = array;
			foreach (string refId in array2)
			{
				list.Add(getGameData().getSmithByRefId(refId));
			}
		}
		return list;
	}

	public static string ConvertSmithExploreStateListToString(List<SmithExploreState> stateList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (stateList.Count > 0)
		{
			for (int i = 0; i < stateList.Count; i++)
			{
				stringBuilder.Append(stateList[i].ToString());
				if (i != stateList.Count - 1)
				{
					stringBuilder.Append("|");
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<SmithExploreState> ConvertStringToSmithExploreStateList(string stateString)
	{
		List<SmithExploreState> list = new List<SmithExploreState>();
		if (stateString != string.Empty)
		{
			string[] array = stateString.Split('|');
			string[] array2 = array;
			foreach (string stateString2 in array2)
			{
				list.Add(convertStringToSmithExploreState(stateString2));
			}
		}
		return list;
	}

	public static string ConvertAreaStatusListToString(List<AreaStatus> statusList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (statusList.Count > 0)
		{
			for (int i = 0; i < statusList.Count; i++)
			{
				stringBuilder.Append(statusList[i].getAreaStatusRefID());
				if (i != statusList.Count - 1)
				{
					stringBuilder.Append("|");
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<AreaStatus> ConvertStringToAreaStatusList(string statusString)
	{
		List<AreaStatus> list = new List<AreaStatus>();
		if (statusString != string.Empty)
		{
			string[] array = statusString.Split('|');
			string[] array2 = array;
			foreach (string aAreaStatusRefID in array2)
			{
				list.Add(getGameData().getAreaStatusByRefID(aAreaStatusRefID));
			}
		}
		return list;
	}

	public static string ConvertStringListToString(List<string> stringList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (stringList.Count > 0)
		{
			for (int i = 0; i < stringList.Count; i++)
			{
				stringBuilder.Append(stringList[i]);
				stringBuilder.Append("|");
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<string> ConvertStringToStringList(string aString)
	{
		List<string> list = new List<string>();
		if (aString != string.Empty)
		{
			string[] array = aString.Split('|');
			for (int i = 0; i < array.Length - 1; i++)
			{
				string item = array[i];
				list.Add(item);
			}
		}
		return list;
	}

	public static string ConvertFloatListToString(List<float> floatList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (floatList.Count > 0)
		{
			for (int i = 0; i < floatList.Count; i++)
			{
				stringBuilder.Append(floatList[i].ToString());
				if (i != floatList.Count - 1)
				{
					stringBuilder.Append("|");
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<float> ConvertStringToFloatList(string aString)
	{
		List<float> list = new List<float>();
		if (aString != string.Empty)
		{
			string[] array = aString.Split('|');
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text != string.Empty)
				{
					debug(parseFloat(text).ToString());
					list.Add(parseFloat(text));
				}
			}
		}
		return list;
	}

	public static string ConvertIntListToString(List<int> intList)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string result = string.Empty;
		if (intList.Count > 0)
		{
			for (int i = 0; i < intList.Count; i++)
			{
				stringBuilder.Append(intList[i].ToString());
				if (i != intList.Count - 1)
				{
					stringBuilder.Append("|");
				}
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	public static List<int> ConvertStringToIntList(string aString)
	{
		List<int> list = new List<int>();
		if (aString != string.Empty)
		{
			string[] array = aString.Split('|');
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text != string.Empty)
				{
					list.Add(parseInt(text));
				}
			}
		}
		return list;
	}

	public static string Md5Sum(string strToEncrypt)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(strToEncrypt);
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	public static string Encrypt(string toEncrypt, string key)
	{
		string empty = string.Empty;
		bool flag = false;
		if (toEncrypt.StartsWith("'") && toEncrypt.EndsWith("'"))
		{
			flag = true;
			toEncrypt = toEncrypt.Remove(0, 1);
			toEncrypt = toEncrypt.Remove(toEncrypt.Length - 1, 1);
		}
		if (toEncrypt.Contains("''"))
		{
			toEncrypt = toEncrypt.Replace("''", "'");
		}
		byte[] bytes = Encoding.UTF8.GetBytes(key);
		byte[] bytes2 = Encoding.UTF8.GetBytes(toEncrypt);
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Key = bytes;
		rijndaelManaged.Mode = CipherMode.ECB;
		rijndaelManaged.Padding = PaddingMode.PKCS7;
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
		byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
		string text = empty + Convert.ToBase64String(array, 0, array.Length) + empty;
		if (text.Contains("'"))
		{
			text.Replace("'", "''");
		}
		if (flag)
		{
			return "'" + text + "'";
		}
		return text;
	}

	public static string Decrypt(string toDecrypt, string key)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(key);
		byte[] array = Convert.FromBase64String(toDecrypt);
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Key = bytes;
		rijndaelManaged.Mode = CipherMode.ECB;
		rijndaelManaged.Padding = PaddingMode.PKCS7;
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
		byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
		return Encoding.UTF8.GetString(bytes2);
	}

	public static string Base64Encode(string plainText)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(plainText);
		return Convert.ToBase64String(bytes);
	}

	public static string take5FormatSendData(Dictionary<string, string> aDict)
	{
		string text = string.Empty;
		int num = 0;
		foreach (string key in aDict.Keys)
		{
			if (num == 0)
			{
				text = text + key + "yyy" + aDict[key];
			}
			else
			{
				string text2 = text;
				text = text2 + "zzz" + key + "yyy" + aDict[key];
			}
			num++;
		}
		return text;
	}

	public static Dictionary<string, string> take5FormatRetrieveData(string aData)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string[] array = aData.Split("zzz".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = array;
		foreach (string text in array2)
		{
			string[] array3 = text.Split("yyy".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			if (array3.Length > 1)
			{
				dictionary.Add(array3[0], array3[1]);
			}
			else
			{
				dictionary.Add(array3[0], string.Empty);
			}
		}
		return dictionary;
	}

	public static void openUrl(string url)
	{
		if (SteamController.Initialized && SteamUtils.IsOverlayEnabled())
		{
			SteamFriends.ActivateGameOverlayToWebPage(url);
		}
		else
		{
			Application.OpenURL(url);
		}
	}

	public static string getSystemFolderPath()
	{
		string empty = string.Empty;
		empty = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SavedGames/";
		debug("System folder path: " + empty);
		return empty;
	}
}
