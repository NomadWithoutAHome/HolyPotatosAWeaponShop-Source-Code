using UnityEngine;

public class GUIMapActivitySelectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_MapActivitySelect").GetComponent<GUIMapActivitySelectController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_MapActivitySelect").GetComponent<GUIMapActivitySelectController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_MapActivitySelect").GetComponent<GUIMapActivitySelectController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_MapActivitySelect").GetComponent<GUIMapActivitySelectController>().processHover(isOver: false, base.name);
	}
}
