using UnityEngine;

public class RenamePopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_RenamePopup").GetComponent<GUIRenamePopupController>().processClick(base.gameObject.name);
	}
}
