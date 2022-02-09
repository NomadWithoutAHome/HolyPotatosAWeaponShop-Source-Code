using UnityEngine;

public class BlackmaskPopupClickScript : MonoBehaviour
{
	private ViewController viewController;

	private void Awake()
	{
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
	}

	private void OnClick()
	{
		if (GameObject.Find("Panel_MainMenu") != null)
		{
			viewController.closeMainMenu(resume: true);
		}
		if (GameObject.Find("Panel_Tier2Menu") != null)
		{
			viewController.closeTier2Menu(hide: true);
		}
		if (GameObject.Find("Panel_SmithManagePopup") != null)
		{
			viewController.closeSmithManagePopupNew();
		}
	}
}
