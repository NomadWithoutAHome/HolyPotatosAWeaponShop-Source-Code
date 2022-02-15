using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIGoldenHammerAwardsController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private float wbas;

	private int catNomination;

	private int bestNomination;

	private int catBenchmark;

	private int bestBenchmark;

	private UILabel clickLabel;

	private BoxCollider clickCollider;

	private bool allowClick;

	private UISprite ledsTop;

	private UISprite ledsBottom;

	private UISprite ledsLeft;

	private UISprite ledsRight;

	private int ledStyle;

	private bool isSwitch;

	private UISprite lightsLeft;

	private UISprite lightsRight;

	private float lightsAngle;

	private bool lightDirUp;

	private bool lightsMoving;

	private GameObject screenPanel;

	private UITexture screenBg;

	private UITexture screenRotate;

	private UITexture screenStars1;

	private UITexture screenStars2;

	private bool isScreenRotating;

	private GameObject currentSlide;

	private int stageSet;

	private float offStageY;

	private float onStageY;

	private TweenPosition emceeTween;

	private UITexture emceeTexture;

	private TweenPosition emceeSpotlightPosTween;

	private TweenScale emceeSpotlightScaleTween;

	private TweenPosition judgesTween;

	private TweenPosition judgesSpotlightPosTween;

	private TweenScale judgesSpotlightScaleTween;

	private TweenPosition patataTween;

	private UITexture patataTexture;

	private TweenPosition patataSpotlightPosTween;

	private TweenScale patataSpotlightScaleTween;

	private UISprite patataSpecialSpot;

	private List<Project> goldenHammerList;

	private List<ProjectAchievement> awardList;

	private List<Project> playerContenderList;

	private List<Weapon> opponentContenderList;

	private List<string> opponentShopList;

	private List<string> awardKeyList = new List<string>();

	private List<string> awardValueList = new List<string>();

	private int currentAwardIndex;

	private List<ProjectAchievement> winList;

	private List<int> winPrizeList;

	private bool playerIsContender;

	private bool playerIsWinner;

	private UILabel speechLabel;

	private bool lineIsAnimating;

	private string lineText;

	private int lineDisplayChar;

	private TweenPosition starchTween;

	private UILabel starchLabel;

	private UILabel starchPopLabel;

	private TweenPosition starchPopPosTween;

	private TweenAlpha starchPopAlphaTween;

	private bool allowBribes;

	private string judge1Bribe;

	private string judge2Bribe;

	private string judge3Bribe;

	private int bribe1Value;

	private int bribe2Value;

	private int bribe3Value;

	private int bribe1Count;

	private int bribe2Count;

	private int bribe3Count;

	private float bribeChanceUpAmt;

	private int prevAlertState;

	private TweenPosition ninja1Tween;

	private Vector3 ninja1OutPos;

	private Vector3 ninja1InPos;

	private TweenPosition ninja2Tween;

	private Vector3 ninja2OutPos;

	private Vector3 ninja2InPos;

	private TweenPosition ninja3Tween;

	private Vector3 ninja3OutPos;

	private Vector3 ninja3InPos;

	private TweenPosition ninja4Tween;

	private Vector3 ninja4OutPos;

	private Vector3 ninja4InPos;

	private int alert0Limit;

	private int alert1Limit;

	private int alert2Limit;

	private int alert3Limit;

	private UIButton judge1Button;

	private UILabel judge1BribeLabel;

	private UILabel judge1BribeAmt;

	private UISprite judge1Bubble;

	private UILabel judge1CommentLabel;

	private UITexture judge1Texture;

	private TweenPosition judge1ChancePosTween;

	private TweenAlpha judge1ChanceAlphaTween;

	private TweenScale judge1CompleteTween;

	private float judge1VoteChance;

	private UISprite judge1ResultBubble;

	private UITexture judge1ChoiceTexture;

	private UILabel judge1ChoiceLabel;

	private UIButton judge2Button;

	private UILabel judge2BribeLabel;

	private UILabel judge2BribeAmt;

	private UISprite judge2Bubble;

	private UILabel judge2CommentLabel;

	private UITexture judge2Texture;

	private TweenPosition judge2ChancePosTween;

	private TweenAlpha judge2ChanceAlphaTween;

	private TweenScale judge2CompleteTween;

	private float judge2VoteChance;

	private UISprite judge2ResultBubble;

	private UITexture judge2ChoiceTexture;

	private UILabel judge2ChoiceLabel;

	private UIButton judge3Button;

	private UILabel judge3BribeLabel;

	private UILabel judge3BribeAmt;

	private UISprite judge3Bubble;

	private UILabel judge3CommentLabel;

	private UITexture judge3Texture;

	private TweenPosition judge3ChancePosTween;

	private TweenAlpha judge3ChanceAlphaTween;

	private TweenScale judge3CompleteTween;

	private float judge3VoteChance;

	private UISprite judge3ResultBubble;

	private UITexture judge3ChoiceTexture;

	private UILabel judge3ChoiceLabel;

	private TweenPosition judgeCountdownTween;

	private UILabel judgeCountdownLabel;

	private List<UISprite> judge1Circles;

	private List<UISprite> judge2Circles;

	private List<UISprite> judge3Circles;

	private int circleState;

	private GameObject Curtains_texture;

	private GameObject Curtain_Left;

	private GameObject Curtain_Right;

	private GameObject Curtain_Top;

	private bool judge1ChosePlayer;

	private bool judge2ChosePlayer;

	private bool judge3ChosePlayer;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		GameData gameData = game.getGameData();
		clickLabel = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Click_label").GetComponent<UILabel>();
		clickLabel.text = gameData.getTextByRefId("TapToContinue");
		clickCollider = commonScreenObject.findChild(base.gameObject, "GoldenHammer_bg").GetComponent<BoxCollider>();
		allowClick = false;
		ledsTop = commonScreenObject.findChild(clickCollider.gameObject, "LEDs_top").GetComponent<UISprite>();
		ledsBottom = commonScreenObject.findChild(clickCollider.gameObject, "LEDs_bottom").GetComponent<UISprite>();
		ledsLeft = commonScreenObject.findChild(clickCollider.gameObject, "LEDs_left").GetComponent<UISprite>();
		ledsRight = commonScreenObject.findChild(clickCollider.gameObject, "LEDs_right").GetComponent<UISprite>();
		ledStyle = 0;
		isSwitch = false;
		lightsLeft = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Lights_left").GetComponent<UISprite>();
		lightsRight = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Lights_right").GetComponent<UISprite>();
		screenPanel = commonScreenObject.findChild(base.gameObject, "GoldenHammerScreen_panel").gameObject;
		screenBg = commonScreenObject.findChild(screenPanel.gameObject, "Screen/Screen_bg").GetComponent<UITexture>();
		screenRotate = commonScreenObject.findChild(screenPanel.gameObject, "Screen/ScreenRotate_bg").GetComponent<UITexture>();
		screenStars1 = commonScreenObject.findChild(screenPanel.gameObject, "Screen/Stars1_texture").GetComponent<UITexture>();
		screenStars2 = commonScreenObject.findChild(screenPanel.gameObject, "Screen/Stars2_texture").GetComponent<UITexture>();
		isScreenRotating = false;
		currentSlide = null;
		emceeTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Emcee_texture").GetComponent<TweenPosition>();
		emceeTexture = emceeTween.gameObject.GetComponent<UITexture>();
		emceeSpotlightPosTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Emcee_spotlight").GetComponent<TweenPosition>();
		emceeSpotlightScaleTween = emceeSpotlightPosTween.gameObject.GetComponent<TweenScale>();
		judgesTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Judges_tween").GetComponent<TweenPosition>();
		judgesSpotlightPosTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Judges_spotlight").GetComponent<TweenPosition>();
		judgesSpotlightScaleTween = judgesSpotlightPosTween.gameObject.GetComponent<TweenScale>();
		patataTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Patata_texture").GetComponent<TweenPosition>();
		patataTexture = patataTween.gameObject.GetComponent<UITexture>();
		patataSpotlightPosTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Patata_spotlight").GetComponent<TweenPosition>();
		patataSpotlightScaleTween = patataSpotlightPosTween.gameObject.GetComponent<TweenScale>();
		patataSpecialSpot = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Patata_specialSpot").GetComponent<UISprite>();
		prevAlertState = 0;
		ninja1Tween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Ninja1_texture").GetComponent<TweenPosition>();
		ninja1OutPos = new Vector3(630f, -150f, 0f);
		ninja1InPos = new Vector3(460f, -150f, 0f);
		ninja2Tween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Ninja2_texture").GetComponent<TweenPosition>();
		ninja2OutPos = new Vector3(-700f, -30f, 0f);
		ninja2InPos = new Vector3(-440f, -30f, 0f);
		ninja3Tween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Ninja3_texture").GetComponent<TweenPosition>();
		ninja3OutPos = new Vector3(715f, 100f, 0f);
		ninja3InPos = new Vector3(460f, 100f, 0f);
		ninja4Tween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Ninja4_texture").GetComponent<TweenPosition>();
		ninja4OutPos = new Vector3(-200f, 500f, 0f);
		ninja4InPos = new Vector3(-200f, 240f, 0f);
		alert0Limit = 5;
		alert1Limit = 10;
		alert2Limit = 15;
		alert3Limit = 20;
		judge1Button = commonScreenObject.findChild(judgesTween.gameObject, "judge1/judge1_button").GetComponent<UIButton>();
		judge1BribeLabel = commonScreenObject.findChild(judge1Button.gameObject, "Bribe_label").GetComponent<UILabel>();
		judge1BribeAmt = commonScreenObject.findChild(judge1Button.gameObject, "BribeAmt_label").GetComponent<UILabel>();
		judge1Bubble = commonScreenObject.findChild(judgesTween.gameObject, "judge1/judge1_bubble").GetComponent<UISprite>();
		judge1CommentLabel = commonScreenObject.findChild(judge1Bubble.gameObject, "judge1Comment_label").GetComponent<UILabel>();
		judge1Texture = commonScreenObject.findChild(judgesTween.gameObject, "judge1/judge1_texture").GetComponent<UITexture>();
		judge1ChancePosTween = commonScreenObject.findChild(judge1Bubble.gameObject, "judge1Pop").GetComponent<TweenPosition>();
		judge1ChanceAlphaTween = judge1ChancePosTween.GetComponent<TweenAlpha>();
		judge1CompleteTween = commonScreenObject.findChild(judge1Bubble.gameObject, "judge1Complete").GetComponent<TweenScale>();
		judge1CompleteTween.GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards07");
		judge1ChancePosTween.GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards06");
		judge1VoteChance = 0f;
		judge1ResultBubble = commonScreenObject.findChild(judgesTween.gameObject, "judge1/judge1Result_bubble").GetComponent<UISprite>();
		judge1ChoiceTexture = commonScreenObject.findChild(judge1ResultBubble.gameObject, "judge1Weapon_texture").GetComponent<UITexture>();
		judge1ChoiceLabel = commonScreenObject.findChild(judge1ResultBubble.gameObject, "judge1Weapon_label").GetComponent<UILabel>();
		judge2Button = commonScreenObject.findChild(judgesTween.gameObject, "judge2/judge2_button").GetComponent<UIButton>();
		judge2BribeLabel = commonScreenObject.findChild(judge2Button.gameObject, "Bribe_label").GetComponent<UILabel>();
		judge2BribeAmt = commonScreenObject.findChild(judge2Button.gameObject, "BribeAmt_label").GetComponent<UILabel>();
		judge2Bubble = commonScreenObject.findChild(judgesTween.gameObject, "judge2/judge2_bubble").GetComponent<UISprite>();
		judge2CommentLabel = commonScreenObject.findChild(judge2Bubble.gameObject, "judge2Comment_label").GetComponent<UILabel>();
		judge2Texture = commonScreenObject.findChild(judgesTween.gameObject, "judge2/judge2_texture").GetComponent<UITexture>();
		judge2ChancePosTween = commonScreenObject.findChild(judge2Bubble.gameObject, "judge2Pop").GetComponent<TweenPosition>();
		judge2ChanceAlphaTween = judge2ChancePosTween.GetComponent<TweenAlpha>();
		judge2CompleteTween = commonScreenObject.findChild(judge2Bubble.gameObject, "judge2Complete").GetComponent<TweenScale>();
		judge2CompleteTween.GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards07");
		judge2ChancePosTween.GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards06");
		judge2VoteChance = 0f;
		judge2ResultBubble = commonScreenObject.findChild(judgesTween.gameObject, "judge2/judge2Result_bubble").GetComponent<UISprite>();
		judge2ChoiceTexture = commonScreenObject.findChild(judge2ResultBubble.gameObject, "judge2Weapon_texture").GetComponent<UITexture>();
		judge2ChoiceLabel = commonScreenObject.findChild(judge2ResultBubble.gameObject, "judge2Weapon_label").GetComponent<UILabel>();
		judge3Button = commonScreenObject.findChild(judgesTween.gameObject, "judge3/judge3_button").GetComponent<UIButton>();
		judge3BribeLabel = commonScreenObject.findChild(judge3Button.gameObject, "Bribe_label").GetComponent<UILabel>();
		judge3BribeAmt = commonScreenObject.findChild(judge3Button.gameObject, "BribeAmt_label").GetComponent<UILabel>();
		judge3Bubble = commonScreenObject.findChild(judgesTween.gameObject, "judge3/judge3_bubble").GetComponent<UISprite>();
		judge3CommentLabel = commonScreenObject.findChild(judge3Bubble.gameObject, "judge3Comment_label").GetComponent<UILabel>();
		judge3Texture = commonScreenObject.findChild(judgesTween.gameObject, "judge3/judge3_texture").GetComponent<UITexture>();
		judge3ChancePosTween = commonScreenObject.findChild(judge3Bubble.gameObject, "judge3Pop").GetComponent<TweenPosition>();
		judge3ChanceAlphaTween = judge3ChancePosTween.GetComponent<TweenAlpha>();
		judge3CompleteTween = commonScreenObject.findChild(judge3Bubble.gameObject, "judge3Complete").GetComponent<TweenScale>();
		judge3CompleteTween.GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards07");
		judge3ChancePosTween.GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards06");
		judge3VoteChance = 0f;
		judge3ResultBubble = commonScreenObject.findChild(judgesTween.gameObject, "judge3/judge3Result_bubble").GetComponent<UISprite>();
		judge3ChoiceTexture = commonScreenObject.findChild(judge3ResultBubble.gameObject, "judge3Weapon_texture").GetComponent<UITexture>();
		judge3ChoiceLabel = commonScreenObject.findChild(judge3ResultBubble.gameObject, "judge3Weapon_label").GetComponent<UILabel>();
		judgeCountdownTween = commonScreenObject.findChild(judgesTween.gameObject, "JudgeTimer_bg").GetComponent<TweenPosition>();
		judgeCountdownLabel = commonScreenObject.findChild(judgeCountdownTween.gameObject, "JudgeTimer_label").GetComponent<UILabel>();
		commonScreenObject.findChild(judgeCountdownTween.gameObject, "JudgeTimer_title").GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards05");
		judge1Circles = new List<UISprite>();
		GameObject gameObject = commonScreenObject.findChild(judge1ResultBubble.gameObject, "judge1Bg_6").gameObject;
		judge1Circles.Add(gameObject.GetComponent<UISprite>());
		judge1Circles.Add(commonScreenObject.findChild(gameObject.gameObject, "judge1Bg_5").GetComponent<UISprite>());
		judge1Circles.Add(commonScreenObject.findChild(gameObject.gameObject, "judge1Bg_4").GetComponent<UISprite>());
		judge1Circles.Add(commonScreenObject.findChild(gameObject.gameObject, "judge1Bg_3").GetComponent<UISprite>());
		judge1Circles.Add(commonScreenObject.findChild(gameObject.gameObject, "judge1Bg_2").GetComponent<UISprite>());
		judge1Circles.Add(commonScreenObject.findChild(gameObject.gameObject, "judge1Bg_1").GetComponent<UISprite>());
		judge2Circles = new List<UISprite>();
		GameObject gameObject2 = commonScreenObject.findChild(judge2ResultBubble.gameObject, "judge2Bg_6").gameObject;
		judge2Circles.Add(gameObject2.GetComponent<UISprite>());
		judge2Circles.Add(commonScreenObject.findChild(gameObject2.gameObject, "judge2Bg_5").GetComponent<UISprite>());
		judge2Circles.Add(commonScreenObject.findChild(gameObject2.gameObject, "judge2Bg_4").GetComponent<UISprite>());
		judge2Circles.Add(commonScreenObject.findChild(gameObject2.gameObject, "judge2Bg_3").GetComponent<UISprite>());
		judge2Circles.Add(commonScreenObject.findChild(gameObject2.gameObject, "judge2Bg_2").GetComponent<UISprite>());
		judge2Circles.Add(commonScreenObject.findChild(gameObject2.gameObject, "judge2Bg_1").GetComponent<UISprite>());
		judge3Circles = new List<UISprite>();
		GameObject gameObject3 = commonScreenObject.findChild(judge3ResultBubble.gameObject, "judge3Bg_6").gameObject;
		judge3Circles.Add(gameObject3.GetComponent<UISprite>());
		judge3Circles.Add(commonScreenObject.findChild(gameObject3.gameObject, "judge3Bg_5").GetComponent<UISprite>());
		judge3Circles.Add(commonScreenObject.findChild(gameObject3.gameObject, "judge3Bg_4").GetComponent<UISprite>());
		judge3Circles.Add(commonScreenObject.findChild(gameObject3.gameObject, "judge3Bg_3").GetComponent<UISprite>());
		judge3Circles.Add(commonScreenObject.findChild(gameObject3.gameObject, "judge3Bg_2").GetComponent<UISprite>());
		judge3Circles.Add(commonScreenObject.findChild(gameObject3.gameObject, "judge3Bg_1").GetComponent<UISprite>());
		circleState = 0;
		Curtains_texture = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Curtains_texture").gameObject;
		Curtain_Left = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Curtain_Left").gameObject;
		Curtain_Right = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Curtain_Right").gameObject;
		Curtain_Top = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Curtain_Top").gameObject;
		speechLabel = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/Textbox_label").GetComponent<UILabel>();
		lineIsAnimating = false;
		lineText = string.Empty;
		lineDisplayChar = 0;
		starchTween = commonScreenObject.findChild(base.gameObject, "GoldenHammerFg_panel/PlayerStarch_bg").GetComponent<TweenPosition>();
		starchLabel = commonScreenObject.findChild(starchTween.gameObject, "PlayerStarch_label").GetComponent<UILabel>();
		starchPopLabel = commonScreenObject.findChild(starchTween.gameObject, "StarchPop_label").GetComponent<UILabel>();
		starchPopPosTween = starchPopLabel.GetComponent<TweenPosition>();
		starchPopAlphaTween = starchPopLabel.GetComponent<TweenAlpha>();
		UILabel component = commonScreenObject.findChild(starchTween.gameObject, "PlayerStarch_title").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("playerStats12");
		disableBribes(showAnim: false);
		offStageY = 500f;
		onStageY = -220f;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "GoldenHammer_bg":
			setStage(isPlayerClick: true);
			break;
		case "judge1_button":
			bribeJudge(1);
			break;
		case "judge2_button":
			bribeJudge(2);
			break;
		case "judge3_button":
			bribeJudge(3);
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName == null)
			{
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		float floatConstantByRefID = gameData.getFloatConstantByRefID(formulaConstantsSet + "_WAWARDS_BASE");
		float floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_WAWARDS_MULT");
		float floatConstantByRefID3 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_WAWARDS_ADD");
		float floatConstantByRefID4 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_WAWARDS_POWER");
		wbas = floatConstantByRefID + floatConstantByRefID2 * Mathf.Pow(floatConstantByRefID3 + (float)player.getNextGoldenHammerYear() + 1f, floatConstantByRefID4);
		catNomination = (int)(0.385f * wbas);
		bestNomination = (int)(0.7f * wbas);
		catBenchmark = (int)(0.55f * wbas);
		bestBenchmark = (int)(1f * wbas);
		Curtain_Left.SetActive(value: false);
		Curtain_Right.SetActive(value: false);
		Curtain_Top.SetActive(value: false);
		audioController.switchBGM("awards");
		stageSet = 0;
		stopLights();
		goldenHammerList = player.getGoldenHammerWeaponList();
		getAwardList();
		List<string> list = new List<string>();
		list.Add("[award1]");
		list.Add("[award2]");
		list.Add("[award3]");
		List<string> list2 = new List<string>();
		list.Add("[award1]");
		list.Add("[award2]");
		list.Add("[award3]");
		currentAwardIndex = 0;
		winList = new List<ProjectAchievement>();
		winPrizeList = new List<int>();
		chooseContenders();
		resetBribes();
		setStage(isPlayerClick: false);
	}

	private void setStage(bool isPlayerClick)
	{
		if (lineIsAnimating)
		{
			forceText();
		}
		else if (!isPlayerClick || allowClick)
		{
			switch (stageSet)
			{
			case 0:
				stageSet = 1;
				StartCoroutine(showStageSetTitle());
				break;
			case 1:
				stageSet = 2;
				StartCoroutine(showStageSetAwards());
				break;
			case 2:
				stageSet = 3;
				StartCoroutine(showStageSetAwardTitle(0));
				break;
			case 3:
				stageSet = 4;
				StartCoroutine(showNominees(0));
				break;
			case 4:
				stageSet = 5;
				StartCoroutine(showStageSetAwardJudging(0));
				break;
			case 5:
				stageSet = 6;
				StartCoroutine(endJudging(0));
				break;
			case 6:
				stageSet = 7;
				StartCoroutine(showResult(0));
				break;
			case 7:
				stageSet = 8;
				StartCoroutine(showWinner(0));
				break;
			case 8:
				stageSet = 9;
				StartCoroutine(showStageSetAwardTitle(1));
				break;
			case 9:
				stageSet = 10;
				StartCoroutine(showNominees(1));
				break;
			case 10:
				stageSet = 11;
				StartCoroutine(showStageSetAwardJudging(1));
				break;
			case 11:
				stageSet = 12;
				StartCoroutine(endJudging(1));
				break;
			case 12:
				stageSet = 13;
				StartCoroutine(showResult(1));
				break;
			case 13:
				stageSet = 14;
				StartCoroutine(showWinner(1));
				break;
			case 14:
				stageSet = 15;
				StartCoroutine(showStageSetAwardTitle(2));
				break;
			case 15:
				stageSet = 16;
				StartCoroutine(showNominees(2));
				break;
			case 16:
				stageSet = 17;
				StartCoroutine(showStageSetAwardJudging(2));
				break;
			case 17:
				stageSet = 18;
				StartCoroutine(endJudging(2));
				break;
			case 18:
				stageSet = 19;
				StartCoroutine(showResult(2));
				break;
			case 19:
				stageSet = 20;
				StartCoroutine(showWinner(2));
				break;
			case 20:
				stageSet = 21;
				StartCoroutine(showEventEnd());
				break;
			default:
				endAwardsEvent();
				break;
			}
		}
	}

	private void endAwardsEvent()
	{
		Player player = game.getPlayer();
		player.addPastGoldenHammerAwards(player.getNextGoldenHammerYear(), player.getNextGoldenHammerAwardListString());
		player.addNextGoldenHammerYear();
		Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
		audioController.changeBGM(CommonAPI.getSeasonBGM(seasonByMonth));
		if (winList.Count > 0)
		{
			viewController.showGoldenHammerResults(winList, winPrizeList);
			viewController.closeGoldenHammerAwards(resumeGame: false);
		}
		else
		{
			viewController.closeGoldenHammerAwards(resumeGame: true);
		}
	}

	private IEnumerator showStageSetTitle()
	{
		GameData gameData = game.getGameData();
		hideClickLabel();
		setScreenBg("bg-normal");
		showScreenSlide("Screen_title");
		showStars();
		stopScreenRotate(hide: true);
		setLEDStyle(5, aSwitch: true);
		yield return new WaitForSeconds(0.5f);
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-arrival scene");
		enterStage(emceeTween, emceeSpotlightPosTween, emceeSpotlightScaleTween, 300f);
		yield return new WaitForSeconds(1f);
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-normal scene-1");
		showText(gameData.getTextByRefId("goldenHammerText01"));
		showClickLabel();
	}

	private IEnumerator showStageSetAwards()
	{
		GameData gameData = game.getGameData();
		showScreenSlide("Screen_awards");
		hideStars();
		setLEDStyle(3, aSwitch: true);
		hideClickLabel();
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-normal scene-2");
		showText(gameData.getTextByRefIdWithDynTextList("goldenHammerText02", awardKeyList, awardValueList));
		int awardIndex = 0;
		foreach (ProjectAchievement award in awardList)
		{
			string aPath = string.Empty;
			switch (awardIndex)
			{
			case 0:
				aPath = "TrophyLeft_sprite";
				break;
			case 1:
				aPath = "TrophyRight_sprite";
				break;
			case 2:
				aPath = "TrophyMid_sprite";
				break;
			}
			UISprite component = commonScreenObject.findChild(currentSlide, aPath).GetComponent<UISprite>();
			switch (award)
			{
			case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
				component.spriteName = "trophy-most powerful";
				component.width = 200;
				component.height = 180;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
				component.spriteName = "trophy-the fastest";
				component.width = 200;
				component.height = 180;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
				component.spriteName = "trophy-most accurate";
				component.width = 200;
				component.height = 180;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
				component.spriteName = "trophy-most magical";
				component.width = 200;
				component.height = 180;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
				component.spriteName = "trophy-overall best";
				component.width = 200;
				component.height = 236;
				break;
			}
			awardIndex++;
		}
		yield return new WaitForSeconds(0.9f);
		showClickLabel();
	}

	private IEnumerator showStageSetAwardTitle(int awardIndex)
	{
		GameData gameData = game.getGameData();
		setScreenBg("bg-normal");
		showScreenSlide("Screen_awardTitle");
		showStars();
		lightsOn();
		stopLights();
		stopScreenRotate(hide: true);
		setLEDStyle(1, aSwitch: false);
		hideClickLabel();
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-normal scene-1");
		if (patataTween.transform.localPosition.y < 0f)
		{
			leaveStage(patataTween, patataSpotlightPosTween, patataSpotlightScaleTween);
			patataSpecialSpot.alpha = 0f;
			audioController.fadeinBGM();
		}
		hideStarch();
		ProjectAchievement showAward = awardList[awardIndex];
		switch (awardIndex)
		{
		case 0:
			showText(gameData.getTextByRefIdWithDynText("goldenHammerText03", awardKeyList[0], awardValueList[0]));
			break;
		case 1:
			showText(gameData.getTextByRefIdWithDynText("goldenHammerText04", awardKeyList[1], awardValueList[1]));
			break;
		case 2:
			showText(gameData.getTextByRefIdWithDynText("goldenHammerText05", awardKeyList[2], awardValueList[2]));
			break;
		}
		UISprite awardSprite = commonScreenObject.findChild(currentSlide, "TrophyMid_sprite").GetComponent<UISprite>();
		switch (showAward)
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			awardSprite.spriteName = "trophy-most powerful";
			awardSprite.width = 200;
			awardSprite.height = 180;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			awardSprite.spriteName = "trophy-the fastest";
			awardSprite.width = 200;
			awardSprite.height = 180;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			awardSprite.spriteName = "trophy-most accurate";
			awardSprite.width = 200;
			awardSprite.height = 180;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			awardSprite.spriteName = "trophy-most magical";
			awardSprite.width = 200;
			awardSprite.height = 180;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			awardSprite.spriteName = "trophy-overall best";
			awardSprite.width = 200;
			awardSprite.height = 236;
			break;
		}
		yield return new WaitForSeconds(0.5f);
		showClickLabel();
	}

	private IEnumerator showNominees(int awardIndex)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		hideClickLabel();
		showScreenSlide("Screen_nominees");
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-normal scene-2");
		switch (awardIndex)
		{
		case 0:
			showText(gameData.getTextByRefId("goldenHammerText16"));
			break;
		case 1:
			showText(gameData.getTextByRefId("goldenHammerText17"));
			break;
		default:
			showText(gameData.getTextByRefId("goldenHammerText18"));
			break;
		}
		ProjectAchievement currentAward = awardList[awardIndex];
		string random1WeaponType = CommonAPI.getRandomWeaponTypeForAward(currentAward);
		Weapon random1Weapon = gameData.getRandomWeaponFromType(random1WeaponType, string.Empty);
		string random1ShopName = gameData.getRandomTextBySetRefId("goldenHammerOpponent");
		string random2WeaponType2 = CommonAPI.getRandomWeaponTypeForAward(currentAward);
		Weapon random2Weapon = gameData.getRandomWeaponFromType(random2WeaponType2, string.Empty);
		string random2ShopName = gameData.getRandomTextBySetRefId("goldenHammerOpponent");
		int safeguard2 = 0;
		while (random2ShopName == random1ShopName && random2Weapon.getWeaponRefId() == random1Weapon.getWeaponRefId() && safeguard2 < 10)
		{
			random2WeaponType2 = CommonAPI.getRandomWeaponTypeForAward(currentAward);
			random2Weapon = gameData.getRandomWeaponFromType(random2WeaponType2, string.Empty);
			random2ShopName = gameData.getRandomTextBySetRefId("goldenHammerOpponent");
			safeguard2++;
		}
		List<int> randomOrder = CommonAPI.getRandomIntList(4, 4);
		string statIcon = string.Empty;
		switch (currentAward)
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			statIcon = "ico_atk";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			statIcon = "ico_speed";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			statIcon = "ico_acc";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			statIcon = "ico_enh";
			break;
		}
		for (int i = 1; i <= 4; i++)
		{
			UISprite component = commonScreenObject.findChild(currentSlide, "Nominee" + i + "_bg").GetComponent<UISprite>();
			string spriteName = "new-nominees-red";
			int num = 0;
			Weapon weapon = random1Weapon;
			string text = weapon.getWeaponName();
			string text2 = random1ShopName;
			switch (randomOrder[i - 1])
			{
			case 1:
				num = ((currentAward != 0) ? catNomination : bestNomination);
				break;
			case 2:
				weapon = random2Weapon;
				text = weapon.getWeaponName();
				text2 = random2ShopName;
				num = ((currentAward != 0) ? Random.Range(catNomination, catBenchmark) : Random.Range(bestNomination, bestBenchmark));
				break;
			case 3:
				weapon = opponentContenderList[awardIndex];
				text = weapon.getWeaponName();
				text2 = opponentShopList[awardIndex];
				num = ((currentAward != 0) ? catBenchmark : bestBenchmark);
				break;
			default:
				if (playerContenderList[awardIndex].getProjectType() != ProjectType.ProjectTypeNothing)
				{
					playerIsContender = true;
					weapon = playerContenderList[awardIndex].getProjectWeapon();
					text = playerContenderList[awardIndex].getProjectName(includePrefix: true);
					text2 = player.getShopName();
					int num2 = ((currentAward != 0) ? catBenchmark : bestBenchmark);
					spriteName = "new-nominees-blue";
					switch (currentAward)
					{
					case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
						num = playerContenderList[awardIndex].getAtk();
						break;
					case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
						num = playerContenderList[awardIndex].getSpd();
						break;
					case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
						num = playerContenderList[awardIndex].getAcc();
						break;
					case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
						num = playerContenderList[awardIndex].getMag();
						break;
					case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
						num = playerContenderList[awardIndex].getTotalStat();
						break;
					}
					if (num > num2)
					{
						playerIsWinner = true;
					}
					else
					{
						playerIsWinner = false;
					}
				}
				else
				{
					playerIsContender = false;
					playerIsWinner = false;
					string randomWeaponTypeForAward = CommonAPI.getRandomWeaponTypeForAward(currentAward);
					weapon = gameData.getRandomWeaponFromType(randomWeaponTypeForAward, string.Empty);
					text = weapon.getWeaponName();
					text2 = gameData.getRandomTextBySetRefId("goldenHammerOpponent");
					safeguard2 = 0;
					while (((text2 == random1ShopName && weapon.getWeaponRefId() == random1Weapon.getWeaponRefId()) || (text2 == random2ShopName && weapon.getWeaponRefId() == random2Weapon.getWeaponRefId())) && safeguard2 < 10)
					{
						randomWeaponTypeForAward = CommonAPI.getRandomWeaponTypeForAward(currentAward);
						weapon = gameData.getRandomWeaponFromType(randomWeaponTypeForAward, string.Empty);
						text = weapon.getWeaponName();
						text2 = gameData.getRandomTextBySetRefId("goldenHammerOpponent");
						safeguard2++;
					}
					num = ((currentAward != 0) ? Random.Range(catNomination, catBenchmark) : Random.Range(bestNomination, bestBenchmark));
				}
				break;
			}
			component.spriteName = spriteName;
			if (statIcon == string.Empty)
			{
				commonScreenObject.findChild(component.gameObject, "stat_title").GetComponent<UILabel>().text = gameData.getTextByRefId("goldenHammerAwards14");
				commonScreenObject.findChild(component.gameObject, "stat_icon").GetComponent<UISprite>().alpha = 0f;
			}
			else
			{
				commonScreenObject.findChild(component.gameObject, "stat_title").GetComponent<UILabel>().text = string.Empty;
				commonScreenObject.findChild(component.gameObject, "stat_icon").GetComponent<UISprite>().spriteName = statIcon;
			}
			commonScreenObject.findChild(component.gameObject, "stat_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(num);
			commonScreenObject.findChild(component.gameObject, "NomineeName_label").GetComponent<UILabel>().text = text;
			commonScreenObject.findChild(component.gameObject, "NomineeShop_label").GetComponent<UILabel>().text = text2;
			commonScreenObject.findChild(component.gameObject, "Nominee_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weapon.getImage());
		}
		yield return new WaitForSeconds(1f);
		showClickLabel();
	}

	private IEnumerator showStageSetAwardJudging(int awardIndex)
	{
		GameData gameData = game.getGameData();
		showScreenSlide("Screen_awardJudging");
		hideStars();
		setLEDStyle(5, aSwitch: true);
		audioController.fadeoutBGM();
		audioController.startWaitingLoopAudio();
		hideClickLabel();
		hideText();
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-arrival scene");
		leaveStage(emceeTween, emceeSpotlightPosTween, emceeSpotlightScaleTween);
		setCharacterImage(judge1Texture, "Image/weapon awards/host & judges/chara-host1-arrival scene");
		setCharacterImage(judge2Texture, "Image/weapon awards/host & judges/chara-host2-arrival scene");
		setCharacterImage(judge3Texture, "Image/weapon awards/host & judges/chara-host3-arrival scene");
		hideJudgeResults();
		showJudgeCountdown();
		enterStage(judgesTween, judgesSpotlightPosTween, judgesSpotlightScaleTween, 0f);
		disableBribes(showAnim: false);
		refreshStarchAmt();
		showStarch();
		ProjectAchievement showAward = awardList[awardIndex];
		UISprite awardSprite = commonScreenObject.findChild(currentSlide, "AwardTitle_sprite").GetComponent<UISprite>();
		switch (showAward)
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			awardSprite.spriteName = "title-most powerful";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			awardSprite.spriteName = "title-the fastest";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			awardSprite.spriteName = "title-most accurate";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			awardSprite.spriteName = "title-most magical";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			awardSprite.spriteName = "title-overall best";
			break;
		}
		setJudgesChoice(awardIndex);
		yield return new WaitForSeconds(1.5f);
		setCharacterImage(judge1Texture, "Image/weapon awards/host & judges/chara-host1-normal scene");
		setCharacterImage(judge2Texture, "Image/weapon awards/host & judges/chara-host2-normal scene");
		setCharacterImage(judge3Texture, "Image/weapon awards/host & judges/chara-host3-normal scene");
		startBribePhase();
		int waitIntervals = 500;
		while (waitIntervals > 0 && allowBribes && playerIsContender && !playerIsWinner)
		{
			doJudgesMovement(waitIntervals);
			updateNinjaState();
			waitIntervals--;
			yield return new WaitForSeconds(0.01f);
		}
		setStage(isPlayerClick: false);
	}

	private IEnumerator endJudging(int awardIndex)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		disableBribes();
		hideStarch();
		audioController.stopWaitingLoopAudio();
		if (checkDisqualified())
		{
			leaveStage(judgesTween, judgesSpotlightPosTween, judgesSpotlightScaleTween);
			setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-disqualification scene");
			enterStage(emceeTween, emceeSpotlightPosTween, emceeSpotlightScaleTween, 300f);
			setScreenBg("bg-disqualified");
			lightsOff();
			setLEDStyle(2, aSwitch: false);
			showScreenSlide(string.Empty);
			audioController.playAwardsDisqualifiedAudio();
			yield return new WaitForSeconds(0.8f);
			showScreenSlide("Screen_disqualify");
			showText(gameData.getTextByRefIdWithDynText("goldenHammerText08", "[playerShop]", player.getShopName()));
			setCharacterImage(patataTexture, "Image/Dialogue/cutscene-patata-cry");
			enterStage(patataTween, patataSpotlightPosTween, patataSpotlightScaleTween, -240f);
			patataSpecialSpot.alpha = 1f;
			stageSet += 2;
			yield return new WaitForSeconds(0.8f);
		}
		else
		{
			setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-arrival scene");
			enterStage(emceeTween, emceeSpotlightPosTween, emceeSpotlightScaleTween, 300f);
			hideJudgeCountdown();
			audioController.startDrumRollLoopAudio();
			yield return new WaitForSeconds(0.8f);
			setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-normal scene-1");
			showText(gameData.getTextByRefIdWithDynText("goldenHammerText06", "[award]", awardValueList[awardIndex]));
			setLEDStyle(2, aSwitch: false);
		}
		showClickLabel();
	}

	private IEnumerator showResult(int awardIndex)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		hideClickLabel();
		setJudgeVotes();
		resetJudgeCircles(judge1Circles);
		resetJudgeCircles(judge2Circles);
		resetJudgeCircles(judge3Circles);
		showJudgeResult(1, awardIndex);
		yield return new WaitForSeconds(0.3f);
		showJudgeResult(2, awardIndex);
		yield return new WaitForSeconds(0.3f);
		showJudgeResult(3, awardIndex);
		yield return new WaitForSeconds(1f);
		audioController.stopDrumRollLoopAudio();
		leaveStage(judgesTween, judgesSpotlightPosTween, judgesSpotlightScaleTween);
		setScreenBg("bg-congratulations base");
		showScreenSlide("Screen_awardResult");
		startLights();
		startScreenRotate();
		setLEDStyle(5, aSwitch: true);
		string awardSpriteName = string.Empty;
		switch (awardList[awardIndex])
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			awardSpriteName = "title-most powerful";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			awardSpriteName = "title-the fastest";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			awardSpriteName = "title-most accurate";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			awardSpriteName = "title-most magical";
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			awardSpriteName = "title-overall best";
			break;
		}
		commonScreenObject.findChild(currentSlide, "Award_header").GetComponent<UISprite>().spriteName = awardSpriteName;
		audioController.playAwardsCongratsAudio();
		audioController.fadeinBGM();
		UISprite resultBg = commonScreenObject.findChild(currentSlide, "Result_bg").GetComponent<UISprite>();
		List<string> keyList = new List<string> { "[winner]", "[winnerShop]" };
		List<string> valueList = new List<string>();
		if (checkWon())
		{
			resultBg.spriteName = "winning result-blue";
			Project project = playerContenderList[awardIndex];
			valueList.Add(project.getProjectName(includePrefix: true));
			valueList.Add(player.getShopName());
			commonScreenObject.findChild(resultBg.gameObject, "WinnerName_label").GetComponent<UILabel>().text = project.getProjectName(includePrefix: true);
			commonScreenObject.findChild(resultBg.gameObject, "WinnerShop_label").GetComponent<UILabel>().text = player.getShopName();
			commonScreenObject.findChild(resultBg.gameObject, "Winner_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
			commonScreenObject.findChild(resultBg.gameObject, "atk_label").GetComponent<UILabel>().text = project.getAtk().ToString();
			commonScreenObject.findChild(resultBg.gameObject, "spd_label").GetComponent<UILabel>().text = project.getSpd().ToString();
			commonScreenObject.findChild(resultBg.gameObject, "acc_label").GetComponent<UILabel>().text = project.getAcc().ToString();
			commonScreenObject.findChild(resultBg.gameObject, "mag_label").GetComponent<UILabel>().text = project.getMag().ToString();
		}
		else
		{
			resultBg.spriteName = "winning result-red";
			Weapon weapon = opponentContenderList[awardIndex];
			string text = opponentShopList[awardIndex];
			valueList.Add(weapon.getWeaponName());
			valueList.Add(text);
			commonScreenObject.findChild(resultBg.gameObject, "WinnerName_label").GetComponent<UILabel>().text = weapon.getWeaponName();
			commonScreenObject.findChild(resultBg.gameObject, "WinnerShop_label").GetComponent<UILabel>().text = text;
			commonScreenObject.findChild(resultBg.gameObject, "Winner_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weapon.getImage());
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			switch (awardList[awardIndex])
			{
			case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
				num = catBenchmark;
				num2 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num3 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num4 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
				num = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num2 = catBenchmark;
				num3 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num4 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
				num = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num2 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num3 = catBenchmark;
				num4 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
				num = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num2 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num3 = (int)((float)catBenchmark * Random.Range(0.1f, 0.9f));
				num4 = catBenchmark;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			{
				_ = bestBenchmark;
				List<int> randomIntList = CommonAPI.getRandomIntList(4, 3);
				num = (int)((float)bestBenchmark * ((float)(randomIntList[0] + 1) / 10f));
				num2 = (int)((float)bestBenchmark * ((float)(randomIntList[1] + 1) / 10f));
				num3 = (int)((float)bestBenchmark * ((float)(randomIntList[2] + 1) / 10f));
				num4 = bestBenchmark - num - num2 - num3;
				if (num4 < 0)
				{
					num4 += 3;
					num--;
					num2--;
					num3--;
				}
				break;
			}
			}
			commonScreenObject.findChild(resultBg.gameObject, "atk_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(num);
			commonScreenObject.findChild(resultBg.gameObject, "spd_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(num2);
			commonScreenObject.findChild(resultBg.gameObject, "acc_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(num3);
			commonScreenObject.findChild(resultBg.gameObject, "mag_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(num4);
			stageSet++;
		}
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-congratulatory scene");
		showText(gameData.getTextByRefIdWithDynTextList("goldenHammerText07", keyList, valueList));
		yield return new WaitForSeconds(1f);
		showClickLabel();
	}

	private IEnumerator showWinner(int awardIndex)
	{
		GameData gameData = game.getGameData();
		hideClickLabel();
		showScreenSlide("Screen_winner");
		showStars();
		setCharacterImage(patataTexture, "Image/Dialogue/cutscene-patata-elated");
		enterStage(patataTween, patataSpotlightPosTween, patataSpotlightScaleTween, -240f);
		patataSpecialSpot.alpha = 1f;
		UISprite awardSprite = commonScreenObject.findChild(currentSlide, "Trophy_sprite").GetComponent<UISprite>();
		switch (awardList[awardIndex])
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			awardSprite.spriteName = "trophy-most powerful";
			awardSprite.width = 180;
			awardSprite.height = 162;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			awardSprite.spriteName = "trophy-the fastest";
			awardSprite.width = 180;
			awardSprite.height = 162;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			awardSprite.spriteName = "trophy-most accurate";
			awardSprite.width = 180;
			awardSprite.height = 162;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			awardSprite.spriteName = "trophy-most magical";
			awardSprite.width = 180;
			awardSprite.height = 162;
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			awardSprite.spriteName = "trophy-overall best";
			awardSprite.width = 180;
			awardSprite.height = 212;
			break;
		}
		int prizeValue = calculateRewardStarch(awardList[awardIndex]);
		showText(gameData.getTextByRefIdWithDynText("goldenHammerText09", "[prize]", CommonAPI.formatNumber(prizeValue)));
		winList.Add(awardList[awardIndex]);
		winPrizeList.Add(prizeValue);
		playerContenderList[awardIndex].addProjectAchievementList(awardList[awardIndex]);
		yield return new WaitForSeconds(0.5f);
		addStarch(prizeValue);
		yield return new WaitForSeconds(0.5f);
		showClickLabel();
	}

	private int calculateRewardStarch(ProjectAchievement awardType)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = player.getNextGoldenHammerYear() + 1;
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		float floatConstantByRefID = gameData.getFloatConstantByRefID(formulaConstantsSet + "_PRIZE_BASE");
		float floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_PRIZE_MULT");
		float floatConstantByRefID3 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_PRIZE_ADD");
		float floatConstantByRefID4 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_PRIZE_POWER");
		float num2 = floatConstantByRefID + floatConstantByRefID2 * Mathf.Pow(floatConstantByRefID3 + (float)num, floatConstantByRefID4);
		if (awardType == ProjectAchievement.ProjectAchievementGoldenHammerOverall)
		{
			return (int)(1f * num2 / 500f + 0.5f) * 500;
		}
		return (int)(0.33f * num2 / 500f + 0.5f) * 500;
	}

	private IEnumerator showEventEnd()
	{
		GameData gameData = game.getGameData();
		hideClickLabel();
		if (patataTween.transform.localPosition.y < 0f)
		{
			leaveStage(patataTween, patataSpotlightPosTween, patataSpotlightScaleTween);
			patataSpecialSpot.alpha = 0f;
			audioController.fadeinBGM();
		}
		hideStarch();
		setScreenBg("bg-normal");
		showScreenSlide("Screen_title");
		showStars();
		stopScreenRotate(hide: true);
		setLEDStyle(3, aSwitch: true);
		setCharacterImage(emceeTexture, "Image/weapon awards/host & judges/chara-host-normal scene-1");
		showText(gameData.getTextByRefId("goldenHammerText10"));
		yield return new WaitForSeconds(0.5f);
		showClickLabel();
	}

	private void setCharacterImage(UITexture aCharacterImage, string imagePath)
	{
		aCharacterImage.mainTexture = commonScreenObject.loadTexture(imagePath);
	}

	private void enterStage(TweenPosition aCharacter, TweenPosition aSpotlightPos, TweenScale aSpotlightScale, float xPos)
	{
		Vector3 aStartPosition = new Vector3(xPos, offStageY, 0f);
		Vector3 aEndPosition = new Vector3(xPos, onStageY, 0f);
		aCharacter.delay = 0.2f;
		commonScreenObject.tweenPosition(aCharacter, aStartPosition, aEndPosition, 0.4f, null, string.Empty);
		Vector3 aStartPosition2 = new Vector3(xPos, offStageY - 100f, 0f);
		Vector3 aEndPosition2 = new Vector3(xPos, onStageY - 100f, 0f);
		aSpotlightPos.delay = 0f;
		commonScreenObject.tweenPosition(aSpotlightPos, aStartPosition2, aEndPosition2, 0.8f, null, string.Empty);
		Vector3 vector = new Vector3(0.1f, 1f, 1f);
		aSpotlightScale.transform.localScale = vector;
		aSpotlightScale.delay = 0.1f;
		commonScreenObject.tweenScale(aSpotlightScale, vector, Vector3.one, 0.6f, null, string.Empty);
	}

	private void leaveStage(TweenPosition aCharacter, TweenPosition aSpotlightPos, TweenScale aSpotlightScale)
	{
		float x = aCharacter.transform.localPosition.x;
		Vector3 localPosition = aCharacter.transform.localPosition;
		Vector3 aEndPosition = new Vector3(x, offStageY, 0f);
		aCharacter.delay = 0.2f;
		commonScreenObject.tweenPosition(aCharacter, localPosition, aEndPosition, 0.4f, null, string.Empty);
		Vector3 aStartPosition = new Vector3(x, offStageY - 100f, 0f);
		Vector3 aEndPosition2 = new Vector3(x, onStageY - 100f, 0f);
		aSpotlightPos.delay = 0f;
		commonScreenObject.tweenPosition(aSpotlightPos, aStartPosition, aEndPosition2, 0.8f, null, string.Empty);
		Vector3 vector = new Vector3(0.1f, 1f, 1f);
		aSpotlightScale.transform.localScale = vector;
		aSpotlightScale.delay = 0.1f;
		commonScreenObject.tweenScale(aSpotlightScale, vector, Vector3.one, 0.6f, null, string.Empty);
	}

	private void showClickLabel()
	{
		allowClick = true;
		clickLabel.text = game.getGameData().getTextByRefId("TapToContinue").ToUpper(CultureInfo.InvariantCulture);
		clickLabel.alpha = 1f;
	}

	private void hideClickLabel()
	{
		allowClick = false;
		clickLabel.alpha = 0f;
	}

	private List<ProjectAchievement> getAwardList()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		awardKeyList = new List<string>();
		awardKeyList.Add("[award1]");
		awardKeyList.Add("[award2]");
		awardKeyList.Add("[award3]");
		awardValueList = new List<string>();
		awardList = player.getNextGoldenHammerAwardList();
		if (awardList.Count > 1)
		{
			using List<ProjectAchievement>.Enumerator enumerator = awardList.GetEnumerator();
			while (enumerator.MoveNext())
			{
				switch (enumerator.Current)
				{
				case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText11"));
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText12"));
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText13"));
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText14"));
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText15"));
					break;
				}
			}
		}
		else
		{
			awardList = new List<ProjectAchievement>();
			awardValueList.Add(gameData.getTextByRefId("goldenHammerText12"));
			awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerSpeed);
			awardValueList.Add(gameData.getTextByRefId("goldenHammerText11"));
			awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerAttack);
			awardValueList.Add(gameData.getTextByRefId("goldenHammerText15"));
			awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerOverall);
		}
		return awardList;
	}

	private void showStarch()
	{
		commonScreenObject.tweenPosition(starchTween, starchTween.transform.localPosition, new Vector3(0f, -335f, 0f), 0.4f, null, string.Empty);
	}

	private void hideStarch()
	{
		commonScreenObject.tweenPosition(starchTween, starchTween.transform.localPosition, new Vector3(0f, -400f, 0f), 0.4f, null, string.Empty);
	}

	private bool checkStarchAmt(int reqAmt)
	{
		Player player = game.getPlayer();
		if (reqAmt > player.getPlayerGold())
		{
			return false;
		}
		return true;
	}

	private void refreshStarchAmt()
	{
		Player player = game.getPlayer();
		starchLabel.text = CommonAPI.formatNumber(player.getPlayerGold());
	}

	private void addStarch(int aAddAmt)
	{
		Player player = game.getPlayer();
		player.addGold(aAddAmt);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, game.getGameData().getTextByRefId("recordTypeName22"), aAddAmt);
		audioController.playGoldGainAudio();
		starchPopLabel.text = "[56AE59]" + CommonAPI.formatNumber(aAddAmt) + "[-]";
		commonScreenObject.tweenPosition(starchPopPosTween, new Vector3(120f, 0f, 0f), new Vector3(140f, 0f, 0f), 0.4f, null, string.Empty);
		commonScreenObject.tweenAlpha(starchPopAlphaTween, 0f, 1f, 0.4f, null, string.Empty);
		refreshStarchAmt();
	}

	private void reduceStarch(int aReduceAmt)
	{
		Player player = game.getPlayer();
		player.reduceGold(aReduceAmt, allowNegative: true);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeSpecial, game.getGameData().getTextByRefId("recordTypeName21"), -aReduceAmt);
		audioController.playPurchaseAudio();
		starchPopLabel.text = "[FF4842]-" + CommonAPI.formatNumber(aReduceAmt) + "[-]";
		commonScreenObject.tweenPosition(starchPopPosTween, new Vector3(120f, 0f, 0f), new Vector3(140f, 0f, 0f), 0.4f, null, string.Empty);
		commonScreenObject.tweenAlpha(starchPopAlphaTween, 0f, 1f, 0.4f, null, string.Empty);
		refreshStarchAmt();
	}

	private void stopAllNinjas(int exclude = -1)
	{
		if (exclude != 1 && ninja1Tween.enabled)
		{
			commonScreenObject.tweenPosition(ninja1Tween, ninja1Tween.transform.localPosition, ninja1OutPos, 0.3f, null, string.Empty);
		}
		if (exclude != 2 && ninja2Tween.enabled)
		{
			commonScreenObject.tweenPosition(ninja2Tween, ninja2Tween.transform.localPosition, ninja2OutPos, 0.3f, null, string.Empty);
		}
		if (exclude != 3 && ninja3Tween.enabled)
		{
			commonScreenObject.tweenPosition(ninja3Tween, ninja3Tween.transform.localPosition, ninja3OutPos, 0.3f, null, string.Empty);
		}
		if (exclude != 4 && ninja4Tween.enabled)
		{
			commonScreenObject.tweenPosition(ninja4Tween, ninja4Tween.transform.localPosition, ninja4OutPos, 0.3f, null, string.Empty);
		}
	}

	private void updateNinjaState()
	{
		int alertState = getAlertState();
		int num = Random.Range(0, 40);
		if (alertState != prevAlertState)
		{
			num = 0;
		}
		prevAlertState = alertState;
		if (num == 0)
		{
			switch (alertState)
			{
			case 4:
				stopAllNinjas(4);
				audioController.playAwardsRiskAudio();
				commonScreenObject.tweenPosition(ninja4Tween, ninja4Tween.transform.localPosition, ninja4InPos, 0.3f, base.gameObject, "moveNinja4OutSlow");
				break;
			case 3:
				stopAllNinjas(3);
				audioController.playAwardsRiskAudio();
				commonScreenObject.tweenPosition(ninja3Tween, ninja3Tween.transform.localPosition, ninja3InPos, 0.3f, base.gameObject, "moveNinja3OutSlow");
				break;
			case 2:
				stopAllNinjas(2);
				audioController.playAwardsRiskAudio();
				commonScreenObject.tweenPosition(ninja2Tween, ninja2Tween.transform.localPosition, ninja2InPos, 0.3f, base.gameObject, "moveNinja2OutSlow");
				break;
			case 1:
				stopAllNinjas(1);
				audioController.playAwardsRiskAudio();
				commonScreenObject.tweenPosition(ninja1Tween, ninja1Tween.transform.localPosition, ninja1InPos, 0.3f, base.gameObject, "moveNinja1OutSlow");
				break;
			}
		}
	}

	public void moveNinja1OutSlow()
	{
		commonScreenObject.tweenPosition(ninja1Tween, ninja1Tween.transform.localPosition, ninja1OutPos, 0.6f, null, string.Empty);
	}

	public void moveNinja2OutSlow()
	{
		commonScreenObject.tweenPosition(ninja2Tween, ninja2Tween.transform.localPosition, ninja2OutPos, 0.6f, null, string.Empty);
	}

	public void moveNinja3OutSlow()
	{
		commonScreenObject.tweenPosition(ninja3Tween, ninja3Tween.transform.localPosition, ninja3OutPos, 0.6f, null, string.Empty);
	}

	public void moveNinja4OutSlow()
	{
		commonScreenObject.tweenPosition(ninja4Tween, ninja4Tween.transform.localPosition, ninja4OutPos, 0.6f, null, string.Empty);
	}

	private int getAlertState()
	{
		int num = bribe1Count + bribe2Count + bribe3Count;
		if (num > alert3Limit)
		{
			return 4;
		}
		if (num > alert2Limit)
		{
			return 3;
		}
		if (num > alert1Limit)
		{
			return 2;
		}
		if (num > alert0Limit)
		{
			return 1;
		}
		return 0;
	}

	private bool checkBribeSuccess()
	{
		int alertState = getAlertState();
		int num = Random.Range(0, 20);
		switch (alertState)
		{
		case 4:
			if (num < 8)
			{
				return false;
			}
			break;
		case 3:
			if (num < 6)
			{
				return false;
			}
			break;
		case 2:
			if (num < 4)
			{
				return false;
			}
			break;
		case 1:
			if (num < 2)
			{
				return false;
			}
			break;
		}
		return true;
	}

	private void resetBribes()
	{
		bribe1Count = 0;
		bribe2Count = 0;
		bribe3Count = 0;
	}

	private void setJudgeVotes()
	{
		float num = Random.Range(0f, 100f);
		if (playerIsWinner || judge1VoteChance > num)
		{
			judge1ChosePlayer = true;
		}
		else
		{
			judge1ChosePlayer = false;
		}
		float num2 = Random.Range(0f, 100f);
		if (playerIsWinner || judge2VoteChance > num2)
		{
			judge2ChosePlayer = true;
		}
		else
		{
			judge2ChosePlayer = false;
		}
		float num3 = Random.Range(0f, 100f);
		if (playerIsWinner || judge3VoteChance > num3)
		{
			judge3ChosePlayer = true;
		}
		else
		{
			judge3ChosePlayer = false;
		}
	}

	private void showJudgeResult(int judgeIndex, int awardIndex)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Project project = playerContenderList[awardIndex];
		Weapon weapon = opponentContenderList[awardIndex];
		string replaceValue = opponentShopList[awardIndex];
		switch (judgeIndex)
		{
		case 1:
			if (judge1ChosePlayer)
			{
				judge1ResultBubble.spriteName = "box-judge's selection-blue";
				judge1ChoiceTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
				judge1ChoiceLabel.text = project.getProjectName(includePrefix: true) + "\n" + gameData.getTextByRefIdWithDynText("goldenHammerAwards04", "[shopName]", player.getShopName());
				setJudgeCircles(judge1Circles, choosePlayer: true);
			}
			else
			{
				judge1ResultBubble.spriteName = "box-judge's selection-red";
				judge1ChoiceTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weapon.getImage());
				judge1ChoiceLabel.text = weapon.getWeaponName() + "\n" + gameData.getTextByRefIdWithDynText("goldenHammerAwards04", "[shopName]", replaceValue);
				setJudgeCircles(judge1Circles, choosePlayer: false);
			}
			commonScreenObject.tweenScale(judge1ResultBubble.GetComponent<TweenScale>(), judge1ResultBubble.transform.localScale, Vector3.one, 0.4f, null, string.Empty);
			break;
		case 2:
			if (judge2ChosePlayer)
			{
				judge2ResultBubble.spriteName = "box-judge's selection-blue";
				judge2ChoiceTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
				judge2ChoiceLabel.text = project.getProjectName(includePrefix: true) + "\n" + gameData.getTextByRefIdWithDynText("goldenHammerAwards04", "[shopName]", player.getShopName());
				setJudgeCircles(judge2Circles, choosePlayer: true);
			}
			else
			{
				judge2ResultBubble.spriteName = "box-judge's selection-red";
				judge2ChoiceTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weapon.getImage());
				judge2ChoiceLabel.text = weapon.getWeaponName() + "\n" + gameData.getTextByRefIdWithDynText("goldenHammerAwards04", "[shopName]", replaceValue);
				setJudgeCircles(judge2Circles, choosePlayer: false);
			}
			commonScreenObject.tweenScale(judge2ResultBubble.GetComponent<TweenScale>(), judge2ResultBubble.transform.localScale, Vector3.one, 0.4f, null, string.Empty);
			break;
		case 3:
			if (judge3ChosePlayer)
			{
				judge3ResultBubble.spriteName = "box-judge's selection-blue";
				judge3ChoiceTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
				judge3ChoiceLabel.text = project.getProjectName(includePrefix: true) + "\n" + gameData.getTextByRefIdWithDynText("goldenHammerAwards04", "[shopName]", player.getShopName());
				setJudgeCircles(judge3Circles, choosePlayer: true);
			}
			else
			{
				judge3ResultBubble.spriteName = "box-judge's selection-red";
				judge3ChoiceTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weapon.getImage());
				judge3ChoiceLabel.text = weapon.getWeaponName() + "\n" + gameData.getTextByRefIdWithDynText("goldenHammerAwards04", "[shopName]", replaceValue);
				setJudgeCircles(judge3Circles, choosePlayer: false);
			}
			commonScreenObject.tweenScale(judge3ResultBubble.GetComponent<TweenScale>(), judge3ResultBubble.transform.localScale, Vector3.one, 0.4f, null, string.Empty);
			break;
		}
	}

	private void hideJudgeResults()
	{
		judge1ResultBubble.transform.localScale = Vector3.zero;
		judge2ResultBubble.transform.localScale = Vector3.zero;
		judge3ResultBubble.transform.localScale = Vector3.zero;
	}

	private bool checkWon()
	{
		int num = 0;
		if (judge1ChosePlayer)
		{
			num++;
		}
		if (judge2ChosePlayer)
		{
			num++;
		}
		if (judge3ChosePlayer)
		{
			num++;
		}
		if (num >= 2)
		{
			return true;
		}
		return false;
	}

	private bool checkDisqualified()
	{
		if (judge1Bribe == "CAUGHT" || judge2Bribe == "CAUGHT" || judge3Bribe == "CAUGHT")
		{
			return true;
		}
		return false;
	}

	private bool checkForceJudgingEnd()
	{
		int num = 0;
		if (judge1Bribe != "STANDBY")
		{
			num++;
		}
		if (judge2Bribe != "STANDBY")
		{
			num++;
		}
		if (judge3Bribe != "STANDBY")
		{
			num++;
		}
		if (num >= 2)
		{
			return true;
		}
		return false;
	}

	private void chooseContenders()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		playerContenderList = new List<Project>();
		opponentContenderList = new List<Weapon>();
		opponentShopList = new List<string>();
		foreach (ProjectAchievement award in awardList)
		{
			int num = catNomination;
			if (award == ProjectAchievement.ProjectAchievementGoldenHammerOverall)
			{
				num = bestNomination;
			}
			Project project = null;
			foreach (Project goldenHammer in goldenHammerList)
			{
				int num2 = 0;
				switch (award)
				{
				case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
					num2 = goldenHammer.getAtk();
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
					num2 = goldenHammer.getSpd();
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
					num2 = goldenHammer.getAcc();
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
					num2 = goldenHammer.getMag();
					break;
				case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
					num2 = goldenHammer.getTotalStat();
					break;
				}
				if (num2 > num)
				{
					project = goldenHammer;
					num = num2;
				}
			}
			if (project != null)
			{
				playerContenderList.Add(project);
			}
			else
			{
				playerContenderList.Add(new Project());
			}
			string randomWeaponTypeForAward = CommonAPI.getRandomWeaponTypeForAward(award);
			Weapon randomWeaponFromType = gameData.getRandomWeaponFromType(randomWeaponTypeForAward, string.Empty);
			opponentContenderList.Add(randomWeaponFromType);
			opponentShopList.Add(gameData.getRandomTextBySetRefId("goldenHammerOpponent"));
		}
	}

	private List<float> calculateJudgeInitialChances(int playerStat, int opponentStat)
	{
		List<float> list = new List<float>();
		list.Add(0f);
		list.Add(0f);
		list.Add(0f);
		return list;
	}

	private void setJudgesChoice(int awardIndex)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		currentAwardIndex = awardIndex;
		Project project = playerContenderList[awardIndex];
		Weapon weapon = opponentContenderList[awardIndex];
		string text = opponentShopList[awardIndex];
		ProjectAchievement projectAchievement = awardList[awardIndex];
		string textByRefId = gameData.getTextByRefId("goldenHammerAwards01");
		judge1BribeLabel.text = textByRefId;
		judge2BribeLabel.text = textByRefId;
		judge3BribeLabel.text = textByRefId;
		resetBribes();
		int playerStat = 0;
		int num = catBenchmark;
		int nominationStat = catNomination;
		switch (projectAchievement)
		{
		case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
			playerStat = project.getAtk();
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
			playerStat = project.getSpd();
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
			playerStat = project.getAcc();
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
			playerStat = project.getMag();
			break;
		case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
			playerStat = project.getTotalStat();
			num = bestBenchmark;
			nominationStat = bestNomination;
			break;
		}
		bribeChanceUpAmt = calculateBribeChanceUpAmt(playerStat, num, nominationStat);
		List<float> list = calculateJudgeInitialChances(playerStat, num);
		if (playerIsContender && !playerIsWinner)
		{
			for (int i = 1; i <= 3; i++)
			{
				switch (i)
				{
				case 1:
					judge1VoteChance = list[0];
					break;
				case 2:
					judge2VoteChance = list[1];
					break;
				case 3:
					judge3VoteChance = list[2];
					break;
				}
				updateJudgeVoteStates(i, recalculateBribe: true);
			}
		}
		else
		{
			judge1VoteChance = 0f;
			judge2VoteChance = 0f;
			judge3VoteChance = 0f;
			updateJudgeVoteStates(1, recalculateBribe: true);
			updateJudgeVoteStates(2, recalculateBribe: true);
			updateJudgeVoteStates(3, recalculateBribe: true);
		}
	}

	private float calculateBribeChanceUpAmt(int playerStat, int benchmarkStat, int nominationStat)
	{
		float num = 0.2f;
		float num2 = 0.02f;
		float a = (float)(playerStat - nominationStat) * (num - num2) / (float)(benchmarkStat - nominationStat) + num2;
		a = Mathf.Max(a, num2);
		a = Mathf.Min(a, num);
		return a * 100f;
	}

	private string getVoteComment(float voteChance)
	{
		GameData gameData = game.getGameData();
		string empty = string.Empty;
		if (voteChance >= 100f)
		{
			return gameData.getTextByRefId("goldenHammerAwards13");
		}
		if (voteChance >= 80f)
		{
			return gameData.getTextByRefId("goldenHammerAwards12");
		}
		if (voteChance >= 60f)
		{
			return gameData.getTextByRefId("goldenHammerAwards11");
		}
		if (voteChance >= 40f)
		{
			return gameData.getTextByRefId("goldenHammerAwards10");
		}
		if (voteChance >= 20f)
		{
			return gameData.getTextByRefId("goldenHammerAwards09");
		}
		return gameData.getTextByRefId("goldenHammerAwards08");
	}

	private int calculateBribeValue(ProjectAchievement awardType, int awardsYear, int bribeCount)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		float floatConstantByRefID = gameData.getFloatConstantByRefID(formulaConstantsSet + "_BRIBE_BASE");
		float floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_BRIBE_POWER");
		float floatConstantByRefID3 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_BRIBE_MULT");
		float num = floatConstantByRefID + floatConstantByRefID3 * (float)awardsYear * Mathf.Pow(bribeCount + 1, floatConstantByRefID2);
		if (awardType == ProjectAchievement.ProjectAchievementGoldenHammerOverall)
		{
			return (int)(1f * num * Random.Range(0.2f, 0.4f) + 0.5f);
		}
		return (int)(0.3f * num * Random.Range(0.2f, 0.4f) + 0.5f);
	}

	private void updateJudgeVoteStates(int judgeIndex, bool recalculateBribe)
	{
		Player player = game.getPlayer();
		int playerGold = player.getPlayerGold();
		int awardsYear = player.getNextGoldenHammerYear() + 1;
		if (playerIsContender && !playerIsWinner)
		{
			switch (judgeIndex)
			{
			case 1:
				if (recalculateBribe)
				{
					bribe1Value = calculateBribeValue(awardList[currentAwardIndex], awardsYear, bribe1Count);
				}
				if (judge1VoteChance >= 100f)
				{
					judge1Bribe = "NO_BRIBE";
					judge1Button.isEnabled = false;
					judge1BribeAmt.text = "-";
					judge1Bubble.spriteName = "new-judgesthought-blue";
					if (judge1CompleteTween.transform.localScale.x == 0f)
					{
						audioController.playAwardsPersuasionCompleteAudio();
						commonScreenObject.tweenScale(judge1CompleteTween, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
					}
				}
				else if (judge1VoteChance >= 60f)
				{
					judge1Bribe = "STANDBY";
					if (playerGold < bribe1Value)
					{
						judge1Button.isEnabled = false;
					}
					else
					{
						judge1Button.isEnabled = true;
					}
					judge1BribeAmt.text = CommonAPI.formatNumber(bribe1Value);
					judge1Bubble.spriteName = "new-judgesthought-blue";
					if (judge1CompleteTween.transform.localScale.x != 0f)
					{
						judge1CompleteTween.transform.localScale = Vector3.zero;
					}
				}
				else
				{
					judge1Bribe = "STANDBY";
					if (playerGold < bribe1Value)
					{
						judge1Button.isEnabled = false;
					}
					else
					{
						judge1Button.isEnabled = true;
					}
					judge1BribeAmt.text = CommonAPI.formatNumber(bribe1Value);
					judge1Bubble.spriteName = "new-judgesthought-red";
					if (judge1CompleteTween.transform.localScale.x != 0f)
					{
						judge1CompleteTween.transform.localScale = Vector3.zero;
					}
				}
				judge1CommentLabel.text = getVoteComment(judge1VoteChance);
				break;
			case 2:
				if (recalculateBribe)
				{
					bribe2Value = calculateBribeValue(awardList[currentAwardIndex], awardsYear, bribe2Count);
				}
				if (judge2VoteChance >= 100f)
				{
					judge2Bribe = "NO_BRIBE";
					judge2Button.isEnabled = false;
					judge2BribeAmt.text = "-";
					judge2Bubble.spriteName = "new-judgesthought-blue";
					if (judge2CompleteTween.transform.localScale.x == 0f)
					{
						audioController.playAwardsPersuasionCompleteAudio();
						commonScreenObject.tweenScale(judge2CompleteTween, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
					}
				}
				else if (judge2VoteChance >= 60f)
				{
					judge2Bribe = "STANDBY";
					if (playerGold < bribe2Value)
					{
						judge2Button.isEnabled = false;
					}
					else
					{
						judge2Button.isEnabled = true;
					}
					judge2BribeAmt.text = CommonAPI.formatNumber(bribe2Value);
					judge2Bubble.spriteName = "new-judgesthought-blue";
					if (judge2CompleteTween.transform.localScale.x != 0f)
					{
						judge2CompleteTween.transform.localScale = Vector3.zero;
					}
				}
				else
				{
					judge2Bribe = "STANDBY";
					if (playerGold < bribe2Value)
					{
						judge2Button.isEnabled = false;
					}
					else
					{
						judge2Button.isEnabled = true;
					}
					judge2BribeAmt.text = CommonAPI.formatNumber(bribe2Value);
					judge2Bubble.spriteName = "new-judgesthought-red";
					if (judge2CompleteTween.transform.localScale.x != 0f)
					{
						judge2CompleteTween.transform.localScale = Vector3.zero;
					}
				}
				judge2CommentLabel.text = getVoteComment(judge2VoteChance);
				break;
			case 3:
				if (recalculateBribe)
				{
					bribe3Value = calculateBribeValue(awardList[currentAwardIndex], awardsYear, bribe3Count);
				}
				if (judge3VoteChance >= 100f)
				{
					judge3Bribe = "NO_BRIBE";
					judge3Button.isEnabled = false;
					judge3BribeAmt.text = "-";
					judge3Bubble.spriteName = "new-judgesthought-blue";
					if (judge3CompleteTween.transform.localScale.x == 0f)
					{
						audioController.playAwardsPersuasionCompleteAudio();
						commonScreenObject.tweenScale(judge3CompleteTween, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
					}
				}
				else if (judge3VoteChance >= 60f)
				{
					judge3Bribe = "STANDBY";
					if (playerGold < bribe3Value)
					{
						judge3Button.isEnabled = false;
					}
					else
					{
						judge3Button.isEnabled = true;
					}
					judge3BribeAmt.text = CommonAPI.formatNumber(bribe3Value);
					judge3Bubble.spriteName = "new-judgesthought-blue";
					if (judge3CompleteTween.transform.localScale.x != 0f)
					{
						judge3CompleteTween.transform.localScale = Vector3.zero;
					}
				}
				else
				{
					judge3Bribe = "STANDBY";
					if (playerGold < bribe3Value)
					{
						judge3Button.isEnabled = false;
					}
					else
					{
						judge3Button.isEnabled = true;
					}
					judge3BribeAmt.text = CommonAPI.formatNumber(bribe3Value);
					judge3Bubble.spriteName = "new-judgesthought-red";
					if (judge3CompleteTween.transform.localScale.x != 0f)
					{
						judge3CompleteTween.transform.localScale = Vector3.zero;
					}
				}
				judge3CommentLabel.text = getVoteComment(judge3VoteChance);
				break;
			}
		}
		else
		{
			switch (judgeIndex)
			{
			case 1:
				judge1Bribe = "NO_BRIBE";
				judge1Button.isEnabled = false;
				judge1BribeAmt.text = "-";
				break;
			case 2:
				judge2Bribe = "NO_BRIBE";
				judge2Button.isEnabled = false;
				judge2BribeAmt.text = "-";
				break;
			case 3:
				judge3Bribe = "NO_BRIBE";
				judge3Button.isEnabled = false;
				judge3BribeAmt.text = "-";
				break;
			}
		}
	}

	private bool checkJudgeCanBribe(int judgeIndex)
	{
		bool result = false;
		switch (judgeIndex)
		{
		case 1:
			if (judge1Bribe == "STANDBY")
			{
				result = true;
			}
			break;
		case 2:
			if (judge2Bribe == "STANDBY")
			{
				result = true;
			}
			break;
		case 3:
			if (judge3Bribe == "STANDBY")
			{
				result = true;
			}
			break;
		}
		return result;
	}

	private void bribeJudge(int judgeIndex)
	{
		GameData gameData = game.getGameData();
		if (!allowBribes || !checkJudgeCanBribe(judgeIndex))
		{
			return;
		}
		Player player = game.getPlayer();
		string empty = string.Empty;
		empty = ((!checkBribeSuccess()) ? "CAUGHT" : "ACCEPT");
		Project project = playerContenderList[currentAwardIndex];
		switch (judgeIndex)
		{
		case 1:
			reduceStarch(bribe1Value);
			bribe1Count++;
			judge1Bribe = empty;
			judge1Button.isEnabled = false;
			if (bribe1Count > 0 && judge1Bubble.transform.localScale.x == 0f)
			{
				CommonAPI.debug("bribe1Count " + bribe1Count);
				commonScreenObject.tweenScale(judge1Bubble.GetComponent<TweenScale>(), judge1Bubble.transform.localScale, Vector3.one, 0.4f, null, string.Empty);
			}
			switch (empty)
			{
			case "ACCEPT":
				judge1VoteChance += bribeChanceUpAmt;
				popChanceUp(judge1ChancePosTween, judge1ChanceAlphaTween);
				break;
			case "CAUGHT":
				setCharacterImage(judge1Texture, "Image/weapon awards/host & judges/chara-host1-disqualification scene");
				disableBribes();
				break;
			}
			break;
		case 2:
			reduceStarch(bribe2Value);
			bribe2Count++;
			judge2Bribe = empty;
			judge2Button.isEnabled = false;
			if (bribe2Count > 0 && judge2Bubble.transform.localScale.x == 0f)
			{
				CommonAPI.debug("bribe2Count " + bribe2Count);
				commonScreenObject.tweenScale(judge2Bubble.GetComponent<TweenScale>(), judge2Bubble.transform.localScale, Vector3.one, 0.4f, null, string.Empty);
			}
			switch (empty)
			{
			case "ACCEPT":
				judge2VoteChance += bribeChanceUpAmt;
				popChanceUp(judge2ChancePosTween, judge2ChanceAlphaTween);
				break;
			case "CAUGHT":
				setCharacterImage(judge2Texture, "Image/weapon awards/host & judges/chara-host2-disqualification scene");
				disableBribes();
				break;
			}
			break;
		case 3:
			reduceStarch(bribe3Value);
			bribe3Count++;
			judge3Bribe = empty;
			judge3Button.isEnabled = false;
			if (bribe3Count > 0 && judge3Bubble.transform.localScale.x == 0f)
			{
				CommonAPI.debug("bribe3Count " + bribe3Count);
				commonScreenObject.tweenScale(judge3Bubble.GetComponent<TweenScale>(), judge3Bubble.transform.localScale, Vector3.one, 0.4f, null, string.Empty);
			}
			switch (empty)
			{
			case "ACCEPT":
				judge3VoteChance += bribeChanceUpAmt;
				popChanceUp(judge3ChancePosTween, judge3ChanceAlphaTween);
				break;
			case "CAUGHT":
				setCharacterImage(judge3Texture, "Image/weapon awards/host & judges/chara-host3-disqualification scene");
				disableBribes();
				break;
			}
			break;
		}
		if (empty != "CAUGHT")
		{
			if (judgeIndex == 1)
			{
				updateJudgeVoteStates(1, recalculateBribe: true);
			}
			else
			{
				updateJudgeVoteStates(1, recalculateBribe: false);
			}
			if (judgeIndex == 2)
			{
				updateJudgeVoteStates(2, recalculateBribe: true);
			}
			else
			{
				updateJudgeVoteStates(2, recalculateBribe: false);
			}
			if (judgeIndex == 3)
			{
				updateJudgeVoteStates(3, recalculateBribe: true);
			}
			else
			{
				updateJudgeVoteStates(3, recalculateBribe: false);
			}
		}
	}

	private void popChanceUp(TweenPosition posTween, TweenAlpha alphaTween)
	{
		audioController.playAwardsChanceUpAudio();
		commonScreenObject.tweenPosition(posTween, new Vector3(0f, 70f, 0f), new Vector3(0f, 100f, 0f), 1f, null, string.Empty);
		commonScreenObject.tweenAlpha(alphaTween, 0f, 1f, 1f, null, string.Empty);
	}

	private void startBribePhase(bool showAnim = true)
	{
		if (judge1Bribe == "STANDBY")
		{
			judge1Button.isEnabled = true;
		}
		if (judge2Bribe == "STANDBY")
		{
			judge2Button.isEnabled = true;
		}
		if (judge3Bribe == "STANDBY")
		{
			judge3Button.isEnabled = true;
		}
		allowBribes = true;
		judge1Bubble.transform.localScale = Vector3.zero;
		judge2Bubble.transform.localScale = Vector3.zero;
		judge3Bubble.transform.localScale = Vector3.zero;
	}

	private void disableBribes(bool showAnim = true)
	{
		judge1Button.isEnabled = false;
		judge2Button.isEnabled = false;
		judge3Button.isEnabled = false;
		allowBribes = false;
		if (showAnim && playerIsContender && !playerIsWinner)
		{
			commonScreenObject.tweenScale(judge1Bubble.GetComponent<TweenScale>(), judge1Bubble.transform.localScale, Vector3.zero, 0.4f, null, string.Empty);
			commonScreenObject.tweenScale(judge2Bubble.GetComponent<TweenScale>(), judge2Bubble.transform.localScale, Vector3.zero, 0.4f, null, string.Empty);
			commonScreenObject.tweenScale(judge3Bubble.GetComponent<TweenScale>(), judge3Bubble.transform.localScale, Vector3.zero, 0.4f, null, string.Empty);
		}
		else
		{
			judge1Bubble.transform.localScale = Vector3.zero;
			judge2Bubble.transform.localScale = Vector3.zero;
			judge3Bubble.transform.localScale = Vector3.zero;
		}
	}

	private void doJudgesMovement(int aTimer)
	{
		Vector3 aStartPosition = new Vector3(0f, 80f, 0f);
		Vector3 aEndPosition = new Vector3(0f, 90f, 0f);
		TweenPosition component = judge1Texture.GetComponent<TweenPosition>();
		if (!component.enabled && Random.Range(0, 100) == 0)
		{
			commonScreenObject.tweenPosition(component, aStartPosition, aEndPosition, 0.3f, null, string.Empty);
		}
		TweenPosition component2 = judge2Texture.GetComponent<TweenPosition>();
		if (!component2.enabled && Random.Range(0, 50) == 0)
		{
			commonScreenObject.tweenPosition(component2, aStartPosition, aEndPosition, 0.3f, null, string.Empty);
		}
		TweenPosition component3 = judge3Texture.GetComponent<TweenPosition>();
		if (!component3.enabled && Random.Range(0, 200) == 0)
		{
			commonScreenObject.tweenPosition(component3, aStartPosition, aEndPosition, 0.3f, null, string.Empty);
		}
		judgeCountdownLabel.text = (Mathf.Max(aTimer - 1, 0f) / 100f).ToString();
	}

	private void hideJudgeCountdown()
	{
		commonScreenObject.tweenPosition(judgeCountdownTween, judgeCountdownTween.transform.localPosition, new Vector3(0f, 650f, 0f), 0.4f, null, string.Empty);
	}

	private void showJudgeCountdown()
	{
		judgeCountdownLabel.text = string.Empty;
		judgeCountdownTween.transform.localPosition = new Vector3(0f, 500f, 0f);
	}

	private void cycleCircles(List<UISprite> circleList)
	{
		circleList[circleState].alpha = 1f;
	}

	private void resetJudgeCircles(List<UISprite> circleList)
	{
		for (int i = 1; i < circleList.Count; i++)
		{
			circleList[i].alpha = 0f;
		}
		circleState = 0;
	}

	private void setJudgeCircles(List<UISprite> circleList, bool choosePlayer)
	{
		if (choosePlayer)
		{
			circleList[0].spriteName = "blue6";
			circleList[1].spriteName = "blue5";
			circleList[2].spriteName = "blue4";
			circleList[3].spriteName = "blue3";
			circleList[4].spriteName = "blue2";
			circleList[5].spriteName = "blue1";
		}
		else
		{
			circleList[0].spriteName = "purple6";
			circleList[1].spriteName = "purple5";
			circleList[2].spriteName = "purple4";
			circleList[3].spriteName = "purple3";
			circleList[4].spriteName = "purple2";
			circleList[5].spriteName = "purple1";
		}
	}

	private void showText(string aText)
	{
		lineIsAnimating = true;
		lineText = aText;
		lineDisplayChar = 0;
		speechLabel.alpha = 1f;
		audioController.playTextTypeAudio();
		StartCoroutine(typewriterText());
	}

	private void hideText()
	{
		speechLabel.alpha = 0f;
		lineIsAnimating = false;
		audioController.stopTextTypeAudio();
	}

	private void forceText()
	{
		speechLabel.alpha = 1f;
		lineIsAnimating = false;
		audioController.stopTextTypeAudio();
		speechLabel.text = lineText;
	}

	private IEnumerator typewriterText()
	{
		while (lineIsAnimating)
		{
			while (lineDisplayChar < lineText.Length && lineText[lineDisplayChar].ToString() == " ")
			{
				lineDisplayChar++;
			}
			while (lineDisplayChar < lineText.Length && lineText[lineDisplayChar].ToString() == "[")
			{
				string text = string.Empty;
				while (text != "]")
				{
					text = lineText[lineDisplayChar].ToString();
					lineDisplayChar++;
				}
				while (lineDisplayChar < lineText.Length && lineText[lineDisplayChar].ToString() == " ")
				{
					lineDisplayChar++;
				}
			}
			CommonAPI.debug(lineDisplayChar + "/" + lineText.Length);
			string displayText = lineText.Insert(lineDisplayChar, "[00]");
			lineDisplayChar++;
			speechLabel.text = displayText;
			if (lineDisplayChar >= lineText.Length)
			{
				lineIsAnimating = false;
				audioController.stopTextTypeAudio();
			}
			yield return new WaitForSeconds(0.02f);
		}
		speechLabel.text = lineText;
	}

	private void showScreenSlide(string slideName)
	{
		clearCurrentSlide();
		if (slideName != string.Empty)
		{
			currentSlide = commonScreenObject.createPrefab(screenPanel, slideName, "Prefab/GoldenHammer/" + slideName, Vector3.zero, Vector3.one, Vector3.zero);
		}
	}

	private void clearCurrentSlide()
	{
		if (currentSlide != null)
		{
			commonScreenObject.destroyPrefabImmediate(currentSlide);
			currentSlide = null;
		}
	}

	private void setScreenBg(string screenTexture)
	{
		screenBg.mainTexture = commonScreenObject.loadTexture("Image/weapon awards/bg assets/" + screenTexture);
	}

	private void startScreenRotate()
	{
		screenRotate.alpha = 1f;
		isScreenRotating = true;
		StartCoroutine(doScreenRotate());
	}

	private IEnumerator doScreenRotate()
	{
		while (isScreenRotating)
		{
			screenRotate.transform.Rotate(0f, 0f, 0.5f);
			yield return new WaitForSeconds(0.01f);
		}
	}

	private void stopScreenRotate(bool hide)
	{
		isScreenRotating = false;
		if (hide)
		{
			screenRotate.alpha = 0f;
		}
	}

	private void showStars()
	{
		commonScreenObject.tweenAlpha(screenStars1.GetComponent<TweenAlpha>(), 0f, 1f, 1f, null, string.Empty);
		commonScreenObject.tweenAlpha(screenStars2.GetComponent<TweenAlpha>(), 1f, 0f, 1f, null, string.Empty);
	}

	private void hideStars()
	{
		screenStars1.GetComponent<TweenAlpha>().enabled = false;
		screenStars2.GetComponent<TweenAlpha>().enabled = false;
		screenStars1.alpha = 0f;
		screenStars2.alpha = 0f;
	}

	private void startLights()
	{
		lightsOn();
		lightDirUp = true;
		lightsMoving = true;
		StartCoroutine(moveLights());
	}

	private void stopLights()
	{
		lightsMoving = false;
	}

	private void lightsOn()
	{
		lightsLeft.spriteName = "bg-spotlight-on";
		lightsRight.spriteName = "bg-spotlight-on";
	}

	private void lightsOff()
	{
		lightsLeft.spriteName = "bg-spotlight-off";
		lightsRight.spriteName = "bg-spotlight-off";
		lightsMoving = false;
	}

	private IEnumerator moveLights()
	{
		while (lightsMoving || lightsAngle != 0f)
		{
			if (lightDirUp)
			{
				lightsLeft.transform.Rotate(0f, 0f, 0.5f);
				lightsRight.transform.Rotate(0f, 0f, -0.5f);
				lightsAngle = lightsLeft.transform.localRotation.z;
				if (lightsAngle >= 0.08f)
				{
					lightDirUp = false;
				}
			}
			else
			{
				lightsLeft.transform.Rotate(0f, 0f, -0.5f);
				lightsRight.transform.Rotate(0f, 0f, 0.5f);
				lightsAngle = lightsLeft.transform.localRotation.z;
				if (lightsAngle <= -0.08f)
				{
					lightDirUp = true;
				}
			}
			yield return new WaitForSeconds(0.01f);
			if (!lightsMoving && lightsAngle < 0.005f && lightsAngle > -0.005f)
			{
				lightsLeft.transform.localRotation = Quaternion.identity;
				lightsRight.transform.localRotation = Quaternion.identity;
				lightsAngle = 0f;
			}
		}
	}

	private void setLEDStyle(int aStyle, bool aSwitch)
	{
		bool flag = false;
		if (ledStyle == 0)
		{
			flag = true;
		}
		ledStyle = aStyle;
		isSwitch = aSwitch;
		if (flag)
		{
			StartCoroutine(moveLEDs());
		}
	}

	private void stopLEDs()
	{
		ledStyle = 0;
		isSwitch = false;
	}

	private IEnumerator moveLEDs()
	{
		while (ledStyle != 0)
		{
			switch (ledStyle)
			{
			case 1:
				ledsTop.spriteName = "bg-lamp-h1";
				ledsBottom.spriteName = "bg-lamp-h1";
				ledsLeft.spriteName = "bg-lamp-v1";
				ledsRight.spriteName = "bg-lamp-v1";
				if (isSwitch)
				{
					ledStyle = 2;
				}
				else
				{
					ledStyle = 0;
				}
				break;
			case 2:
				ledsTop.spriteName = "bg-lamp-h2";
				ledsBottom.spriteName = "bg-lamp-h2";
				ledsLeft.spriteName = "bg-lamp-v2";
				ledsRight.spriteName = "bg-lamp-v2";
				if (isSwitch)
				{
					ledStyle = 1;
				}
				else
				{
					ledStyle = 0;
				}
				break;
			case 3:
				ledsTop.spriteName = "bg-lamp-h1";
				ledsBottom.spriteName = "bg-lamp-h1";
				ledsLeft.spriteName = "bg-lamp-v2";
				ledsRight.spriteName = "bg-lamp-v2";
				if (isSwitch)
				{
					ledStyle = 4;
				}
				else
				{
					ledStyle = 0;
				}
				break;
			case 4:
				ledsTop.spriteName = "bg-lamp-h2";
				ledsBottom.spriteName = "bg-lamp-h2";
				ledsLeft.spriteName = "bg-lamp-v1";
				ledsRight.spriteName = "bg-lamp-v1";
				if (isSwitch)
				{
					ledStyle = 3;
				}
				else
				{
					ledStyle = 0;
				}
				break;
			case 5:
				ledsTop.spriteName = "bg-lamp-h3";
				ledsBottom.spriteName = "bg-lamp-h3";
				ledsLeft.spriteName = "bg-lamp-v3";
				ledsRight.spriteName = "bg-lamp-v3";
				if (isSwitch)
				{
					ledStyle = 6;
				}
				else
				{
					ledStyle = 0;
				}
				break;
			case 6:
				ledsTop.spriteName = "bg-lamp-h4";
				ledsBottom.spriteName = "bg-lamp-h4";
				ledsLeft.spriteName = "bg-lamp-v4";
				ledsRight.spriteName = "bg-lamp-v4";
				if (isSwitch)
				{
					ledStyle = 5;
				}
				else
				{
					ledStyle = 0;
				}
				break;
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
}
