using UnityEngine;

public class MenuBlueprintClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_MenuBlueprint").GetComponent<GUIMenuBlueprintController>().processClick(base.gameObject.name);
	}
}
