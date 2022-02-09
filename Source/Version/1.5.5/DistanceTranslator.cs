using UnityEngine;

public class DistanceTranslator : MonoBehaviour
{
	private void Start()
	{
		base.transform.position = Vector3.zero;
		int num = 320;
		int num2 = 88;
		int num3 = (Screen.width - num) / 2;
		int num4 = (Screen.height - num2) / 2;
		base.gameObject.GetComponent<GUITexture>().pixelInset = new Rect(num3, num4, 320f, 88f);
	}
}
