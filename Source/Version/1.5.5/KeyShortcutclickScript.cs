using UnityEngine;

public class KeyShortcutclickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_KeyboardShortcut").GetComponent<GUIKeyShortcutController>().processClick(base.gameObject.name);
	}
}
