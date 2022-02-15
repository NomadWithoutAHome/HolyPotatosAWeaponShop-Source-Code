using System.Globalization;
using UnityEngine;

public class StatNameTooltipScript : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private TooltipTextScript tooltipScript;

	private void OnHover(bool isOver)
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
			gameData = game.getGameData();
		}
		if (tooltipScript == null)
		{
			tooltipScript = GameObject.Find("ShortBubble").GetComponent<TooltipTextScript>();
		}
		if (isOver)
		{
			string text = base.gameObject.name.ToLower(CultureInfo.InvariantCulture);
			if (text.Contains("atk"))
			{
				tooltipScript.showText(gameData.getTextByRefId("smithStatsShort02"));
			}
			else if (text.Contains("spd"))
			{
				tooltipScript.showText(gameData.getTextByRefId("smithStatsShort03"));
			}
			else if (text.Contains("acc"))
			{
				tooltipScript.showText(gameData.getTextByRefId("smithStatsShort04"));
			}
			else if (text.Contains("mag"))
			{
				tooltipScript.showText(gameData.getTextByRefId("smithStatsShort05"));
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void OnDrag()
	{
		if (tooltipScript == null)
		{
			tooltipScript = GameObject.Find("ShortBubble").GetComponent<TooltipTextScript>();
		}
		tooltipScript.setInactive();
	}
}
