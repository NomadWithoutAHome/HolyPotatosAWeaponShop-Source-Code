using UnityEngine;

public class NGUIClickCheckScript : MonoBehaviour
{
	private void OnClick()
	{
		CommonAPI.debug("Clicked: " + base.gameObject.name + " " + base.gameObject.layer);
	}

	private void OnMouseDown()
	{
		CommonAPI.debug("Clicked: " + base.gameObject.name + " " + base.gameObject.layer);
	}
}
