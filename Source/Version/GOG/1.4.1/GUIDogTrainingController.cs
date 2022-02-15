using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDogTrainingController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private List<string> patataIdle;

	private List<string> patataSmile;

	private List<string> patataFrown;

	private List<string> dogIdle;

	private List<string> dogCommand;

	private bool gameStarted;

	private bool gameEnded;

	private string gamePhase;

	private int phaseTimer;

	private UILabel questionNumLabel;

	private int questionNum;

	private int questionNumMax;

	private bool isCorrect;

	private UITexture commandTexture;

	private int commandIndex;

	private UILabel instructionLabel;

	private UILabel scoreLabel;

	private int score;

	private UITexture patataTexture;

	private UISprite patataGlow;

	private int currentExpression;

	private UITexture dogTexture;

	private UISprite dogGlow;

	private int dogActionIndex;

	private UIButton startButton;

	private UIButton closeButton;

	private UIButton smileButton;

	private UIButton frownButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		gameStarted = false;
		gameEnded = false;
		gamePhase = string.Empty;
		phaseTimer = 0;
		questionNum = 0;
		questionNumMax = 5;
		isCorrect = false;
		score = 0;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Start_button":
			startDogTraining();
			break;
		case "Expression1_button":
			changeExpression(1);
			break;
		case "Expression2_button":
			changeExpression(2);
			break;
		case "Close_button":
			viewController.closeDogTraining();
			break;
		}
	}

	public void setReference()
	{
		fillTextureLists();
		questionNumLabel = commonScreenObject.findChild(base.gameObject, "Command_bg/Question_label").GetComponent<UILabel>();
		commandTexture = commonScreenObject.findChild(base.gameObject, "Command_bg/Command_texture").GetComponent<UITexture>();
		instructionLabel = commonScreenObject.findChild(base.gameObject, "Command_bg/Instruction_label").GetComponent<UILabel>();
		scoreLabel = commonScreenObject.findChild(base.gameObject, "Score_title/Score_label").GetComponent<UILabel>();
		updateScore(isCorrect: false);
		patataTexture = commonScreenObject.findChild(base.gameObject, "Patata_texture").GetComponent<UITexture>();
		patataGlow = commonScreenObject.findChild(patataTexture.gameObject, "PatataGlow_sprite").GetComponent<UISprite>();
		dogTexture = commonScreenObject.findChild(base.gameObject, "Jagamaru_texture").GetComponent<UITexture>();
		dogGlow = commonScreenObject.findChild(dogTexture.gameObject, "JagamaruGlow_sprite").GetComponent<UISprite>();
		startButton = commonScreenObject.findChild(base.gameObject, "Start_button").GetComponent<UIButton>();
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
		smileButton = commonScreenObject.findChild(base.gameObject, "Expression1_button").GetComponent<UIButton>();
		frownButton = commonScreenObject.findChild(base.gameObject, "Expression2_button").GetComponent<UIButton>();
		setIdleState();
		resetCharacters();
	}

	private void startDogTraining()
	{
		gameStarted = true;
		gameEnded = false;
		instructionLabel.alpha = 0f;
		gamePhase = "QUESTION";
		phaseTimer = 0;
		questionNum = 1;
		score = 0;
		updateScore(isCorrect: false);
		isCorrect = false;
		StartCoroutine(gameTimer());
	}

	private IEnumerator gameTimer()
	{
		while (gameStarted && !gameEnded)
		{
			switch (gamePhase)
			{
			case "QUESTION":
				setQuestion(questionNum);
				gamePhase = "DOG_THINKING";
				phaseTimer = 0;
				setButtonsPhase("WAITING");
				break;
			case "DOG_THINKING":
				phaseTimer++;
				if (phaseTimer > 10)
				{
					dogAnswer();
					gamePhase = "WAITING_ANSWER";
					phaseTimer = 0;
					setButtonsPhase("ANSWER");
				}
				break;
			case "WAITING_ANSWER":
				phaseTimer++;
				checkAnswer();
				if (phaseTimer > 50)
				{
					questionNum++;
					if (questionNum <= questionNumMax)
					{
						gamePhase = "QUESTION";
						phaseTimer = 0;
						break;
					}
					questionNum = 0;
					gamePhase = "ENDED";
					phaseTimer = 0;
					gameEnded = true;
					setIdleState();
					resetCharacters();
				}
				else if (phaseTimer == 26 && dogActionIndex != commandIndex)
				{
					dogAnswer();
				}
				break;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void checkAnswer()
	{
		bool flag = false;
		if ((commandIndex == dogActionIndex && currentExpression == 1) || (commandIndex != dogActionIndex && currentExpression == 2))
		{
			if (!isCorrect)
			{
				flag = true;
				isCorrect = true;
			}
			patataGlow.alpha = 1f;
			score++;
		}
		else
		{
			patataGlow.alpha = 0f;
			isCorrect = false;
		}
		updateScore(isCorrect);
	}

	private void setQuestion(int qNum)
	{
		questionNum = qNum;
		questionNumLabel.text = questionNum + "/" + questionNumMax;
		commandIndex = CommonAPI.getRandomInt(dogCommand.Count);
		commandTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + dogCommand[commandIndex]);
		commandTexture.alpha = 1f;
		TweenScale component = commandTexture.GetComponent<TweenScale>();
		Vector3 aEndScale = new Vector3(1.3f, 1.3f, 1.3f);
		commonScreenObject.tweenScale(component, Vector3.one, aEndScale, 0.8f, null, string.Empty);
		resetCharacters();
	}

	private void dogAnswer()
	{
		int randomInt = CommonAPI.getRandomInt(3);
		if (randomInt == 1)
		{
			dogActionIndex = commandIndex;
		}
		else
		{
			int num = CommonAPI.getRandomInt(dogCommand.Count - 1);
			if (num >= commandIndex)
			{
				num++;
			}
			dogActionIndex = num;
		}
		dogTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + dogCommand[dogActionIndex]);
		dogGlow.alpha = 1f;
		TweenScale component = dogTexture.GetComponent<TweenScale>();
		Vector3 aEndScale = new Vector3(1.3f, 1.3f, 1.3f);
		commonScreenObject.tweenScale(component, Vector3.one, aEndScale, 0.8f, null, string.Empty);
	}

	private void changeExpression(int expression)
	{
		currentExpression = expression;
		switch (expression)
		{
		case 1:
		{
			int randomInt3 = CommonAPI.getRandomInt(patataSmile.Count);
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + patataSmile[randomInt3]);
			TweenScale component2 = patataTexture.GetComponent<TweenScale>();
			Vector3 aEndScale2 = new Vector3(1.3f, 1.3f, 1.3f);
			commonScreenObject.tweenScale(component2, Vector3.one, aEndScale2, 0.8f, null, string.Empty);
			break;
		}
		case 2:
		{
			int randomInt2 = CommonAPI.getRandomInt(patataFrown.Count);
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + patataFrown[randomInt2]);
			TweenScale component = patataTexture.GetComponent<TweenScale>();
			Vector3 aEndScale = new Vector3(1.3f, 1.3f, 1.3f);
			commonScreenObject.tweenScale(component, Vector3.one, aEndScale, 0.8f, null, string.Empty);
			break;
		}
		default:
		{
			int randomInt = CommonAPI.getRandomInt(patataIdle.Count);
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + patataIdle[randomInt]);
			patataGlow.alpha = 0f;
			break;
		}
		}
	}

	private void resetCharacters()
	{
		changeExpression(0);
		int randomInt = CommonAPI.getRandomInt(dogIdle.Count);
		dogTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + dogIdle[randomInt]);
		dogGlow.alpha = 0f;
		isCorrect = false;
	}

	private void updateScore(bool isCorrect)
	{
		scoreLabel.text = CommonAPI.formatNumber(score);
		TweenScale component = scoreLabel.GetComponent<TweenScale>();
		if (isCorrect && !component.enabled)
		{
			Vector3 aEndScale = new Vector3(1.4f, 1.4f, 1.4f);
			commonScreenObject.tweenScale(component, Vector3.one, aEndScale, 0.8f, null, string.Empty);
		}
	}

	private void setIdleState()
	{
		questionNumLabel.text = string.Empty;
		commandTexture.alpha = 0f;
		instructionLabel.alpha = 1f;
		setButtonsPhase("IDLE");
	}

	private void fillTextureLists()
	{
		patataIdle = new List<string>();
		patataIdle.Add("cutscene-patata-curious");
		patataIdle.Add("cutscene-patata-normal");
		patataIdle.Add("cutscene-patata-calm");
		patataSmile = new List<string>();
		patataSmile.Add("cutscene-patata-cheerful");
		patataSmile.Add("cutscene-patata-elated");
		patataSmile.Add("cutscene-patata-cute");
		patataSmile.Add("cutscene-patata-excited");
		patataSmile.Add("cutscene-patata-proud");
		patataFrown = new List<string>();
		patataFrown.Add("cutscene-patata-annoyed");
		patataFrown.Add("cutscene-patata-disgusted");
		patataFrown.Add("cutscene-patata-serious");
		patataFrown.Add("cutscene-patata-cry");
		patataFrown.Add("cutscene-patata-scolding");
		dogIdle = new List<string>();
		dogIdle.Add("cutscene-jagamaru-curious");
		dogIdle.Add("cutscene-jagamaru-scratching");
		dogIdle.Add("cutscene-jagamaru-yawning");
		dogCommand = new List<string>();
		dogCommand.Add("cutscene-jagamaru-barking");
		dogCommand.Add("cutscene-jagamaru-drooling");
		dogCommand.Add("cutscene-jagamaru-hopeful");
		dogCommand.Add("cutscene-jagamaru-puppyeyes1");
		dogCommand.Add("cutscene-jagamaru-puppyeyes2");
		dogCommand.Add("cutscene-jagamaru-panicking");
		dogCommand.Add("cutscene-jagamaru-excited");
	}

	private void setButtonsPhase(string buttonPhase)
	{
		switch (buttonPhase)
		{
		case "ANSWER":
			startButton.isEnabled = false;
			closeButton.isEnabled = false;
			smileButton.isEnabled = true;
			frownButton.isEnabled = true;
			break;
		case "WAITING":
			startButton.isEnabled = false;
			closeButton.isEnabled = false;
			smileButton.isEnabled = false;
			frownButton.isEnabled = false;
			break;
		case "IDLE":
			startButton.isEnabled = true;
			closeButton.isEnabled = true;
			smileButton.isEnabled = false;
			frownButton.isEnabled = false;
			break;
		}
	}
}
