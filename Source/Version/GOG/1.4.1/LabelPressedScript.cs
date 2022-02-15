using UnityEngine;

public class LabelPressedScript : MonoBehaviour
{
	private void OnCollisionEnter()
	{
		CommonAPI.debug("enter");
		Vector3 localPosition = base.gameObject.GetComponentInChildren<Transform>().localPosition;
		localPosition.y -= 2.5f;
		base.gameObject.GetComponentInChildren<Transform>().localPosition = localPosition;
		base.gameObject.GetComponentInChildren<UILabel>().color = Color.grey;
	}

	private void OnCollisionExit()
	{
		CommonAPI.debug("exit");
		base.gameObject.GetComponentInChildren<Transform>().localPosition = Vector3.zero;
		base.gameObject.GetComponentInChildren<UILabel>().color = Color.white;
	}
}
