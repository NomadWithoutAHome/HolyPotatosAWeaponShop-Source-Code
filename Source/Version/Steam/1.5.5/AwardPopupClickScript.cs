using UnityEngine;

public class AwardPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_AwardPopup").GetComponent<GUIAwardPopupController>().processClick(base.gameObject.name);
	}
}
