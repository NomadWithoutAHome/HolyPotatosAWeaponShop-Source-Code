using UnityEngine;

public class GUIAssignPatataDogHUDController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private GUIAnimationClickController animClickController;

	private UITexture characterImg;

	private UILabel characterName;

	private UILabel infoLabel;

	private UILabel assignNoteLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		animClickController = GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>();
		characterImg = commonScreenObject.findChild(base.gameObject, "CharacterImg").GetComponent<UITexture>();
		characterName = commonScreenObject.findChild(base.gameObject, "NameFrame/CharacterName").GetComponent<UILabel>();
		infoLabel = commonScreenObject.findChild(base.gameObject, "InfoLabel").GetComponent<UILabel>();
		assignNoteLabel = GameObject.Find("AssignNoteLabel").GetComponent<UILabel>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "CloseButton")
		{
			animClickController.resetAssignValues();
			viewController.closePatataDogAssignMenu(resumeFromPlayerPause: false);
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
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("CloseButton");
		}
	}

	public void setPatataReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string playerName = player.getPlayerName();
		characterImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/patata1");
		characterImg.width = 97;
		characterImg.height = 155;
		characterName.text = playerName;
		infoLabel.text = gameData.getTextByRefIdWithDynText("assignPatata02", "[playerName]", playerName);
		assignNoteLabel.text = gameData.getTextByRefIdWithDynText("assignPatata03", "[playerName]", playerName);
	}

	public void setDogReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string dogName = player.getDogName();
		characterImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/dog");
		characterImg.width = 150;
		characterImg.height = 106;
		characterName.text = dogName;
		assignNoteLabel.text = gameData.getTextByRefIdWithDynText("assignDog02", "[dogName]", dogName);
		string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("assignDog01", "[dogName]", dogName);
		textByRefIdWithDynText += "\n\n[s]                         [/s]";
		switch (player.getDogMoodState())
		{
		case 5:
			textByRefIdWithDynText = textByRefIdWithDynText + "\n\n[i][00AAC7]" + gameData.getTextByRefId("dogMoodText05") + "[-][/i]";
			break;
		case 4:
			textByRefIdWithDynText = textByRefIdWithDynText + "\n\n[i][56AE59]" + gameData.getTextByRefId("dogMoodText04") + "[-][/i]";
			break;
		case 3:
			textByRefIdWithDynText = textByRefIdWithDynText + "\n\n[i][FFD84A]" + gameData.getTextByRefId("dogMoodText03") + "[-][/i]";
			break;
		case 2:
			textByRefIdWithDynText = textByRefIdWithDynText + "\n\n[i][FF9000]" + gameData.getTextByRefId("dogMoodText02") + "[-][/i]";
			break;
		case 1:
			textByRefIdWithDynText = textByRefIdWithDynText + "\n\n[i][FF4842]" + gameData.getTextByRefId("dogMoodText01") + "[-][/i]";
			break;
		}
		infoLabel.text = textByRefIdWithDynText;
	}
}
