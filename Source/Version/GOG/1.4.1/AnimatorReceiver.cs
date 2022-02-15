using UnityEngine;

public class AnimatorReceiver : MonoBehaviour
{
	private GUIPathController pathController;

	private void Awake()
	{
		pathController = base.transform.parent.GetComponent<GUIPathController>();
	}

	public void stopStand()
	{
		pathController.stopAction("stand");
	}

	public void stopDaydream()
	{
		pathController.stopAction("daydream");
	}

	public void stopSad()
	{
		pathController.stopAction("sad");
	}

	public void stopWorking()
	{
		pathController.stopAction("isWorking");
	}

	public void stopChi()
	{
		pathController.stopAction("chi");
	}

	public void stopCoffee()
	{
		pathController.stopAction("coffee");
	}

	public void stopShiver()
	{
		pathController.stopAction("shiver");
	}

	public void stopFire()
	{
		pathController.stopFire();
	}

	private void OnClick()
	{
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processClick(base.gameObject);
	}

	private void OnHover(bool isOver)
	{
	}
}
