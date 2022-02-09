using UnityEngine;

public class TutorialClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Tutorial").GetComponent<GUITutorialController>().processClick(base.name);
	}
}
