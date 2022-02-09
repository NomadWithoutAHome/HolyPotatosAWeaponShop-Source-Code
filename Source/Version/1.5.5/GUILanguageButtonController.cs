using System.Globalization;
using UnityEngine;

public class GUILanguageButtonController : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private Game game;

	private GameData gameData;

	private UISprite languageFlag;

	private UILabel languageSelection;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		languageFlag = commonScreenObject.findChild(base.gameObject, "LanguageFlag").GetComponent<UISprite>();
		languageSelection = commonScreenObject.findChild(base.gameObject, "LanguageSelection").GetComponent<UILabel>();
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		string text = CommonAPI.convertLanguageTypeToLanguageString(Constants.LANGUAGE).ToUpper(CultureInfo.InvariantCulture);
		languageFlag.spriteName = "flag_" + text.ToLower(CultureInfo.InvariantCulture);
		languageSelection.text = text;
	}
}
