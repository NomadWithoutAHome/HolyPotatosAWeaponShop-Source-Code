using UnityEngine;

public class EventPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_EventPopup").GetComponent<GUIEventPopupController>().processClick(base.gameObject.name);
	}
}
