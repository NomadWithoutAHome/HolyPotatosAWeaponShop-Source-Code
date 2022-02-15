using UnityEngine;

public class ActivitySelectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		doClickEvent();
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			doHoverEvent();
		}
		else
		{
			endHoverEvent();
		}
	}

	private void OnDrag()
	{
		endHoverEvent();
	}

	public void doClickEvent()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().processHover(isOver: false, base.name);
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().processClick(base.gameObject.name);
	}

	public void doHoverEvent()
	{
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().processHover(isOver: true, base.name);
	}

	public void endHoverEvent()
	{
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().processHover(isOver: false, base.name);
	}
}
