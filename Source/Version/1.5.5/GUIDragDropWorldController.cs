using UnityEngine;

public class GUIDragDropWorldController : MonoBehaviour
{
	private GUICursorController cursorCtr;

	private UIRoot sceneRoot;

	private Vector3 initialPoint;

	private float distX;

	private float distY;

	private float distZ;

	private void Awake()
	{
		cursorCtr = GameObject.Find("GUICursorController").GetComponent<GUICursorController>();
		sceneRoot = GameObject.Find("NGUICameraScene").transform.parent.GetComponent<UIRoot>();
	}

	private void OnClick()
	{
		cursorCtr.SetSprite(GetComponent<SpriteRenderer>().sprite);
		GetComponent<SpriteRenderer>().sprite = null;
	}
}
