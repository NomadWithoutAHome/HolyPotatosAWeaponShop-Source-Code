using System.Collections.Generic;
using SmoothMoves;
using UnityEngine;

public class TestHeroAnimsController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GameObject scaler;

	private UILabel nameLabel;

	private List<Hero> heroList;

	private int displayIndex;

	private GameObject heroObj;

	private Shader animShader;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		heroList = new List<Hero>();
		displayIndex = 0;
		heroObj = null;
		animShader = Resources.Load("Custom Shader/Alpha Blended - QuestSelect Hero") as Shader;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "HeroAnim_right":
			navigateHeroList(1);
			break;
		case "HeroAnim_left":
			navigateHeroList(-1);
			break;
		case "Close_button":
			viewController.closeHeroAnimTest();
			break;
		}
	}

	public void processHover(bool isOver, GameObject hoverObj)
	{
		string text = hoverObj.name;
		CommonAPI.debug("processHover " + text);
		if (scaler != null)
		{
			if (isOver && text == "PlayAnim_inter")
			{
				commonScreenObject.playAnimationImmediate(scaler.GetComponentInChildren<BoneAnimation>(), "inter");
				commonScreenObject.queueAnimation(scaler.GetComponentInChildren<BoneAnimation>(), "idle");
			}
			else
			{
				commonScreenObject.playAnimationImmediate(scaler.GetComponentInChildren<BoneAnimation>(), "idle");
			}
		}
	}

	public void setReference()
	{
		heroList = game.getGameData().getHeroList(string.Empty);
		displayIndex = 0;
		scaler = commonScreenObject.findChild(base.gameObject, "HeroAnim_scaler").gameObject;
		nameLabel = commonScreenObject.findChild(base.gameObject, "HeroName_label").GetComponent<UILabel>();
		showHero();
	}

	private void navigateHeroList(int aIndexAdd)
	{
		displayIndex += aIndexAdd;
		if (displayIndex < 0)
		{
			displayIndex = heroList.Count - 1;
		}
		if (displayIndex >= heroList.Count)
		{
			displayIndex = 0;
		}
		showHero();
	}

	private void showHero()
	{
		commonScreenObject.destroyPrefabImmediate(heroObj);
		Hero hero = heroList[displayIndex];
		nameLabel.text = hero.getHeroName() + "\n" + hero.getJobClassName() + "\n" + hero.getImage() + "\n" + (displayIndex + 1) + "/" + heroList.Count;
		heroObj = commonScreenObject.createPrefab(scaler, hero.getImage(), "Animation/Hero/" + hero.getImage() + "/" + hero.getImage() + "_animObj", Vector3.zero, Vector3.one, Vector3.zero);
		heroObj.GetComponentInChildren<BoneAnimation>().mMaterials[0].shader = animShader;
	}
}
