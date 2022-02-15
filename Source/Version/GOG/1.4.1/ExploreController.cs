using System.Collections.Generic;
using UnityEngine;

public class ExploreController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private GUISmithListMenuController smithListMenuController;

	private void Start()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
	}

	public void timePassOnExplore(int timeElapsed)
	{
		List<Smith> outOfShopSmithList = game.getPlayer().getOutOfShopSmithList();
		GameObject gameObject = GameObject.Find("Panel_Explore");
		int num = 0;
		foreach (Smith item in outOfShopSmithList)
		{
			if (item.decreaseActionDuration(timeElapsed))
			{
				switch (item.getExploreState())
				{
				case SmithExploreState.SmithExploreStateTravelToExplore:
					item.nextExploreState();
					break;
				case SmithExploreState.SmithExploreStateExplore:
				{
					Area exploreArea4 = item.getExploreArea();
					item.nextExploreState();
					break;
				}
				case SmithExploreState.SmithExploreStateExploreTravelHome:
				{
					List<string> list7 = new List<string>();
					list7.Add("[smithName]");
					list7.Add("[areaName]");
					List<string> list8 = new List<string>();
					list8.Add(item.getSmithName());
					list8.Add(item.getExploreArea().getAreaName());
					string textByRefIdWithDynTextList4 = game.getGameData().getTextByRefIdWithDynTextList("smithActionNotice01", list7, list8);
					showSmithNotice("Image/Smith/Portraits/" + item.getImage(), textByRefIdWithDynTextList4, item.getSmithRefId());
					addSmithReturnWhetsapp(item, "whetsappSmithReturnExplore");
					item.nextExploreState();
					if (gameObject != null)
					{
						gameObject.GetComponent<GUIExploreController>().enableClick(num);
					}
					break;
				}
				case SmithExploreState.SmithExploreStateTravelToBuyMaterial:
					item.nextExploreState();
					break;
				case SmithExploreState.SmithExploreStateBuyMaterial:
				{
					Area exploreArea = item.getExploreArea();
					item.nextExploreState();
					break;
				}
				case SmithExploreState.SmithExploreStateBuyMaterialTravelHome:
				{
					List<string> list11 = new List<string>();
					list11.Add("[smithName]");
					list11.Add("[areaName]");
					List<string> list12 = new List<string>();
					list12.Add(item.getSmithName());
					list12.Add(item.getExploreArea().getAreaName());
					string textByRefIdWithDynTextList6 = game.getGameData().getTextByRefIdWithDynTextList("smithActionNotice02", list11, list12);
					showSmithNotice("Image/Smith/Portraits/" + item.getImage(), textByRefIdWithDynTextList6, item.getSmithRefId());
					addSmithReturnWhetsapp(item, "whetsappSmithReturnBuy");
					item.nextExploreState();
					if (gameObject != null)
					{
						gameObject.GetComponent<GUIExploreController>().enableClick(num);
					}
					break;
				}
				case SmithExploreState.SmithExploreStateTravelToSellWeapon:
					item.nextExploreState();
					break;
				case SmithExploreState.SmithExploreStateSellWeapon:
				{
					List<string> list9 = new List<string>();
					list9.Add("[smithName]");
					list9.Add("[areaName]");
					List<string> list10 = new List<string>();
					list10.Add(item.getSmithName());
					list10.Add(item.getExploreArea().getAreaName());
					string textByRefIdWithDynTextList5 = game.getGameData().getTextByRefIdWithDynTextList("smithActionNotice03", list9, list10);
					showSmithNotice("Image/Smith/Portraits/" + item.getImage(), textByRefIdWithDynTextList5, item.getSmithRefId());
					addSmithReturnWhetsapp(item, "whetsappSmithOffers");
					item.nextExploreState();
					if (gameObject != null)
					{
						gameObject.GetComponent<GUIExploreController>().enableClick(num);
					}
					break;
				}
				case SmithExploreState.SmithExploreStateSellWeaponTravelHome:
				{
					List<string> list5 = new List<string>();
					list5.Add("[smithName]");
					list5.Add("[areaName]");
					List<string> list6 = new List<string>();
					list6.Add(item.getSmithName());
					list6.Add(item.getExploreArea().getAreaName());
					string textByRefIdWithDynTextList3 = game.getGameData().getTextByRefIdWithDynTextList("smithActionNotice04", list5, list6);
					showSmithNotice("Image/Smith/Portraits/" + item.getImage(), textByRefIdWithDynTextList3, item.getSmithRefId());
					addSmithReturnWhetsapp(item, "whetsappSmithReturnSell");
					item.nextExploreState();
					if (gameObject != null)
					{
						gameObject.GetComponent<GUIExploreController>().enableClick(num);
					}
					break;
				}
				case SmithExploreState.SmithExploreStateTravelToVacation:
					item.nextExploreState();
					break;
				case SmithExploreState.SmithExploreStateVacation:
				{
					Area exploreArea3 = item.getExploreArea();
					item.nextExploreState();
					break;
				}
				case SmithExploreState.SmithExploreStateVacationTravelHome:
				{
					List<string> list3 = new List<string>();
					list3.Add("[smithName]");
					list3.Add("[areaName]");
					List<string> list4 = new List<string>();
					list4.Add(item.getSmithName());
					list4.Add(item.getExploreArea().getAreaName());
					string textByRefIdWithDynTextList2 = game.getGameData().getTextByRefIdWithDynTextList("smithActionNotice05", list3, list4);
					showSmithNotice("Image/Smith/Portraits/" + item.getImage(), textByRefIdWithDynTextList2, item.getSmithRefId());
					addSmithReturnWhetsapp(item, "whetsappSmithReturnVacation");
					item.nextExploreState();
					if (gameObject != null)
					{
						gameObject.GetComponent<GUIExploreController>().enableClick(num);
					}
					break;
				}
				case SmithExploreState.SmithExploreStateTravelToTraining:
					item.nextExploreState();
					break;
				case SmithExploreState.SmithExploreStateTraining:
				{
					Area exploreArea2 = item.getExploreArea();
					item.nextExploreState();
					break;
				}
				case SmithExploreState.SmithExploreStateTrainingTravelHome:
				{
					List<string> list = new List<string>();
					list.Add("[smithName]");
					list.Add("[areaName]");
					List<string> list2 = new List<string>();
					list2.Add(item.getSmithName());
					list2.Add(item.getExploreArea().getAreaName());
					string textByRefIdWithDynTextList = game.getGameData().getTextByRefIdWithDynTextList("smithActionNotice06", list, list2);
					showSmithNotice("Image/Smith/Portraits/" + item.getImage(), textByRefIdWithDynTextList, item.getSmithRefId());
					addSmithReturnWhetsapp(item, "whetsappSmithReturnTraining");
					item.nextExploreState();
					if (gameObject != null)
					{
						gameObject.GetComponent<GUIExploreController>().enableClick(num);
					}
					break;
				}
				}
			}
			num++;
		}
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIExploreController>().updateDuration();
		}
	}

	private void addSmithReturnWhetsapp(Smith smith, string randomSetId)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText(randomSetId, "[areaName]", smith.getExploreArea().getAreaName());
		gameData.addNewWhetsappMsg(smith.getSmithName(), randomTextBySetRefIdWithDynText, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
	}

	private void showSmithNotice(string image, string text, string smithRefId)
	{
		if (smithListMenuController == null)
		{
			smithListMenuController = GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>();
		}
		smithListMenuController.addSmithActionNotification(image, text, smithRefId);
	}
}
