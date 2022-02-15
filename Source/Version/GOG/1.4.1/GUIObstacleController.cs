using System.Collections.Generic;
using UnityEngine;

public class GUIObstacleController : MonoBehaviour
{
	private Game game;

	private GUIGridController gridController;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private List<Obstacle> obstacleList;

	private Dictionary<string, GameObject> obstacleGameobjectList;

	private RuntimeAnimatorController originalController;

	private GameObject Panel_Obstacles;

	private string obstaclePrefix;

	private List<string> furnitureType;

	private bool isPaused;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		obstacleGameobjectList = new Dictionary<string, GameObject>();
		Panel_Obstacles = GameObject.Find("Panel_Obstacles");
		obstaclePrefix = "Obstacle_";
		furnitureType = new List<string>();
		furnitureType.Add("201");
		furnitureType.Add("301");
		furnitureType.Add("401");
		furnitureType.Add("501");
		furnitureType.Add("601");
		furnitureType.Add("701");
		furnitureType.Add("801");
		furnitureType.Add("901");
		isPaused = false;
	}

	public void createObstacles()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		foreach (GameObject value in obstacleGameobjectList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		obstacleGameobjectList.Clear();
		obstacleList = gameData.getObstacleByShopLevelNoFurniture(player.getShopLevelInt());
		Furniture furniture = null;
		List<Obstacle> list = null;
		foreach (string item in furnitureType)
		{
			furniture = player.getHighestPlayerFurnitureByType(item);
			if (!(furniture.getFurnitureRefId() != string.Empty))
			{
				continue;
			}
			list = gameData.getObstacleByFurnitureRefIDAndLevel(furniture.getFurnitureRefId(), furniture.getFurnitureLevel(), player.getShopLevelInt());
			foreach (Obstacle item2 in list)
			{
				obstacleList.Add(item2);
			}
		}
		foreach (Obstacle obstacle in obstacleList)
		{
			if (obstacle.getFurnitureRefID() == "-1")
			{
				generateObstacle(obstacle);
				continue;
			}
			Furniture ownedFurnitureByRefID = gameData.getOwnedFurnitureByRefID(obstacle.getFurnitureRefID());
			if (ownedFurnitureByRefID.getFurnitureRefId() != string.Empty)
			{
				generateObstacle(obstacle, ownedFurnitureByRefID);
			}
		}
	}

	public void generateObstacle(Obstacle oneObstacle, Furniture oneFurniture = null)
	{
		if (obstacleGameobjectList.ContainsKey(obstaclePrefix + oneObstacle.getRefObstacleID()))
		{
			return;
		}
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Vector3 position = gridController.getPosition((int)oneObstacle.getObstaclePoint().x, (int)oneObstacle.getObstaclePoint().y);
		float num = position.x;
		float num2 = position.z;
		if (oneObstacle.getWidth() > 1)
		{
			num += (float)(oneObstacle.getWidth() - 1) / 2f * 0.73f;
		}
		if (oneObstacle.getHeight() > 1)
		{
			num2 += (float)(oneObstacle.getHeight() - 1) / 2f * 0.74f;
		}
		Vector3 aPosition = new Vector3(num, oneObstacle.getYValue(), num2);
		GameObject gameObject = commonScreenObject.createPrefab(Panel_Obstacles, obstaclePrefix + oneObstacle.getRefObstacleID(), "Prefab/Obstacle/Obstacle", aPosition, Vector3.one, Vector3.zero);
		if (player.getEnchantLocked() && oneObstacle.getFurnitureRefID() == "90101")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageLocked());
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
		}
		gameObject.transform.localRotation = Quaternion.Euler(oneObstacle.getXDegree(), oneObstacle.getYDegree(), 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = oneObstacle.getSortOrder();
		obstacleGameobjectList.Add(gameObject.name, gameObject);
		if (oneFurniture != null && oneFurniture.getFurnitureType() == "301")
		{
			player.setDogBedRefID(oneObstacle.getRefObstacleID());
			gameObject.AddComponent<BoxCollider>();
			gameObject.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 50f);
		}
		if (oneObstacle.getFurnitureRefID() != "-1")
		{
			Furniture furnitureByRefId = gameData.getFurnitureByRefId(oneObstacle.getFurnitureRefID());
			switch (furnitureByRefId.getFurnitureType())
			{
			case "201":
				playObstacleAnim(oneObstacle.getRefObstacleID(), spriteNull: true, string.Empty);
				if (!isPaused)
				{
					audioController.playAirconAudio();
				}
				break;
			case "401":
				playObstacleAnim(oneObstacle.getRefObstacleID(), spriteNull: true, string.Empty);
				if (!isPaused)
				{
					audioController.playFireplaceAudio();
				}
				break;
			}
		}
		handleObstaclePart(gameObject, oneObstacle);
	}

	private void handleObstaclePart(GameObject aObstacle, Obstacle oneObstacle)
	{
		GameObject gameObject = commonScreenObject.findChild(aObstacle, "Obstacle_Anim").gameObject;
		GameObject gameObject2 = commonScreenObject.findChild(aObstacle, "Obstacle_Anim1").gameObject;
		GameObject gameObject3 = commonScreenObject.findChild(aObstacle, "Obstacle_Anim2").gameObject;
		GameObject gameObject4 = commonScreenObject.findChild(aObstacle, "Obstacle_Adjust").gameObject;
		switch (oneObstacle.getRefObstacleID())
		{
		case "10004":
			aObstacle.GetComponent<SpriteRenderer>().sprite = null;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = oneObstacle.getSortOrder();
			gameObject4.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
			gameObject4.transform.localPosition = new Vector3(0f, 0f, -0.18f);
			break;
		case "20010":
		case "30001":
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 1;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl2_table_arm");
			gameObject.transform.localPosition = new Vector3(1.16f, -0.43f, 0f);
			break;
		case "30002":
		case "40001":
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 1;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_design_arm");
			gameObject.transform.localPosition = new Vector3(-1.99f, -0.69f, 0f);
			gameObject2.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 2;
			gameObject2.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_design_gear");
			gameObject2.transform.localPosition = new Vector3(2.7f, -1.02f, 0f);
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 3;
			gameObject3.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_design_lamp");
			gameObject3.transform.localPosition = new Vector3(1.89f, -0.25f, 0f);
			break;
		case "30008":
		case "40005":
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_grinder1_handle");
			gameObject.transform.localPosition = Vector3.zero;
			break;
		case "30022":
		case "40006":
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder - 1;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_grinder2_backhandle");
			gameObject.transform.localPosition = new Vector3(0f, 0f, -0.1f);
			gameObject2.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject2.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_grinder2_fronthandle");
			gameObject2.transform.localPosition = new Vector3(0f, 0f, -0.1f);
			break;
		case "30006":
		case "40003":
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 1;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_anvil_hammer");
			gameObject.transform.localPosition = new Vector3(0.42f, -0.5f, 0f);
			break;
		case "30023":
		case "40004":
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_anvil_pump");
			gameObject.transform.localPosition = new Vector3(-0.07f, 0f, 0f);
			break;
		case "30010":
		case "40007":
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 1;
			gameObject3.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_kiln_fan");
			gameObject3.transform.localPosition = new Vector3(-1.74f, -1.23f, 0f);
			gameObject2.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 2;
			gameObject2.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl3_kiln_chimney");
			gameObject2.transform.localPosition = new Vector3(0.5f, 3.4f, 0f);
			break;
		case "30021":
		case "40002":
			aObstacle.GetComponent<SpriteRenderer>().sprite = null;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject4.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
			gameObject4.transform.localPosition = new Vector3(0f, 0f, -0.0375f);
			break;
		case "40009":
			aObstacle.GetComponent<SpriteRenderer>().sprite = null;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject4.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
			gameObject4.transform.localPosition = new Vector3(0f, 0f, 0.55f);
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 1;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl4_design_lamp");
			gameObject.transform.localPosition = new Vector3(-0.79f, -0.06f, 0f);
			break;
		case "40010":
			aObstacle.GetComponent<SpriteRenderer>().sprite = null;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = oneObstacle.getSortOrder();
			gameObject4.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
			gameObject4.transform.localPosition = new Vector3(0f, 0f, 0.5f);
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl4_anvil_pump");
			gameObject.transform.localPosition = new Vector3(-1.89f, -0.38f, 0f);
			break;
		case "40012":
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder + 5;
			gameObject3.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lvl4_kiln_pump");
			gameObject3.transform.localPosition = new Vector3(-1.11f, -0.22f, 0f);
			break;
		case "40011":
			aObstacle.GetComponent<SpriteRenderer>().sprite = null;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject4.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
			gameObject4.transform.localPosition = new Vector3(0f, 0f, -0.7f);
			break;
		case "40027":
			aObstacle.GetComponent<SpriteRenderer>().sprite = null;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = aObstacle.GetComponent<SpriteRenderer>().sortingOrder;
			gameObject4.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + oneObstacle.getImageUnlocked());
			gameObject4.transform.localPosition = new Vector3(0f, 0f, -0.528f);
			break;
		}
	}

	public void destroyObstacle(Obstacle aObstacle)
	{
		if (obstacleGameobjectList.ContainsKey(obstaclePrefix + aObstacle.getRefObstacleID()))
		{
			GameObject aObj = obstacleGameobjectList[obstaclePrefix + aObstacle.getRefObstacleID()];
			obstacleGameobjectList.Remove(obstaclePrefix + aObstacle.getRefObstacleID());
			commonScreenObject.destroyPrefabImmediate(aObj);
		}
	}

	public GameObject getObstacleObject(string aRefID)
	{
		string key = obstaclePrefix + aRefID;
		if (obstacleGameobjectList.ContainsKey(key))
		{
			return obstacleGameobjectList[key];
		}
		return null;
	}

	public void playObstacleAnim(string aRefID, bool spriteNull = false, string bookshelfUpDown = "")
	{
		string key = obstaclePrefix + aRefID;
		if (!obstacleGameobjectList.ContainsKey(key))
		{
			return;
		}
		GameObject gameObject = obstacleGameobjectList[key];
		Obstacle obstacleByRefID = game.getGameData().getObstacleByRefID(aRefID);
		Animator component = commonScreenObject.findChild(gameObject, "Obstacle_Anim").GetComponent<Animator>();
		if (!(component == null))
		{
			return;
		}
		component = commonScreenObject.findChild(gameObject, "Obstacle_Anim").gameObject.AddComponent<Animator>();
		RuntimeAnimatorController runtimeAnimatorController = (component.runtimeAnimatorController = (runtimeAnimatorController = Resources.Load("Animation/Obstacle/ObstacleAnimator") as RuntimeAnimatorController));
		AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController();
		if (originalController == null)
		{
			originalController = runtimeAnimatorController;
		}
		animatorOverrideController.runtimeAnimatorController = originalController;
		if (bookshelfUpDown == string.Empty)
		{
			animatorOverrideController["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked()) as AnimationClip;
		}
		else
		{
			animatorOverrideController["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + bookshelfUpDown) as AnimationClip;
		}
		if (spriteNull)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = null;
		}
		component.runtimeAnimatorController = animatorOverrideController;
		if (isPaused)
		{
			component.speed = 0f;
		}
		AnimatorOverrideController animatorOverrideController2 = new AnimatorOverrideController();
		AnimatorOverrideController animatorOverrideController3 = new AnimatorOverrideController();
		AnimatorOverrideController animatorOverrideController4 = new AnimatorOverrideController();
		AnimatorOverrideController animatorOverrideController5 = new AnimatorOverrideController();
		GameObject gameObject2 = commonScreenObject.findChild(gameObject, "Obstacle_Adjust").gameObject;
		GameObject gameObject3 = commonScreenObject.findChild(gameObject, "Obstacle_Anim").gameObject;
		GameObject gameObject4 = commonScreenObject.findChild(gameObject, "Obstacle_Anim1").gameObject;
		GameObject gameObject5 = commonScreenObject.findChild(gameObject, "Obstacle_Anim2").gameObject;
		GameObject gameObject6 = commonScreenObject.findChild(gameObject, "Obstacle_Anim3").gameObject;
		switch (aRefID)
		{
		case "10008":
		case "20023":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			if (!isPaused)
			{
				audioController.playDesignStationAudio(1);
				break;
			}
			Animator[] componentsInChildren11 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array11 = componentsInChildren11;
			foreach (Animator animator22 in array11)
			{
				animator22.speed = 0f;
			}
			break;
		}
		case "20010":
		case "30001":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			Animator animator4 = gameObject4.GetComponent<Animator>();
			if (animator4 == null)
			{
				animator4 = gameObject4.AddComponent<Animator>();
			}
			animator4.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part") as AnimationClip;
			animator4.runtimeAnimatorController = animatorOverrideController2;
			if (!isPaused)
			{
				audioController.playDesignStationAudio(2);
				break;
			}
			Animator[] componentsInChildren4 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array4 = componentsInChildren4;
			foreach (Animator animator5 in array4)
			{
				animator5.speed = 0f;
			}
			break;
		}
		case "30002":
		case "40001":
		{
			Animator animator6 = gameObject4.GetComponent<Animator>();
			if (animator6 == null)
			{
				animator6 = gameObject4.AddComponent<Animator>();
			}
			animatorOverrideController2.runtimeAnimatorController = originalController;
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part") as AnimationClip;
			animator6.runtimeAnimatorController = animatorOverrideController2;
			Animator animator7 = gameObject5.GetComponent<Animator>();
			if (animator7 == null)
			{
				animator7 = gameObject5.AddComponent<Animator>();
			}
			animatorOverrideController3.runtimeAnimatorController = originalController;
			animatorOverrideController3["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part2") as AnimationClip;
			animator7.runtimeAnimatorController = animatorOverrideController3;
			if (!isPaused)
			{
				audioController.playDesignStationAudio(3);
				break;
			}
			Animator[] componentsInChildren5 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array5 = componentsInChildren5;
			foreach (Animator animator8 in array5)
			{
				animator8.speed = 0f;
			}
			break;
		}
		case "40009":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			if (!isPaused)
			{
				audioController.playDesignStationAudio(4);
				break;
			}
			Animator[] componentsInChildren10 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array10 = componentsInChildren10;
			foreach (Animator animator21 in array10)
			{
				animator21.speed = 0f;
			}
			break;
		}
		case "10004":
		case "20019":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			if (!isPaused)
			{
				audioController.playCraftStationAudio(1);
				break;
			}
			Animator[] componentsInChildren2 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array2 = componentsInChildren2;
			foreach (Animator animator2 in array2)
			{
				animator2.speed = 0f;
			}
			break;
		}
		case "20004":
		case "30003":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			if (!isPaused)
			{
				audioController.playCraftStationAudio(2);
				break;
			}
			Animator[] componentsInChildren16 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array16 = componentsInChildren16;
			foreach (Animator animator29 in array16)
			{
				animator29.speed = 0f;
			}
			break;
		}
		case "30006":
		case "40003":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			if (!isPaused)
			{
				audioController.playCraftStationAudio(3);
				break;
			}
			Animator[] componentsInChildren14 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array14 = componentsInChildren14;
			foreach (Animator animator26 in array14)
			{
				animator26.speed = 0f;
			}
			break;
		}
		case "40010":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			Animator animator17 = gameObject4.GetComponent<Animator>();
			if (animator17 == null)
			{
				animator17 = gameObject4.AddComponent<Animator>();
			}
			animator17.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part") as AnimationClip;
			animator17.runtimeAnimatorController = animatorOverrideController2;
			Animator animator18 = gameObject5.GetComponent<Animator>();
			if (animator18 == null)
			{
				animator18 = gameObject5.AddComponent<Animator>();
			}
			animator18.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			animatorOverrideController3.runtimeAnimatorController = originalController;
			animatorOverrideController3["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part2") as AnimationClip;
			animator18.runtimeAnimatorController = animatorOverrideController3;
			Animator animator19 = gameObject6.GetComponent<Animator>();
			if (animator19 == null)
			{
				animator19 = gameObject6.AddComponent<Animator>();
			}
			animator19.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			animatorOverrideController4.runtimeAnimatorController = originalController;
			animatorOverrideController4["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part3") as AnimationClip;
			animator19.runtimeAnimatorController = animatorOverrideController4;
			if (!isPaused)
			{
				audioController.playCraftStationAudio(4);
				break;
			}
			Animator[] componentsInChildren9 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array9 = componentsInChildren9;
			foreach (Animator animator20 in array9)
			{
				animator20.speed = 0f;
			}
			break;
		}
		case "10005":
		case "20020":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			if (!isPaused)
			{
				audioController.playPolishStationAudio(1);
				break;
			}
			Animator[] componentsInChildren12 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array12 = componentsInChildren12;
			foreach (Animator animator23 in array12)
			{
				animator23.speed = 0f;
			}
			break;
		}
		case "20009":
		case "30007":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			Animator animator30 = gameObject4.GetComponent<Animator>();
			if (animator30 == null)
			{
				animator30 = gameObject4.AddComponent<Animator>();
			}
			animator30.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part") as AnimationClip;
			animator30.runtimeAnimatorController = animatorOverrideController2;
			if (!isPaused)
			{
				audioController.playPolishStationAudio(2);
				break;
			}
			Animator[] componentsInChildren17 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array17 = componentsInChildren17;
			foreach (Animator animator31 in array17)
			{
				animator31.speed = 0f;
			}
			break;
		}
		case "30022":
		case "40006":
		{
			CommonAPI.debug("here");
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			Animator animator27 = gameObject4.GetComponent<Animator>();
			if (animator27 == null)
			{
				animator27 = gameObject4.gameObject.AddComponent<Animator>();
			}
			animator27.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Part") as AnimationClip;
			animator27.runtimeAnimatorController = animatorOverrideController2;
			if (!isPaused)
			{
				audioController.playPolishStationAudio(3);
				break;
			}
			Animator[] componentsInChildren15 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array15 = componentsInChildren15;
			foreach (Animator animator28 in array15)
			{
				animator28.speed = 0f;
			}
			break;
		}
		case "40011":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			if (!isPaused)
			{
				audioController.playPolishStationAudio(4);
				break;
			}
			Animator[] componentsInChildren7 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array7 = componentsInChildren7;
			foreach (Animator animator12 in array7)
			{
				animator12.speed = 0f;
			}
			break;
		}
		case "40027":
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			break;
		case "10003":
		case "20018":
		case "20003":
		case "30009":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			Animator animator24 = gameObject4.GetComponent<Animator>();
			if (animator24 == null)
			{
				animator24 = gameObject4.AddComponent<Animator>();
			}
			animator24.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			int num9 = Random.Range(1, 4);
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "_smoke" + num9) as AnimationClip;
			animator24.runtimeAnimatorController = animatorOverrideController2;
			if (!isPaused)
			{
				if (aRefID == "10003" || aRefID == "20018")
				{
					audioController.playEnchantStationAudio(1);
				}
				else
				{
					audioController.playEnchantStationAudio(2);
				}
				break;
			}
			Animator[] componentsInChildren13 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array13 = componentsInChildren13;
			foreach (Animator animator25 in array13)
			{
				animator25.speed = 0f;
			}
			break;
		}
		case "30010":
		case "40007":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			Animator animator9 = gameObject4.GetComponent<Animator>();
			if (animator9 == null)
			{
				animator9 = gameObject4.gameObject.AddComponent<Animator>();
			}
			animator9.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			int num = Random.Range(1, 4);
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "_smoke" + num) as AnimationClip;
			animator9.runtimeAnimatorController = animatorOverrideController2;
			Animator animator10 = gameObject5.GetComponent<Animator>();
			if (animator10 == null)
			{
				animator10 = gameObject5.gameObject.AddComponent<Animator>();
			}
			animator10.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			animatorOverrideController3.runtimeAnimatorController = originalController;
			animatorOverrideController3["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Pump") as AnimationClip;
			animator10.runtimeAnimatorController = animatorOverrideController3;
			if (!isPaused)
			{
				audioController.playEnchantStationAudio(3);
				break;
			}
			Animator[] componentsInChildren6 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array6 = componentsInChildren6;
			foreach (Animator animator11 in array6)
			{
				animator11.speed = 0f;
			}
			break;
		}
		case "40012":
		{
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
			Animator animator13 = gameObject4.GetComponent<Animator>();
			if (animator13 == null)
			{
				animator13 = gameObject4.gameObject.AddComponent<Animator>();
			}
			animator13.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
			animatorOverrideController2.runtimeAnimatorController = originalController;
			int num3 = Random.Range(1, 4);
			animatorOverrideController2["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "_smoke" + num3) as AnimationClip;
			animator13.runtimeAnimatorController = animatorOverrideController2;
			Animator animator14 = gameObject5.GetComponent<Animator>();
			if (animator14 == null)
			{
				animator14 = gameObject5.gameObject.AddComponent<Animator>();
			}
			animator14.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			animatorOverrideController3.runtimeAnimatorController = originalController;
			animatorOverrideController3["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Pump") as AnimationClip;
			animator14.runtimeAnimatorController = animatorOverrideController3;
			Animator animator15 = gameObject2.GetComponent<Animator>();
			if (animator15 == null)
			{
				animator15 = gameObject2.gameObject.AddComponent<Animator>();
			}
			animator15.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
			animatorOverrideController4.runtimeAnimatorController = originalController;
			animatorOverrideController4["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Hole") as AnimationClip;
			animator15.runtimeAnimatorController = animatorOverrideController4;
			Animator component2 = gameObject6.GetComponent<Animator>();
			if (component2 == null)
			{
				component2 = gameObject6.gameObject.AddComponent<Animator>();
				component2.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 3;
				animatorOverrideController5.runtimeAnimatorController = originalController;
				animatorOverrideController5["ObstacleDefault"] = Resources.Load("Animation/Obstacle/" + obstacleByRefID.getImageUnlocked() + "Cover") as AnimationClip;
				component2.runtimeAnimatorController = animatorOverrideController5;
			}
			if (!isPaused)
			{
				audioController.playEnchantStationAudio(4);
				break;
			}
			Animator[] componentsInChildren8 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array8 = componentsInChildren8;
			foreach (Animator animator16 in array8)
			{
				animator16.speed = 0f;
			}
			break;
		}
		case "10010":
		case "20013":
		case "30024":
		case "40025":
		{
			component.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			if (spriteNull && bookshelfUpDown == "Down")
			{
				audioController.playPortalAudio();
			}
			Animator[] componentsInChildren3 = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array3 = componentsInChildren3;
			foreach (Animator animator3 in array3)
			{
				animator3.speed = 1f;
			}
			break;
		}
		case "10015":
		case "20017":
		case "30020":
		case "40026":
		{
			component.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			Animator[] componentsInChildren = gameObject.GetComponentsInChildren<Animator>();
			Animator[] array = componentsInChildren;
			foreach (Animator animator in array)
			{
				animator.speed = 1f;
			}
			break;
		}
		default:
			component.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
			break;
		}
	}

	public void stopObstacleAnim(string aRefID, bool enableSprite = false, bool researchInterim = false)
	{
		string key = obstaclePrefix + aRefID;
		if (!obstacleGameobjectList.ContainsKey(key))
		{
			return;
		}
		GameObject gameObject = obstacleGameobjectList[key];
		Obstacle obstacleByRefID = game.getGameData().getObstacleByRefID(aRefID);
		if (enableSprite)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + obstacleByRefID.getImageUnlocked());
		}
		Animator component = commonScreenObject.findChild(gameObject, "Obstacle_Anim").GetComponent<Animator>();
		if (component != null)
		{
			component.StopPlayback();
			component.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/None");
			Object.Destroy(component);
		}
		switch (aRefID)
		{
		case "10008":
		case "20023":
		case "20010":
		case "30001":
		case "30002":
		case "40001":
		case "40009":
			audioController.stopDesignStationAudio(destroy: true);
			break;
		case "10004":
		case "20019":
		case "20004":
		case "30003":
		case "30006":
		case "40003":
		case "40010":
			audioController.stopCraftStationAudio(destroy: true);
			break;
		case "10005":
		case "20020":
		case "20009":
		case "30007":
		case "30022":
		case "40006":
		case "40011":
			audioController.stopPolishStationAudio(destroy: true);
			break;
		case "10003":
		case "20018":
		case "20003":
		case "30009":
		case "30010":
		case "40007":
		case "40012":
			audioController.stopEnchantStationAudio(destroy: true);
			break;
		case "30024":
			if (researchInterim)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lv3_bookshelfDown");
			}
			break;
		case "40025":
			if (researchInterim)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/lv4_bookshelfDown");
			}
			break;
		}
		Animator[] componentsInChildren = gameObject.GetComponentsInChildren<Animator>();
		Animator[] array = componentsInChildren;
		foreach (Animator animator in array)
		{
			animator.StopPlayback();
			animator.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/None");
			Object.Destroy(animator);
		}
		handleObstaclePart(gameObject, obstacleByRefID);
	}

	public void pauseObstacleAnim(string aRefID)
	{
		isPaused = true;
		string key = obstaclePrefix + aRefID;
		if (obstacleGameobjectList.ContainsKey(key))
		{
			GameObject gameObject = obstacleGameobjectList[key];
			Animator[] componentsInChildren = gameObject.GetComponentsInChildren<Animator>();
			switch (aRefID)
			{
			case "10008":
			case "20023":
			case "20010":
			case "30001":
			case "30002":
			case "40001":
			case "40009":
				audioController.stopDesignStationAudio(destroy: false);
				break;
			case "10004":
			case "20019":
			case "20004":
			case "30003":
			case "30006":
			case "40003":
			case "40010":
				audioController.stopCraftStationAudio(destroy: false);
				break;
			case "10005":
			case "20020":
			case "20009":
			case "30007":
			case "30022":
			case "40006":
			case "40011":
				audioController.stopPolishStationAudio(destroy: false);
				break;
			case "10003":
			case "20018":
			case "20003":
			case "30009":
			case "30010":
			case "40007":
			case "40012":
				audioController.stopEnchantStationAudio(destroy: false);
				break;
			}
			Animator[] array = componentsInChildren;
			foreach (Animator animator in array)
			{
				animator.speed = 0f;
			}
		}
	}

	public void resumeObstacleAnim(string aRefID)
	{
		isPaused = false;
		string key = obstaclePrefix + aRefID;
		if (obstacleGameobjectList.ContainsKey(key))
		{
			GameObject gameObject = obstacleGameobjectList[key];
			Animator[] componentsInChildren = gameObject.GetComponentsInChildren<Animator>();
			switch (aRefID)
			{
			case "10008":
			case "20023":
				audioController.resumeDesignStationAudio(1);
				break;
			case "20010":
			case "30001":
				audioController.resumeDesignStationAudio(2);
				break;
			case "30002":
			case "40001":
				audioController.resumeDesignStationAudio(3);
				break;
			case "40009":
				audioController.resumeDesignStationAudio(4);
				break;
			case "10004":
			case "20019":
				audioController.resumeCraftStationAudio(1);
				break;
			case "20004":
			case "30003":
				audioController.resumeCraftStationAudio(2);
				break;
			case "30006":
			case "40003":
				audioController.resumeCraftStationAudio(3);
				break;
			case "40010":
				audioController.resumeCraftStationAudio(4);
				break;
			case "10005":
			case "20020":
				audioController.resumePolishStationAudio(1);
				break;
			case "20009":
			case "30007":
				audioController.resumePolishStationAudio(2);
				break;
			case "30022":
			case "40006":
				audioController.resumePolishStationAudio(3);
				break;
			case "40011":
				audioController.resumePolishStationAudio(4);
				break;
			case "10003":
			case "20018":
				audioController.resumeEnchantStationAudio(1);
				break;
			case "20003":
			case "30009":
				audioController.resumeEnchantStationAudio(2);
				break;
			case "30010":
			case "40007":
				audioController.resumeEnchantStationAudio(3);
				break;
			case "40012":
				audioController.resumeEnchantStationAudio(4);
				break;
			}
			Animator[] array = componentsInChildren;
			foreach (Animator animator in array)
			{
				animator.speed = 1f;
			}
		}
	}

	public void resumeOtherObstacleAnim()
	{
		GameData gameData = game.getGameData();
		foreach (string key in obstacleGameobjectList.Keys)
		{
			string[] array = key.Split('_');
			Obstacle obstacleByRefID = gameData.getObstacleByRefID(array[1]);
			GameObject gameObject = null;
			Animator[] array2 = null;
			if (!(obstacleByRefID.getFurnitureRefID() != "-1"))
			{
				continue;
			}
			Furniture furnitureByRefId = gameData.getFurnitureByRefId(obstacleByRefID.getFurnitureRefID());
			switch (furnitureByRefId.getFurnitureType())
			{
			case "201":
			{
				audioController.resumeAirconAudio();
				gameObject = obstacleGameobjectList[key];
				array2 = gameObject.GetComponentsInChildren<Animator>();
				Animator[] array4 = array2;
				foreach (Animator animator2 in array4)
				{
					animator2.speed = 1f;
				}
				break;
			}
			case "401":
			{
				audioController.resumeFireplaceAudio();
				gameObject = obstacleGameobjectList[key];
				array2 = gameObject.GetComponentsInChildren<Animator>();
				Animator[] array3 = array2;
				foreach (Animator animator in array3)
				{
					animator.speed = 1f;
				}
				break;
			}
			}
		}
	}

	public void pauseOtherObstacleAnim()
	{
		GameData gameData = game.getGameData();
		foreach (string key in obstacleGameobjectList.Keys)
		{
			string[] array = key.Split('_');
			Obstacle obstacleByRefID = gameData.getObstacleByRefID(array[1]);
			GameObject gameObject = null;
			Animator[] array2 = null;
			if (!(obstacleByRefID.getFurnitureRefID() != "-1"))
			{
				continue;
			}
			Furniture furnitureByRefId = gameData.getFurnitureByRefId(obstacleByRefID.getFurnitureRefID());
			switch (furnitureByRefId.getFurnitureType())
			{
			case "201":
			{
				audioController.stopAirconAudio(destroy: false);
				gameObject = obstacleGameobjectList[key];
				array2 = gameObject.GetComponentsInChildren<Animator>();
				Animator[] array4 = array2;
				foreach (Animator animator2 in array4)
				{
					animator2.speed = 0f;
				}
				break;
			}
			case "401":
			{
				audioController.stopFireplaceAudio(destroy: false);
				gameObject = obstacleGameobjectList[key];
				array2 = gameObject.GetComponentsInChildren<Animator>();
				Animator[] array3 = array2;
				foreach (Animator animator in array3)
				{
					animator.speed = 0f;
				}
				break;
			}
			}
		}
	}

	public void changeSprite(string obstacleRefID, bool locked)
	{
		if (obstacleGameobjectList.ContainsKey(obstaclePrefix + obstacleRefID))
		{
			GameData gameData = game.getGameData();
			GameObject gameObject = obstacleGameobjectList[obstaclePrefix + obstacleRefID];
			Obstacle obstacleByRefID = gameData.getObstacleByRefID(obstacleRefID);
			string empty = string.Empty;
			empty = ((!locked) ? obstacleByRefID.getImageUnlocked() : obstacleByRefID.getImageLocked());
			gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + empty);
		}
	}

	public void disableAllCollider(bool exceptDogBed = false)
	{
		foreach (GameObject value in obstacleGameobjectList.Values)
		{
			if (!exceptDogBed || (exceptDogBed && value.name != obstaclePrefix + game.getPlayer().getDogBedRefID()))
			{
				BoxCollider component = value.GetComponent<BoxCollider>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
		}
	}

	public void enableAllCollider()
	{
		foreach (GameObject value in obstacleGameobjectList.Values)
		{
			BoxCollider component = value.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
	}

	public void changeDogBedLayer(bool bringUp)
	{
		string empty = string.Empty;
		empty = ((!bringUp) ? "Character" : "Blackmask");
		string dogBedRefID = game.getPlayer().getDogBedRefID();
		if (obstacleGameobjectList.ContainsKey(obstaclePrefix + dogBedRefID))
		{
			GameObject gameObject = obstacleGameobjectList[obstaclePrefix + dogBedRefID];
			SpriteRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array = componentsInChildren;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				spriteRenderer.sortingLayerName = empty;
			}
		}
	}
}
