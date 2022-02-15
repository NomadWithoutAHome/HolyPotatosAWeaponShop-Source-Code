using UnityEngine;

public class SettingsClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Settings").GetComponent<GUISettingsController>().processClick(base.gameObject.name);
	}
}
