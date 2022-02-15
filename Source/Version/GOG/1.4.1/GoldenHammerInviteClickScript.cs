using UnityEngine;

public class GoldenHammerInviteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_GoldenHammerInvite").GetComponent<GUIGoldenHammerInviteController>().processClick(base.name);
	}
}
