using UnityEngine;

public class SettingsConfirmClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SettingConfirm").GetComponent<GUISettingsConfirmController>().processClick(base.gameObject.name);
	}
}
