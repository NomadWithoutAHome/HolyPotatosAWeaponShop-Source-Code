using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUICodePopupController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UILabel titleLabel;

	private UIButton continueButton;

	private UIButton backButton;

	private UILabel renameText;

	private UILabel inputNameInputLbl;

	private UIInput inputNameInput;

	private PopupType currentPopupType;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "Title_bg/Title_label").GetComponent<UILabel>();
		continueButton = commonScreenObject.findChild(base.gameObject, "continueButton").GetComponent<UIButton>();
		backButton = commonScreenObject.findChild(base.gameObject, "backButton").GetComponent<UIButton>();
		renameText = GameObject.Find("RenameText").GetComponent<UILabel>();
		GameObject gameObject = GameObject.Find("InputNameInputLbl");
		inputNameInputLbl = gameObject.GetComponent<UILabel>();
		inputNameInput = gameObject.GetComponent<UIInput>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")) && continueButton.isEnabled)
		{
			processClick("continueButton");
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("backButton");
		}
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "continueButton":
		{
			string text = inputNameInputLbl.text;
			text = text.Replace("\n", string.Empty);
			processCode(text);
			break;
		}
		case "backButton":
			viewController.closeCodePopup();
			viewController.showSettingsMenu();
			break;
		}
	}

	public void checkInput()
	{
		if (inputNameInputLbl.text == string.Empty)
		{
			CommonAPI.debug("input false");
			continueButton.isEnabled = false;
		}
		else
		{
			continueButton.isEnabled = true;
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		titleLabel.text = gameData.getTextByRefId("codePopup01").ToUpper(CultureInfo.InvariantCulture);
		continueButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("codePopup02");
		backButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("codePopup03");
		renameText.text = gameData.getTextByRefId("codePopup04");
		inputNameInput.defaultText = string.Empty;
		checkInput();
	}

	private void processCode(string inputName)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		List<Code> codeByCode = gameData.getCodeByCode(inputName);
		string text = string.Empty;
		if (codeByCode.Count > 0)
		{
			foreach (Code item in codeByCode)
			{
				if (item.checkUsed())
				{
					continue;
				}
				item.setUsed(aUsed: true);
				switch (item.getUnlockType())
				{
				case UnlockType.UnlockTypeSmith:
				{
					Smith smithByRefId = gameData.getSmithByRefId(item.getRefID());
					if (text != string.Empty)
					{
						text += ", ";
					}
					text += smithByRefId.getSmithName();
					break;
				}
				case UnlockType.UnlockTypeWeapon:
				{
					Weapon weaponByRefId = gameData.getWeaponByRefId(item.getRefID());
					player.unlockWeapon(weaponByRefId);
					viewController.queueItemGetPopup(gameData.getTextByRefId("weaponUnlock01"), "Image/Weapons/" + weaponByRefId.getImage(), gameData.getTextByRefIdWithDynText("weaponUnlock02", "[weaponName]", weaponByRefId.getWeaponName()));
					if (text != string.Empty)
					{
						text += ", ";
					}
					text += weaponByRefId.getWeaponName();
					break;
				}
				}
			}
			if (text != string.Empty)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("codePopup05"), gameData.getTextByRefIdWithDynText("codePopup07", "[Unlockables]", text), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, gameData.getTextByRefId("codePopup09").ToUpper(CultureInfo.InvariantCulture));
			}
			else
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("codePopup11"), gameData.getTextByRefId("codePopup10"), PopupType.PopupTypeEnterCode, null, colorTag: false, null, map: false, gameData.getTextByRefId("codePopup09").ToUpper(CultureInfo.InvariantCulture));
			}
		}
		else
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("codePopup06"), gameData.getTextByRefId("codePopup08"), PopupType.PopupTypeEnterCode, null, colorTag: false, null, map: false, gameData.getTextByRefId("codePopup09").ToUpper(CultureInfo.InvariantCulture));
		}
		viewController.closeCodePopup();
	}
}
