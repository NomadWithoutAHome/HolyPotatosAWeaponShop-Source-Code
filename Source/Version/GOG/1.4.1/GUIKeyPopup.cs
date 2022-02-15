using UnityEngine;

public class GUIKeyPopup : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private ViewController viewController;

	private Game game;

	private GameData gameData;

	private GUIKeyShortcutController keyCtr;

	private UILabel generalText;

	private UIButton button_ok;

	private UIButton button_yes;

	private UIButton button_no;

	private bool fromStart;

	private bool resetPopup;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		keyCtr = GameObject.Find("Panel_KeyboardShortcut").GetComponent<GUIKeyShortcutController>();
		generalText = commonScreenObject.findChild(base.gameObject, "GeneralText").GetComponent<UILabel>();
		button_yes = commonScreenObject.findChild(base.gameObject, "Button_yes").GetComponent<UIButton>();
		button_no = commonScreenObject.findChild(base.gameObject, "Button_no").GetComponent<UIButton>();
		fromStart = false;
		resetPopup = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Button_yes":
			viewController.closeKeyPopup();
			if (resetPopup)
			{
				keyCtr.resetKeysToDefault();
				keyCtr.enableAllButtons();
			}
			else
			{
				keyCtr.saveChanges();
			}
			break;
		case "Button_no":
			viewController.closeKeyPopup();
			keyCtr.enableAllButtons();
			break;
		}
	}

	public void setReference(bool start, string aText, bool confirmReset)
	{
		resetPopup = confirmReset;
		GameData gameData = game.getGameData();
		generalText.text = aText;
		button_yes.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral02");
		button_no.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral03");
		fromStart = start;
		if (start)
		{
			GetComponent<UIPanel>().depth = 107;
		}
	}
}
