using UnityEngine;

public class ForgeWeaponMenuClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ForgeWeaponMenu").GetComponent<GUIForgeWeaponMenuController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_ForgeWeaponMenu").GetComponent<GUIForgeWeaponMenuController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_ForgeWeaponMenu").GetComponent<GUIForgeWeaponMenuController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_ForgeWeaponMenu").GetComponent<GUIForgeWeaponMenuController>().processHover(isOver: false, base.name);
	}
}
