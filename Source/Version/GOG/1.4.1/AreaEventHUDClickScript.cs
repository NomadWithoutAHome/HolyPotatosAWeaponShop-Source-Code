using UnityEngine;

public class AreaEventHUDClickScript : MonoBehaviour
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
	}

	public void doHoverEvent()
	{
		GameObject.Find("AreaEventHUD").GetComponent<GUIAreaEventHUDController>().processHover(isOver: true, base.name);
	}

	public void endHoverEvent()
	{
		GameObject.Find("AreaEventHUD").GetComponent<GUIAreaEventHUDController>().processHover(isOver: false, base.name);
	}
}
