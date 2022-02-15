using System.Collections.Generic;
using UnityEngine;

public class GUIEventSelectController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UILabel eventText;

	private UILabel selection1Text;

	private UILabel selection2Text;

	private UILabel weekendText;

	private UILabel weekend1Text;

	private UILabel weekend2Text;

	private UILabel weekend3Text;

	private EventSelectType currentPopupType;

	private UILabel resultText;

	private UIButton okButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		eventText = GameObject.Find("EventText").GetComponent<UILabel>();
		selection1Text = GameObject.Find("Selection1Text").GetComponent<UILabel>();
		selection2Text = GameObject.Find("Selection2Text").GetComponent<UILabel>();
		weekendText = GameObject.Find("WeekendText").GetComponent<UILabel>();
		weekend1Text = GameObject.Find("Weekend1Text").GetComponent<UILabel>();
		weekend2Text = GameObject.Find("Weekend2Text").GetComponent<UILabel>();
		weekend3Text = GameObject.Find("Weekend3Text").GetComponent<UILabel>();
		resultText = GameObject.Find("ResultText").GetComponent<UILabel>();
		okButton = GameObject.Find("OkButton").GetComponent<UIButton>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "OkButton")
		{
			viewController.closeEventSelectPopup();
			switch (currentPopupType)
			{
			case EventSelectType.EventSelectTypeResult:
				viewController.resumeEverything();
				shopMenuController.showDayStart();
				break;
			case EventSelectType.EventSelectTypeWeekendResult:
				viewController.resumeEverything();
				shopMenuController.startNewDay();
				break;
			}
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			int input = CommonAPI.parseInt(array[1]);
			EventSelectType eventSelectType = currentPopupType;
			if (eventSelectType != 0 && eventSelectType == EventSelectType.EventSelectTypeWeekend)
			{
				shopMenuController.doWeekendActivity(input);
			}
		}
	}

	private void disableAll()
	{
		eventText.text = string.Empty;
		selection1Text.transform.parent.gameObject.SetActive(value: false);
		selection2Text.transform.parent.gameObject.SetActive(value: false);
		weekendText.text = string.Empty;
		weekend1Text.transform.parent.gameObject.SetActive(value: false);
		weekend2Text.transform.parent.gameObject.SetActive(value: false);
		weekend3Text.transform.parent.gameObject.SetActive(value: false);
		resultText.text = string.Empty;
		okButton.gameObject.SetActive(value: false);
	}

	public void setReference(EventSelectType aPopupType, string resultString, List<string> verdandiText, List<Subquest> subquestList = null)
	{
		disableAll();
		GameData gameData = game.getGameData();
		currentPopupType = aPopupType;
		switch (aPopupType)
		{
		case EventSelectType.EventSelectTypeDayEnd:
			eventText.text = gameData.getTextByRefId("dayEndEvent01") + "\n\n" + verdandiText[0];
			selection1Text.transform.parent.gameObject.SetActive(value: true);
			selection2Text.transform.parent.gameObject.SetActive(value: true);
			selection1Text.text = verdandiText[1];
			selection2Text.text = verdandiText[2];
			break;
		case EventSelectType.EventSelectTypeResult:
		case EventSelectType.EventSelectTypeWeekendResult:
			resultText.text = resultString;
			okButton.gameObject.SetActive(value: true);
			break;
		case EventSelectType.EventSelectTypeWeekend:
			weekendText.text = gameData.getTextByRefId("weekendEvent01") + "\n\n" + gameData.getTextByRefId("weekendEvent02");
			weekend1Text.transform.parent.gameObject.SetActive(value: true);
			weekend2Text.transform.parent.gameObject.SetActive(value: true);
			weekend3Text.transform.parent.gameObject.SetActive(value: true);
			weekend1Text.text = verdandiText[0];
			weekend2Text.text = verdandiText[1];
			weekend3Text.text = verdandiText[2];
			break;
		}
	}
}
