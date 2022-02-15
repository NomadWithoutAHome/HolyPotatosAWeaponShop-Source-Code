using UnityEngine;

public class GUIProgressHUDController : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private UIGrid grid_topRight;

	private GameObject forgeProgressObject;

	private GameObject questProgressObject0;

	private GameObject questProgressObject1;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		grid_topRight = GetComponent<UIGrid>();
		forgeProgressObject = null;
		questProgressObject0 = null;
		questProgressObject1 = null;
	}

	public void setForgeProgressObject()
	{
		if (forgeProgressObject == null)
		{
			forgeProgressObject = commonScreenObject.createPrefab(base.gameObject, "Panel_ForgeProgress_0", "Prefab/NewHUD/Panel_ForgeProgress", Vector3.zero, Vector3.one, Vector3.zero);
			forgeProgressObject.GetComponent<GUIForgingProgressNewController>().setReference();
		}
		grid_topRight.Reposition();
	}

	public void refreshStats()
	{
		if (forgeProgressObject != null)
		{
			forgeProgressObject.GetComponent<GUIForgingProgressNewController>().refreshStats();
		}
	}

	public void destroyForgeProgressObject()
	{
		commonScreenObject.destroyPrefabImmediate(forgeProgressObject);
		forgeProgressObject = null;
		grid_topRight.Reposition();
	}

	public void refreshQuestStats()
	{
		if (questProgressObject0 != null)
		{
			questProgressObject0.GetComponent<GUIForgingProgressNewController>().refreshQuestStats();
		}
		if (questProgressObject1 != null)
		{
			questProgressObject1.GetComponent<GUIForgingProgressNewController>().refreshQuestStats();
		}
	}

	public void setQuestProgressObject()
	{
		if (forgeProgressObject != null)
		{
			if (questProgressObject0 == null)
			{
				questProgressObject0 = forgeProgressObject;
				forgeProgressObject = null;
				questProgressObject0.name = "Panel_ForgeProgress_1";
				questProgressObject0.GetComponent<GUIForgingProgressNewController>().setQuest();
			}
			else
			{
				questProgressObject1 = forgeProgressObject;
				forgeProgressObject = null;
				questProgressObject1.name = "Panel_ForgeProgress_2";
				questProgressObject1.GetComponent<GUIForgingProgressNewController>().setQuest();
			}
		}
		grid_topRight.Reposition();
	}

	public void destroyQuestProgressObject(Project aProject)
	{
		if (questProgressObject0 != null && questProgressObject0.GetComponent<GUIForgingProgressNewController>().getProjectInfo().getProjectId() == aProject.getProjectId())
		{
			commonScreenObject.destroyPrefabImmediate(questProgressObject0);
			CommonAPI.debug(" aProject.getProjectId(): " + aProject.getProjectId());
			questProgressObject0 = null;
		}
		if (questProgressObject1 != null && questProgressObject1.GetComponent<GUIForgingProgressNewController>().getProjectInfo().getProjectId() == aProject.getProjectId())
		{
			commonScreenObject.destroyPrefabImmediate(questProgressObject1);
			CommonAPI.debug(" aProject.getProjectId(): " + aProject.getProjectId());
			questProgressObject1 = null;
		}
		grid_topRight.Reposition();
	}
}
