using UnityEngine;

public class AssignSmithHUDClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_AssignSmithHUD").GetComponent<GUIAssignSmithHUDController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		GameObject.Find("Panel_AssignSmithHUD").GetComponent<GUIAssignSmithHUDController>().processHover(isOver, base.gameObject.name);
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_AssignSmithHUD").GetComponent<GUIAssignSmithHUDController>().processHover(isOver: false, base.gameObject.name);
	}
}
