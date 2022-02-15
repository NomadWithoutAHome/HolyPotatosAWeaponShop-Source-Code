using System;
using UnityEngine;

[Serializable]
public class KeyShortcut
{
	private string keyShortcutRefID;

	private string function;

	private string button;

	private string category;

	private KeyCode assignedKey;

	private bool canBeChanged;

	public KeyShortcut()
	{
		keyShortcutRefID = string.Empty;
		function = string.Empty;
		button = string.Empty;
		category = string.Empty;
		assignedKey = KeyCode.None;
		canBeChanged = false;
	}

	public KeyShortcut(string aKeyShortcutRefID, string aFunction, string aButton, string aCategory, bool aCanBeChanged)
	{
		keyShortcutRefID = aKeyShortcutRefID;
		function = aFunction;
		button = aButton;
		category = aCategory;
		if (aKeyShortcutRefID != string.Empty && aButton != string.Empty)
		{
			string @string = PlayerPrefs.GetString(aKeyShortcutRefID, aButton);
			assignedKey = (KeyCode)Enum.Parse(typeof(KeyCode), @string, ignoreCase: true);
		}
		canBeChanged = aCanBeChanged;
	}

	public string getKeyShortcutRefID()
	{
		return keyShortcutRefID;
	}

	public string getFunction()
	{
		return function;
	}

	public string getButton()
	{
		return button;
	}

	public string getCategory()
	{
		return category;
	}

	public KeyCode getAssignedKey()
	{
		return assignedKey;
	}

	public void resetToDefault()
	{
		PlayerPrefs.SetString(keyShortcutRefID, button);
		assignedKey = (KeyCode)Enum.Parse(typeof(KeyCode), button, ignoreCase: true);
	}

	public KeyCode getDefaultKey()
	{
		return assignedKey = (KeyCode)Enum.Parse(typeof(KeyCode), button, ignoreCase: true);
	}

	public void setAssignedKey(KeyCode aKeyCode)
	{
		PlayerPrefs.SetString(keyShortcutRefID, aKeyCode.ToString());
		assignedKey = aKeyCode;
	}

	public bool getCanBeChanged()
	{
		return canBeChanged;
	}
}
