using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITextureSequencePopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private UITexture texture;

	private UITexture prevTexture;

	private TweenAlpha textureAlphaTween;

	private List<Hashtable> textureList;

	private int displayIndex;

	private bool allowClick;

	private bool slideTimerActive;

	private int slideTimerCount;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		texture = commonScreenObject.findChild(base.gameObject, "TextureSequence_texture").GetComponent<UITexture>();
		prevTexture = commonScreenObject.findChild(base.gameObject, "TextureSequence_prevTexture").GetComponent<UITexture>();
		textureAlphaTween = texture.GetComponent<TweenAlpha>();
		textureList = new List<Hashtable>();
		displayIndex = 0;
		allowClick = true;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "TextureSequence_texture":
			if (allowClick)
			{
				nextTexture();
			}
			break;
		case "Continue_button":
			StartCoroutine(endLastSlide(backToStart: false));
			break;
		case "BackToStart_button":
			StartCoroutine(endLastSlide(backToStart: true));
			break;
		}
	}

	public void showCurrentTexture()
	{
		Hashtable hashtable = textureList[displayIndex];
		if (hashtable.ContainsKey("special"))
		{
			switch (hashtable["special"].ToString())
			{
			case "ENDING":
				prevTexture.alpha = 0f;
				allowClick = false;
				slideTimerActive = false;
				showEndingOptions(showContinue: true);
				return;
			case "ENDING_NOCONTINUE":
				prevTexture.alpha = 0f;
				allowClick = false;
				slideTimerActive = false;
				showEndingOptions(showContinue: false);
				return;
			}
		}
		if (hashtable.ContainsKey("sound"))
		{
			string text = hashtable["sound"].ToString();
			if (text != null && text == "WHETSAPP_SFX")
			{
				audioController.playWhetsappAudio();
			}
		}
		if (hashtable.ContainsKey("bgm"))
		{
			switch (hashtable["bgm"].ToString())
			{
			case "BGMFADE":
				audioController.fadeoutBGM();
				break;
			case "STARTFINALBGM":
				audioController.switchBGM("credits");
				break;
			}
		}
		Texture texture = null;
		if (this.texture.mainTexture != null)
		{
			texture = this.texture.mainTexture;
		}
		Texture mainTexture = commonScreenObject.loadTexture(hashtable["texture"].ToString());
		this.texture.mainTexture = mainTexture;
		this.texture.alpha = 1f;
		if (hashtable.ContainsKey("effect"))
		{
			string text2 = hashtable["effect"].ToString();
			if (text2 != null && text2 == "CROSSFADE")
			{
				if (texture != null)
				{
					prevTexture.mainTexture = texture;
					prevTexture.alpha = 1f;
				}
				allowClick = false;
				this.texture.alpha = 0f;
				commonScreenObject.tweenAlpha(textureAlphaTween, 0f, 1f, 0.8f, base.gameObject, "enableClick");
			}
		}
		else
		{
			textureAlphaTween.enabled = false;
			prevTexture.alpha = 0f;
			this.texture.alpha = 1f;
			allowClick = true;
		}
		float x = (float)Convert.ToDouble(hashtable["posX"]);
		float y = (float)Convert.ToDouble(hashtable["posY"]);
		this.texture.transform.localPosition = new Vector3(x, y, 0f);
		int w = Convert.ToInt32(hashtable["sizeX"]);
		int h = Convert.ToInt32(hashtable["sizeY"]);
		this.texture.SetDimensions(w, h);
		if (hashtable.ContainsKey("timer"))
		{
			slideTimerCount = Convert.ToInt32(hashtable["timer"]);
		}
		else
		{
			slideTimerCount = 50;
		}
	}

	public IEnumerator slideshowTimer()
	{
		while (slideTimerActive)
		{
			slideTimerCount--;
			if (slideTimerCount < 0)
			{
				nextTexture();
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void enableClick()
	{
		allowClick = true;
		Texture texture = null;
		if (this.texture.mainTexture != null)
		{
			texture = this.texture.mainTexture;
		}
		if (texture != null)
		{
			prevTexture.mainTexture = texture;
			prevTexture.alpha = 1f;
		}
	}

	public IEnumerator endLastSlide(bool backToStart)
	{
		hideEndingOption();
		if (backToStart)
		{
			commonScreenObject.tweenAlpha(textureAlphaTween, 1f, 0f, 1.2f, null, string.Empty);
			audioController.fadeoutBGM();
			yield return new WaitForSeconds(4f);
			Application.LoadLevel("ALLNGUIMENU");
		}
		else
		{
			commonScreenObject.tweenAlpha(textureAlphaTween, 1f, 0f, 1.2f, null, string.Empty);
			Season playerSeason = CommonAPI.getSeasonByMonth(game.getPlayer().getSeasonIndex());
			audioController.changeBGM(CommonAPI.getSeasonBGM(playerSeason));
			yield return new WaitForSeconds(2f);
			viewController.closeTextureSequencePopup(hideBlackMask: true, doResume: true);
		}
	}

	public void showEndingOptions(bool showContinue)
	{
		GameData gameData = game.getGameData();
		string text = "EndingOptionsObj";
		if (!showContinue)
		{
			text = "EndingOptionsNoContinueObj";
		}
		GameObject gameObject = commonScreenObject.createPrefab(base.gameObject, "EndingOptionsObj", "Prefab/Popup/" + text, Vector3.zero, Vector3.one, Vector3.zero);
		UILabel[] componentsInChildren = gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "Continue_label":
				uILabel.text = gameData.getTextByRefId("endingOptions01");
				break;
			case "BackToStart_label":
				uILabel.text = gameData.getTextByRefId("endingOptions02");
				break;
			}
		}
	}

	public void hideEndingOption()
	{
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "EndingOptionsObj").gameObject;
		if (gameObject != null)
		{
			commonScreenObject.destroyPrefabImmediate(gameObject);
		}
	}

	public void nextTexture()
	{
		displayIndex++;
		if (displayIndex < textureList.Count)
		{
			showCurrentTexture();
		}
		else
		{
			viewController.closeTextureSequencePopup(hideBlackMask: true, doResume: true);
		}
	}

	public void setTextureList(List<Hashtable> aList)
	{
		textureList = aList;
		displayIndex = 0;
		showCurrentTexture();
		slideTimerActive = true;
		StartCoroutine(slideshowTimer());
	}
}
