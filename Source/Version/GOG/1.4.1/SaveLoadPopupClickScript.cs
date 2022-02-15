using UnityEngine;

public class SaveLoadPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SaveLoadPopup").GetComponent<GUISaveLoadPopupController>().processClick(base.gameObject.name);
	}
}
