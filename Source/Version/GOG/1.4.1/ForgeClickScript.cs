using UnityEngine;

public class ForgeClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Forge").GetComponent<GUIForgeController>().processClick(base.gameObject.name);
	}
}
