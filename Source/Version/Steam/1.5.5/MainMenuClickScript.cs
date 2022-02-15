using UnityEngine;

public class MainMenuClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_MainMenu").GetComponent<GUIMainMenuController>().processClick(base.gameObject.name);
	}
}
