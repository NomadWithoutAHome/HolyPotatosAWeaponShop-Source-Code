using UnityEngine;

public class KeyPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_KeyboardPopup").GetComponent<GUIKeyPopup>().processClick(base.gameObject.name);
	}
}
