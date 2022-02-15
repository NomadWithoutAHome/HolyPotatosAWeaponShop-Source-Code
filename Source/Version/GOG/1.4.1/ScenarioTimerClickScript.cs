using UnityEngine;

public class ScenarioTimerClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ScenarioTimer").GetComponent<GUIScenarioTimerController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_ScenarioTimer").GetComponent<GUIScenarioTimerController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_ScenarioTimer").GetComponent<GUIScenarioTimerController>().processHover(isOver: false, base.name);
		}
	}
}
