using UnityEngine;

public class ExploreResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ExploreResult").GetComponent<GUIExploreResultController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_ExploreResult").GetComponent<GUIExploreResultController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_ExploreResult").GetComponent<GUIExploreResultController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_ExploreResult").GetComponent<GUIExploreResultController>().processHover(isOver: false, base.name);
	}
}
