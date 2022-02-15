using UnityEngine;

public class ForgeCategoryClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("CategorySelection").GetComponent<GUIForgeCategoryController>().processClick(base.gameObject.name);
	}
}
