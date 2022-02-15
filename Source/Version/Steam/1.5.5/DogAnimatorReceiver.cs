using UnityEngine;

public class DogAnimatorReceiver : MonoBehaviour
{
	private GUIDogPathController pathController;

	private void Awake()
	{
		pathController = base.transform.parent.GetComponent<GUIDogPathController>();
	}

	public void stopStand()
	{
		pathController.stopAction("stand");
	}

	public void stopBark()
	{
		pathController.stopAction("bark");
	}

	private void OnClick()
	{
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processClick(base.gameObject.transform.parent.gameObject);
	}
}
