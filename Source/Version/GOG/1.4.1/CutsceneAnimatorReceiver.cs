using UnityEngine;

public class CutsceneAnimatorReceiver : MonoBehaviour
{
	private GUICutscenePathController pathController;

	private void Awake()
	{
		pathController = base.transform.parent.GetComponent<GUICutscenePathController>();
	}

	public void stopStand()
	{
		pathController.stopStand();
	}

	public void stopAction()
	{
		pathController.stopAction();
	}
}
