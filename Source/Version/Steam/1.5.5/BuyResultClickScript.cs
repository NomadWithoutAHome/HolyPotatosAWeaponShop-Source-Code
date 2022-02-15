using UnityEngine;

public class BuyResultClickScript : MonoBehaviour
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
		GameObject.Find("Panel_BuyResult").GetComponent<GUIBuyResultController>().processClick(base.name);
	}

	public void doHoverEvent()
	{
		GameObject.Find("Panel_BuyResult").GetComponent<GUIBuyResultController>().processHover(isOver: true, base.name);
	}

	public void endHoverEvent()
	{
		GameObject.Find("Panel_BuyResult").GetComponent<GUIBuyResultController>().processHover(isOver: false, base.name);
	}
}
