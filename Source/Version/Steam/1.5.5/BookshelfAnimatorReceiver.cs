using UnityEngine;

public class BookshelfAnimatorReceiver : MonoBehaviour
{
	private Game game;

	private GUIObstacleController obstacleController;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		obstacleController = GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>();
	}

	public void stopAnimate()
	{
		string aRefID = base.transform.parent.name.Split('_')[1];
		obstacleController.stopObstacleAnim(aRefID);
	}

	public void stopBookshelfDown()
	{
		string aRefID = base.transform.parent.name.Split('_')[1];
		string aRefID2 = string.Empty;
		switch (game.getPlayer().getShopLevelInt())
		{
		case 1:
			obstacleController.stopObstacleAnim(aRefID);
			aRefID2 = "10015";
			break;
		case 2:
			obstacleController.stopObstacleAnim(aRefID);
			aRefID2 = "20017";
			break;
		case 3:
			obstacleController.stopObstacleAnim(aRefID, enableSprite: false, researchInterim: true);
			aRefID2 = "30020";
			break;
		case 4:
			obstacleController.stopObstacleAnim(aRefID, enableSprite: false, researchInterim: true);
			aRefID2 = "40026";
			break;
		}
		obstacleController.playObstacleAnim(aRefID2, spriteNull: true, string.Empty);
	}

	public void stopBookshelfUp()
	{
		string aRefID = base.transform.parent.name.Split('_')[1];
		obstacleController.stopObstacleAnim(aRefID, enableSprite: true);
		string aRefID2 = string.Empty;
		switch (game.getPlayer().getShopLevelInt())
		{
		case 1:
			aRefID2 = "10015";
			break;
		case 2:
			aRefID2 = "20017";
			break;
		case 3:
			aRefID2 = "30020";
			break;
		case 4:
			aRefID2 = "40026";
			break;
		}
		if (game.getPlayer().getCurrentResearchSmith() != null)
		{
			obstacleController.playObstacleAnim(aRefID2, spriteNull: true, "blink");
		}
	}

	public void stopPortal()
	{
		string aRefID = base.transform.parent.name.Split('_')[1];
		obstacleController.stopObstacleAnim(aRefID, enableSprite: true);
		string aRefID2 = string.Empty;
		switch (game.getPlayer().getShopLevelInt())
		{
		case 1:
			aRefID2 = "10010";
			break;
		case 2:
			aRefID2 = "20013";
			break;
		case 3:
			aRefID2 = "30024";
			break;
		case 4:
			aRefID2 = "40025";
			break;
		}
		obstacleController.playObstacleAnim(aRefID2, spriteNull: false, "Up");
	}
}
