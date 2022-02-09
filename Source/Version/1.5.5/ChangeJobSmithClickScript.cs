using UnityEngine;

public class ChangeJobSmithClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ChangeJobSmith").GetComponent<GUIChangeJobSmithController>().processClick(base.name);
	}
}
