using UnityEngine;

public class SmithManageClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithManage").GetComponent<GUISmithManageController>().processClick(base.gameObject.name);
	}
}
