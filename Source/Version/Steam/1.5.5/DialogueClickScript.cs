using UnityEngine;

public class DialogueClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Dialogue").GetComponent<GUIDialogueController>().processClick(base.name);
	}
}
