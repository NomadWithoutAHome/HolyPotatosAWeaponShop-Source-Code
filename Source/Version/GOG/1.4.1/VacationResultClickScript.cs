using UnityEngine;

public class VacationResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_VacationResult").GetComponent<GUIVacationResultController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_VacationResult").GetComponent<GUIVacationResultController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_VacationResult").GetComponent<GUIVacationResultController>().processHover(isOver: false, base.name);
		}
	}
}
