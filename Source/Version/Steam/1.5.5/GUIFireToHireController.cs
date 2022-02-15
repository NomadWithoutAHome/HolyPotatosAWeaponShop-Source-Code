using System.Collections.Generic;
using UnityEngine;

public class GUIFireToHireController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UILabel titleLabel;

	private GameObject menuSmithManage_Grid;

	private UILabel fireLabel;

	private UILabel fireSmithNameLabel;

	private UILabel hireLabel;

	private UILabel hireSmithNameLabel;

	private GameObject okButton;

	private GameObject noButton;

	private GameObject arrowSign;

	private GameObject oldSmithInfo;

	private GameObject newSmithInfo;

	private List<Smith> smithList;

	private int newSmithIndex;

	private int oldSmithIndex;

	private string smithPrefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		titleLabel = GameObject.Find("TitleLabel").GetComponent<UILabel>();
		menuSmithManage_Grid = GameObject.Find("MenuSmithManage_Grid");
		fireLabel = GameObject.Find("FireLabel").GetComponent<UILabel>();
		fireSmithNameLabel = GameObject.Find("FireSmithNameLabel").GetComponent<UILabel>();
		hireLabel = GameObject.Find("HireLabel").GetComponent<UILabel>();
		hireSmithNameLabel = GameObject.Find("HireSmithNameLabel").GetComponent<UILabel>();
		okButton = GameObject.Find("OkButton");
		noButton = GameObject.Find("NoButton");
		arrowSign = GameObject.Find("ArrowSign");
		oldSmithInfo = GameObject.Find("OldSmithInfo");
		newSmithInfo = GameObject.Find("NewSmithInfo");
		smithList = new List<Smith>();
		newSmithIndex = -1;
		oldSmithIndex = -1;
		smithPrefix = "Smith_";
		disableButton();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "NoButton":
		case "CloseButton":
			viewController.closeFireHire(hide: false);
			viewController.showSmithManage(PopupType.PopupTypeHire);
			return;
		case "OkButton":
			return;
		}
		string[] array = gameObjectName.Split('_');
		string text = array[0];
		int index = CommonAPI.parseInt(array[1]);
		if (text != null && text == "Smith")
		{
			selectSmith(index);
			loadOldSmithInfo(index);
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		titleLabel.text = gameData.getTextByRefId("menuFireToHire01");
		for (int i = 0; i < 2; i++)
		{
			GameObject aObject = ((i >= 1) ? newSmithInfo : oldSmithInfo);
			commonScreenObject.findChild(aObject, "SmithStats/PowerLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort02");
			commonScreenObject.findChild(aObject, "SmithStats/IntLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort03");
			commonScreenObject.findChild(aObject, "SmithStats/TechLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort04");
			commonScreenObject.findChild(aObject, "SmithStats/LuckLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort05");
			commonScreenObject.findChild(aObject, "SmithStats/StaminaLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort06");
			commonScreenObject.findChild(aObject, "GoldIcon/PerMonthLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort07");
		}
		smithList = game.getPlayer().getSmithList();
		for (int j = 0; j < smithList.Count; j++)
		{
			GameObject gameObject = commonScreenObject.createPrefab(menuSmithManage_Grid, smithPrefix + j, "Prefab/SmithManage/Button_FireToHire", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UILabel>().text = smithList[j].getSmithName();
			if (j == 0)
			{
				selectSmith(j);
				loadOldSmithInfo(j);
			}
		}
		menuSmithManage_Grid.GetComponent<UIGrid>().Reposition();
		menuSmithManage_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		loadNewSmithInfo();
	}

	private void disableButton()
	{
		GameData gameData = game.getGameData();
		okButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral02");
		okButton.GetComponent<UIButton>().isEnabled = false;
		noButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral03");
		arrowSign.GetComponent<TweenPosition>().enabled = false;
	}

	private void selectSmith(int index)
	{
		if (index != oldSmithIndex)
		{
			if (oldSmithIndex != -1)
			{
				GameObject.Find(smithPrefix + oldSmithIndex).GetComponent<UISprite>().spriteName = "parent-inactive";
			}
			oldSmithIndex = index;
			GameObject.Find(smithPrefix + oldSmithIndex).GetComponent<UISprite>().spriteName = "parent-active";
		}
		if (oldSmithIndex != -1)
		{
			okButton.GetComponent<UIButton>().isEnabled = true;
			commonScreenObject.tweenPosition(arrowSign.GetComponent<TweenPosition>(), new Vector3(110f, 65f, 0f), new Vector3(125f, 65f, 0f), 1f, null, string.Empty);
		}
	}

	private void loadOldSmithInfo(int index)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		newSmithIndex = shopMenuController.referStoredInput("hire") - 1;
		Smith smithByIndex = player.getSmithByIndex(index);
		Smith recruitByIndex = player.getRecruitByIndex(newSmithIndex);
		commonScreenObject.findChild(oldSmithInfo, "SmithImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smithByIndex.getImage() + "_manage");
		commonScreenObject.findChild(oldSmithInfo, "SmithName").GetComponent<UILabel>().text = smithByIndex.getSmithName();
		commonScreenObject.findChild(oldSmithInfo, "SmithRole").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort01") + smithByIndex.getSmithLevel() + " " + smithByIndex.getSmithJob().getSmithJobName();
		commonScreenObject.findChild(oldSmithInfo, "GoldIcon/SalaryValue").GetComponent<UILabel>().text = smithByIndex.getSmithSalary().ToString();
		commonScreenObject.findChild(oldSmithInfo, "SmithStats/PowerValue").GetComponent<UILabel>().text = smithByIndex.getSmithPower().ToString();
		commonScreenObject.findChild(oldSmithInfo, "SmithStats/IntValue").GetComponent<UILabel>().text = smithByIndex.getSmithIntelligence().ToString();
		commonScreenObject.findChild(oldSmithInfo, "SmithStats/TechValue").GetComponent<UILabel>().text = smithByIndex.getSmithTechnique().ToString();
		commonScreenObject.findChild(oldSmithInfo, "SmithStats/LuckValue").GetComponent<UILabel>().text = smithByIndex.getSmithLuck().ToString();
		commonScreenObject.findChild(oldSmithInfo, "SmithStats/StaminaValue").GetComponent<UILabel>().text = smithByIndex.getSmithMaxMood().ToString();
		commonScreenObject.findChild(oldSmithInfo, "InfoFrame/InfoLabel").GetComponent<UILabel>().text = smithByIndex.getSmithDesc();
		fireLabel.text = gameData.getTextByRefId("menuSmithManagement19");
		fireSmithNameLabel.text = smithByIndex.getSmithName();
		hireLabel.text = gameData.getTextByRefId("menuSmithManagement20");
		hireSmithNameLabel.text = recruitByIndex.getSmithName();
	}

	private void loadNewSmithInfo()
	{
		newSmithIndex = shopMenuController.referStoredInput("hire") - 1;
		Smith recruitByIndex = game.getPlayer().getRecruitByIndex(newSmithIndex);
		commonScreenObject.findChild(newSmithInfo, "SmithImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/" + recruitByIndex.getImage() + "_manage");
		commonScreenObject.findChild(newSmithInfo, "SmithName").GetComponent<UILabel>().text = recruitByIndex.getSmithName();
		commonScreenObject.findChild(newSmithInfo, "SmithRole").GetComponent<UILabel>().text = game.getGameData().getTextByRefId("smithStatsShort01") + recruitByIndex.getSmithLevel() + " " + recruitByIndex.getSmithJob().getSmithJobName();
		commonScreenObject.findChild(newSmithInfo, "GoldIcon/SalaryValue").GetComponent<UILabel>().text = recruitByIndex.getSmithSalary().ToString();
		commonScreenObject.findChild(newSmithInfo, "SmithStats/PowerValue").GetComponent<UILabel>().text = recruitByIndex.getSmithPower().ToString();
		commonScreenObject.findChild(newSmithInfo, "SmithStats/IntValue").GetComponent<UILabel>().text = recruitByIndex.getSmithIntelligence().ToString();
		commonScreenObject.findChild(newSmithInfo, "SmithStats/TechValue").GetComponent<UILabel>().text = recruitByIndex.getSmithTechnique().ToString();
		commonScreenObject.findChild(newSmithInfo, "SmithStats/LuckValue").GetComponent<UILabel>().text = recruitByIndex.getSmithLuck().ToString();
		commonScreenObject.findChild(newSmithInfo, "SmithStats/StaminaValue").GetComponent<UILabel>().text = recruitByIndex.getSmithMaxMood().ToString();
		commonScreenObject.findChild(newSmithInfo, "InfoFrame/InfoLabel").GetComponent<UILabel>().text = recruitByIndex.getSmithDesc();
	}
}
