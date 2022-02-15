using UnityEngine;

public class MapHUDTooltipScript : MonoBehaviour
{
	private Game game;

	private string tooltipString;

	private TooltipTextScript tooltipScript;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		tooltipScript = GameObject.Find("MapTooltipInfo").GetComponent<TooltipTextScript>();
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			switch (base.gameObject.name)
			{
			case "MoneyFrame":
			{
				tooltipString = game.getGameData().getTextByRefId("gold") + "\n";
				tooltipString += "[s]                         [/s]\n";
				string text = tooltipString;
				tooltipString = text + game.getGameData().getTextByRefId("salaryEvent07") + ": $" + game.getPlayer().getSmithTotalSalary();
				tooltipScript.showText(tooltipString);
				break;
			}
			case "FameFrame":
				tooltipString = game.getGameData().getTextByRefId("shopFame");
				tooltipScript.showText(tooltipString);
				break;
			case "TicketFrame":
				tooltipString = game.getGameData().getTextByRefId("tickets");
				tooltipScript.showText(tooltipString);
				break;
			case "SmithFrame":
				tooltipString = game.getGameData().getTextByRefId("shopCapacity");
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
