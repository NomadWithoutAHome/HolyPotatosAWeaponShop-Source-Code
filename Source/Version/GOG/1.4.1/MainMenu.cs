using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private delegate void GUIMethod();

	private Texture2D buttonImage;

	private GUIMethod currentGUIMethod;

	private void Start()
	{
		buttonImage = Resources.Load("Image/inbox-button-claim") as Texture2D;
		currentGUIMethod = mainMenu;
	}

	public void mainMenu()
	{
		if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 3 * 2, Screen.width - 220, 40f), buttonImage))
		{
			currentGUIMethod = OptionsMenu;
		}
	}

	private void OptionsMenu()
	{
		if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 3, Screen.width - 220, 40f), "Main Menu"))
		{
			currentGUIMethod = mainMenu;
		}
	}

	public void OnGUI()
	{
		currentGUIMethod();
	}
}
