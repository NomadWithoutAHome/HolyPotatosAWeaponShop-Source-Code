using UnityEngine;

public class TooltipScript : MonoBehaviour
{
	private Game game;

	private string tooltipString;

	private TooltipTextScript tooltipScript;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		tooltipScript = GameObject.Find("ShortBubble").GetComponent<TooltipTextScript>();
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			switch (base.gameObject.name)
			{
			case "Tooltip_starch":
			{
				tooltipString = game.getGameData().getTextByRefId("gold") + "\n";
				tooltipString += "[s]                         [/s]\n";
				string text = tooltipString;
				tooltipString = text + game.getGameData().getTextByRefId("salaryEvent07") + ": $" + game.getPlayer().getSmithTotalSalary();
				tooltipScript.showText(tooltipString);
				break;
			}
			case "Tooltip_fame":
				tooltipString = game.getGameData().getTextByRefId("shopFame");
				tooltipScript.showText(tooltipString);
				break;
			case "Tooltip_tickets":
				tooltipString = game.getGameData().getTextByRefId("tickets");
				tooltipScript.showText(tooltipString);
				break;
			case "Tooltip_shopCapacity":
				tooltipString = game.getGameData().getTextByRefId("shopCapacity");
				tooltipScript.showText(tooltipString);
				break;
			case "Weather_icon":
			{
				Weather weather = game.getPlayer().getWeather();
				tooltipString = weather.getWeatherName() + "\n";
				tooltipString += "[s]                         [/s]\n";
				tooltipString += weather.getWeatherText();
				tooltipScript.showText(tooltipString);
				break;
			}
			case "Season_icon":
			{
				Season seasonByMonth = CommonAPI.getSeasonByMonth(game.getPlayer().getSeasonIndex());
				tooltipString = CommonAPI.convertSeasonToString(seasonByMonth) + "\n";
				tooltipString += "[s]                         [/s]\n";
				tooltipString += CommonAPI.getSeasonDesc(seasonByMonth);
				tooltipScript.showText(tooltipString);
				break;
			}
			case "button_record":
				tooltipString = game.getGameData().getTextByRefId("shopProfile01") + "\n";
				tooltipString += "[s]                         [/s]\n";
				tooltipString += game.getGameData().getTextByRefId("shopProfile33");
				tooltipScript.showText(tooltipString);
				break;
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void OnDrag()
	{
		tooltipScript.setInactive();
	}
}
