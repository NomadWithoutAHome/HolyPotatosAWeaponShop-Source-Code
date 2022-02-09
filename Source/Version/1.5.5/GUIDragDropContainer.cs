using UnityEngine;

public class GUIDragDropContainer : MonoBehaviour
{
	private GUICursorController cursorCtr;

	private void Awake()
	{
		cursorCtr = GameObject.Find("GUICursorController").GetComponent<GUICursorController>();
	}

	private void OnClick()
	{
		Sprite sprite = cursorCtr.GetComponent<SpriteRenderer>().sprite;
		if (sprite != null)
		{
			GetComponent<SpriteRenderer>().sprite = sprite;
			cursorCtr.GetComponent<SpriteRenderer>().sprite = null;
		}
	}
}
