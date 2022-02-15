using UnityEngine;

public class GUILockedIslandPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_LockedIslandPopup").GetComponent<GUILockedIslandPopupController>().processClick(base.name);
	}
}
