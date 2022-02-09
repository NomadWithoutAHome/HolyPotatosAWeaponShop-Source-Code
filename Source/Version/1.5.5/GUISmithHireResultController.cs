using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISmithHireResultController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private TooltipTextScript smithInfoScript;

	private AudioController audioController;

	private Smith candidate;

	private List<Smith> candidateList;

	private List<SmithExperience> candidateExperienceList;

	private int candidateShowIndex;

	private Smith employee;

	private List<Smith> employeeList;

	private UILabel candidateNameLabel;

	private UILabel candidateJobClassLabel;

	private UILabel candidateMerchantLabel;

	private UILabel candidateExplorerLabel;

	private UILabel candidateDescLabel;

	private UILabel candidatePowLabel;

	private UILabel candidateIntLabel;

	private UILabel candidateTecLabel;

	private UILabel candidateLucLabel;

	private UITexture candidateImage;

	private UILabel candidateSalaryLabel;

	private UILabel candidateHireLabel;

	private UIButton candidateHireButton;

	private UIGrid candidateWorkExpGrid;

	private UIScrollBar candidateWorkExpScrollbar;

	private UIGrid candidateListGrid;

	private UIScrollBar candidateListScrollbar;

	private Vector3 employeeListOpenPos;

	private Vector3 employeeListClosedPos;

	private TweenPosition employeeListTween;

	private UIGrid employeeListGrid;

	private UIScrollBar employeeListScrollbar;

	private bool employeeListOpen;

	private UILabel shopCapacityLabel;

	private UILabel shopGoldLabel;

	private List<string> justHired;

	private List<Smith> justFired;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		smithInfoScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "HireResult_bg").gameObject;
		candidateNameLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/CandidateName_bg/CandidateName_label").GetComponent<UILabel>();
		candidateJobClassLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/CandidateJobClass_bg/CandidateJobClass_label").GetComponent<UILabel>();
		candidateMerchantLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/CandidateJobClass_bg/CandidateMerchant_label").GetComponent<UILabel>();
		candidateExplorerLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/CandidateJobClass_bg/CandidateExplore_label").GetComponent<UILabel>();
		candidateDescLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/Candidate_smithDescBg/Candidate_smithDescLabel").GetComponent<UILabel>();
		candidatePowLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/Candidate_stats/Candidate_statsAtkBg/Candidate_statsAtkLabel").GetComponent<UILabel>();
		candidateIntLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/Candidate_stats/Candidate_statsSpdBg/Candidate_statsSpdLabel").GetComponent<UILabel>();
		candidateTecLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/Candidate_stats/Candidate_statsAccBg/Candidate_statsAccLabel").GetComponent<UILabel>();
		candidateLucLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_right/Candidate_stats/Candidate_statsMagBg/Candidate_statsMagLabel").GetComponent<UILabel>();
		candidateImage = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_left/CandidateImage_bg/CandidateImage_texture").GetComponent<UITexture>();
		candidateSalaryLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_left/Candidate_salaryBg/Candidate_salaryLabel").GetComponent<UILabel>();
		candidateHireLabel = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_left/Candidate_hireButton/Candidate_hireButtonLabel").GetComponent<UILabel>();
		candidateHireButton = commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_left/Candidate_hireButton").GetComponent<UIButton>();
		candidateWorkExpGrid = commonScreenObject.findChild(aObject, "Candidate_bg/WorkExp_bg/WorkExpList_panel/WorkExpList_grid").GetComponent<UIGrid>();
		candidateWorkExpScrollbar = commonScreenObject.findChild(aObject, "Candidate_bg/WorkExp_bg/WorkExpScrollbar").GetComponent<UIScrollBar>();
		candidateListGrid = commonScreenObject.findChild(aObject, "CandidateList_bg/CandidateList_panel/CandidateList_grid").GetComponent<UIGrid>();
		candidateListScrollbar = commonScreenObject.findChild(aObject, "CandidateList_bg/CandidateScrollbar").GetComponent<UIScrollBar>();
		employeeListOpenPos = new Vector3(288f, 0f, 0f);
		employeeListClosedPos = new Vector3(90f, 0f, 0f);
		GameObject aObject2 = commonScreenObject.findChild(base.gameObject, "FireMenu_bg").gameObject;
		employeeListTween = commonScreenObject.findChild(aObject2, "EmployeeList_bg").GetComponent<TweenPosition>();
		employeeListTween.transform.localPosition = employeeListClosedPos;
		employeeListGrid = commonScreenObject.findChild(aObject2, "EmployeeList_bg/EmployeeList_scaler/EmployeeList_panel/EmployeeList_grid").GetComponent<UIGrid>();
		employeeListScrollbar = commonScreenObject.findChild(aObject2, "EmployeeList_bg/EmployeeScrollbar").GetComponent<UIScrollBar>();
		employeeListOpen = false;
		shopCapacityLabel = commonScreenObject.findChild(aObject, "ShopCapacity_bg/ShopCapacity_label").GetComponent<UILabel>();
		shopGoldLabel = commonScreenObject.findChild(aObject, "ShopGold_bg/ShopGold_label").GetComponent<UILabel>();
		commonScreenObject.findChild(aObject, "CandidateTitle_bg/CandidateTitle_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuSmithHireResult02").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(aObject2, "EmployeeList_bg/EmployeeTab_bg/EmployeeTab_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuSmithHireResult03");
		justHired = new List<string>();
		justFired = new List<Smith>();
	}

	public void processClick(string gameObjectName)
	{
		GameData gameData = game.getGameData();
		switch (gameObjectName)
		{
		case "Close_button":
			smithInfoScript.setInactive();
			viewController.closeHireResult(hide: true);
			GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().fireSmiths(justFired);
			viewController.resumeEverything();
			return;
		case "EmployeeTab_bg":
			toggleEmployeeList();
			return;
		case "Candidate_hireButton":
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, gameData.getTextByRefId("menuSmith02"), game.getGameData().getTextByRefIdWithDynText("menuSmithManagement15", "[smithName]", candidate.getSmithName()), PopupType.PopupTypeHire, null, colorTag: false, null, map: false, string.Empty);
			return;
		}
		string[] array = gameObjectName.Split('_');
		if (array[0] == "CandidateObj")
		{
			selectCandidate(CommonAPI.parseInt(array[1]));
			return;
		}
		if (!(array[0] == "Fire"))
		{
			return;
		}
		CommonAPI.debug("here");
		if (employeeListOpen)
		{
			Player player = game.getPlayer();
			if (player.getSmithList().Count > 1 && selectEmployee(CommonAPI.parseInt(array[1])))
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, gameData.getTextByRefId("menuSmith03"), game.getGameData().getTextByRefIdWithDynText("menuSmithManagement03", "[smithName]", employee.getSmithName()), PopupType.PopupTypeFire, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			string[] array = hoverName.Split('_');
			if (employeeListOpen && (array[0] == "EmployeeObj" || array[0] == "Fire"))
			{
				Player player = game.getPlayer();
				Smith smith = employeeList[CommonAPI.parseInt(array[1])];
				if (player.getSmithList().Count <= 1)
				{
					smithInfoScript.showText(game.getGameData().getTextByRefId("errorCommon03"));
				}
				else if (smith.checkSmithInShopOrStandby())
				{
					smithInfoScript.showText(smith.getSmithStandardInfoString(showFullJobDetails: true));
				}
				else
				{
					smithInfoScript.showText(game.getGameData().getTextByRefId("errorCommon07"));
				}
			}
		}
		else
		{
			smithInfoScript.setInactive();
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (candidateHireButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Candidate_hireButton");
		}
		else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	public void setReference(bool isAfterHire = false)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		candidateList = player.getRecruitList();
		employeeList = player.getSmithList();
		if (candidateList.Count > 0)
		{
			refreshCandidateList();
			selectCandidate(0);
			refreshEmployeeList();
			refreshShopStats();
			return;
		}
		if (!isAfterHire)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("menuSmithManagement52"), gameData.getTextByRefId("menuSmithManagement12"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
		smithInfoScript.setInactive();
		viewController.closeHireResult(hide: true);
		GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().fireSmiths(justFired);
	}

	private void selectCandidate(int index)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		candidateShowIndex = index;
		candidate = candidateList[index];
		candidateNameLabel.text = candidate.getSmithName();
		candidateJobClassLabel.text = candidate.getSmithJob().getSmithJobName() + " Lv" + candidate.getSmithLevel();
		candidateMerchantLabel.text = gameData.getTextByRefId("smithStats17") + candidate.getMerchantLevel();
		candidateExplorerLabel.text = gameData.getTextByRefId("smithStats18") + candidate.getExploreLevel();
		candidateDescLabel.text = candidate.getSmithDesc();
		candidateSalaryLabel.text = "$" + CommonAPI.formatNumber(candidate.getSmithSalary()) + gameData.getTextByRefId("smithStatsShort07");
		candidatePowLabel.text = candidate.getSmithPower().ToString();
		candidateIntLabel.text = candidate.getSmithIntelligence().ToString();
		candidateTecLabel.text = candidate.getSmithTechnique().ToString();
		candidateLucLabel.text = candidate.getSmithLuck().ToString();
		candidateImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + candidate.getImage() + "_manage");
		if (player.getSmithList().Count >= player.getShopMaxSmith())
		{
			candidateHireLabel.text = gameData.getTextByRefId("errorCommon06");
			candidateHireButton.isEnabled = false;
		}
		else if (player.getPlayerGold() < candidate.getSmithHireCost())
		{
			candidateHireLabel.text = gameData.getTextByRefId("errorCommon05");
			candidateHireButton.isEnabled = false;
		}
		else
		{
			candidateHireLabel.text = gameData.getTextByRefIdWithDynText("menuSmithHireResult01", "[hireCost]", CommonAPI.formatNumber(candidate.getSmithHireCost())).ToUpper(CultureInfo.InvariantCulture);
			candidateHireButton.isEnabled = true;
		}
		refreshCandidateExpList();
		refreshCandidateListSelection();
	}

	private void refreshCandidateExpList()
	{
		GameData gameData = game.getGameData();
		while (candidateWorkExpGrid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(candidateWorkExpGrid.transform.GetChild(0).gameObject);
		}
		List<SmithExperience> experienceList = candidate.getExperienceList();
		int num = 0;
		foreach (SmithExperience item in experienceList)
		{
			SmithJobClass smithJobClass = gameData.getSmithJobClass(item.getSmithJobClassRefId());
			GameObject gameObject = commonScreenObject.createPrefab(candidateWorkExpGrid.gameObject, "CandidateExpObj_" + num, "Prefab/SmithManage/WorkExpObj", Vector3.zero, Vector3.one, Vector3.zero);
			if (smithJobClass.getSmithJobRefId() == candidate.getSmithJob().getSmithJobRefId())
			{
				gameObject.GetComponent<UISprite>().spriteName = "hirefire_cyanframe";
			}
			commonScreenObject.findChild(gameObject, "WorkExp_jobName").GetComponent<UILabel>().text = smithJobClass.getSmithJobName();
			commonScreenObject.findChild(gameObject, "WorkExp_jobLevel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort01") + " " + item.getSmithJobClassLevel() + "/" + smithJobClass.getMaxLevel();
			num++;
		}
		candidateWorkExpGrid.Reposition();
		candidateWorkExpScrollbar.value = 0f;
		candidateWorkExpGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		candidateWorkExpGrid.enabled = true;
	}

	private void refreshCandidateListSelection()
	{
		int num = 0;
		foreach (Smith candidate in candidateList)
		{
			GameObject aObject = commonScreenObject.findChild(candidateListGrid.gameObject, "CandidateObj_" + num).gameObject;
			if (num == candidateShowIndex)
			{
				commonScreenObject.findChild(aObject, "Candidate_bg").GetComponent<UISprite>().spriteName = "bg_weaponselected";
			}
			else
			{
				commonScreenObject.findChild(aObject, "Candidate_bg").GetComponent<UISprite>().spriteName = "bg_weapon";
			}
			num++;
		}
	}

	private void refreshCandidateList()
	{
		GameData gameData = game.getGameData();
		while (candidateListGrid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(candidateListGrid.transform.GetChild(0).gameObject);
		}
		int num = 0;
		foreach (Smith candidate in candidateList)
		{
			GameObject aObject = commonScreenObject.createPrefab(candidateListGrid.gameObject, "CandidateObj_" + num, "Prefab/SmithManage/CandidateListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "Candidate_bg/Candidate_image").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + candidate.getImage());
			commonScreenObject.findChild(aObject, "Candidate_hireCostBg/Candidate_hireCostValue").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(candidate.getSmithHireCost());
			commonScreenObject.findChild(aObject, "Candidate_salaryBg/Candidate_salaryLabel").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(candidate.getSmithSalary()) + gameData.getTextByRefId("smithStatsShort07");
			commonScreenObject.findChild(aObject, "SmithJob_bar/SmithJob_name").GetComponent<UILabel>().text = candidate.getSmithJob().getSmithJobName();
			commonScreenObject.findChild(aObject, "SmithJob_bar/SmithLevel_star/SmithLevel_value").GetComponent<UILabel>().text = candidate.getSmithLevel().ToString();
			commonScreenObject.findChild(aObject, "SmithJob_bar").GetComponent<UIProgressBar>().value = (float)candidate.getSmithExp() / (float)candidate.getMaxExp();
			commonScreenObject.findChild(aObject, "Candidate_hireCostBg/Candidate_hireCostLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuSmithManagement16");
			num++;
		}
		candidateListGrid.Reposition();
		candidateListScrollbar.value = 0f;
		candidateListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		candidateListGrid.enabled = true;
		refreshCandidateListSelection();
	}

	public void hireSmith()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		shopMenuController.doHireSmith(candidate);
		shopMenuController.removeRecruit(candidate);
		player.reduceGold(candidate.getSmithHireCost(), allowNegative: true);
		audioController.playPurchaseAudio();
		Dictionary<string, Decoration> displayDecorationList = player.getDisplayDecorationList();
		foreach (Decoration value in displayDecorationList.Values)
		{
			player.addDecoSmithEffect(candidate, value.getDecorationEffectList());
		}
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseRecruitHire, string.Empty, -1 * candidate.getSmithHireCost());
		bool resume = false;
		if (player.getRecruitList().Count < 1)
		{
			resume = true;
		}
		string randomTextBySetRefId = gameData.getRandomTextBySetRefId("whetsappSmithHire");
		gameData.addNewWhetsappMsg(candidate.getSmithName(), randomTextBySetRefId, "Image/Smith/Portraits/" + candidate.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		viewController.showGeneralDialoguePopup(GeneralPopupType.GeneralPopupTypeDialogueGeneral, resume, gameData.getTextByRefId("menuSmith02"), gameData.getTextByRefIdWithDynText("menuSmithManagement31", "[smithName]", candidate.getSmithName()), randomTextBySetRefId, "Image/Smith/Portraits/" + candidate.getImage());
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("905");
		candidate.setSmithAction(smithActionByRefId, -1);
		justHired.Add(candidate.getSmithRefId());
		loadSmithList();
		setReference(isAfterHire: true);
	}

	private bool selectEmployee(int index)
	{
		if (employeeList[index].checkSmithInShopOrStandby() && (!employeeList[index].checkSmithInShop() || game.getPlayer().getInShopSmithList().Count > 1))
		{
			employee = employeeList[index];
			return true;
		}
		return false;
	}

	private void refreshEmployeeList()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		while (employeeListGrid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(employeeListGrid.transform.GetChild(0).gameObject);
		}
		for (int i = 0; i < player.getShopMaxSmith(); i++)
		{
			if (i < employeeList.Count)
			{
				Smith smith = employeeList[i];
				GameObject gameObject = commonScreenObject.createPrefab(employeeListGrid.gameObject, "EmployeeObj_" + i, "Prefab/SmithManage/EmployeeListObj", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(gameObject, "Employee_bg/Employee_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
				commonScreenObject.findChild(gameObject, "EmployeeName_bg/EmployeeName_label").GetComponent<UILabel>().text = smith.getSmithName();
				commonScreenObject.findChild(gameObject, "EmployeeSalary_bg/EmployeeSalary_label").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(smith.getSmithSalary()) + gameData.getTextByRefId("smithStatsShort07");
				commonScreenObject.findChild(gameObject, "EmployeeStats/atk_bg/atk_label").GetComponent<UILabel>().text = smith.getSmithPower().ToString();
				commonScreenObject.findChild(gameObject, "EmployeeStats/spd_bg/spd_label").GetComponent<UILabel>().text = smith.getSmithIntelligence().ToString();
				commonScreenObject.findChild(gameObject, "EmployeeStats/acc_bg/acc_label").GetComponent<UILabel>().text = smith.getSmithTechnique().ToString();
				commonScreenObject.findChild(gameObject, "EmployeeStats/mag_bg/mag_label").GetComponent<UILabel>().text = smith.getSmithLuck().ToString();
				UIButton componentInChildren = gameObject.GetComponentInChildren<UIButton>();
				componentInChildren.gameObject.name = "Fire_" + i;
				if (player.getSmithList().Count > 1)
				{
					if (smith.checkSmithInShopOrStandby())
					{
						if (smith.checkSmithInShop() && player.getInShopSmithList().Count == 1)
						{
							setFireButton(componentInChildren, aEnable: false);
						}
						else
						{
							setFireButton(componentInChildren, aEnable: true);
						}
					}
					else
					{
						setFireButton(componentInChildren, aEnable: false);
					}
				}
				else
				{
					setFireButton(componentInChildren, aEnable: false);
				}
				if (justHired.Contains(smith.getSmithRefId()))
				{
					commonScreenObject.findChild(gameObject, "Employee_bg/Employee_new").GetComponent<UISprite>().alpha = 1f;
				}
				else
				{
					commonScreenObject.findChild(gameObject, "Employee_bg/Employee_new").GetComponent<UISprite>().alpha = 0f;
				}
			}
			else
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(employeeListGrid.gameObject, "EmployeeObj_" + i, "Prefab/SmithManage/EmployeeListEmpty", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject2.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuSmithHireResult04");
			}
		}
		employeeListGrid.Reposition();
		employeeListScrollbar.value = 0f;
		employeeListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		employeeListGrid.enabled = true;
	}

	private void setFireButton(UIButton fireButton, bool aEnable)
	{
		GameData gameData = game.getGameData();
		if (aEnable)
		{
			fireButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("FIRE").ToUpper(CultureInfo.InvariantCulture);
			fireButton.normalSprite = "bt_red";
			fireButton.pressedSprite = "bt_redpressed";
			fireButton.disabledSprite = "bt_disabled";
			fireButton.hover = new Color(0.75f, 0.75f, 0.75f);
		}
		else
		{
			fireButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("errorCommon08").ToUpper(CultureInfo.InvariantCulture);
			fireButton.normalSprite = "bt_disabled";
			fireButton.pressedSprite = "bt_disabled";
			fireButton.disabledSprite = "bt_disabled";
			fireButton.hover = Color.white;
		}
	}

	public void fireSmith()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		shopMenuController.doFireSmith(employee);
		justFired.Add(employee);
		string randomTextBySetRefId = gameData.getRandomTextBySetRefId("whetsappSmithFire");
		gameData.addNewWhetsappMsg(employee.getSmithName(), randomTextBySetRefId, "Image/Smith/Portraits/" + employee.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		viewController.showGeneralDialoguePopup(GeneralPopupType.GeneralPopupTypeDialogueGeneral, resume: false, gameData.getTextByRefId("menuSmith03"), gameData.getTextByRefIdWithDynText("menuSmithManagement35", "[smithName]", employee.getSmithName()), randomTextBySetRefId, "Image/Smith/Portraits/" + employee.getImage());
		loadSmithList();
		setReference();
	}

	private void loadSmithList()
	{
		Player player = game.getPlayer();
		List<Smith> smithList = player.getSmithList();
		GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>().loadSmithList(smithList);
	}

	private void toggleEmployeeList()
	{
		if (employeeListOpen)
		{
			employeeListOpen = false;
			commonScreenObject.tweenPosition(employeeListTween, employeeListTween.transform.localPosition, employeeListClosedPos, 0.4f, null, string.Empty);
		}
		else
		{
			commonScreenObject.tweenPosition(employeeListTween, employeeListTween.transform.localPosition, employeeListOpenPos, 0.4f, base.gameObject, "employeeListOpenComplete");
		}
	}

	public void employeeListOpenComplete()
	{
		employeeListOpen = true;
	}

	private void refreshShopStats()
	{
		Player player = game.getPlayer();
		shopCapacityLabel.text = player.getSmithList().Count + "/" + player.getShopMaxSmith();
		shopGoldLabel.text = player.getPlayerGold().ToString();
	}
}
