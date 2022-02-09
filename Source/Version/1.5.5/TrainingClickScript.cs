using UnityEngine;

public class TrainingClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("Panel_Training").GetComponent<GUITrainingController>().processClick(base.gameObject.name);
	}
}
