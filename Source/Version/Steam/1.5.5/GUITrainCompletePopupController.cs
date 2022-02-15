using UnityEngine;

public class GUITrainCompletePopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GameObject panel_ProcessCompletePopup;

	private UISprite banner;

	private GameObject generalText;

	private GameObject button_ok;

	private int selectedIndex;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Button_ok")
		{
			viewController.closeTrainCompletePopup();
			viewController.showSmithManage(PopupType.PopupTypeManageSmith, selectedIndex);
		}
	}

	public void showPopup(string displayText, int aIndex)
	{
		setReference();
		generalText.SetActive(value: true);
		generalText.GetComponent<UILabel>().text = displayText;
		selectedIndex = aIndex;
		button_ok.SetActive(value: true);
		banner.spriteName = "banner-train";
	}

	private void setReference()
	{
		banner = commonScreenObject.findChild(base.gameObject, "Banner").GetComponent<UISprite>();
		generalText = commonScreenObject.findChild(base.gameObject, "GeneralText").gameObject;
		button_ok = commonScreenObject.findChild(base.gameObject, "Button_ok").gameObject;
		button_ok.GetComponentInChildren<UILabel>().text = game.getGameData().getTextByRefId("menuGeneral04");
		generalText.SetActive(value: false);
		button_ok.SetActive(value: false);
	}
}
