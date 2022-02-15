using UnityEngine;

public class SmithManageNewClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithManagePopup").GetComponent<GUISmithManageNewController>().processClick(base.gameObject.name);
	}
}
