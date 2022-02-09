using System.Collections;
using UnityEngine;

public class GUISettingsConfirmController : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private ViewController viewController;

	private Game game;

	private GameData gameData;

	private UILabel generalText;

	private UILabel countdownText;

	private UIButton button_yes;

	private UIButton button_no;

	private int countdown;

	private bool fromStart;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		generalText = commonScreenObject.findChild(base.gameObject, "GeneralText").GetComponent<UILabel>();
		countdownText = commonScreenObject.findChild(base.gameObject, "CountdownText").GetComponent<UILabel>();
		button_yes = commonScreenObject.findChild(base.gameObject, "Button_yes").GetComponent<UIButton>();
		button_no = commonScreenObject.findChild(base.gameObject, "Button_no").GetComponent<UIButton>();
		countdown = 15;
		fromStart = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Button_yes":
			StopCoroutine("startCountdown");
			GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().keepNewResolution();
			GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().enableAllButtons();
			viewController.closeSettingsConfirm(fromStart);
			break;
		case "Button_no":
			StopCoroutine("startCountdown");
			GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().revertResolution();
			GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().enableAllButtons();
			viewController.closeSettingsConfirm(fromStart);
			break;
		}
	}

	private void Update()
	{
		handleInput();
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Button_no");
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Button_yes");
		}
	}

	public void setReference(bool start)
	{
		GameData gameData = game.getGameData();
		generalText.text = gameData.getTextByRefId("settings12");
		countdownText.text = countdown.ToString();
		button_yes.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral02");
		button_no.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral03");
		fromStart = start;
		if (start)
		{
			GetComponent<UIPanel>().depth = 107;
		}
		StartCoroutine("startCountdown");
	}

	private IEnumerator startCountdown()
	{
		while (countdown > 0)
		{
			countdown--;
			yield return new WaitForSeconds(1f);
			countdownText.text = countdown.ToString();
		}
		processClick("Button_no");
	}
}
