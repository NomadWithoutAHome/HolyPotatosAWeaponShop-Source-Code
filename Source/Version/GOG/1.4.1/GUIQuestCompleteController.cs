using System.Collections.Generic;
using UnityEngine;

public class GUIQuestCompleteController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Project currProject;

	private Dictionary<string, RewardChestItem> chestItemList;

	private UILabel questCompleteTitle;

	private UILabel rewardValue;

	private UILabel openLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		questCompleteTitle = commonScreenObject.findChild(base.gameObject, "QuestCompleteTitle").GetComponent<UILabel>();
		rewardValue = commonScreenObject.findChild(base.gameObject, "RewardBg/RewardValue").GetComponent<UILabel>();
		openLabel = commonScreenObject.findChild(base.gameObject, "Open_Button/OpenLabel").GetComponent<UILabel>();
	}

	public void processClick(string gameobjectName)
	{
		if (gameobjectName != null && gameobjectName == "Open_Button")
		{
			CommonAPI.debug("opening treasure chest");
			viewController.closeQuestComplete();
			viewController.closeQuestProgress(currProject);
			if (chestItemList.Count > 0)
			{
				viewController.showOpenTreasure(chestItemList);
				return;
			}
			viewController.hideBlackMask();
			viewController.resumeEverything();
		}
	}

	public void setReference(Project aProject, int questGold, Dictionary<string, RewardChestItem> aChestItemList)
	{
		GameData gameData = game.getGameData();
		currProject = aProject;
		chestItemList = aChestItemList;
		questCompleteTitle.text = gameData.getTextByRefId("questComplete01");
		openLabel.text = gameData.getTextByRefId("questComplete02");
	}
}
