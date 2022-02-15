using UnityEngine;

public class RandomScenarioClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_RandomScenario").GetComponent<GUIRandomScenarioController>().processClick(base.gameObject.name);
	}
}
