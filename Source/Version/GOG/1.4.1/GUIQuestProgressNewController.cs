using System.Collections;
using UnityEngine;

public class GUIQuestProgressNewController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private UILabel questTitle;

	private GameObject heroBubbleText;

	private UILabel heroBubbleLabel;

	private UITexture heroImg;

	private UILabel heroNameLabel;

	private UITexture weaponImg;

	private UILabel weaponNameLabel;

	private UISlider questProgressBar;

	private UILabel questPercentageLabel;

	private int currentPerc;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		questTitle = commonScreenObject.findChild(base.gameObject, "QuestTitle").GetComponent<UILabel>();
		heroBubbleText = commonScreenObject.findChild(base.gameObject, "HeroFrame/HeroBubbleText").gameObject;
		heroBubbleLabel = commonScreenObject.findChild(heroBubbleText, "HeroBubbleLabel").GetComponent<UILabel>();
		heroImg = commonScreenObject.findChild(base.gameObject, "HeroFrame/HeroImg").GetComponent<UITexture>();
		heroNameLabel = commonScreenObject.findChild(base.gameObject, "HeroFrame/HeroNameLabel").GetComponent<UILabel>();
		weaponImg = commonScreenObject.findChild(base.gameObject, "WeaponFrame/WeaponImg").GetComponent<UITexture>();
		weaponNameLabel = commonScreenObject.findChild(base.gameObject, "WeaponFrame/WeaponNameLabel").GetComponent<UILabel>();
		questProgressBar = commonScreenObject.findChild(base.gameObject, "QuestProgressBar").GetComponent<UISlider>();
		questPercentageLabel = commonScreenObject.findChild(base.gameObject, "QuestProgressBar/QuestProgressThumb/QuestPercentageBubble/QuestPercentageLabel").GetComponent<UILabel>();
		currentPerc = 0;
		setReference();
		StartCoroutine("startTest");
	}

	public void processClick(string gameObjectName)
	{
	}

	public void showBubbleText(string aText)
	{
		heroBubbleLabel.text = aText;
		commonScreenObject.tweenScale(heroBubbleText.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 2f, null, string.Empty);
	}

	public void setPercentage(int aValue)
	{
		questPercentageLabel.text = aValue + "%";
		questProgressBar.value = (float)aValue / 100f;
	}

	private IEnumerator startTest()
	{
		while (currentPerc <= 100)
		{
			yield return new WaitForSeconds(0.1f);
			currentPerc++;
			setPercentage(currentPerc);
			if (currentPerc == 25 || currentPerc == 50 || currentPerc == 75)
			{
				showBubbleText("it's now" + currentPerc + "%");
			}
			if (currentPerc == 100)
			{
				showBubbleText("it's now" + currentPerc + "%");
				StopCoroutine("startTest");
			}
		}
	}

	public void setReference()
	{
		heroBubbleText.transform.localScale = Vector3.zero;
	}
}
