using UnityEngine;

public class LoadingScript : MonoBehaviour
{
	private UISprite blackMask;

	private bool loadingFromBlack;

	private bool loadingToBlack;

	private LoadingScript sceneLoadScript;

	private string postHandle;

	private void Awake()
	{
		blackMask = base.gameObject.GetComponent<UISprite>();
		loadingFromBlack = false;
		loadingToBlack = false;
		if (base.gameObject.name == "LoadingMaskMap")
		{
			sceneLoadScript = GameObject.Find("LoadingMask").GetComponent<LoadingScript>();
		}
		else
		{
			sceneLoadScript = null;
			setToBlack();
		}
		postHandle = string.Empty;
	}

	private void Update()
	{
		if (loadingFromBlack)
		{
			if (blackMask.fillAmount > 0f)
			{
				blackMask.fillAmount -= 0.05f;
			}
			else if (blackMask.fillAmount == 0f)
			{
				loadingFromBlack = false;
				base.gameObject.GetComponent<BoxCollider>().enabled = false;
				handleExtra();
			}
		}
		if (loadingToBlack)
		{
			if (blackMask.fillAmount < 1f)
			{
				blackMask.fillAmount += 0.05f;
			}
			else if (blackMask.fillAmount == 1f)
			{
				loadingToBlack = false;
				base.gameObject.GetComponent<BoxCollider>().enabled = true;
				handleExtra();
			}
		}
	}

	private void handleExtra()
	{
		switch (postHandle)
		{
		case "BACKWORKSHOP":
			sceneLoadScript.setToBlack();
			GameObject.Find("GUICameraController").GetComponent<GUICameraController>().changeSceneCamera();
			setToTransparent();
			sceneLoadScript.startLoadingFromBlack("NEXTREGION");
			break;
		case "NEXTREGION":
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().resumeEverything();
			GameObject gameObject = GameObject.Find("Game");
			if (!(gameObject != null))
			{
				break;
			}
			Game component = gameObject.GetComponent<Game>();
			if (component.getPlayer().getAreaRegion() == 2)
			{
				GameObject gameObject2 = GameObject.Find("ShopMenuController");
				if (gameObject2 != null)
				{
					gameObject2.GetComponent<ShopMenuController>().tryStartTutorial("REGION_2");
				}
				GameObject gameObject3 = GameObject.Find("ShopViewController");
				if (gameObject3 != null)
				{
					gameObject3.GetComponent<ShopViewController>().showTimeStatus();
				}
			}
			break;
		}
		}
		postHandle = string.Empty;
	}

	public bool checkIsLoadingMaskVisible()
	{
		if (blackMask.fillAmount > 0f)
		{
			return true;
		}
		return false;
	}

	public void startLoadingFromBlack(string aHandle = "")
	{
		blackMask.fillAmount = 1f;
		loadingFromBlack = true;
		postHandle = aHandle;
	}

	public void startLoadingToBlack(string aHandle = "")
	{
		blackMask.fillAmount = 0f;
		loadingToBlack = true;
		postHandle = aHandle;
	}

	public void setToBlack()
	{
		blackMask.fillAmount = 1f;
		base.gameObject.GetComponent<BoxCollider>().enabled = true;
	}

	public void setToTransparent()
	{
		blackMask.fillAmount = 0f;
		base.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
}
