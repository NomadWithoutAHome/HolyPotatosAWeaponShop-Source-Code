using UnityEngine;

public class DogTrainingClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_DogTraining").GetComponent<GUIDogTrainingController>().processClick(base.gameObject.name);
	}
}
