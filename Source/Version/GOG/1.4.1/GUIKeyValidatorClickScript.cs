using UnityEngine;

public class GUIKeyValidatorClickScript : MonoBehaviour
{
	private GUIKeyShortcutController keyCtr;

	private void Awake()
	{
		keyCtr = GameObject.Find("Panel_KeyboardShortcut").GetComponent<GUIKeyShortcutController>();
	}

	private void OnClick()
	{
		keyCtr.setModifiedKey(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_KeyboardShortcut").GetComponent<GUIKeyShortcutController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_KeyboardShortcut").GetComponent<GUIKeyShortcutController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_KeyboardShortcut").GetComponent<GUIKeyShortcutController>().processHover(isOver: false, base.name);
	}
}
