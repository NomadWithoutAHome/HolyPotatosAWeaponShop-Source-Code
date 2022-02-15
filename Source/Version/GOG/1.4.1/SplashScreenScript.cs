using System.Collections;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private UITexture splashTex;

	private TweenAlpha tweenA;

	private bool escapePressed;

	private void Start()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		splashTex = base.gameObject.GetComponent<UITexture>();
		tweenA = GetComponent<TweenAlpha>();
		escapePressed = false;
		UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		float num = (float)uIRoot.activeHeight / (float)Screen.height;
		float num2 = 1.6f;
		int num3 = (int)Mathf.Ceil((float)Screen.height * num);
		int width = (int)Mathf.Ceil((float)num3 * num2);
		splashTex.width = width;
		splashTex.height = num3;
		GameObject.Find("AudioController").GetComponent<AudioController>().switchBGM("SILENT");
		StartCoroutine(loadStartScreen());
	}

	private void Update()
	{
		if (!escapePressed && Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameObject.Find("DaedalicSplashVideo") != null)
			{
				GameObject.Find("DaedalicSplashVideo").GetComponent<VideoController>().stopVideo();
			}
			if (GameObject.Find("DaylightSplashVideo") != null)
			{
				GameObject.Find("DaylightSplashVideo").GetComponent<VideoController>().stopVideo();
			}
			escapePressed = true;
			StopAllCoroutines();
			viewController.startGame();
			StartCoroutine(closeSplashScreen());
		}
	}

	private IEnumerator closeSplashScreen()
	{
		yield return new WaitForSeconds(1f);
		viewController.closeSplashScreen();
		GameObject.Find("AudioController").GetComponent<AudioController>().switchBGM("startmenu");
	}

	private IEnumerator showStartScreen()
	{
		viewController.startGame();
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(closeSplashScreen());
	}

	private IEnumerator loadStartScreen()
	{
		yield return new WaitForSeconds(1f);
		commonScreenObject.createPrefab(GameObject.Find("Panel_SplashScreen").gameObject, "DaedalicSplashVideo", "Prefab/Splash/SplashVideo", Vector3.zero, Vector3.one, Vector3.zero);
		yield return new WaitForSeconds(15f);
		if (GameObject.Find("DaedalicSplashVideo") != null)
		{
			commonScreenObject.destroyPrefabImmediate(GameObject.Find("DaedalicSplashVideo").gameObject);
		}
		yield return new WaitForSeconds(0.5f);
		commonScreenObject.createPrefab(GameObject.Find("Panel_SplashScreen").gameObject, "DaylightSplashVideo", "Prefab/Splash/DaylightSplashVideo", Vector3.zero, Vector3.one, Vector3.zero);
		yield return new WaitForSeconds(10f);
		commonScreenObject.tweenAlpha(GameObject.Find("ScreenBlackCover").GetComponent<TweenAlpha>(), 0f, 1f, 0.75f, null, string.Empty);
		yield return new WaitForSeconds(1f);
		if (GameObject.Find("DaylightSplashVideo") != null)
		{
			commonScreenObject.destroyPrefabImmediate(GameObject.Find("DaylightSplashVideo").gameObject);
		}
		escapePressed = true;
		viewController.startGame();
		StartCoroutine(closeSplashScreen());
	}
}
