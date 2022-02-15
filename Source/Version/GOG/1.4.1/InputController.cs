using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private string userInput;

	private void Start()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		userInput = string.Empty;
	}

	public void handleMenuInput(GameState gameState)
	{
		int num = convertInputToInt();
	}

	public int convertInputToInt()
	{
		int result = -1;
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
		{
			result = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
		{
			result = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
		{
			result = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
		{
			result = 4;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
		{
			result = 5;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
		{
			result = 6;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
		{
			result = 7;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
		{
			result = 8;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
		{
			result = 9;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
		{
			result = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			result = 99;
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			result = 98;
		}
		else if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			result = 101;
		}
		else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			result = 102;
		}
		else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
			result = 200;
		}
		else if (Input.GetKeyDown(KeyCode.Print) || Input.GetKeyDown(KeyCode.SysReq))
		{
			Application.CaptureScreenshot("Screenshot" + DateTime.Now);
		}
		return result;
	}

	public string getLongInput(string longInputText)
	{
		string empty = string.Empty;
		string inputString = Input.inputString;
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
			empty = userInput;
			userInput = string.Empty;
		}
		else if (Input.GetKeyDown(KeyCode.Backspace) && userInput.Length > 0)
		{
			userInput = userInput.Remove(userInput.Length - 1, 1);
		}
		else if (checkNameChar(inputString))
		{
			userInput += inputString;
		}
		return empty;
	}

	public bool checkNameChar(string input)
	{
		if (input.Length > 0)
		{
			char c = input.ToCharArray(0, 1)[0];
			if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || c == '_' || c == '-' || c == '@' || c == '.' || c == ' ' || c == '!' || c == '?' || c == '\'')
			{
				return true;
			}
		}
		return false;
	}
}
