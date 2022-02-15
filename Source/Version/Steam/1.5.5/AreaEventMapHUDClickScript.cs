using UnityEngine;

public class AreaEventMapHUDClickScript : MonoBehaviour
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
		GameObject.Find("AreaEventMapHUD").GetComponent<GUIAreaEventHUDController>().processHover(isOver: true, base.name);
	}

	public void endHoverEvent()
	{
		GameObject.Find("AreaEventMapHUD").GetComponent<GUIAreaEventHUDController>().processHover(isOver: false, base.name);
	}
}
