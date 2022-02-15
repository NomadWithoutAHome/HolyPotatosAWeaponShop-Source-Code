using UnityEngine;

public class CodePopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_CodePopup").GetComponent<GUICodePopupController>().processClick(base.name);
	}
}
