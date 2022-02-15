using UnityEngine;

public class RequestDeliveryClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_WeaponRequestSubmit").GetComponent<GUIRequestDeliveryController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_WeaponRequestSubmit").GetComponent<GUIRequestDeliveryController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_WeaponRequestSubmit").GetComponent<GUIRequestDeliveryController>().processHover(isOver: false, base.gameObject);
		}
	}
}
