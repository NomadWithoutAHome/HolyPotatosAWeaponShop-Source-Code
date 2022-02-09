using System.Collections.Generic;
using UnityEngine;

public class GUIQuestResultController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GameObject actionHeader;

	private UILabel choiceText;

	private GameObject action_1;

	private GameObject action_2;

	private GameObject questCompleteHeader;

	private UILabel completeText;

	private UILabel questRewardLabel;

	private GameObject questRewardFrame;

	private GameObject questReward;

	private UILabel congratsText;

	private UILabel scenarioText;

	private QuestResultType currentPopupType;

	private Subquest currentSubquest;

	private int indexSelected;

	private UIButton goButton;

	private UIButton okButton;

	private string warText;

	private string unlockText;

	private bool isBGMStopped;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		actionHeader = GameObject.Find("ActionHeader");
		choiceText = GameObject.Find("ChoiceText").GetComponent<UILabel>();
		action_1 = GameObject.Find("Action_1");
		action_2 = GameObject.Find("Action_2");
		questCompleteHeader = GameObject.Find("QuestCompleteHeader");
		completeText = GameObject.Find("CompleteText").GetComponent<UILabel>();
		questRewardLabel = GameObject.Find("QuestRewardLabel").GetComponent<UILabel>();
		questRewardFrame = GameObject.Find("QuestRewardFrame");
		questReward = GameObject.Find("QuestReward");
		congratsText = GameObject.Find("CongratsText").GetComponent<UILabel>();
		scenarioText = GameObject.Find("ScenarioText").GetComponent<UILabel>();
		currentSubquest = null;
		indexSelected = 0;
		goButton = GameObject.Find("GoButton").GetComponent<UIButton>();
		okButton = GameObject.Find("OkButton").GetComponent<UIButton>();
		warText = string.Empty;
		unlockText = string.Empty;
		isBGMStopped = false;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "OkButton")
		{
			switch (currentPopupType)
			{
			case QuestResultType.QuestResultTypeQuestScenario:
				viewController.closeQuestResultPopup(hide: true, resume: true);
				break;
			case QuestResultType.QuestResultTypeQuestChoiceResult:
				if (currentSubquest != null && currentSubquest.getSubquestType() == SubquestType.SubquestTypeEnding && warText != string.Empty)
				{
					setReference(QuestResultType.QuestResultTypeQuestScenario, null, null, currentSubquest);
				}
				else if (unlockText != string.Empty)
				{
					setReference(QuestResultType.QuestResultTypeUnlockJobClass);
				}
				else
				{
					viewController.closeQuestResultPopup(hide: true, resume: true);
				}
				break;
			case QuestResultType.QuestResultTypeUnlockJobClass:
				viewController.closeQuestResultPopup(hide: true, resume: true);
				break;
			}
		}
		else if (gameObjectName == "GoButton")
		{
			viewController.closeQuestResultPopup(hide: false, resume: false);
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			if (indexSelected != CommonAPI.parseInt(array[1]))
			{
				if (indexSelected != 0)
				{
					commonScreenObject.findChild(GameObject.Find("Action_" + indexSelected), "ActionSelectFrame").GetComponent<UISprite>().enabled = false;
				}
				indexSelected = CommonAPI.parseInt(array[1]);
				commonScreenObject.findChild(GameObject.Find(gameObjectName), "ActionSelectFrame").GetComponent<UISprite>().enabled = true;
				goButton.GetComponent<UIButton>().isEnabled = true;
			}
		}
		if (isBGMStopped)
		{
			isBGMStopped = false;
			audioController.playBGMAudio(string.Empty);
		}
	}

	private void disableAll()
	{
		actionHeader.SetActive(value: false);
		choiceText.text = string.Empty;
		action_1.SetActive(value: false);
		action_2.SetActive(value: false);
		questCompleteHeader.SetActive(value: false);
		completeText.text = string.Empty;
		questRewardFrame.SetActive(value: false);
		questReward.SetActive(value: false);
		congratsText.text = string.Empty;
		scenarioText.text = string.Empty;
		goButton.gameObject.SetActive(value: false);
		okButton.gameObject.SetActive(value: false);
	}

	public void setReference(QuestResultType aPopupType, List<Subquest> choiceList = null, Quest quest = null, Subquest subquest = null)
	{
		disableAll();
		GameData gameData = game.getGameData();
		currentPopupType = aPopupType;
		switch (aPopupType)
		{
		case QuestResultType.QuestResultTypeQuestChoice:
			actionHeader.SetActive(value: true);
			actionHeader.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuQuest08");
			choiceText.text = quest.getQuestEndText();
			action_1.SetActive(value: true);
			action_2.SetActive(value: true);
			action_1.GetComponentInChildren<UILabel>().text = choiceList[0].getSubquestTitle();
			action_2.GetComponentInChildren<UILabel>().text = choiceList[1].getSubquestTitle();
			goButton.gameObject.SetActive(value: true);
			goButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral10");
			goButton.GetComponent<UIButton>().isEnabled = false;
			break;
		case QuestResultType.QuestResultTypeQuestChoiceResult:
		{
			currentSubquest = subquest;
			warText = subquest.getWarText();
			questCompleteHeader.SetActive(value: true);
			questCompleteHeader.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuQuest09");
			completeText.text = subquest.getSubquestDesc();
			questRewardFrame.SetActive(value: true);
			questReward.SetActive(value: true);
			questRewardLabel.text = gameData.getTextByRefId("questResult01");
			showFireworks();
			int subquestGold = subquest.getSubquestGold();
			if (subquestGold > 0)
			{
				commonScreenObject.findChild(questReward, "GoldIcon").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(questReward, "GoldSubquestLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(subquestGold);
			}
			List<Hero> list = new List<Hero>();
			string subquestItemRefId = subquest.getSubquestItemRefId();
			int subquestItemQuantity = subquest.getSubquestItemQuantity();
			if (subquestItemRefId != string.Empty && subquestItemQuantity > 0)
			{
				Item itemByRefId = game.getGameData().getItemByRefId(subquestItemRefId);
				commonScreenObject.findChild(questReward, "RewardQty").GetComponent<UILabel>().text = subquestItemQuantity + "X";
				commonScreenObject.findChild(questReward, "ItemName").GetComponent<UILabel>().text = itemByRefId.getItemName();
			}
			okButton.gameObject.SetActive(value: true);
			okButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
			break;
		}
		case QuestResultType.QuestResultTypeQuestScenario:
		{
			congratsText.text = gameData.getTextByRefId("menuGeneral07");
			int subquestGood = subquest.getSubquestGood();
			int subquestEvil = subquest.getSubquestEvil();
			string text = string.Empty;
			if (subquest.getSubquestType() == SubquestType.SubquestTypeEnding && warText != string.Empty)
			{
				text = text + "\n" + gameData.getTextByRefId(warText) + "\n";
				if (subquestGood > 0)
				{
					string text2 = text;
					text = text2 + "\n" + gameData.getTextByRefId("alignmentChange04") + " +" + subquestGood + "\n";
				}
				if (subquestEvil > 0)
				{
					if (game.getPlayer().getTofuKnown())
					{
						string text2 = text;
						text = text2 + "\n" + gameData.getTextByRefId("alignmentChange05") + " +" + subquestEvil + "\n";
					}
					else
					{
						string text2 = text;
						text = text2 + "\n" + gameData.getTextByRefId("alignmentChange06") + " +" + subquestEvil + "\n";
					}
				}
			}
			scenarioText.text = text;
			okButton.gameObject.SetActive(value: true);
			okButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
			break;
		}
		case QuestResultType.QuestResultTypeUnlockJobClass:
			congratsText.text = gameData.getTextByRefId("menuGeneral07");
			scenarioText.text = unlockText;
			okButton.gameObject.SetActive(value: true);
			okButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
			break;
		}
	}

	public void showFireworks()
	{
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "Firework").gameObject;
		ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = componentsInChildren;
		foreach (ParticleSystem particleSystem in array)
		{
			particleSystem.Play();
		}
		isBGMStopped = true;
		audioController.stopBGMAudio();
		audioController.playForgeCompleteAudio();
	}
}
