using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISmithListMenuController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private int currentIndex;

	private List<GameObject> smithHUDObjectList;

	private GameObject addSmithHUDObject;

	private UIGrid smithGridHorizontalTop;

	private GameObject hireSmithPanel;

	private TweenPosition hireSmithPanelTween;

	private UIGrid hireSmithPanelGrid;

	private float showPanelLength;

	private bool isHirePanelAnimating;

	private bool isHirePanelOpen;

	private int currentDisplayIndex;

	private UIButton hireSmithLeftButton;

	private UIButton hireSmithRightButton;

	private GameObject currentSelectedPanel;

	private Vector3 hireSmithPanelClosedPos;

	private TweenPosition smithActionTween;

	private UITexture smithActionImage;

	private UILabel smithActionText;

	private List<Hashtable> smithActionList;

	private string notificationSmithRefId;

	private TweenPosition legendaryNotificationTween;

	private bool hasLegendaryDisplay;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		currentIndex = 0;
		smithHUDObjectList = new List<GameObject>();
		smithGridHorizontalTop = commonScreenObject.findChild(base.gameObject, "SmithGridHorizontalTop").GetComponent<UIGrid>();
		hireSmithPanel = commonScreenObject.findChild(base.gameObject, "HireSmithPanel").gameObject;
		hireSmithPanelTween = hireSmithPanel.GetComponent<TweenPosition>();
		hireSmithPanelGrid = hireSmithPanel.GetComponentInChildren<UIGrid>();
		showPanelLength = 0f;
		isHirePanelAnimating = false;
		isHirePanelOpen = false;
		currentDisplayIndex = 0;
		hireSmithLeftButton = commonScreenObject.findChild(base.gameObject, "HireSmithPanel/HireSmith_arrowL").GetComponent<UIButton>();
		hireSmithRightButton = commonScreenObject.findChild(base.gameObject, "HireSmithPanel/HireSmith_arrowR").GetComponent<UIButton>();
		currentSelectedPanel = null;
		hireSmithPanelClosedPos = new Vector3(-25f, 0f, 0f);
		smithActionTween = commonScreenObject.findChild(base.gameObject, "SmithNotification_bg").GetComponent<TweenPosition>();
		smithActionImage = commonScreenObject.findChild(smithActionTween.gameObject, "SmithImage_bg/SmithImage_texture").GetComponent<UITexture>();
		smithActionText = commonScreenObject.findChild(smithActionTween.gameObject, "SmithAction_label").GetComponent<UILabel>();
		smithActionList = new List<Hashtable>();
		notificationSmithRefId = string.Empty;
		legendaryNotificationTween = commonScreenObject.findChild(base.gameObject, "LegendaryNotification").GetComponent<TweenPosition>();
	}

	public void setReference()
	{
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		commonScreenObject.findChild(hireSmithPanel, "HireSmith_tab/HireSmith_label").GetComponent<UILabel>().text = game.getGameData().getTextByRefId("menuMain05").ToUpper(CultureInfo.InvariantCulture);
		List<Smith> smithList = game.getPlayer().getSmithList();
		loadSmithList(smithList);
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "SMITHHIRE", completedTutorialIndex))
		{
			disableSmithHire();
		}
		else
		{
			enableSmithHire();
		}
		hireSmithPanel.transform.localPosition = hireSmithPanelClosedPos;
		hasLegendaryDisplay = false;
		legendaryNotificationTween.transform.localPosition = new Vector3(-220f, 150f, 0f);
	}

	public void loadSmithList(List<Smith> smithList)
	{
		foreach (GameObject smithHUDObject in smithHUDObjectList)
		{
			commonScreenObject.destroyPrefabImmediate(smithHUDObject);
		}
		smithHUDObjectList.Clear();
		currentIndex = 0;
		foreach (Smith smith in smithList)
		{
			GameObject aParent = smithGridHorizontalTop.gameObject;
			GameObject gameObject = commonScreenObject.createPrefab(aParent, "SmithIcon_" + currentIndex, "Prefab/NewHUD/SmithIconObject", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponent<GUISmithListController>().setReference(smith);
			smithHUDObjectList.Add(gameObject);
			currentIndex++;
		}
		smithGridHorizontalTop.GetComponent<UIGrid>().Reposition();
	}

	public void refreshSmithActionProgress()
	{
		foreach (GameObject smithHUDObject in smithHUDObjectList)
		{
			smithHUDObject.GetComponent<GUISmithListController>().refreshSmithActionProgress();
		}
		GameObject gameObject = GameObject.Find("Panel_BottomMenu");
		if (gameObject != null)
		{
			gameObject.GetComponent<BottomMenuController>().updateResearchProgress();
		}
		showSmithActionNotification();
	}

	public GameObject getSmithIconObj(string aRefId)
	{
		foreach (GameObject smithHUDObject in smithHUDObjectList)
		{
			if (smithHUDObject.GetComponent<GUISmithListController>().getSmith().getSmithRefId() == aRefId)
			{
				return smithHUDObject;
			}
		}
		return null;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "HireSmith_tab":
			toggleHireSmithPanel();
			return;
		case "HireSmith_arrowR":
			scrollHireSmith(-1);
			return;
		case "HireSmith_arrowL":
			scrollHireSmith(1);
			return;
		case "HireSmithListObj_confirm":
			confirmRecruitment();
			return;
		case "SmithNotification_bg":
			clickNotification();
			return;
		case "LegendaryNotification":
			clickLegendaryNotification();
			return;
		}
		if (!isHirePanelAnimating)
		{
			openRecruitTab(gameObjectName);
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (commonScreenObject.findChild(hireSmithPanel.gameObject, "HireSmith_tab").GetComponent<UIButton>().isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("200011")) && !viewController.getHasPopup())
		{
			processClick("HireSmith_tab");
		}
	}

	private int getCurrentSelectPanelIndex()
	{
		int result = 0;
		if (currentSelectedPanel != null)
		{
			string[] array = currentSelectedPanel.name.Split('_');
			result = currentDisplayIndex + CommonAPI.parseInt(array[1]);
		}
		return result;
	}

	private void confirmRecruitment()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int currentSelectPanelIndex = getCurrentSelectPanelIndex();
		List<RecruitmentType> recruitmentTypeList = gameData.getRecruitmentTypeList(player.getShopLevelInt(), player.getPlayerMonths());
		recruitmentTypeList.Reverse();
		GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().doStartRecruitment(recruitmentTypeList[currentSelectPanelIndex]);
		closeHirePanel();
	}

	private void scrollHireSmith(int scroll)
	{
		if (!isHirePanelAnimating)
		{
			currentDisplayIndex += scroll;
			arrangeHireSmithObjects();
		}
	}

	private void openRecruitTab(string gameObjectName)
	{
		if (currentSelectedPanel != null)
		{
			if (currentSelectedPanel.name == gameObjectName)
			{
				return;
			}
			closeRecruitTab(currentSelectedPanel);
		}
		currentSelectedPanel = commonScreenObject.findChild(hireSmithPanelGrid.gameObject, gameObjectName).gameObject;
		isHirePanelAnimating = true;
		commonScreenObject.tweenPosition(currentSelectedPanel.GetComponentInChildren<TweenPosition>(), new Vector3(0f, -45f, 0f), Vector3.zero, 0.4f, base.gameObject, "panelAnimationComplete");
		setConfirmButton(setTo: false);
	}

	private void closeRecruitTab(GameObject closeGameObject)
	{
		commonScreenObject.tweenPosition(closeGameObject.GetComponentInChildren<TweenPosition>(), Vector3.zero, new Vector3(0f, -45f, 0f), 0.4f, null, string.Empty);
		closeGameObject.GetComponentInChildren<UIButton>().isEnabled = false;
	}

	public void disableSmithHire()
	{
		hireSmithPanelClosedPos.y = -100f;
		commonScreenObject.findChild(hireSmithPanel.gameObject, "HireSmith_tab").GetComponent<UIButton>().isEnabled = false;
	}

	public void enableSmithHire()
	{
		hireSmithPanelClosedPos.y = 0f;
		commonScreenObject.findChild(hireSmithPanel.gameObject, "HireSmith_tab").GetComponent<UIButton>().isEnabled = true;
	}

	public void toggleHireSmithPanel()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (player.checkTimedAction(TimedAction.TimedActionRecruit))
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getTextByRefId("menuSmithManagement22"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
		else if (!isHirePanelAnimating)
		{
			if (isHirePanelOpen)
			{
				closeHirePanel();
			}
			else
			{
				openHirePanel();
			}
		}
	}

	public void openHirePanel()
	{
		arrangeHireSmithObjects();
		isHirePanelOpen = true;
		isHirePanelAnimating = true;
		Vector3 aEndPosition = hireSmithPanelClosedPos;
		aEndPosition.x += showPanelLength;
		audioController.playSlideEnterAudio();
		commonScreenObject.tweenPosition(hireSmithPanelTween, hireSmithPanelClosedPos, aEndPosition, 0.4f, base.gameObject, "panelAnimationComplete");
		commonScreenObject.tweenPosition(GameObject.Find("Panel_BottomMenu").GetComponent<TweenPosition>(), Vector3.zero, new Vector3(0f, -160f, 0f), 0.4f, null, string.Empty);
	}

	public void closeHirePanel(bool isDismissSmithList = false)
	{
		isHirePanelOpen = false;
		isHirePanelAnimating = true;
		if (currentSelectedPanel != null)
		{
			closeRecruitTab(currentSelectedPanel);
		}
		audioController.playSlideExitAudio();
		commonScreenObject.tweenPosition(hireSmithPanelTween, hireSmithPanel.transform.localPosition, hireSmithPanelClosedPos, 0.4f, base.gameObject, "panelAnimationComplete");
		if (!isDismissSmithList)
		{
			viewController.moveHUD(GameObject.Find("Panel_BottomMenu"), MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
		}
	}

	public void panelAnimationComplete()
	{
		isHirePanelAnimating = false;
		if (!isHirePanelOpen)
		{
			destroyTabsImmediate();
		}
		else if (currentSelectedPanel != null)
		{
			setConfirmButton(setTo: true);
		}
	}

	public void destroyTabsImmediate()
	{
		while (hireSmithPanelGrid.gameObject.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(hireSmithPanelGrid.gameObject.transform.GetChild(0).gameObject);
		}
	}

	private void setConfirmButton(bool setTo)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<RecruitmentType> recruitmentTypeList = gameData.getRecruitmentTypeList(player.getShopLevelInt(), player.getPlayerMonths());
		recruitmentTypeList.Reverse();
		float num = player.checkDecoEffect("RECRUIT_COST", string.Empty);
		int num2 = (int)((float)recruitmentTypeList[getCurrentSelectPanelIndex()].getRecruitmentCost() * num);
		if (num2 > player.getPlayerGold())
		{
			currentSelectedPanel.GetComponentInChildren<UIButton>().gameObject.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("errorCommon05");
			setTo = false;
		}
		if (setTo)
		{
			currentSelectedPanel.GetComponentInChildren<UIButton>().isEnabled = true;
		}
		else
		{
			currentSelectedPanel.GetComponentInChildren<UIButton>().isEnabled = false;
		}
	}

	private void arrangeHireSmithObjects()
	{
		destroyTabsImmediate();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<RecruitmentType> recruitmentTypeList = gameData.getRecruitmentTypeList(player.getShopLevelInt(), player.getPlayerMonths());
		int num = Mathf.Min(recruitmentTypeList.Count, 3);
		if (currentDisplayIndex + num > recruitmentTypeList.Count)
		{
			currentDisplayIndex = recruitmentTypeList.Count - num;
			currentDisplayIndex = Mathf.Max(currentDisplayIndex, 0);
		}
		int num2 = 0;
		int num3 = 0;
		recruitmentTypeList.Reverse();
		foreach (RecruitmentType item in recruitmentTypeList)
		{
			if (num2 >= currentDisplayIndex + 3)
			{
				break;
			}
			if (num2 >= currentDisplayIndex)
			{
				float num4 = player.checkDecoEffect("RECRUIT_COST", string.Empty);
				int num5 = (int)((float)item.getRecruitmentCost() * num4);
				string text = "$" + num5;
				if (num4 > 1f)
				{
					text = "[FF4842]$" + num5 + "[-]";
				}
				else if (num4 < 1f)
				{
					text = "[56AE59]$" + num5 + "[-]";
				}
				GameObject gameObject = commonScreenObject.createPrefab(hireSmithPanelGrid.gameObject, "recruitmentType_" + num3, "Prefab/SmithManage/HireSmithListObj", new Vector3((float)num3 * -138f, 0f, 0f), Vector3.one, Vector3.zero);
				gameObject.transform.Find("HireSmithListObj_bg/HireSmithListObj_name").GetComponent<UILabel>().text = item.getRecruitmentName();
				gameObject.transform.Find("HireSmithListObj_bg/HireSmithListObj_cost").GetComponent<UILabel>().text = text;
				gameObject.transform.Find("HireSmithListObj_bg/HireSmithListObj_confirm/HireSmithListObj_confirmLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuGeneral15");
				num3++;
			}
			num2++;
		}
		showPanelLength = hireSmithPanelGrid.cellWidth * (float)num + 44f;
		hireSmithLeftButton.transform.localPosition = new Vector3(257f - showPanelLength, 0f, 0f);
		if (currentDisplayIndex == 0)
		{
			hireSmithRightButton.isEnabled = false;
		}
		else
		{
			hireSmithRightButton.isEnabled = true;
		}
		if (currentDisplayIndex + num >= recruitmentTypeList.Count)
		{
			hireSmithLeftButton.isEnabled = false;
		}
		else
		{
			hireSmithLeftButton.isEnabled = true;
		}
	}

	public void setOfferWeaponTutorialState()
	{
		foreach (GameObject smithHUDObject in smithHUDObjectList)
		{
			Smith smith = smithHUDObject.GetComponent<GUISmithListController>().getSmith();
			if (smith.getExploreState() == SmithExploreState.SmithExploreStateOffersWaiting)
			{
				smithHUDObject.GetComponentInChildren<UIButton>().isEnabled = true;
			}
			else
			{
				smithHUDObject.GetComponentInChildren<UIButton>().isEnabled = false;
			}
		}
	}

	public void setSellResultTutorialState()
	{
		foreach (GameObject smithHUDObject in smithHUDObjectList)
		{
			Smith smith = smithHUDObject.GetComponent<GUISmithListController>().getSmith();
			if (smith.getExploreState() == SmithExploreState.SmithExploreStateSellWeaponReturned)
			{
				smithHUDObject.GetComponentInChildren<UIButton>().isEnabled = true;
			}
			else
			{
				smithHUDObject.GetComponentInChildren<UIButton>().isEnabled = false;
			}
		}
	}

	public void revertSmithListAfterTutorialState()
	{
		foreach (GameObject smithHUDObject in smithHUDObjectList)
		{
			smithHUDObject.GetComponentInChildren<UIButton>().isEnabled = true;
		}
	}

	public void addSmithActionNotification(string image, string text, string smithRefId)
	{
		Hashtable hashtable = new Hashtable();
		hashtable["image"] = image;
		hashtable["text"] = text;
		hashtable["smithRefId"] = smithRefId;
		smithActionList.Add(hashtable);
	}

	private void showSmithActionNotification()
	{
		if (smithActionTween != null && !smithActionTween.enabled && smithActionList.Count > 0)
		{
			smithActionImage.mainTexture = commonScreenObject.loadTexture(smithActionList[0]["image"].ToString());
			smithActionText.text = smithActionList[0]["text"].ToString();
			notificationSmithRefId = smithActionList[0]["smithRefId"].ToString();
			smithActionList.RemoveAt(0);
		}
	}

	private void clickNotification()
	{
		GameObject smithIconObj = getSmithIconObj(notificationSmithRefId);
		if (smithIconObj != null)
		{
			smithIconObj.GetComponent<GUISmithListController>().processClick(smithIconObj.name);
		}
	}

	public void showLegendaryNotification()
	{
		legendaryNotificationTween.GetComponentInChildren<UILabel>().text = game.getGameData().getTextByRefId("legendaryNotification01");
		if (audioController == null)
		{
			audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		}
		audioController.playLegendAppearAudio();
		hasLegendaryDisplay = true;
		commonScreenObject.tweenPosition(legendaryNotificationTween, new Vector3(-220f, 150f, 0f), new Vector3(0f, 150f, 0f), 0.4f, null, string.Empty);
	}

	public void dismissLegendaryNotification()
	{
		hasLegendaryDisplay = false;
		commonScreenObject.tweenPosition(legendaryNotificationTween, legendaryNotificationTween.transform.localPosition, new Vector3(-220f, 150f, 0f), 0.4f, null, string.Empty);
	}

	public bool checkLegendaryNotificationDisplay()
	{
		return hasLegendaryDisplay;
	}

	private void clickLegendaryNotification()
	{
		if (!legendaryNotificationTween.enabled && legendaryNotificationTween.transform.localPosition.x > -50f)
		{
			GameData gameData = game.getGameData();
			Player player = game.getPlayer();
			List<LegendaryHero> legendaryHeroRequestList = player.getLegendaryHeroRequestList();
			LegendaryHero legendaryHero = legendaryHeroRequestList[legendaryHeroRequestList.Count - 1];
			legendaryHero.setRequestState(RequestState.RequestStateAccepted);
			Weapon weaponByRefId = gameData.getWeaponByRefId(legendaryHero.getWeaponRefId());
			WeaponType weaponTypeByRefId = gameData.getWeaponTypeByRefId(weaponByRefId.getWeaponTypeRefId());
			if (!weaponTypeByRefId.checkUnlocked())
			{
				player.unlockWeaponType(weaponTypeByRefId);
			}
			player.unlockWeapon(weaponByRefId);
			string heroVisitDialogueSetId = legendaryHero.getHeroVisitDialogueSetId();
			if (heroVisitDialogueSetId != string.Empty)
			{
				viewController.showDialoguePopup(heroVisitDialogueSetId, gameData.getDialogueBySetId(heroVisitDialogueSetId), PopupType.PopupTypeLegendaryRequestAccept);
			}
			else
			{
				viewController.showLegendaryRequest(legendaryHero);
			}
			dismissLegendaryNotification();
		}
	}
}
