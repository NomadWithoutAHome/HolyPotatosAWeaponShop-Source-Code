using UnityEngine;

public class FireToHireClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_FireToHire").GetComponent<GUIFireToHireController>().processClick(base.gameObject.name);
	}
}
