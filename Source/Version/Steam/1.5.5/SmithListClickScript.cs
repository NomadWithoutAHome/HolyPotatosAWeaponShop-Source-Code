using UnityEngine;

public class SmithListClickScript : MonoBehaviour
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
		base.transform.parent.GetComponent<GUISmithListController>().processClick(base.transform.parent.gameObject.name);
	}

	public void doHoverEvent()
	{
		base.transform.parent.GetComponent<GUISmithListController>().processHover(isOver: true, base.name);
	}

	public void endHoverEvent()
	{
		base.transform.parent.GetComponent<GUISmithListController>().processHover(isOver: false, base.name);
	}
}
