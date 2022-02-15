using UnityEngine;

public class TestHeroAnimsClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_TestHeroAnims").GetComponent<TestHeroAnimsController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_TestHeroAnims").GetComponent<TestHeroAnimsController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_TestHeroAnims").GetComponent<TestHeroAnimsController>().processHover(isOver: false, base.gameObject);
		}
	}
}
