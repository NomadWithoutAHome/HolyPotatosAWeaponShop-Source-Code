using UnityEngine;

public class StartScreenLoadClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().processHover(isOver: false, base.name);
	}
}
