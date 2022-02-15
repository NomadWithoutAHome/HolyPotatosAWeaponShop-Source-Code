using UnityEngine;

public class GeneralPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_GeneralPopup").GetComponent<GUIGeneralPopupController>().processClick(base.gameObject.name);
	}
}
