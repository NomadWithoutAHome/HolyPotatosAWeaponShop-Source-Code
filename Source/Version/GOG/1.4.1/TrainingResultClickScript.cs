using UnityEngine;

public class TrainingResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_TrainingResult").GetComponent<GUITrainingResultController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_TrainingResult").GetComponent<GUITrainingResultController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_TrainingResult").GetComponent<GUITrainingResultController>().processHover(isOver: false, base.name);
		}
	}
}
