using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISmithManageController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private PopupType currentPopupType;

	private UILabel titleLabel;

	private GameObject menuSmithManage_Grid;

	private GameObject fireButton;

	private GameObject trainButton;

	private UILabel trainCostLabel;

	private GameObject changeJobButton;

	private GameObject changeConfirmButton;

	private UILabel changeJobCostLabel;

	private GameObject hireNowButton;

	private UILabel hireCostLabel;

	private UITexture smithImg;

	private UILabel smithName;

	private UILabel smithRole;

	private UILabel salaryValue;

	private UILabel perMonthLabel;

	private UILabel powerLabel;

	private UILabel powerValue;

	private UISprite powerChange;

	private UILabel intLabel;

	private UILabel intValue;

	private UISprite intChange;

	private UILabel techLabel;

	private UILabel techValue;

	private UISprite techChange;

	private UILabel luckLabel;

	private UILabel luckValue;

	private UISprite luckChange;

	private UILabel staminaLabel;

	private UILabel staminaValue;

	private UISprite staminaChange;

	private UILabel infoLabel;

	private List<Smith> smithList;

	private int selectedSmithIndex;

	private List<SmithJobClass> jobClassList;

	private int selectedJobIndex;

	private List<Smith> recruitList;

	private int selectedRecruitIndex;

	private string smithPrefix;

	private string jobPrefix;

	private string recruitPrefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		currentPopupType = PopupType.PopupTypeNothing;
		titleLabel = commonScreenObject.findChild(base.gameObject, "LeftFrame/TitleLabel").GetComponent<UILabel>();
		menuSmithManage_Grid = commonScreenObject.findChild(base.gameObject, "LeftFrame/Panel_SmithManagePanel/MenuSmithManage_Grid").gameObject;
		fireButton = commonScreenObject.findChild(base.gameObject, "FireButton").gameObject;
		trainButton = commonScreenObject.findChild(base.gameObject, "TrainButton").gameObject;
		trainCostLabel = commonScreenObject.findChild(base.gameObject, "TrainButton/CostFrame/TrainCostLabel").GetComponent<UILabel>();
		changeJobButton = commonScreenObject.findChild(base.gameObject, "ChangeJobButton").gameObject;
		changeConfirmButton = commonScreenObject.findChild(base.gameObject, "ChangeConfirmButton").gameObject;
		changeJobCostLabel = commonScreenObject.findChild(base.gameObject, "ChangeConfirmButton/CostFrame/ChangeJobCostLabel").GetComponent<UILabel>();
		hireNowButton = commonScreenObject.findChild(base.gameObject, "HireNowButton").gameObject;
		hireCostLabel = commonScreenObject.findChild(base.gameObject, "HireNowButton/CostFrame/HireCostLabel").GetComponent<UILabel>();
		smithImg = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithImg").GetComponent<UITexture>();
		smithName = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithName").GetComponent<UILabel>();
		smithRole = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithRole").GetComponent<UILabel>();
		salaryValue = commonScreenObject.findChild(base.gameObject, "SmithInfo/Salary/SalaryValue").GetComponent<UILabel>();
		perMonthLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/Salary/PerMonthLabel").GetComponent<UILabel>();
		powerLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/PowerLabel").GetComponent<UILabel>();
		powerValue = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/PowerValue").GetComponent<UILabel>();
		powerChange = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/PowerChange").GetComponent<UISprite>();
		intLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/IntLabel").GetComponent<UILabel>();
		intValue = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/IntValue").GetComponent<UILabel>();
		intChange = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/IntChange").GetComponent<UISprite>();
		techLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/TechLabel").GetComponent<UILabel>();
		techValue = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/TechValue").GetComponent<UILabel>();
		techChange = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/TechChange").GetComponent<UISprite>();
		luckLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/LuckLabel").GetComponent<UILabel>();
		luckValue = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/LuckValue").GetComponent<UILabel>();
		luckChange = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/LuckChange").GetComponent<UISprite>();
		staminaLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/StaminaLabel").GetComponent<UILabel>();
		staminaValue = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/StaminaValue").GetComponent<UILabel>();
		staminaChange = commonScreenObject.findChild(base.gameObject, "SmithInfo/SmithStats/StaminaChange").GetComponent<UISprite>();
		infoLabel = commonScreenObject.findChild(base.gameObject, "SmithInfo/InfoFrame/InfoLabel").GetComponent<UILabel>();
		smithList = new List<Smith>();
		selectedSmithIndex = -1;
		selectedJobIndex = -1;
		selectedRecruitIndex = -1;
		smithPrefix = "Smith_";
		jobPrefix = "Job_";
		recruitPrefix = "Recruit_";
		disableButton();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "CloseButton":
			switch (currentPopupType)
			{
			case PopupType.PopupTypeHire:
				viewController.closeSmithManagePopup(hide: true);
				viewController.resumeEverything();
				break;
			case PopupType.PopupTypeManageSmith:
				viewController.closeSmithManagePopup(hide: false);
				viewController.showMainMenu(MenuState.MenuStateSmithMain);
				break;
			case PopupType.PopupTypeChangeSmithJob:
				viewController.closeSmithManagePopup(hide: false);
				viewController.showSmithManage(PopupType.PopupTypeManageSmith);
				break;
			}
			return;
		case "TrainButton":
			return;
		case "ChangeJobButton":
			viewController.closeSmithManagePopup(hide: false);
			viewController.showSmithManage(PopupType.PopupTypeChangeSmithJob, selectedSmithIndex);
			return;
		case "FireButton":
		{
			shopMenuController.GetComponent<ShopMenuController>().insertStoredInput("fire", selectedSmithIndex + 1);
			Smith smithByIndex = game.getPlayer().getSmithByIndex(selectedSmithIndex);
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, game.getGameData().getTextByRefIdWithDynText("menuSmithManagement03", "[smithName]", smithByIndex.getSmithName()), PopupType.PopupTypeManageSmith, null, colorTag: false, null, map: false, string.Empty);
			return;
		}
		case "ChangeConfirmButton":
			viewController.closeSmithManagePopup(hide: true);
			shopMenuController.insertStoredInput("jobChangeSmith", selectedSmithIndex + 1);
			shopMenuController.insertStoredInput("jobChangeJob", selectedJobIndex + 1);
			shopMenuController.changeSmithJob();
			return;
		case "HireNowButton":
			return;
		}
		string[] array = gameObjectName.Split('_');
		string text = array[0];
		int num = CommonAPI.parseInt(array[1]);
		switch (text)
		{
		case "Smith":
			selectSmith(num);
			loadSmithInfo(num);
			break;
		case "Job":
			selectJob(num);
			loadSmithChangeJobInfo(num);
			break;
		case "Recruit":
			selectRecruit(num);
			loadRecruitInfo(num);
			break;
		}
	}

	private void disableButton()
	{
		GameData gameData = game.getGameData();
		fireButton.SetActive(value: false);
		commonScreenObject.findChild(trainButton, "TrainLabel").GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmithManagement36");
		trainButton.SetActive(value: false);
		trainCostLabel.text = string.Empty;
		changeJobButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmithManagement37");
		changeJobCostLabel.text = string.Empty;
		changeJobButton.SetActive(value: false);
		commonScreenObject.findChild(changeConfirmButton, "ChangeLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuSmithManagement39");
		changeConfirmButton.SetActive(value: false);
		commonScreenObject.findChild(hireNowButton, "HireNowLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuSmithManagement38");
		hireCostLabel.text = string.Empty;
		hireNowButton.SetActive(value: false);
		perMonthLabel.text = gameData.getTextByRefId("smithStatsShort07");
		powerLabel.text = gameData.getTextByRefId("smithStatsShort02");
		intLabel.text = gameData.getTextByRefId("smithStatsShort03");
		techLabel.text = gameData.getTextByRefId("smithStatsShort04");
		luckLabel.text = gameData.getTextByRefId("smithStatsShort05");
		staminaLabel.text = gameData.getTextByRefId("smithStatsShort06");
		powerChange.spriteName = "None";
		intChange.spriteName = "None";
		techChange.spriteName = "None";
		luckChange.spriteName = "None";
		staminaChange.spriteName = "None";
	}

	public void setReference(PopupType aPopupType, int currentSmithIndex)
	{
		currentPopupType = aPopupType;
		switch (aPopupType)
		{
		case PopupType.PopupTypeManageSmith:
			showManageSmith(currentSmithIndex);
			break;
		case PopupType.PopupTypeChangeSmithJob:
			showChangeSmithJob(currentSmithIndex);
			break;
		case PopupType.PopupTypeHire:
			showHireSmith();
			break;
		}
	}

	private void showManageSmith(int aIndex)
	{
		titleLabel.text = game.getGameData().getTextByRefId("menuMain02").ToUpper(CultureInfo.InvariantCulture);
		fireButton.SetActive(value: true);
		trainButton.SetActive(value: true);
		changeJobButton.SetActive(value: true);
		fireButton.GetComponent<UIButton>().isEnabled = false;
		trainButton.GetComponent<UIButton>().isEnabled = false;
		changeJobButton.GetComponent<UIButton>().isEnabled = false;
		smithList = game.getPlayer().getSmithList();
		for (int i = 0; i < smithList.Count; i++)
		{
			GameObject gameObject = commonScreenObject.createPrefab(menuSmithManage_Grid, smithPrefix + i, "Prefab/SmithManage/Button_SmithManage", Vector3.zero, Vector3.one, Vector3.zero);
			if (i == 0)
			{
				selectSmith(0);
				loadSmithInfo(0);
			}
			gameObject.GetComponentInChildren<UILabel>().text = smithList[i].getSmithName();
		}
		menuSmithManage_Grid.GetComponent<UIGrid>().Reposition();
		menuSmithManage_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		if (aIndex != -1)
		{
			selectSmith(aIndex);
			loadSmithInfo(aIndex);
		}
	}

	private void showChangeSmithJob(int currentSmithIndex)
	{
		selectedSmithIndex = currentSmithIndex;
		Smith smithByIndex = game.getPlayer().getSmithByIndex(selectedSmithIndex);
		titleLabel.text = game.getGameData().getTextByRefId("forgeMenu01");
		changeConfirmButton.SetActive(value: true);
		changeConfirmButton.GetComponent<UIButton>().isEnabled = false;
		jobClassList = game.getGameData().getJobChangeList(smithByIndex.getExperienceList(), smithByIndex.getSmithJob().getSmithJobRefId());
		for (int i = 0; i < jobClassList.Count; i++)
		{
			GameObject gameObject = commonScreenObject.createPrefab(menuSmithManage_Grid, jobPrefix + i, "Prefab/SmithManage/Button_SmithManage", Vector3.zero, Vector3.one, Vector3.zero);
			if (i == 0)
			{
				selectJob(i);
				loadSmithChangeJobInfo(i);
			}
			gameObject.GetComponentInChildren<UILabel>().text = jobClassList[i].getSmithJobName();
		}
		menuSmithManage_Grid.GetComponent<UIGrid>().Reposition();
		menuSmithManage_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		loadSmithInfo(currentSmithIndex);
	}

	private void showHireSmith()
	{
		titleLabel.text = game.getGameData().getTextByRefId("menuSmith02");
		hireNowButton.SetActive(value: true);
		hireNowButton.GetComponent<UIButton>().isEnabled = false;
		recruitList = game.getPlayer().getRecruitList();
		for (int i = 0; i < recruitList.Count; i++)
		{
			GameObject gameObject = commonScreenObject.createPrefab(menuSmithManage_Grid, recruitPrefix + i, "Prefab/SmithManage/Button_SmithManage", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UILabel>().text = recruitList[i].getSmithName();
			if (i == 0)
			{
				selectRecruit(0);
				loadRecruitInfo(0);
			}
		}
		menuSmithManage_Grid.GetComponent<UIGrid>().Reposition();
		menuSmithManage_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}

	private void selectSmith(int index)
	{
		if (index != selectedSmithIndex)
		{
			if (selectedSmithIndex != -1)
			{
				GameObject.Find(smithPrefix + selectedSmithIndex).GetComponent<UISprite>().spriteName = "parent-inactive";
			}
			selectedSmithIndex = index;
			GameObject.Find(smithPrefix + selectedSmithIndex).GetComponent<UISprite>().spriteName = "parent-active";
		}
		if (selectedSmithIndex != -1)
		{
			PopupType popupType = currentPopupType;
			if (popupType == PopupType.PopupTypeManageSmith)
			{
				fireButton.GetComponent<UIButton>().isEnabled = true;
				changeJobButton.GetComponent<UIButton>().isEnabled = true;
			}
		}
	}

	private void loadSmithInfo(int index)
	{
		Smith smithByIndex = game.getPlayer().getSmithByIndex(index);
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smithByIndex.getImage() + "_manage");
		smithName.text = smithByIndex.getSmithName();
		smithRole.text = game.getGameData().getTextByRefId("smithStatsShort01") + smithByIndex.getSmithLevel() + " " + smithByIndex.getSmithJob().getSmithJobName();
		salaryValue.text = CommonAPI.formatNumber(smithByIndex.getSmithSalary());
		powerValue.text = CommonAPI.formatNumber(smithByIndex.getSmithPower());
		intValue.text = CommonAPI.formatNumber(smithByIndex.getSmithIntelligence());
		techValue.text = CommonAPI.formatNumber(smithByIndex.getSmithTechnique());
		luckValue.text = CommonAPI.formatNumber(smithByIndex.getSmithLuck());
		staminaValue.text = CommonAPI.formatNumber(smithByIndex.getSmithMaxMood());
		if (smithByIndex.getSmithLevel() != 20 && currentPopupType == PopupType.PopupTypeManageSmith)
		{
			trainButton.GetComponent<UIButton>().isEnabled = true;
		}
		else if (smithByIndex.getSmithLevel() == 20 && currentPopupType == PopupType.PopupTypeManageSmith)
		{
			trainButton.GetComponent<UIButton>().isEnabled = false;
		}
		infoLabel.text = smithByIndex.getSmithDesc();
	}

	private void selectJob(int index)
	{
		if (index != selectedJobIndex)
		{
			if (selectedJobIndex != -1)
			{
				GameObject.Find(jobPrefix + selectedJobIndex).GetComponent<UISprite>().spriteName = "parent-inactive";
			}
			selectedJobIndex = index;
			GameObject.Find(jobPrefix + selectedJobIndex).GetComponent<UISprite>().spriteName = "parent-active";
		}
		if (selectedJobIndex != -1)
		{
			changeConfirmButton.GetComponent<UIButton>().isEnabled = true;
		}
	}

	private void loadSmithChangeJobInfo(int jobIndex)
	{
		Smith smithByIndex = game.getPlayer().getSmithByIndex(selectedSmithIndex);
		SmithJobClass smithJobClass = jobClassList[jobIndex];
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smithByIndex.getImage() + "_manage");
		smithName.text = smithByIndex.getSmithName();
		smithRole.text = game.getGameData().getTextByRefId("smithStatsShort01") + smithByIndex.getSmithLevel() + " " + smithByIndex.getSmithJob().getSmithJobName();
		salaryValue.text = CommonAPI.formatNumber(smithByIndex.getSmithSalary());
		powerValue.text = CommonAPI.formatNumber(smithByIndex.fitSmithPower(smithJobClass));
		if (smithByIndex.fitSmithPower(smithJobClass) > smithByIndex.getSmithPower())
		{
			powerValue.color = Color.red;
			powerChange.spriteName = "statUp";
		}
		else if (smithByIndex.fitSmithPower(smithJobClass) < smithByIndex.getSmithPower())
		{
			powerValue.color = Color.blue;
			powerChange.spriteName = "statDown";
		}
		else
		{
			powerValue.color = Color.black;
			powerChange.spriteName = "None";
		}
		intValue.text = CommonAPI.formatNumber(smithByIndex.fitSmithIntelligence(smithJobClass));
		if (smithByIndex.fitSmithIntelligence(smithJobClass) > smithByIndex.getSmithIntelligence())
		{
			intValue.color = Color.red;
			intChange.spriteName = "statUp";
		}
		else if (smithByIndex.fitSmithIntelligence(smithJobClass) < smithByIndex.getSmithIntelligence())
		{
			intValue.color = Color.blue;
			intChange.spriteName = "statDown";
		}
		else
		{
			intValue.color = Color.black;
			intChange.spriteName = "None";
		}
		techValue.text = CommonAPI.formatNumber(smithByIndex.fitSmithTechnique(smithJobClass));
		if (smithByIndex.fitSmithTechnique(smithJobClass) > smithByIndex.getSmithTechnique())
		{
			techValue.color = Color.red;
			techChange.spriteName = "statUp";
		}
		else if (smithByIndex.fitSmithTechnique(smithJobClass) < smithByIndex.getSmithTechnique())
		{
			techValue.color = Color.blue;
			techChange.spriteName = "statDown";
		}
		else
		{
			techValue.color = Color.black;
			techChange.spriteName = "None";
		}
		luckValue.text = CommonAPI.formatNumber(smithByIndex.fitSmithLuck(smithJobClass));
		if (smithByIndex.fitSmithLuck(smithJobClass) > smithByIndex.getSmithLuck())
		{
			luckValue.color = Color.red;
			luckChange.spriteName = "statUp";
		}
		else if (smithByIndex.fitSmithLuck(smithJobClass) < smithByIndex.getSmithLuck())
		{
			luckValue.color = Color.blue;
			luckChange.spriteName = "statDown";
		}
		else
		{
			luckValue.color = Color.black;
			luckChange.spriteName = "None";
		}
		staminaValue.text = CommonAPI.formatNumber(smithByIndex.getSmithMaxMood());
		infoLabel.text = smithByIndex.getSmithDesc();
		changeJobCostLabel.text = CommonAPI.formatNumber(smithJobClass.getSmithJobChangeCost());
	}

	private void selectRecruit(int index)
	{
		if (index != selectedRecruitIndex)
		{
			if (selectedRecruitIndex != -1)
			{
				GameObject.Find(recruitPrefix + selectedRecruitIndex).GetComponent<UISprite>().spriteName = "parent-inactive";
			}
			selectedRecruitIndex = index;
			GameObject.Find(recruitPrefix + selectedRecruitIndex).GetComponent<UISprite>().spriteName = "parent-active";
		}
		if (selectedRecruitIndex != -1)
		{
			hireNowButton.GetComponent<UIButton>().isEnabled = true;
		}
	}

	private void loadRecruitInfo(int index)
	{
		Smith recruitByIndex = game.getPlayer().getRecruitByIndex(index);
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + recruitByIndex.getImage() + "_manage");
		smithName.text = recruitByIndex.getSmithName();
		smithRole.text = game.getGameData().getTextByRefId("smithStatsShort01") + recruitByIndex.getSmithLevel() + " " + recruitByIndex.getSmithJob().getSmithJobName();
		salaryValue.text = CommonAPI.formatNumber(recruitByIndex.getSmithSalary());
		powerValue.text = CommonAPI.formatNumber(recruitByIndex.getSmithPower());
		intValue.text = CommonAPI.formatNumber(recruitByIndex.getSmithIntelligence());
		techValue.text = CommonAPI.formatNumber(recruitByIndex.getSmithTechnique());
		luckValue.text = CommonAPI.formatNumber(recruitByIndex.getSmithLuck());
		staminaValue.text = CommonAPI.formatNumber(recruitByIndex.getSmithMaxMood());
		infoLabel.text = recruitByIndex.getSmithDesc();
		hireCostLabel.text = CommonAPI.formatNumber(recruitByIndex.getSmithHireCost());
	}
}
