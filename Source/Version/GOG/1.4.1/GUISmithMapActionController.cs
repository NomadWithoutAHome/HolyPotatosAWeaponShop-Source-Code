using UnityEngine;

public class GUISmithMapActionController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private TweenPosition panelPosTween;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		panelPosTween = base.gameObject.GetComponent<TweenPosition>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "VacationButton":
			doHideButtons();
			viewController.showWorldMap(ActivityType.ActivityTypeVacation);
			break;
		case "TrainingButton":
			doHideButtons();
			viewController.showWorldMap(ActivityType.ActivityTypeTraining);
			break;
		case "CancelButton":
			doHideButtons(resume: true);
			break;
		}
	}

	public void doHideButtons(bool resume = false)
	{
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().closeSmithActionMenu(resume);
	}
}
