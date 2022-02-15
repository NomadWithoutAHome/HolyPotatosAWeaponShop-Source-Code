using UnityEngine;

public class SmithHireResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithHireResult").GetComponent<GUISmithHireResultController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_SmithHireResult").GetComponent<GUISmithHireResultController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_SmithHireResult").GetComponent<GUISmithHireResultController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_SmithHireResult").GetComponent<GUISmithHireResultController>().processHover(isOver: false, base.name);
	}
}
