using UnityEngine;

public class EventSelectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_EventSelect").GetComponent<GUIEventSelectController>().processClick(base.gameObject.name);
	}
}
