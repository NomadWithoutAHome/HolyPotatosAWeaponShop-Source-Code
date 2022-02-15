using UnityEngine;

public class AreaInfoHoverScript : MonoBehaviour
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
		GameObject.Find("GUIMapController").GetComponent<GUIMapController>().processHover(isOver: true, base.name, string.Empty);
	}

	public void endHoverEvent()
	{
		GameObject.Find("GUIMapController").GetComponent<GUIMapController>().processHover(isOver: false, base.name, string.Empty);
	}
}
