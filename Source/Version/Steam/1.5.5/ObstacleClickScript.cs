using UnityEngine;

public class ObstacleClickScript : MonoBehaviour
{
	private void OnClick()
	{
		if (UICamera.currentTouchID == -1)
		{
			GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processClick(base.gameObject);
		}
	}
}
