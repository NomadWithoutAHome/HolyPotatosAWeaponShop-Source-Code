using UnityEngine;

public class GUILockedIslandPopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GUIMapController mapController;

	private GameObject popup;

	private GameObject generalText;

	private GameObject generalTitle;

	private GameObject button_go;

	private GameObject button_later;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Button_go":
			mapController.destroyArea();
			mapController.createArea(ActivityType.ActivityTypeUnlock);
			viewController.destroyLockedIslandPopup();
			break;
		case "Button_later":
			viewController.destroyLockedIslandPopup();
			break;
		}
	}

	private void setReference()
	{
		popup = GameObject.Find("Panel_LockedIslandPopup");
		generalText = commonScreenObject.findChild(popup, "GeneralText").gameObject;
		generalTitle = commonScreenObject.findChild(popup, "Title_bg").gameObject;
		button_go = commonScreenObject.findChild(popup, "Button_go").gameObject;
		button_later = commonScreenObject.findChild(popup, "Button_later").gameObject;
		GameData gameData = game.getGameData();
		button_go.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("mapText68");
		button_later.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("mapText69");
	}

	public void showPopup(string title, Area aArea)
	{
		setReference();
		GameData gameData = game.getGameData();
		string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("mapText70", "[AreaName]", aArea.getAreaName());
		generalTitle.GetComponentInChildren<UILabel>().text = title;
		generalText.GetComponentInChildren<UILabel>().text = textByRefIdWithDynText;
	}
}
