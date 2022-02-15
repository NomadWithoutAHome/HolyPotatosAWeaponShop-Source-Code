using UnityEngine;

public class GUIAwardPopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GameObject awardPopup;

	private GameObject generalText;

	private GameObject button_ok;

	private bool toResume;

	private PopupType additionalPopup;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		toResume = false;
		additionalPopup = PopupType.PopupTypeNothing;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Button_ok" && additionalPopup != PopupType.PopupTypeNothing)
		{
			PopupType popupType = additionalPopup;
			if (popupType == PopupType.PopupTypeAward)
			{
				GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().showGoldenHammerAwards();
			}
		}
	}

	public void showPopup(bool resume = false, string displayText = null, PopupType popupToLoad = PopupType.PopupTypeNothing)
	{
		setReference();
		toResume = resume;
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
		button_ok.SetActive(value: true);
		if (popupToLoad != PopupType.PopupTypeNothing)
		{
			additionalPopup = popupToLoad;
		}
	}

	private void setReference()
	{
		awardPopup = GameObject.Find("Panel_AwardPopup");
		generalText = commonScreenObject.findChild(awardPopup, "GeneralText").gameObject;
		button_ok = commonScreenObject.findChild(awardPopup, "Button_ok").gameObject;
		button_ok.GetComponentInChildren<UILabel>().text = game.getGameData().getTextByRefId("menuGeneral04");
		generalText.SetActive(value: false);
		button_ok.SetActive(value: false);
	}

	public void changeGeneralText(string displayText)
	{
		generalText.GetComponent<UILabel>().text = displayText;
	}
}
