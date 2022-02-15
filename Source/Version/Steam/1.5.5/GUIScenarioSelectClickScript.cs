using UnityEngine;

public class GUIScenarioSelectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ScenarioSelect").GetComponent<GUIScenarioSelectController>().processClick(base.gameObject.name);
	}
}
