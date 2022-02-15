using UnityEngine;

public class SellResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SellResult").GetComponent<GUISellResultController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_SellResult").GetComponent<GUISellResultController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_SellResult").GetComponent<GUISellResultController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_SellResult").GetComponent<GUISellResultController>().processHover(isOver: false, base.name);
	}
}
