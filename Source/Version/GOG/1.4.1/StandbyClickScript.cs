using UnityEngine;

public class StandbyClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processClick(base.gameObject);
	}
}
