using UnityEngine;

public class ForgeMenuNewClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		CommonAPI.debug("CLICK " + base.gameObject.name);
		GameObject.Find("Panel_ForgeMenuNEW").GetComponent<GUIForgeMenuNewController>().processClick(base.gameObject.name);
	}
}
