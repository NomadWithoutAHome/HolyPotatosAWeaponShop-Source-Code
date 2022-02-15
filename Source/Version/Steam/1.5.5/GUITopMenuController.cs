using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUITopMenuController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private UILabel moneyLabel;

	private UILabel currentSmithCountLabel;

	private UILabel maxSmithCountLabel;

	private UILabel[] clockTimeList;

	private UISprite weatherIcon;

	private UILabel weatherDetailLabel;

	private UILabel yearLabel;

	private UILabel yearValue;

	private UILabel monthLabel;

	private UILabel monthValue;

	private UILabel dayLabel;

	private UILabel dayValue;

	private bool noon;

	private bool evening;

	private bool labelSet;

	private UISprite clockFrameBG;

	private GameObject clockHand;

	private float startingDegree;

	private float degreeMove;

	private Color lightBlue;

	private Color yellow;

	private Color black;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		moneyLabel = GameObject.Find("MoneyLabel").GetComponent<UILabel>();
		currentSmithCountLabel = GameObject.Find("CurrentSmithCountLabel").GetComponent<UILabel>();
		maxSmithCountLabel = GameObject.Find("MaxSmithCountLabel").GetComponent<UILabel>();
		clockTimeList = GameObject.Find("ClockSlot").GetComponentsInChildren<UILabel>();
		weatherIcon = GameObject.Find("WeatherIcon").GetComponent<UISprite>();
		weatherIcon.spriteName = "none";
		weatherDetailLabel = GameObject.Find("WeatherDetailLabel").GetComponent<UILabel>();
		yearLabel = GameObject.Find("YearLabel").GetComponent<UILabel>();
		yearValue = GameObject.Find("YearValue").GetComponent<UILabel>();
		monthLabel = GameObject.Find("MonthLabel").GetComponent<UILabel>();
		monthValue = GameObject.Find("MonthValue").GetComponent<UILabel>();
		dayLabel = GameObject.Find("DayLabel").GetComponent<UILabel>();
		dayValue = GameObject.Find("DayValue").GetComponent<UILabel>();
		labelSet = false;
		clockFrameBG = GameObject.Find("ClockFrameBG").GetComponent<UISprite>();
		clockHand = GameObject.Find("ClockHand");
		startingDegree = -80f;
		degreeMove = 5.33333349f;
		lightBlue = new Color32(28, 197, 253, byte.MaxValue);
		yellow = new Color32(251, byte.MaxValue, 0, byte.MaxValue);
		black = new Color32(0, 0, 0, byte.MaxValue);
	}

	public void refreshPlayerStats()
	{
		Player player = game.getPlayer();
		if (!labelSet)
		{
			GameData gameData = game.getGameData();
			labelSet = true;
			yearLabel.text = gameData.getTextByRefId("YEAR");
			monthLabel.text = gameData.getTextByRefId("MONTH");
			dayLabel.text = gameData.getTextByRefId("DATE");
		}
		List<int> list = CommonAPI.convertHalfHoursToIntList(player.getPlayerTimeLong());
		char[] array = list[4].ToString().ToCharArray();
		string text;
		string text2;
		if (array.Length > 1)
		{
			text = array[0].ToString();
			text2 = array[1].ToString();
		}
		else
		{
			text = "0";
			text2 = array[0].ToString();
		}
		string text3;
		string text4;
		if (list[5] == 1)
		{
			text3 = "3";
			text4 = "0";
		}
		else
		{
			text3 = "0";
			text4 = "0";
		}
		Season seasonByMonth = CommonAPI.getSeasonByMonth(list[1]);
		moneyLabel.text = CommonAPI.formatNumber(player.getPlayerGold());
		currentSmithCountLabel.text = player.getSmithList().Count.ToString();
		maxSmithCountLabel.text = "/" + player.getShopMaxSmith();
		UILabel[] array2 = clockTimeList;
		foreach (UILabel uILabel in array2)
		{
			switch (uILabel.gameObject.name.Split('_')[1])
			{
			case "0":
				uILabel.text = text;
				break;
			case "1":
				uILabel.text = text2;
				break;
			case "2":
				uILabel.text = ":";
				uILabel.GetComponent<TweenAlpha>().ResetToBeginning();
				uILabel.GetComponent<TweenAlpha>().PlayForward();
				break;
			case "3":
				uILabel.text = text3;
				break;
			case "4":
				uILabel.text = text4;
				break;
			}
		}
		weatherIcon.spriteName = CommonAPI.convertSeasonToString(seasonByMonth).ToLower(CultureInfo.InvariantCulture);
		weatherDetailLabel.text = player.getWeather().getWeatherName();
		yearValue.text = (list[0] + 1).ToString();
		monthValue.text = (list[1] + 1).ToString();
		dayValue.text = (list[2] * 7 + list[3] + 1).ToString();
		long num = list[4] * 2 + list[5] - 16;
		if (num < 1)
		{
			clockFrameBG.GetComponent<TweenColor>().enabled = false;
			clockFrameBG.color = lightBlue;
			clockHand.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, startingDegree));
			if (noon)
			{
				noon = false;
			}
			if (evening)
			{
				evening = false;
			}
			return;
		}
		if (num > 8 && num < 16)
		{
			if (!noon)
			{
				noon = true;
				commonScreenObject.tweenColor(clockFrameBG.GetComponent<TweenColor>(), lightBlue, yellow, 2f, null, string.Empty);
			}
		}
		else if (num > 17 && !evening)
		{
			evening = true;
			commonScreenObject.tweenColor(clockFrameBG.GetComponent<TweenColor>(), yellow, black, 2f, null, string.Empty);
		}
		clockHand.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, startingDegree + (float)num * degreeMove));
	}
}
