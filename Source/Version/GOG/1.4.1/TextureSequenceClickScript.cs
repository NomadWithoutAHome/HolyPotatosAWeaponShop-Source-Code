using UnityEngine;

public class TextureSequenceClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_TextureSequencePopup").GetComponent<GUITextureSequencePopupController>().processClick(base.name);
	}
}
