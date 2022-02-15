using System.Collections;
using System.Globalization;
using UnityEngine;

public class GUIResearchCompleteController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private Project project;

	private UILabel researchCompleteTitle;

	private UITexture researchImg;

	private UILabel researchName;

	private UIButton okButton;

	private UILabel okLabel;

	private bool isAnimating;

	private int displayStatSpeed;

	private Smith smithInfo;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		researchCompleteTitle = commonScreenObject.findChild(base.gameObject, "ResearchCompleteTitle_bg/ResearchCompleteTitle_label").GetComponent<UILabel>();
		researchImg = commonScreenObject.findChild(base.gameObject, "ResearchBg/ResearchImg").GetComponent<UITexture>();
		researchName = commonScreenObject.findChild(base.gameObject, "ResearchWeaponText_label").GetComponent<UILabel>();
		okButton = commonScreenObject.findChild(base.gameObject, "Ok_button").GetComponent<UIButton>();
		okLabel = commonScreenObject.findChild(base.gameObject, "Ok_button/Ok_label").GetComponent<UILabel>();
		isAnimating = true;
		displayStatSpeed = 1;
	}

	public void processClick(string gameobjectName)
	{
		if (gameobjectName != null && gameobjectName == "Ok_button" && !isAnimating)
		{
			audioController.playBGMAudio(string.Empty);
			GameObject gameObject = GameObject.Find("Panel_BottomMenu");
			if (gameObject != null)
			{
				gameObject.GetComponent<BottomMenuController>().refreshBottomButtons();
			}
			if (smithInfo != null)
			{
				smithInfo.returnToShopStandby();
				viewController.closeResearchComplete(hide: true, resume: false);
				viewController.showAssignSmithMenu(smithInfo);
			}
			else
			{
				viewController.closeResearchComplete(hide: true, resume: true);
			}
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
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Ok_button");
		}
	}

	public void setSmith(Smith aSmith)
	{
		smithInfo = aSmith;
	}

	public void setStats(Smith aSmith)
	{
		smithInfo = aSmith;
		GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().unassignDeleteCharacter(smithInfo.getSmithRefId());
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Weapon currentResearchWeapon = player.getCurrentResearchWeapon();
		researchCompleteTitle.text = gameData.getTextByRefId("menuResearch21").ToUpper(CultureInfo.InvariantCulture);
		okLabel.text = string.Empty;
		okButton.disabledColor = Color.clear;
		okButton.isEnabled = false;
		researchImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + currentResearchWeapon.getImage());
		researchName.text = gameData.getTextByRefIdWithDynText("menuResearch27", "[weaponName]", currentResearchWeapon.getWeaponName());
		StartCoroutine(displayResults());
	}

	private IEnumerator displayResults()
	{
		while (isAnimating)
		{
			yield return new WaitForSeconds(0.05f);
			GameData gameData = game.getGameData();
			isAnimating = false;
			okLabel.text = gameData.getTextByRefId("menuGeneral05");
			audioController.stopBGMAudio();
			audioController.playResearchCompleteAudio();
			okButton.isEnabled = true;
		}
	}
}
