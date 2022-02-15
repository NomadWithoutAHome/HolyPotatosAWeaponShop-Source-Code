using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIPayDayController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private UILabel confirmLabel;

	private UILabel payDayLabel;

	private UILabel salaryTitleLabel;

	private UILabel nextMthSalaryLabel;

	private UILabel totalSalaryLabel;

	private UIGrid smithListGrid;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		confirmLabel = commonScreenObject.findChild(base.gameObject, "ConfirmButton/ConfirmLabel").GetComponent<UILabel>();
		payDayLabel = commonScreenObject.findChild(base.gameObject, "PayDayBg/PayDayFrame/PayDayLabel").GetComponent<UILabel>();
		salaryTitleLabel = commonScreenObject.findChild(base.gameObject, "SalaryTitleLabel").GetComponent<UILabel>();
		nextMthSalaryLabel = commonScreenObject.findChild(base.gameObject, "NextMthSalaryLabel").GetComponent<UILabel>();
		totalSalaryLabel = commonScreenObject.findChild(base.gameObject, "TotalSalaryFrame/TotalSalaryLabel").GetComponent<UILabel>();
		smithListGrid = commonScreenObject.findChild(base.gameObject, "SmithListBg/Panel_SmithList/SmithListGrid").GetComponent<UIGrid>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "CloseButton":
			audioController.changeBGM(CommonAPI.getSeasonBGM(CommonAPI.getSeasonByMonth(game.getPlayer().getSeasonIndex())));
			viewController.closePayDayPopup();
			break;
		case "ConfirmButton":
			audioController.changeBGM(CommonAPI.getSeasonBGM(CommonAPI.getSeasonByMonth(game.getPlayer().getSeasonIndex())));
			viewController.closePayDayPopup();
			break;
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		audioController.changeBGM("SILENT");
		audioController.playPopupAudio();
		int num = player.getSmithTotalSalary();
		float num2 = player.checkDecoEffect("SALARY", string.Empty);
		int num3 = num;
		int num4 = num;
		string empty = string.Empty;
		if (num2 != 1f)
		{
			num = (int)((float)num * num2);
		}
		confirmLabel.text = gameData.getTextByRefId("menuGeneral04");
		payDayLabel.text = gameData.getTextByRefId("salaryEvent06").ToUpper(CultureInfo.InvariantCulture);
		salaryTitleLabel.text = gameData.getTextByRefId("salaryEvent07");
		nextMthSalaryLabel.text = gameData.getTextByRefId("salaryEvent08");
		totalSalaryLabel.text = CommonAPI.formatNumber(num).ToString();
		List<Smith> smithList = player.getSmithList();
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (Smith item in smithList)
		{
			dictionary.Add(item.getSmithRefId(), item.getSmithSalary());
		}
		int num5 = player.refreshSmithSalaries();
		Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
		foreach (Smith item2 in smithList)
		{
			dictionary2.Add(item2.getSmithRefId(), item2.getSmithSalary());
		}
		foreach (Smith item3 in smithList)
		{
			GameObject aObject = commonScreenObject.createPrefab(smithListGrid.gameObject, "SmithSalary_" + item3.getSmithRefId(), "Prefab/PayDay/PayDaySmithListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "SmithImage_bg/SmithImage_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + item3.getImage());
			commonScreenObject.findChild(aObject, "SmithImage_bg/SmithMood_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(item3.getMoodState()));
			commonScreenObject.findChild(aObject, "SmithName_bg/SmithName_label").GetComponent<UILabel>().text = item3.getSmithName();
			SmithJobClass smithJob = item3.getSmithJob();
			commonScreenObject.findChild(aObject, "SmithLevel_bg/SmithLevel_label").GetComponent<UILabel>().text = smithJob.getSmithJobName() + " Lv" + item3.getSmithLevel();
			commonScreenObject.findChild(aObject, "SmithLevel_bg/SmithLevel_bg").GetComponent<UISprite>().fillAmount = (float)item3.getSmithExp() / (float)item3.getMaxExp();
			if (dictionary2[item3.getSmithRefId()] == dictionary[item3.getSmithRefId()])
			{
				commonScreenObject.findChild(aObject, "SmithSalary/SalaryChange/SalaryArrow").GetComponent<UISprite>().enabled = false;
				commonScreenObject.findChild(aObject, "SmithSalary/SalaryNoChangeLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(item3.getSmithSalary());
			}
			else
			{
				commonScreenObject.findChild(aObject, "SmithSalary/SalaryChange/SalaryArrow").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(aObject, "SmithSalary/SalaryChange/SalaryBefore").GetComponent<UILabel>().text = CommonAPI.formatNumber(dictionary[item3.getSmithRefId()]);
				commonScreenObject.findChild(aObject, "SmithSalary/SalaryChange/SalaryAfter").GetComponent<UILabel>().text = CommonAPI.formatNumber(dictionary2[item3.getSmithRefId()]);
			}
		}
		UIScrollView component = smithListGrid.transform.parent.GetComponent<UIScrollView>();
		smithListGrid.Reposition();
		component.UpdateScrollbars();
		smithListGrid.enabled = true;
		component.horizontalScrollBar.value = 0f;
		string empty2 = string.Empty;
		if (!tryPaySalary(num))
		{
			int salaryChancesUsed = player.getSalaryChancesUsed();
			salaryChancesUsed++;
			player.setSalaryChancesUsed(salaryChancesUsed);
			if (salaryChancesUsed == 1)
			{
				player.addGold(num);
				int grantAmount = gameData.getAreaRegionByRefID(player.getAreaRegion()).getGrantAmount();
				player.addShopMonthlyStarchByType(player.getPlayerMonths() - 1, RecordType.RecordTypeSpecial, gameData.getTextByRefId("recordTypeName05"), num + grantAmount);
				viewController.showDialoguePopup("99902", gameData.getDialogueBySetId("99902"), PopupType.PopupTypeGrantNotice);
				hidePayDayPopup();
			}
			else if (salaryChancesUsed <= 3)
			{
				showBankruptWarning(isFirstTime: false);
			}
			else
			{
				viewController.showDialoguePopup("99903", gameData.getDialogueBySetId("99903"), PopupType.PopupTypeGameOver);
				hidePayDayPopup();
			}
		}
		player.addShopMonthlyStarchByType(player.getPlayerMonths() - 1, RecordType.RecordTypeExpenseSalary, string.Empty, -1 * num);
		string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("salaryEvent01", "[totalSalary]", CommonAPI.formatNumber(num));
		gameData.addNewWhetsappMsg(player.getShopName(), textByRefIdWithDynText, "Image/whetsapp/news2", player.getPlayerTimeLong() - 24, WhetsappFilterType.WhetsappFilterTypeNotice);
	}

	public void hidePayDayPopup()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, 2000f, 0f);
	}

	public void showPayDayPopup()
	{
		base.gameObject.transform.localPosition = Vector3.zero;
	}

	public void showBankruptWarning(bool isFirstTime)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string text = gameData.getTextByRefId("salaryEvent02") + "\n\n";
		if (isFirstTime)
		{
			int grantAmount = gameData.getAreaRegionByRefID(player.getAreaRegion()).getGrantAmount();
			player.addGold(grantAmount);
			audioController.playGoldGainAudio();
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			text = text + gameData.getTextByRefIdWithDynText("salaryEvent03", "[gold]", CommonAPI.formatNumber(grantAmount)) + "\n\n";
		}
		else
		{
			text = text + gameData.getTextByRefId("salaryEvent14") + "\n\n";
		}
		int salaryChancesLeft = player.getSalaryChancesLeft();
		text = ((salaryChancesLeft <= 0) ? (text + "[E54242]" + gameData.getTextByRefId("salaryEvent05") + "[-]") : (text + gameData.getTextByRefIdWithDynText("salaryEvent04", "[chances]", "[E54242]" + salaryChancesLeft + "[-]")));
		viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("salaryEvent15"), text, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
	}

	public bool tryPaySalary(int goldReq)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		player.reduceGold(goldReq, allowNegative: true);
		audioController.playPurchaseAudio();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		if (player.getPlayerGold() < 0)
		{
			return false;
		}
		return true;
	}
}
