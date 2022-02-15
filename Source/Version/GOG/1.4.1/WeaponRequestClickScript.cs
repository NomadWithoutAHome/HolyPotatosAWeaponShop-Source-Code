using UnityEngine;

public class WeaponRequestClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_WeaponRequest").GetComponent<GUIWeaponRequestController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_WeaponRequest").GetComponent<GUIWeaponRequestController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_WeaponRequest").GetComponent<GUIWeaponRequestController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_WeaponRequest").GetComponent<GUIWeaponRequestController>().processHover(isOver: false, base.name);
	}
}
