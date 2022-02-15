using UnityEngine;

public class ResearchClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ResearchNEW").GetComponent<GUIResearchNEW2Controller>().processClick(base.gameObject);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_ResearchNEW").GetComponent<GUIResearchNEW2Controller>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_ResearchNEW").GetComponent<GUIResearchNEW2Controller>().processHover(isOver: false, base.gameObject);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_ResearchNEW").GetComponent<GUIResearchNEW2Controller>().processHover(isOver: false, base.gameObject);
	}
}
