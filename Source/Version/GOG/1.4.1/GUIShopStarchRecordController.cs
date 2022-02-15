using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIShopStarchRecordController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel titleLabel;

	private UIButton starchReportTab;

	private UIButton statisticsTab;

	private UIButton progressTab;

	private string currentTab;

	private UISprite starchReportBg;

	private UILabel yearLabel;

	private UIButton prevYearArrow;

	private UIButton nextYearArrow;

	private int currentYear;

	private GameObject chart;

	private GameObject chartAxes;

	private GameObject chartPointer;

	private GameObject monthRecordBg;

	private UILabel monthRecordTitleLabel;

	private UIGrid monthRecordGrid;

	private UIScrollBar monthRecordScrollbar;

	private UILabel totalProfitTitle;

	private UILabel totalProfitValue;

	private UILabel noRecordLabel;

	private int displayMonth;

	private List<ShopMonthlyStarch> displayRecordList;

	private List<int> monthProfit;

	private List<int> monthExpense;

	private UISprite statisticsBg;

	private bool isStatsCalculated;

	private Dictionary<string, int> statsList;

	private float displayStat;

	private UISprite progressBg;

	private bool isProgressCalculated;

	private Dictionary<string, int> progressList;

	private float displayProgress;

	private bool isOverallProfit;

	private UILabel patataBubble;

	private UITexture patataTexture;

	private Vector3 onScreenPos;

	private Vector3 offScreenPos;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("ShortBubble").GetComponent<TooltipTextScript>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "RecordTitle_bg/RecordTitle_label").GetComponent<UILabel>();
		starchReportTab = commonScreenObject.findChild(base.gameObject, "ShopProfileTabs/StarchReportTab_bg").GetComponent<UIButton>();
		statisticsTab = commonScreenObject.findChild(base.gameObject, "ShopProfileTabs/ShopStatsTab_bg").GetComponent<UIButton>();
		progressTab = commonScreenObject.findChild(base.gameObject, "ShopProfileTabs/ProgressTab_bg").GetComponent<UIButton>();
		currentTab = "REPORT";
		starchReportBg = commonScreenObject.findChild(base.gameObject, "StarchReport_bg").GetComponent<UISprite>();
		yearLabel = commonScreenObject.findChild(starchReportBg.gameObject, "Year_bg/Year_label").GetComponent<UILabel>();
		prevYearArrow = commonScreenObject.findChild(starchReportBg.gameObject, "Year_bg/PreviousYear_arrow").GetComponent<UIButton>();
		nextYearArrow = commonScreenObject.findChild(starchReportBg.gameObject, "Year_bg/NextYear_arrow").GetComponent<UIButton>();
		currentYear = 1;
		chart = commonScreenObject.findChild(starchReportBg.gameObject, "Chart_bg/Chart").gameObject;
		chartAxes = commonScreenObject.findChild(chart.gameObject, "Chart_axes").gameObject;
		chartPointer = commonScreenObject.findChild(chart.gameObject, "ChartPointer_bg").gameObject;
		monthRecordBg = commonScreenObject.findChild(starchReportBg.gameObject, "MonthRecord_bg").gameObject;
		monthRecordTitleLabel = commonScreenObject.findChild(monthRecordBg, "MonthRecordTitle_label").GetComponent<UILabel>();
		monthRecordGrid = commonScreenObject.findChild(monthRecordBg, "MonthRecord_clipPanel/MonthRecord_grid").GetComponent<UIGrid>();
		monthRecordScrollbar = commonScreenObject.findChild(monthRecordBg, "record_scrollbar").GetComponent<UIScrollBar>();
		totalProfitTitle = commonScreenObject.findChild(starchReportBg.gameObject, "MonthTotalRecord/MonthProfitTitle_label").GetComponent<UILabel>();
		totalProfitValue = commonScreenObject.findChild(starchReportBg.gameObject, "MonthTotalRecord/MonthProfitValue_label").GetComponent<UILabel>();
		noRecordLabel = commonScreenObject.findChild(monthRecordBg, "NoRecord_label").GetComponent<UILabel>();
		displayMonth = -1;
		displayRecordList = new List<ShopMonthlyStarch>();
		monthProfit = new List<int>();
		monthExpense = new List<int>();
		statisticsBg = commonScreenObject.findChild(base.gameObject, "Statistics_bg").GetComponent<UISprite>();
		isStatsCalculated = false;
		statsList = new Dictionary<string, int>();
		displayStat = 0f;
		progressBg = commonScreenObject.findChild(base.gameObject, "Progress_bg").GetComponent<UISprite>();
		isProgressCalculated = false;
		progressList = new Dictionary<string, int>();
		displayProgress = 0f;
		isOverallProfit = true;
		patataBubble = commonScreenObject.findChild(base.gameObject, "PatataBubble_label").GetComponent<UILabel>();
		patataTexture = commonScreenObject.findChild(base.gameObject, "PatataImage_texture").GetComponent<UITexture>();
		onScreenPos = new Vector3(0f, -36f, 0f);
		offScreenPos = new Vector3(0f, 1000f, 0f);
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			audioController.stopNumberUpAudio();
			viewController.closeShopRecordPopup();
			return;
		case "PreviousYear_arrow":
			if (currentYear > 0)
			{
				currentYear--;
				updateDisplayByYear();
			}
			return;
		case "NextYear_arrow":
			if (currentYear < game.getPlayer().getPlayerYear())
			{
				currentYear++;
				updateDisplayByYear();
			}
			return;
		case "StarchReportTab_bg":
			currentTab = "REPORT";
			showTab();
			return;
		case "ShopStatsTab_bg":
			currentTab = "STATS";
			showTab();
			return;
		case "ProgressTab_bg":
			currentTab = "PROGRESS";
			showTab();
			return;
		case "Month1":
			showMonth(0);
			return;
		case "Month2":
			showMonth(1);
			return;
		case "Month3":
			showMonth(2);
			return;
		case "Month4":
			showMonth(3);
			return;
		}
		string[] array = gameObjectName.Split('_');
		if (!(array[0] == "ChartItemObj"))
		{
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			string[] array = hoverName.Split('_');
			if (!(array[0] == "ChartItemObj"))
			{
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted() && !GameObject.Find("blackmask_popup").GetComponent<BoxCollider>().enabled)
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		titleLabel.text = gameData.getTextByRefId("shopProfile01").ToUpper(CultureInfo.InvariantCulture);
		totalProfitTitle.text = gameData.getTextByRefId("shopProfile02").ToUpper(CultureInfo.InvariantCulture);
		nextYearArrow.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("shopProfile36");
		prevYearArrow.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("shopProfile35");
		starchReportTab.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("starchReport01");
		statisticsTab.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("starchReport02");
		progressTab.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("starchReport03");
		UILabel component = commonScreenObject.findChild(starchReportBg.gameObject, "Chart_bg/EarningsKey_sprite/EarningsKey_label").GetComponent<UILabel>();
		UILabel component2 = commonScreenObject.findChild(starchReportBg.gameObject, "Chart_bg/ExpenseKey_sprite/ExpenseKey_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("starchReport04");
		component2.text = gameData.getTextByRefId("starchReport05");
		currentTab = "REPORT";
		showTab();
	}

	private void showTab()
	{
		switch (currentTab)
		{
		case "REPORT":
			showReportTab();
			hideStatsTab();
			hideProgressTab();
			break;
		case "STATS":
			showStatsTab();
			hideReportTab();
			hideProgressTab();
			break;
		case "PROGRESS":
			showProgressTab();
			hideReportTab();
			hideStatsTab();
			break;
		}
		showPatata();
	}

	private void showReportTab()
	{
		audioController.stopNumberUpAudio();
		starchReportTab.isEnabled = false;
		starchReportBg.transform.localPosition = onScreenPos;
		Player player = game.getPlayer();
		currentYear = player.getPlayerYear();
		updateDisplayByYear();
	}

	private void updateDisplayByYear()
	{
		showYear();
		makeChart();
		displayMonth = -1;
		Player player = game.getPlayer();
		int aMonth = 3;
		if (currentYear == player.getPlayerYear())
		{
			aMonth = player.getPlayerMonths() % 4;
		}
		showMonth(aMonth);
	}

	private void hideReportTab()
	{
		starchReportTab.isEnabled = true;
		starchReportBg.transform.localPosition = offScreenPos;
	}

	private void setReportColliders(bool aSet)
	{
		BoxCollider[] componentsInChildren = starchReportBg.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider boxCollider in componentsInChildren)
		{
			boxCollider.enabled = aSet;
		}
	}

	private void showYear()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		yearLabel.text = gameData.getTextByRefIdWithDynText("yearLong", "[year]", (currentYear + 1).ToString());
		if (currentYear > 0)
		{
			prevYearArrow.isEnabled = true;
		}
		else
		{
			prevYearArrow.isEnabled = false;
		}
		if (currentYear < player.getPlayerYear())
		{
			nextYearArrow.isEnabled = true;
		}
		else
		{
			nextYearArrow.isEnabled = false;
		}
	}

	private void makeChart()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		int num = int.MinValue;
		int num2 = int.MaxValue;
		int num3 = currentYear * 4;
		int num4 = num3 + 3;
		if (currentYear == player.getPlayerYear())
		{
			num4 = player.getPlayerMonths();
		}
		for (int i = num3; i <= num4; i++)
		{
			List<ShopMonthlyStarch> shopMonthlyStarchByMonth = player.getShopMonthlyStarchByMonth(i);
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			foreach (ShopMonthlyStarch item in shopMonthlyStarchByMonth)
			{
				if (item.getAmount() > 0)
				{
					num5 += item.getAmount();
				}
				else
				{
					num6 += item.getAmount();
				}
			}
			num7 = num5 + num6;
			list.Add(num5);
			list2.Add(num6);
			if (num < num5)
			{
				num = num5;
			}
			if (num2 > num6)
			{
				num2 = num6;
			}
		}
		int num8 = makeChartAxes(Mathf.Min(0, num2), Mathf.Max(0, num));
		monthProfit = new List<int>();
		monthExpense = new List<int>();
		for (int j = 0; j < 4; j++)
		{
			GameObject gameObject = commonScreenObject.findChild(chart, "Month" + (j + 1)).gameObject;
			UIProgressBar component = commonScreenObject.findChild(gameObject, "Month" + (j + 1) + "Profit_bar").GetComponent<UIProgressBar>();
			UIProgressBar component2 = commonScreenObject.findChild(gameObject, "Month" + (j + 1) + "Expense_bar").GetComponent<UIProgressBar>();
			UILabel component3 = commonScreenObject.findChild(gameObject, "Month" + (j + 1) + "_label").GetComponent<UILabel>();
			if (list.Count > j)
			{
				gameObject.GetComponent<BoxCollider>().enabled = true;
				component.alpha = 1f;
				component2.alpha = 1f;
				component3.alpha = 1f;
				component.value = (float)list[j] / (float)num8;
				component2.value = (float)(list2[j] * -1) / (float)num8;
				component3.text = gameData.getTextByRefIdWithDynText("monthLong", "[month]", (j + 1).ToString());
				monthProfit.Add(list[j]);
				monthExpense.Add(list2[j]);
			}
			else
			{
				gameObject.GetComponent<BoxCollider>().enabled = false;
				component.alpha = 0f;
				component2.alpha = 0f;
				component3.alpha = 0f;
				monthProfit.Add(0);
				monthExpense.Add(0);
			}
		}
	}

	private int makeChartAxes(int aMin, int aMax)
	{
		int a = (aMax / 5000 + 1) * 5000;
		int b = (-aMin / 5000 + 1) * 5000;
		int num = Mathf.Max(a, b);
		UILabel[] componentsInChildren = chartAxes.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "Axis0_label":
				uILabel.text = "0";
				break;
			case "AxisNeg2_label":
				uILabel.text = (-num / 5).ToString();
				break;
			case "AxisNeg4_label":
				uILabel.text = (-num / 5 * 2).ToString();
				break;
			case "AxisNeg6_label":
				uILabel.text = (-num / 5 * 3).ToString();
				break;
			case "AxisNeg8_label":
				uILabel.text = (-num / 5 * 4).ToString();
				break;
			case "AxisNeg10_label":
				uILabel.text = (-num / 5 * 5).ToString();
				break;
			case "AxisPos2_label":
				uILabel.text = (num / 5).ToString();
				break;
			case "AxisPos4_label":
				uILabel.text = (num / 5 * 2).ToString();
				break;
			case "AxisPos6_label":
				uILabel.text = (num / 5 * 3).ToString();
				break;
			case "AxisPos8_label":
				uILabel.text = (num / 5 * 4).ToString();
				break;
			case "AxisPos10_label":
				uILabel.text = (num / 5 * 5).ToString();
				break;
			}
		}
		return num;
	}

	private void updateChartBars()
	{
		for (int i = 0; i < 4; i++)
		{
			string aPath = "Month" + (i + 1) + "/Month" + (i + 1) + "Profit_bar/Month" + (i + 1) + "Profit_fg";
			string aPath2 = "Month" + (i + 1) + "/Month" + (i + 1) + "Expense_bar/Month" + (i + 1) + "Expense_fg";
			UISprite component = commonScreenObject.findChild(chart, aPath).GetComponent<UISprite>();
			UISprite component2 = commonScreenObject.findChild(chart, aPath2).GetComponent<UISprite>();
			if (displayMonth == i)
			{
				component.spriteName = "green_select";
				component2.spriteName = "purple_select";
			}
			else
			{
				component.spriteName = "green_normal";
				component2.spriteName = "purple_normal";
			}
		}
	}

	private void updatePointer()
	{
		Vector3 localPosition = new Vector3(2000f, 2000f, 0f);
		switch (displayMonth)
		{
		case 0:
			localPosition = commonScreenObject.findChild(chart, "Month1").transform.localPosition;
			break;
		case 1:
			localPosition = commonScreenObject.findChild(chart, "Month2").transform.localPosition;
			break;
		case 2:
			localPosition = commonScreenObject.findChild(chart, "Month3").transform.localPosition;
			break;
		case 3:
			localPosition = commonScreenObject.findChild(chart, "Month4").transform.localPosition;
			break;
		}
		localPosition.x -= 4f;
		localPosition.y += 10f;
		chartPointer.transform.localPosition = localPosition;
		commonScreenObject.findChild(chartPointer, "PointerProfit_label").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(monthProfit[displayMonth]);
		commonScreenObject.findChild(chartPointer, "PointerExpense_label").GetComponent<UILabel>().text = "-$" + CommonAPI.formatNumber(monthExpense[displayMonth] * -1);
	}

	private void showMonth(int aMonth)
	{
		if (aMonth != displayMonth)
		{
			setMonth(aMonth);
			showMonthSummary();
			showMonthDetails();
			updateChartBars();
			updatePointer();
		}
	}

	private void setMonth(int aMonth)
	{
		displayMonth = aMonth;
		List<ShopMonthlyStarch> shopMonthlyStarchByMonth = game.getPlayer().getShopMonthlyStarchByMonth(currentYear * 4 + displayMonth);
		List<ShopMonthlyStarch> list = new List<ShopMonthlyStarch>();
		List<ShopMonthlyStarch> list2 = new List<ShopMonthlyStarch>();
		foreach (ShopMonthlyStarch item in shopMonthlyStarchByMonth)
		{
			if (item.getAmount() > 0)
			{
				list.Add(item);
			}
			else if (item.getAmount() < 0)
			{
				list2.Add(item);
			}
		}
		displayRecordList = new List<ShopMonthlyStarch>();
		displayRecordList.AddRange(list);
		displayRecordList.AddRange(list2);
	}

	private void showMonthSummary()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (ShopMonthlyStarch displayRecord in displayRecordList)
		{
			if (displayRecord.getAmount() > 0)
			{
				num += displayRecord.getAmount();
			}
			else
			{
				num2 += displayRecord.getAmount();
			}
		}
		num3 = num + num2;
		if (num3 >= 0)
		{
			totalProfitValue.text = "[56AE59]$" + CommonAPI.formatNumber(num3) + "[-]";
			isOverallProfit = true;
		}
		else
		{
			totalProfitValue.text = "[D484F5]-$" + CommonAPI.formatNumber(num3 * -1) + "[-]";
			isOverallProfit = false;
		}
	}

	private string getMonthString(int playerMonth)
	{
		GameData gameData = game.getGameData();
		int num = currentYear + 1;
		int num2 = displayMonth + 1;
		string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("yearLong", "[year]", num.ToString());
		string textByRefIdWithDynText2 = gameData.getTextByRefIdWithDynText("monthLong", "[month]", num2.ToString());
		return textByRefIdWithDynText + " " + textByRefIdWithDynText2;
	}

	private void showMonthDetails()
	{
		GameData gameData = game.getGameData();
		monthRecordTitleLabel.text = gameData.getTextByRefIdWithDynText("shopProfile03", "[date]", getMonthString(displayMonth));
		int num = 0;
		foreach (Transform child in monthRecordGrid.GetChildList())
		{
			num++;
			if (num > displayRecordList.Count)
			{
				commonScreenObject.destroyPrefabImmediate(child.gameObject);
			}
		}
		int num2 = 0;
		foreach (ShopMonthlyStarch displayRecord in displayRecordList)
		{
			GameObject aObject = commonScreenObject.createPrefab(monthRecordGrid.gameObject, "MonthRecordObj_" + num2, "Prefab/Shop/MonthRecordObj", Vector3.zero, Vector3.one, Vector3.zero);
			string text = displayRecord.getRecordName();
			if (text == string.Empty)
			{
				text = CommonAPI.convertShopStarchRecordTypeToString(displayRecord.getRecordType());
			}
			commonScreenObject.findChild(aObject, "EntryName_label").GetComponent<UILabel>().text = text;
			int amount = displayRecord.getAmount();
			if (amount > 0)
			{
				commonScreenObject.findChild(aObject, "EntryValue_label").GetComponent<UILabel>().text = "[56AE59]$" + CommonAPI.formatNumber(amount) + "[-]";
			}
			else if (amount < 0)
			{
				commonScreenObject.findChild(aObject, "EntryValue_label").GetComponent<UILabel>().text = "[D484F5]-$" + CommonAPI.formatNumber(amount * -1) + "[-]";
			}
			num2++;
		}
		if (displayRecordList.Count == 0)
		{
			noRecordLabel.text = gameData.getTextByRefId("shopProfile34");
		}
		else
		{
			noRecordLabel.text = string.Empty;
		}
		monthRecordGrid.Reposition();
		monthRecordGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		monthRecordGrid.enabled = true;
		monthRecordScrollbar.value = 0f;
	}

	private void showStatsTab()
	{
		GameData gameData = game.getGameData();
		statisticsTab.isEnabled = false;
		statisticsBg.transform.localPosition = onScreenPos;
		if (!isStatsCalculated)
		{
			calculateStats();
		}
		UILabel[] componentsInChildren = statisticsBg.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "TotalExp_title":
				uILabel.text = gameData.getTextByRefId("shopProfile04");
				break;
			case "TotalExp_value":
				uILabel.text = "0";
				break;
			case "MaxLevelHero_title":
				uILabel.text = gameData.getTextByRefId("shopProfile05");
				break;
			case "MaxLevelHero_value":
				uILabel.text = "0";
				break;
			case "CompleteRequest_title":
				uILabel.text = gameData.getTextByRefId("shopProfile06");
				break;
			case "CompleteRequest_value":
				uILabel.text = "0";
				break;
			case "AcceptedRequest_title":
				uILabel.text = gameData.getTextByRefId("shopProfile07");
				break;
			case "AcceptedRequest_value":
				uILabel.text = "0";
				break;
			case "CompleteLegendary_title":
				uILabel.text = gameData.getTextByRefId("shopProfile08");
				break;
			case "CompleteLegendary_value":
				uILabel.text = "0";
				break;
			case "LegendaryAttempt_title":
				uILabel.text = gameData.getTextByRefId("shopProfile09");
				break;
			case "LegendaryAttempt_value":
				uILabel.text = "0";
				break;
			case "CustomersTitle_label":
				uILabel.text = gameData.getTextByRefId("shopProfile10").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "WeaponsForged_title":
				uILabel.text = gameData.getTextByRefId("shopProfile11");
				break;
			case "WeaponsForged_value":
				uILabel.text = "0";
				break;
			case "TotalStarch_title":
				uILabel.text = gameData.getTextByRefId("shopProfile12");
				break;
			case "TotalStarch_value":
				uILabel.text = "$0";
				break;
			case "HighestSellPrice_title":
				uILabel.text = gameData.getTextByRefId("shopProfile13");
				break;
			case "HighestSellPrice_value":
				uILabel.text = "$0";
				break;
			case "WeaponSold_title":
				uILabel.text = gameData.getTextByRefId("shopProfile14");
				break;
			case "WeaponSold_value":
				uILabel.text = "0";
				break;
			case "Research_title":
				uILabel.text = gameData.getTextByRefId("shopProfile15");
				break;
			case "Research_value":
				uILabel.text = "0";
				break;
			case "CompleteContract_title":
				uILabel.text = gameData.getTextByRefId("shopProfile16");
				break;
			case "CompleteContract_value":
				uILabel.text = "0";
				break;
			case "AcceptedContract_title":
				uILabel.text = gameData.getTextByRefId("shopProfile17");
				break;
			case "AcceptedContract_value":
				uILabel.text = "0";
				break;
			case "WeaponsTitle_label":
				uILabel.text = gameData.getTextByRefId("shopProfile18").ToUpper(CultureInfo.InvariantCulture);
				break;
			}
		}
		displayStat = 0f;
		audioController.playNumberUpAudio();
		StartCoroutine(updateStats());
	}

	private string getDisplayStatNumString(string statKey, float displayNum)
	{
		if (displayNum >= 100f)
		{
			return CommonAPI.formatNumber(statsList[statKey]);
		}
		float num = (float)statsList[statKey] * displayNum / 100f + 1f;
		int aNumber = Mathf.Min((int)num, statsList[statKey]);
		return CommonAPI.formatNumber(aNumber);
	}

	public IEnumerator updateStats()
	{
		while (displayStat <= 100f && currentTab == "STATS")
		{
			yield return new WaitForSeconds(0.01f);
			displayStat += 2f;
			if (displayStat == 100f)
			{
				audioController.stopNumberUpAudio();
			}
			UILabel[] componentsInChildren = statisticsBg.GetComponentsInChildren<UILabel>();
			foreach (UILabel uILabel in componentsInChildren)
			{
				switch (uILabel.gameObject.name)
				{
				case "TotalExp_value":
					uILabel.text = getDisplayStatNumString("TotalExp", displayStat);
					break;
				case "MaxLevelHero_value":
					uILabel.text = getDisplayStatNumString("MaxLevelHero", displayStat);
					break;
				case "CompleteRequest_value":
					uILabel.text = getDisplayStatNumString("CompleteRequest", displayStat);
					break;
				case "AcceptedRequest_value":
					uILabel.text = getDisplayStatNumString("AcceptedRequest", displayStat);
					break;
				case "CompleteLegendary_value":
					uILabel.text = getDisplayStatNumString("CompleteLegendary", displayStat);
					break;
				case "LegendaryAttempt_value":
					uILabel.text = getDisplayStatNumString("LegendaryAttempt", displayStat);
					break;
				case "WeaponsForged_value":
					uILabel.text = getDisplayStatNumString("WeaponsForged", displayStat);
					break;
				case "TotalStarch_value":
					uILabel.text = "$" + getDisplayStatNumString("TotalStarch", displayStat);
					break;
				case "HighestSellPrice_value":
					uILabel.text = "$" + getDisplayStatNumString("HighestSellPrice", displayStat);
					break;
				case "WeaponSold_value":
					uILabel.text = getDisplayStatNumString("WeaponSold", displayStat);
					break;
				case "Research_value":
					uILabel.text = getDisplayStatNumString("Research", displayStat);
					break;
				case "CompleteContract_value":
					uILabel.text = getDisplayStatNumString("CompleteContract", displayStat);
					break;
				case "AcceptedContract_value":
					uILabel.text = getDisplayStatNumString("AcceptedContract", displayStat);
					break;
				}
			}
		}
	}

	private void calculateStats()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		statsList["TotalExp"] = player.getTotalHeroExpGain();
		statsList["MaxLevelHero"] = gameData.getMaxLevelHeroList().Count;
		statsList["CompleteRequest"] = player.getCompletedRequestList().Count;
		statsList["AcceptedRequest"] = player.getTotalRequestCount();
		statsList["CompleteLegendary"] = player.getCompletedLegendaryList().Count;
		statsList["LegendaryAttempt"] = player.getLegendaryAttemptCount();
		statsList["WeaponsForged"] = player.getCompletedWeaponCount();
		statsList["TotalStarch"] = player.getTotalEarnings();
		statsList["HighestSellPrice"] = player.getHighestSellPriceProject().getFinalPrice();
		statsList["WeaponSold"] = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, "00000").Count;
		statsList["Research"] = player.getResearchCount();
		statsList["CompleteContract"] = gameData.getTotalContractCompleteCount();
		statsList["AcceptedContract"] = gameData.getTotalContractAttempts();
	}

	private void hideStatsTab()
	{
		statisticsTab.isEnabled = true;
		statisticsBg.transform.localPosition = offScreenPos;
	}

	private void showProgressTab()
	{
		GameData gameData = game.getGameData();
		progressTab.isEnabled = false;
		progressBg.transform.localPosition = onScreenPos;
		if (!isProgressCalculated)
		{
			calculateProgress();
		}
		UIProgressBar[] componentsInChildren = progressBg.GetComponentsInChildren<UIProgressBar>();
		foreach (UIProgressBar uIProgressBar in componentsInChildren)
		{
			uIProgressBar.value = 0f;
		}
		UISprite[] componentsInChildren2 = progressBg.GetComponentsInChildren<UISprite>();
		foreach (UISprite uISprite in componentsInChildren2)
		{
			if (uISprite.gameObject.name.Contains("circleFg"))
			{
				uISprite.fillAmount = 0f;
			}
		}
		UILabel[] componentsInChildren3 = progressBg.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren3)
		{
			if (uILabel.gameObject.name.Contains("_max"))
			{
				string[] array = uILabel.gameObject.name.Split('_');
				if (progressList.ContainsKey(array[0]))
				{
					uILabel.text = gameData.getTextByRefIdWithDynText("shopProfile19", "[max]", progressList[array[0] + "Max"].ToString());
				}
			}
			else if (uILabel.gameObject.name.Contains("_label"))
			{
				switch (uILabel.gameObject.name)
				{
				case "GoalsAchieved_label":
					uILabel.text = gameData.getTextByRefId("shopProfile20");
					break;
				case "SmithsHired_label":
					uILabel.text = gameData.getTextByRefId("shopProfile21");
					break;
				case "AreasUnlocked_label":
					uILabel.text = gameData.getTextByRefId("shopProfile22");
					break;
				case "WeaponsUnlocked_label":
					uILabel.text = gameData.getTextByRefId("shopProfile23");
					break;
				case "NormalHeroes_label":
					uILabel.text = gameData.getTextByRefId("shopProfile24");
					break;
				case "LegendaryHeroes_label":
					uILabel.text = gameData.getTextByRefId("shopProfile25");
					break;
				case "LegendarySmiths_label":
					uILabel.text = gameData.getTextByRefId("shopProfile26");
					break;
				case "MaterialsFound_label":
					uILabel.text = gameData.getTextByRefId("shopProfile27");
					break;
				case "Enchantments_label":
					uILabel.text = gameData.getTextByRefId("shopProfile28");
					break;
				case "RelicsFound_label":
					uILabel.text = gameData.getTextByRefId("shopProfile29");
					break;
				case "NormalDeco_label":
					uILabel.text = gameData.getTextByRefId("shopProfile30");
					break;
				case "SpecialDeco_label":
					uILabel.text = gameData.getTextByRefId("shopProfile31");
					break;
				case "SpecialEnchantments_label":
					uILabel.text = gameData.getTextByRefId("shopProfile32");
					break;
				}
			}
			else if (uILabel.gameObject.name.Contains("_value"))
			{
				uILabel.text = "0";
			}
		}
		displayProgress = 0f;
		audioController.playNumberUpAudio();
		StartCoroutine(updateProgress());
	}

	private string getDisplayProgressNumString(string statKey, float displayNum)
	{
		if (displayNum >= 100f)
		{
			return CommonAPI.formatNumber(progressList[statKey]);
		}
		float num = (float)progressList[statKey] * displayNum / 100f + 1f;
		int aNumber = Mathf.Min((int)num, progressList[statKey]);
		return CommonAPI.formatNumber(aNumber);
	}

	private float getDisplayProgressFloat(string statKey, float displayNum)
	{
		if (displayNum >= 100f)
		{
			return (float)progressList[statKey] / (float)progressList[statKey + "Max"];
		}
		float num = (float)progressList[statKey] / (float)progressList[statKey + "Max"];
		float a = num * displayNum / 100f;
		return Mathf.Min(a, num);
	}

	public IEnumerator updateProgress()
	{
		while (displayProgress <= 100f && currentTab == "PROGRESS")
		{
			yield return new WaitForSeconds(0.01f);
			displayProgress += 2f;
			if (displayProgress == 100f)
			{
				audioController.stopNumberUpAudio();
			}
			UIProgressBar[] componentsInChildren = progressBg.GetComponentsInChildren<UIProgressBar>();
			foreach (UIProgressBar uIProgressBar in componentsInChildren)
			{
				string[] array = uIProgressBar.gameObject.name.Split('_');
				uIProgressBar.value = getDisplayProgressFloat(array[0], displayProgress);
			}
			UISprite[] componentsInChildren2 = progressBg.GetComponentsInChildren<UISprite>();
			foreach (UISprite uISprite in componentsInChildren2)
			{
				if (uISprite.gameObject.name.Contains("circleFg"))
				{
					string[] array2 = uISprite.gameObject.name.Split('_');
					uISprite.fillAmount = getDisplayProgressFloat(array2[0], displayProgress);
				}
			}
			UILabel[] componentsInChildren3 = progressBg.GetComponentsInChildren<UILabel>();
			foreach (UILabel uILabel in componentsInChildren3)
			{
				switch (uILabel.gameObject.name)
				{
				case "GoalsAchieved_value":
					uILabel.text = getDisplayProgressNumString("GoalsAchieved", displayProgress);
					break;
				case "SmithsHired_value":
					uILabel.text = getDisplayProgressNumString("SmithsHired", displayProgress);
					break;
				case "AreasUnlocked_value":
					uILabel.text = getDisplayProgressNumString("AreasUnlocked", displayProgress);
					break;
				case "WeaponsUnlocked_value":
					uILabel.text = getDisplayProgressNumString("WeaponsUnlocked", displayProgress);
					break;
				case "NormalHeroes_value":
					uILabel.text = getDisplayProgressNumString("NormalHeroes", displayProgress);
					break;
				case "LegendaryHeroes_value":
					uILabel.text = getDisplayProgressNumString("LegendaryHeroes", displayProgress);
					break;
				case "LegendarySmiths_value":
					uILabel.text = getDisplayProgressNumString("LegendarySmiths", displayProgress);
					break;
				case "MaterialsFound_value":
					uILabel.text = getDisplayProgressNumString("MaterialsFound", displayProgress);
					break;
				case "Enchantments_value":
					uILabel.text = getDisplayProgressNumString("Enchantments", displayProgress);
					break;
				case "RelicsFound_value":
					uILabel.text = getDisplayProgressNumString("RelicsFound", displayProgress);
					break;
				case "NormalDeco_value":
					uILabel.text = getDisplayProgressNumString("NormalDeco", displayProgress);
					break;
				case "SpecialDeco_value":
					uILabel.text = getDisplayProgressNumString("SpecialDeco", displayProgress);
					break;
				case "SpecialEnchantments_value":
					uILabel.text = getDisplayProgressNumString("SpecialEnchantments", displayProgress);
					break;
				}
			}
		}
	}

	private void calculateProgress()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string objectiveSet = gameScenarioByRefId.getObjectiveSet();
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		progressList["GoalsAchieved"] = gameData.getSucceededObjectiveList().Count;
		progressList["GoalsAchievedMax"] = gameData.getObjectiveList(objectiveSet, includeNotCounted: false).Count;
		progressList["SmithsHired"] = gameData.getUniqueHiredSmithList(includeNormal: true, includeLegendary: false).Count;
		progressList["SmithsHiredMax"] = gameData.getSmithList(checkDLC: true, includeNormal: true, includeLegendary: false, itemLockSet).Count;
		progressList["AreasUnlocked"] = gameData.getUnlockedAreaList(itemLockSet).Count;
		progressList["AreasUnlockedMax"] = gameData.getAreaList(itemLockSet).Count;
		progressList["WeaponsUnlocked"] = player.getUnlockedWeaponList().Count;
		progressList["WeaponsUnlockedMax"] = gameData.getWeaponList(checkDLC: true, itemLockSet).Count;
		progressList["NormalHeroes"] = gameData.getHeroCustomerList().Count;
		progressList["NormalHeroesMax"] = gameData.getHeroList(itemLockSet).Count;
		progressList["LegendaryHeroes"] = gameData.getLegendaryHeroCustomerList().Count;
		progressList["LegendaryHeroesMax"] = gameData.getLegendaryHeroList(checkDLC: true, itemLockSet).Count;
		progressList["LegendarySmiths"] = gameData.getUniqueHiredSmithList(includeNormal: false).Count;
		progressList["LegendarySmithsMax"] = gameData.getSmithList(checkDLC: true, includeNormal: false, includeLegendary: true, itemLockSet).Count;
		progressList["MaterialsFound"] = gameData.getOwnedItemListByType(ItemType.ItemTypeMaterial, includeSpecial: true, includeNormal: true).Count;
		progressList["MaterialsFoundMax"] = gameData.getItemListByType(ItemType.ItemTypeMaterial, ownedOnly: false, includeSpecial: true, itemLockSet).Count;
		progressList["Enchantments"] = gameData.getOwnedItemListByType(ItemType.ItemTypeEnhancement, includeSpecial: false, includeNormal: true).Count;
		progressList["EnchantmentsMax"] = gameData.getItemListByType(ItemType.ItemTypeEnhancement, ownedOnly: false, includeSpecial: false, itemLockSet).Count;
		progressList["RelicsFound"] = gameData.getOwnedItemListByType(ItemType.ItemTypeRelic, includeSpecial: true, includeNormal: true).Count;
		progressList["RelicsFoundMax"] = gameData.getItemListByType(ItemType.ItemTypeRelic, ownedOnly: false, includeSpecial: true, itemLockSet).Count;
		progressList["NormalDeco"] = gameData.getNormalDecorationList(ownedOnly: true, itemLockSet).Count;
		progressList["NormalDecoMax"] = gameData.getNormalDecorationList(ownedOnly: false, itemLockSet).Count;
		progressList["SpecialDeco"] = gameData.getSpecialDecorationList(ownedOnly: true, itemLockSet).Count;
		progressList["SpecialDecoMax"] = gameData.getSpecialDecorationList(ownedOnly: false, itemLockSet).Count;
		progressList["SpecialEnchantments"] = gameData.getOwnedItemListByType(ItemType.ItemTypeEnhancement, includeSpecial: true, includeNormal: false).Count;
		progressList["SpecialEnchantmentsMax"] = gameData.getSpecialItemListByType(ItemType.ItemTypeEnhancement, ownedOnly: false, itemLockSet).Count;
	}

	private void hideProgressTab()
	{
		progressTab.isEnabled = true;
		progressBg.transform.localPosition = offScreenPos;
	}

	private void showPatata()
	{
		GameData gameData = game.getGameData();
		switch (currentTab)
		{
		case "REPORT":
			if (isOverallProfit)
			{
				patataBubble.text = gameData.getRandomTextBySetRefId("shopProfileProfit");
				patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-cheerful");
			}
			else
			{
				patataBubble.text = gameData.getRandomTextBySetRefId("shopProfileLoss");
				patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-cry");
			}
			break;
		case "STATS":
			patataBubble.text = gameData.getRandomTextBySetRefId("shopProfileStats");
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-normal");
			break;
		case "PROGRESS":
			patataBubble.text = gameData.getRandomTextBySetRefId("shopProfileProgress");
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-normal");
			break;
		}
	}
}
