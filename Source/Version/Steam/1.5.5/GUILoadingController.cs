using System.Collections;
using UnityEngine;

public class GUILoadingController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private float progress;

	private float maxProgress;

	private void Start()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
	}

	public void processClick(string gameObjectName)
	{
		CommonAPI.debug("click" + gameObjectName);
		if (gameObjectName == null)
		{
		}
	}

	public void startProgess()
	{
		progress = 0f;
		maxProgress = 0f;
		UIProgressBar component = GameObject.Find("LoadingProgressBar").GetComponent<UIProgressBar>();
		component.value = progress;
		StartCoroutine(progressCount());
	}

	public void updateMaxProgress(float aValue)
	{
		maxProgress = aValue;
	}

	private IEnumerator progressCount()
	{
		UIProgressBar loadingProgressBar = GameObject.Find("LoadingProgressBar").GetComponent<UIProgressBar>();
		bool isLoading = true;
		while (isLoading)
		{
			if (progress < maxProgress)
			{
				loadingProgressBar.value = progress;
				progress += 0.1f;
			}
			else if (maxProgress >= 1f && progress >= 1f)
			{
				loadingProgressBar.value = progress;
				startGame();
				isLoading = false;
			}
			yield return new WaitForSeconds(0.001f);
		}
	}

	public void startGame()
	{
		ViewController component = GameObject.Find("ViewController").GetComponent<ViewController>();
		component.closeLoading();
		component.startGame();
	}

	public void finishLoading()
	{
		if (GameObject.Find("Panel_LoadingLanguage") != null)
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().closeLoadRef();
		}
		bool flag = bool.Parse(PlayerPrefs.GetString("FirstTime", "TRUE"));
		if (flag)
		{
			PlayerPrefs.SetString("FirstTime", "FALSE");
		}
		GameObject.Find("ViewController").GetComponent<ViewController>().showStartScreen(flag);
	}
}
