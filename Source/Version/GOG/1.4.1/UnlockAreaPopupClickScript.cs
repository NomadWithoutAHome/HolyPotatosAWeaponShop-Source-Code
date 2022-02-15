using UnityEngine;

public class UnlockAreaPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_UnlockAreaPopup").GetComponent<GUIMapUnlockAreaPopupController>().processClick(base.gameObject.name);
	}
}
