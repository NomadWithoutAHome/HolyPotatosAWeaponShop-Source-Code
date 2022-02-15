using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIObjectiveCompletePopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GameObject scalerObj;

	private TweenScale scaler;

	private UILabel titleLeftLabel;

	private UILabel titleRightLabel;

	private UILabel objectiveText;

	private UILabel percentLabel;

	private TweenScale starScaler;

	private ParticleSystem starParticles;

	private Queue<string> objectiveQueue;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		scalerObj = commonScreenObject.findChild(base.gameObject, "ObjectiveComplete_scaler").gameObject;
		scaler = scalerObj.GetComponent<TweenScale>();
		titleLeftLabel = commonScreenObject.findChild(scalerObj, "ObjectiveComplete_titleLeft").GetComponent<UILabel>();
		titleRightLabel = commonScreenObject.findChild(scalerObj, "ObjectiveComplete_titleRight").GetComponent<UILabel>();
		objectiveText = commonScreenObject.findChild(scalerObj, "ObjectiveComplete_text").GetComponent<UILabel>();
		percentLabel = commonScreenObject.findChild(scalerObj, "ObjectiveComplete_percent").GetComponent<UILabel>();
		starScaler = commonScreenObject.findChild(scalerObj, "ObjectiveComplete_star").GetComponent<TweenScale>();
		starParticles = starScaler.GetComponentInChildren<ParticleSystem>();
		objectiveQueue = new Queue<string>();
	}

	public void setLabels()
	{
		GameData gameData = game.getGameData();
		titleLeftLabel.text = gameData.getTextByRefId("objectiveComplete01").ToUpper(CultureInfo.InvariantCulture);
		titleRightLabel.text = gameData.getTextByRefId("objectiveComplete02").ToUpper(CultureInfo.InvariantCulture);
		percentLabel.text = "100%";
	}

	public void showObjectiveComplete()
	{
		setLabels();
		if (objectiveQueue != null && !scaler.enabled && objectiveQueue.Count > 0)
		{
			string text = objectiveQueue.Dequeue();
			objectiveText.text = text;
			commonScreenObject.tweenScale(starScaler, Vector3.one, new Vector3(1.1f, 1.1f, 1f), 0.4f, null, string.Empty);
			starParticles.Play();
			commonScreenObject.tweenScale(scaler, Vector3.zero, Vector3.one, 2.5f, null, string.Empty);
			audioController.playObjectiveCompleteAudio();
		}
	}

	public void addObjectiveComplete(string aText)
	{
		if (objectiveQueue == null)
		{
			objectiveQueue = new Queue<string>();
		}
		objectiveQueue.Enqueue(aText);
	}

	public void clearObjectiveCompleteQueue()
	{
		objectiveQueue.Clear();
		if (scaler == null || scalerObj == null)
		{
			scalerObj = commonScreenObject.findChild(base.gameObject, "ItemGet_scaler").gameObject;
			scaler = scalerObj.GetComponent<TweenScale>();
		}
		scaler.enabled = false;
		scalerObj.transform.localScale = Vector3.zero;
	}
}
