using UnityEngine;

public class LegendaryRequestClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_LegendaryWeaponRequest").GetComponent<GUILegendaryRequestController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_LegendaryWeaponRequest").GetComponent<GUILegendaryRequestController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_LegendaryWeaponRequest").GetComponent<GUILegendaryRequestController>().processHover(isOver: false, base.name);
		}
	}
}
