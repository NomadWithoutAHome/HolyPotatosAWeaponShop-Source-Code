using System.Globalization;
using UnityEngine;

public class GUIGeneralPopupController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private GameObject generalPopup;

	private GameObject generalText;

	private GameObject generalTitle;

	private GameObject button_ok;

	private GameObject button_yes;

	private GameObject button_no;

	private bool toResume;

	private PopupType additionalPopup;

	private Smith smithInfo;

	private Item selectedItem;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		toResume = false;
		additionalPopup = PopupType.PopupTypeNothing;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Button_ok":
		{
			GameObject gameObject = GameObject.Find("Panel_PayDay");
			if (gameObject == null)
			{
				viewController.closeGeneralPopup(toResume, hide: true, resumeFromPlayerPause: false);
			}
			else
			{
				viewController.closeGeneralPopup(toResume: false, hide: true, resumeFromPlayerPause: false);
			}
			if (additionalPopup != PopupType.PopupTypeNothing)
			{
				switch (additionalPopup)
				{
				case PopupType.PopupTypeManageSmith:
					viewController.showSmithManage(PopupType.PopupTypeManageSmith);
					break;
				case PopupType.PopupTypeHire:
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().showSmithRecruitmentMenu(100);
					break;
				case PopupType.PopupTypeHireFire:
					viewController.showHireFire();
					break;
				case PopupType.PopupTypeForgeWeaponEnchant:
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().showPopEvent(PopEventType.PopEventTypeNaming);
					break;
				case PopupType.PopupTypeSellItem:
					viewController.closeShopPopup(hide: false);
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().showSellMenu(100, selectedItem);
					break;
				case PopupType.PopupTypeSellItemError:
					viewController.showMainMenu(MenuState.MenuStateInventory);
					break;
				case PopupType.PopupTypeLoadSuccess:
					GameObject.Find("LoadingMask").GetComponent<LoadingScript>().startLoadingFromBlack(string.Empty);
					GameObject.Find("GUIGridController").GetComponent<GUIGridController>().createWorld();
					GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().spawnCharacters();
					break;
				case PopupType.PopupTypeGameOver:
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGameOver, resume: false, gameData.getTextByRefId("gameOver"), null, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
					break;
				case PopupType.PopupTypeDogDayStart:
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().checkStartOfDayActionList();
					break;
				case PopupType.PopupTypeBuyFurniture:
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().showFurnitureShop(100);
					break;
				case PopupType.PopupTypeBuyFurnitureNoItem:
					viewController.showMainMenu(MenuState.MenuStateInventory);
					viewController.showTier2Menu(MenuState.MenuStateShopBuyMenu);
					break;
				case PopupType.PopuptypeFinishActivity:
					viewController.showAssignSmithMenu(smithInfo);
					break;
				case PopupType.PopupTypeResearchComplete:
					viewController.showAssignSmithMenu(smithInfo);
					break;
				case PopupType.PopupTypeRequestExpiry:
					GameObject.Find("Panel_RequestList").GetComponent<GUIRequestListController>().checkRequestExpiry();
					break;
				case PopupType.PopupTypeGoldenHammerEvent:
					viewController.showGoldenHammerAwards();
					break;
				case PopupType.PopupTypeEnterCode:
					viewController.showCodePopup();
					break;
				case PopupType.PopupTypeLoadFail:
					Application.LoadLevel("ALLNGUIMENU");
					break;
				}
			}
			break;
		}
		case "Button_yes":
			if (additionalPopup == PopupType.PopupTypeUpgradeShop)
			{
				if (GameObject.Find("Panel_ActivitySelect") != null)
				{
					GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().upgradeShop();
				}
				else
				{
					GameObject.Find("Panel_MapActivitySelect").GetComponent<GUIMapActivitySelectController>().upgradeShop();
				}
				break;
			}
			viewController.closeGeneralPopup(toResume, hide: true, resumeFromPlayerPause: false);
			if (additionalPopup != PopupType.PopupTypeNothing)
			{
				switch (additionalPopup)
				{
				case PopupType.PopupTypeFireAssignSmith:
					GameObject.Find("Panel_AssignSmithHUD").GetComponent<GUIAssignSmithHUDController>().fireSmith();
					break;
				case PopupType.PopupTypeFire:
					GameObject.Find("Panel_SmithHireResult").GetComponent<GUISmithHireResultController>().fireSmith();
					break;
				case PopupType.PopupTypeHire:
					GameObject.Find("Panel_SmithHireResult").GetComponent<GUISmithHireResultController>().hireSmith();
					break;
				case PopupType.PopupTypeSellWeapon:
					GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponControllerOLD>().closeOffers();
					viewController.closeGeneralPopup(toResume: true, hide: true, resumeFromPlayerPause: false);
					break;
				case PopupType.PopupTypeForgeContractAbandonConfirm:
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().forceEndContract();
					break;
				case PopupType.PopupTypeForgeWeaponAbandonConfirm:
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().forceEndForging();
					break;
				case PopupType.PopupTypeOfferNoSell:
					GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().nextWeapon(doSell: false);
					break;
				case PopupType.PopupTypeOfferSell:
					GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().nextWeapon(doSell: true);
					break;
				case PopupType.PopupTypeMapBuyMats:
					GameObject.Find("GUIMapController").GetComponent<GUIMapController>().startBuyMats();
					break;
				case PopupType.PopupTypeMapVacation:
					GameObject.Find("GUIMapController").GetComponent<GUIMapController>().startVacation();
					break;
				case PopupType.PopupTypeMapTraining:
					GameObject.Find("GUIMapController").GetComponent<GUIMapController>().startTraining();
					break;
				case PopupType.PopupTypeSaveLoad:
					GameObject.Find("Panel_SaveLoadPopup").GetComponent<GUISaveLoadPopupController>().saveLoad();
					break;
				case PopupType.PopupTypeSkipTutorial:
				{
					Player player2 = game.getPlayer();
					player2.setSkipTutorials(aBool: true);
					viewController.setGameStarted(started: true);
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().startGame();
					break;
				}
				case PopupType.PopupTypeQuit:
					GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().quit();
					break;
				case PopupType.PopupTypeReturnHome:
					GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().returnToHome();
					break;
				}
			}
			break;
		case "Button_no":
			if (additionalPopup == PopupType.PopupTypeUpgradeShop && GameObject.Find("Panel_ActivitySelect") != null)
			{
				viewController.closeGeneralPopup(toResume: true, hide: true, resumeFromPlayerPause: false);
			}
			else if (additionalPopup == PopupType.PopupTypeUpgradeShop && GameObject.Find("Panel_ActivitySelect") == null)
			{
				viewController.closeGeneralPopup(toResume: false, hide: true, resumeFromPlayerPause: false);
			}
			else
			{
				viewController.closeGeneralPopup(toResume, hide: true, resumeFromPlayerPause: false);
			}
			if (additionalPopup != PopupType.PopupTypeNothing)
			{
				switch (additionalPopup)
				{
				case PopupType.PopupTypeOfferNoSell:
					GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().returnToPopup();
					break;
				case PopupType.PopupTypeOfferSell:
					GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().returnToPopup();
					break;
				case PopupType.PopupTypeQuit:
					GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().enableAllButtons();
					break;
				case PopupType.PopupTypeSkipTutorial:
				{
					Player player = game.getPlayer();
					player.setSkipTutorials(aBool: false);
					viewController.setGameStarted(started: false);
					GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().startGame();
					break;
				}
				}
			}
			break;
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
		if (button_yes.activeSelf && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Button_yes");
		}
		else if (button_ok.activeSelf && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Button_ok");
		}
	}

	public void showDialoguePopup(GeneralPopupType type, bool resume = false, string displayTitle = "", string displayText = "", string displayDialogue = "", string displayImagePath = "", PopupType popupToLoad = PopupType.PopupTypeNothing, bool colorTag = false)
	{
		CommonAPI.debug("showPopup");
		setReference();
		toResume = resume;
		switch (type)
		{
		case GeneralPopupType.GeneralPopupTypeDialogueGeneral:
			showGeneralDialoguePopup(displayTitle, displayText, displayDialogue, displayImagePath, popupToLoad, colorTag);
			break;
		case GeneralPopupType.GeneralPopupTypeDialogueYesAndNo:
			showGeneralDialogueYesAndNoPopup(displayTitle, displayText, displayDialogue, displayImagePath, popupToLoad, colorTag);
			break;
		}
	}

	public void showPopup(GeneralPopupType type, bool resume = false, string displayTitle = null, string displayText = null, PopupType popupToLoad = PopupType.PopupTypeNothing, Item aItem = null, bool colorTag = false, Smith aSmith = null)
	{
		CommonAPI.debug("showPopup");
		setReference();
		toResume = resume;
		if (aSmith != null)
		{
			smithInfo = aSmith;
		}
		audioController.playPopupAudio();
		selectedItem = aItem;
		switch (type)
		{
		case GeneralPopupType.GeneralPopupTypeGeneral:
			showGeneralPopup(displayTitle, displayText, popupToLoad, colorTag);
			break;
		case GeneralPopupType.GeneralPopupTypeYesAndNo:
			showYesAndNoPopup(displayTitle, displayText, popupToLoad);
			break;
		case GeneralPopupType.GeneralPopupTypeGameOver:
			showGameOverPopup(displayTitle, displayText);
			break;
		default:
			showGeneralPopup(displayTitle, displayText, popupToLoad, colorTag);
			break;
		}
	}

	private void setReference()
	{
		generalPopup = GameObject.Find("Panel_GeneralPopup");
		generalText = commonScreenObject.findChild(generalPopup, "GeneralText").gameObject;
		generalTitle = commonScreenObject.findChild(generalPopup, "Title_bg").gameObject;
		button_ok = commonScreenObject.findChild(generalPopup, "Button_ok").gameObject;
		button_yes = commonScreenObject.findChild(generalPopup, "Button_yes").gameObject;
		button_no = commonScreenObject.findChild(generalPopup, "Button_no").gameObject;
		button_ok.SetActive(value: true);
		button_yes.SetActive(value: true);
		button_no.SetActive(value: true);
		CommonAPI.debug("button_yes: " + button_yes.gameObject.name);
		button_ok.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		button_yes.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral02");
		button_no.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral03");
		generalText.SetActive(value: false);
		generalTitle.SetActive(value: false);
		button_ok.SetActive(value: false);
		button_yes.SetActive(value: false);
		button_no.SetActive(value: false);
	}

	public void showGeneralDialoguePopup(string displayTitle, string displayText, string displayDialogue, string displayImagePath, PopupType popupToLoad, bool colorTag)
	{
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
		UILabel component = commonScreenObject.findChild(generalPopup, "GeneralDialogue_label").GetComponent<UILabel>();
		component.text = displayDialogue;
		if (component.height / 16 <= 3)
		{
			int num = 3 - component.height / 16;
			for (int i = 0; i < num; i++)
			{
				component.text += "\n";
			}
		}
		UITexture component2 = commonScreenObject.findChild(generalPopup, "GeneralImage_bg/GeneralImage_texture").GetComponent<UITexture>();
		component2.mainTexture = commonScreenObject.loadTexture(displayImagePath);
		if (displayTitle != string.Empty)
		{
			generalTitle.SetActive(value: true);
			generalTitle.GetComponentInChildren<UILabel>().text = displayTitle.ToUpper(CultureInfo.InvariantCulture);
		}
		if (colorTag)
		{
			generalText.GetComponent<UILabel>().color = Color.white;
		}
		button_ok.SetActive(value: true);
		if (popupToLoad != PopupType.PopupTypeNothing)
		{
			additionalPopup = popupToLoad;
			if (popupToLoad == PopupType.PopupTypeLoadSuccess || popupToLoad == PopupType.PopupTypeLoadFail)
			{
				base.gameObject.GetComponent<UIPanel>().depth = 201;
			}
		}
	}

	public void showGeneralDialogueYesAndNoPopup(string displayTitle, string displayText, string displayDialogue, string displayImagePath, PopupType popupToLoad, bool colorTag)
	{
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
		UILabel component = commonScreenObject.findChild(generalPopup, "GeneralDialogue_label").GetComponent<UILabel>();
		component.text = displayDialogue;
		if (component.height / 16 <= 3)
		{
			int num = 3 - component.height / 16;
			for (int i = 0; i < num; i++)
			{
				component.text += "\n";
			}
		}
		UITexture component2 = commonScreenObject.findChild(generalPopup, "GeneralImage_bg/GeneralImage_texture").GetComponent<UITexture>();
		component2.mainTexture = commonScreenObject.loadTexture(displayImagePath);
		if (displayTitle != string.Empty)
		{
			generalTitle.SetActive(value: true);
			generalTitle.GetComponentInChildren<UILabel>().text = displayTitle.ToUpper(CultureInfo.InvariantCulture);
		}
		button_yes.SetActive(value: true);
		button_no.SetActive(value: true);
		if (popupToLoad != PopupType.PopupTypeNothing)
		{
			additionalPopup = popupToLoad;
		}
	}

	public void showGeneralPopup(string displayTitle, string displayText, PopupType popupToLoad, bool colorTag)
	{
		generalText.SetActive(value: true);
		UILabel component = generalText.GetComponent<UILabel>();
		component.text = displayText;
		if (displayTitle != string.Empty)
		{
			generalTitle.SetActive(value: true);
			generalTitle.GetComponentInChildren<UILabel>().text = displayTitle.ToUpper(CultureInfo.InvariantCulture);
		}
		if (colorTag)
		{
			generalText.GetComponent<UILabel>().color = Color.white;
		}
		button_ok.SetActive(value: true);
		if (popupToLoad != PopupType.PopupTypeNothing)
		{
			additionalPopup = popupToLoad;
			if (popupToLoad == PopupType.PopupTypeLoadSuccess || popupToLoad == PopupType.PopupTypeLoadFail)
			{
				base.gameObject.GetComponent<UIPanel>().depth = 201;
			}
		}
	}

	public void showYesAndNoPopup(string displayTitle, string displayText, PopupType popupToLoad)
	{
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
		if (displayTitle != string.Empty)
		{
			generalTitle.SetActive(value: true);
			generalTitle.GetComponentInChildren<UILabel>().text = displayTitle.ToUpper(CultureInfo.InvariantCulture);
		}
		button_yes.SetActive(value: true);
		button_no.SetActive(value: true);
		if (popupToLoad != PopupType.PopupTypeNothing)
		{
			additionalPopup = popupToLoad;
		}
	}

	public void showGameOverPopup(string displayTitle, string displayText)
	{
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
	}

	public void changeGeneralText(string displayText)
	{
		generalText.GetComponent<UILabel>().text = displayText;
	}
}
