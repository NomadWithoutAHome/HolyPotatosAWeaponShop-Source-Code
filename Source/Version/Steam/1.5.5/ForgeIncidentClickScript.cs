using UnityEngine;

public class ForgeIncidentClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ForgeIncident").GetComponent<GUIForgeIncidentController>().processClick(base.gameObject.name);
	}
}
