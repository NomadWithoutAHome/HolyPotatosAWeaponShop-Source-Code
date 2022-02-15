using UnityEngine;

public class ForgeCategoryObjectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("CategorySelection").GetComponent<GUIForgeCategoryController>().processClick(base.transform.parent.name);
	}
}
