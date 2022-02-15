using UnityEngine;

public class Tier2MenuClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Tier2Menu").GetComponent<GUITier2MenuController>().processClick(base.gameObject.name);
	}
}
