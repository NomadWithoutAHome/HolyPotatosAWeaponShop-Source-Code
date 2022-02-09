using UnityEngine;

public class LanguageClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_LanguageSelect").GetComponent<GUILanguageController>().processClick(base.gameObject.name);
	}
}
