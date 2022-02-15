using UnityEngine;

public class ExploreClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Explore").GetComponent<GUIExploreController>().processClick(base.gameObject.name);
	}
}
