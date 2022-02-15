using UnityEngine;

public class ForgeProgressClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		base.transform.parent.GetComponent<GUIForgingProgressNewController>().processClick(base.gameObject.name);
	}
}
