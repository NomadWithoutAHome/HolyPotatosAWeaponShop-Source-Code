using UnityEngine;

public class RequestListClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_RequestList").GetComponent<GUIRequestListController>().processClick(base.gameObject);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_RequestList").GetComponent<GUIRequestListController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_RequestList").GetComponent<GUIRequestListController>().processHover(isOver: false, base.gameObject);
		}
	}
}
