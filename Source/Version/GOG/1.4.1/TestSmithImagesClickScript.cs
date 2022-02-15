using UnityEngine;

public class TestSmithImagesClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_TestSmithImages").GetComponent<TestSmithImagesController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_TestSmithImages").GetComponent<TestSmithImagesController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_TestSmithImages").GetComponent<TestSmithImagesController>().processHover(isOver: false, base.gameObject);
		}
	}
}
