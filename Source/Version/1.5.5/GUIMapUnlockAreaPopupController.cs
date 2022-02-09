using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIMapUnlockAreaPopupController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GUIMapController mapController;

	private GameObject generalPopup;

	private GameObject generalText;

	private GameObject generalTitle;

	private GameObject button_ok;

	private GameObject button_yes;

	private GameObject button_no;

	private Area area;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Button_ok":
			viewController.closeUnlockAreaPopup(resume: false);
			break;
		case "Button_yes":
		{
			Player player = game.getPlayer();
			if (player.tryUseTicket(area.getUnlockTickets()))
			{
				area.setUnlock(aUnlock: true);
				viewController.closeUnlockAreaPopup(resume: false);
			}
			else
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, "You do not have the required amount of ticket", PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
			}
			break;
		}
		case "Button_no":
			viewController.closeUnlockAreaPopup(resume: false);
			break;
		}
	}

	private void Update()
	{
		handleInput();
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Button_ok");
		}
	}

	public void showUnlockAreaPopup(Area aArea)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		area = aArea;
		setReference();
		if (aArea.checkIsUnlock())
		{
			showGeneralPopup(gameData.getTextByRefId("mapText87"), gameData.getTextByRefIdWithDynText("mapText89", "[AreaName]", aArea.getAreaName()));
			return;
		}
		if (player.getUnusedTickets() >= aArea.getUnlockTickets())
		{
			showYesAndNoPopup(gameData.getTextByRefId("mapText88"), gameData.getTextByRefIdWithDynText("mapText90", "[TicketAmount]", aArea.getUnlockTickets().ToString()));
			return;
		}
		List<string> list = new List<string>();
		list.Add("[TicketAmount]");
		list.Add("[TicketAmountReq]");
		List<string> list2 = new List<string>();
		list2.Add(player.getUnusedTickets().ToString());
		list2.Add(aArea.getUnlockTickets().ToString());
		showGeneralPopup(gameData.getTextByRefId("mapText88"), gameData.getTextByRefIdWithDynTextList("mapText91", list, list2));
	}

	private void setReference()
	{
		generalPopup = GameObject.Find("Panel_UnlockAreaPopup");
		generalText = commonScreenObject.findChild(generalPopup, "GeneralText").gameObject;
		generalTitle = commonScreenObject.findChild(generalPopup, "Title_bg").gameObject;
		button_ok = commonScreenObject.findChild(generalPopup, "Button_ok").gameObject;
		button_yes = commonScreenObject.findChild(generalPopup, "Button_yes").gameObject;
		button_no = commonScreenObject.findChild(generalPopup, "Button_no").gameObject;
		button_ok.SetActive(value: true);
		button_yes.SetActive(value: true);
		button_no.SetActive(value: true);
		GameData gameData = game.getGameData();
		button_ok.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		button_yes.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral02");
		button_no.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral03");
		generalText.SetActive(value: false);
		generalTitle.SetActive(value: false);
		button_ok.SetActive(value: false);
		button_yes.SetActive(value: false);
		button_no.SetActive(value: false);
	}

	public void showGeneralPopup(string displayTitle, string displayText)
	{
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
		if (displayTitle != string.Empty)
		{
			generalTitle.SetActive(value: true);
			generalTitle.GetComponentInChildren<UILabel>().text = displayTitle.ToUpper(CultureInfo.InvariantCulture);
		}
		button_ok.SetActive(value: true);
	}

	public void showYesAndNoPopup(string displayTitle, string displayText)
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
	}
}
