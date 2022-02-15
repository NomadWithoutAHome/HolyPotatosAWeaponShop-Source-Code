using UnityEngine;

public class AvatarAnimatorReceiver : MonoBehaviour
{
	private GUIAvatarPathController pathController;

	private void Awake()
	{
		pathController = base.transform.parent.GetComponent<GUIAvatarPathController>();
	}

	public void stopStand()
	{
		pathController.stopAction("stand");
	}

	private void OnClick()
	{
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processClick(base.gameObject.transform.parent.gameObject);
	}
}
