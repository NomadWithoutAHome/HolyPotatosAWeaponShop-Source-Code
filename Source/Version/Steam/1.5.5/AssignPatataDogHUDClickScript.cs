using UnityEngine;

public class AssignPatataDogHUDClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_AssignPatataDogHUD").GetComponent<GUIAssignPatataDogHUDController>().processClick(base.gameObject.name);
	}
}
