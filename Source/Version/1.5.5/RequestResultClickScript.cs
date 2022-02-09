using UnityEngine;

public class RequestResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_RequestResult").GetComponent<GUIRequestResultController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_RequestResult").GetComponent<GUIRequestResultController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_RequestResult").GetComponent<GUIRequestResultController>().processHover(isOver: false, base.gameObject);
		}
	}
}
