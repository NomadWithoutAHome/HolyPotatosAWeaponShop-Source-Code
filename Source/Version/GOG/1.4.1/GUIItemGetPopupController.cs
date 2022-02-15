using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIItemGetPopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GameObject dogScalerObj;

	private TweenScale dogScaler;

	private UILabel dogTitleLabel;

	private UITexture dogImageTexture;

	private UILabel dogTextLabel;

	private GameObject scalerObj;

	private TweenScale scaler;

	private UILabel titleLabel;

	private UITexture imageTexture;

	private UILabel textLabel;

	private int maxHeight;

	private Queue<Hashtable> itemGetQueue;

	private Queue<Hashtable> dogItemGetQueue;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		dogScalerObj = commonScreenObject.findChild(base.gameObject, "DogItemGet_scaler").gameObject;
		dogScaler = dogScalerObj.GetComponent<TweenScale>();
		dogTitleLabel = commonScreenObject.findChild(dogScalerObj, "DogItemGet_title").GetComponent<UILabel>();
		dogImageTexture = commonScreenObject.findChild(dogScalerObj, "DogItemImage_texture").GetComponent<UITexture>();
		dogTextLabel = commonScreenObject.findChild(dogScalerObj, "DogItemGet_text").GetComponent<UILabel>();
		scalerObj = commonScreenObject.findChild(base.gameObject, "ItemGet_scaler").gameObject;
		scaler = scalerObj.GetComponent<TweenScale>();
		titleLabel = commonScreenObject.findChild(scalerObj, "ItemGet_title").GetComponent<UILabel>();
		imageTexture = commonScreenObject.findChild(scalerObj, "ItemImage_texture").GetComponent<UITexture>();
		textLabel = commonScreenObject.findChild(scalerObj, "ItemGet_text").GetComponent<UILabel>();
		maxHeight = 120;
		itemGetQueue = new Queue<Hashtable>();
		dogItemGetQueue = new Queue<Hashtable>();
	}

	public void showItemGet()
	{
		if (scaler.enabled || dogScaler.enabled)
		{
			return;
		}
		if (dogItemGetQueue != null && dogItemGetQueue.Count > 0)
		{
			Hashtable hashtable = dogItemGetQueue.Dequeue();
			dogTitleLabel.text = hashtable["title"].ToString();
			dogTextLabel.text = hashtable["text"].ToString();
			dogImageTexture.mainTexture = commonScreenObject.loadTexture(hashtable["image"].ToString());
			if (dogImageTexture.mainTexture != null)
			{
				commonScreenObject.tweenScale(dogScaler, Vector3.zero, Vector3.one, 3f, null, string.Empty);
			}
			else
			{
				CommonAPI.debug("Missing texture: " + hashtable["image"].ToString());
			}
			audioController.playItemGetAudio();
		}
		else
		{
			if (itemGetQueue == null || itemGetQueue.Count <= 0)
			{
				return;
			}
			Hashtable hashtable2 = itemGetQueue.Dequeue();
			titleLabel.text = hashtable2["title"].ToString();
			textLabel.text = hashtable2["text"].ToString();
			imageTexture.mainTexture = commonScreenObject.loadTexture(hashtable2["image"].ToString());
			if (imageTexture.mainTexture != null)
			{
				int num = imageTexture.mainTexture.width;
				int height = imageTexture.mainTexture.height;
				if (height > maxHeight)
				{
					num = num * maxHeight / height;
					height = maxHeight;
				}
				imageTexture.SetDimensions(num, height);
				commonScreenObject.tweenScale(scaler, Vector3.zero, Vector3.one, 3f, null, string.Empty);
			}
			else
			{
				CommonAPI.debug("Missing texture: " + hashtable2["image"].ToString());
			}
			audioController.playItemGetAudio();
		}
	}

	public void addDogItemGet(string aTitle, string aImage, string aText)
	{
		if (dogItemGetQueue != null)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["title"] = aTitle;
			hashtable["image"] = aImage;
			hashtable["text"] = aText;
			dogItemGetQueue.Enqueue(hashtable);
		}
	}

	public void addItemGet(string aTitle, string aImage, string aText)
	{
		if (itemGetQueue != null)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["title"] = aTitle;
			hashtable["image"] = aImage;
			string text2 = (string)(hashtable["text"] = aText.Replace("[dogName]", game.getPlayer().getDogName()));
			itemGetQueue.Enqueue(hashtable);
		}
	}

	public void clearItemGetQueue()
	{
		if (itemGetQueue != null)
		{
			itemGetQueue.Clear();
		}
		if (dogItemGetQueue != null)
		{
			dogItemGetQueue.Clear();
		}
		if (dogScaler == null || dogScalerObj == null)
		{
			dogScalerObj = commonScreenObject.findChild(base.gameObject, "DogItemGet_scaler").gameObject;
			dogScaler = dogScalerObj.GetComponent<TweenScale>();
		}
		dogScaler.enabled = false;
		dogScalerObj.transform.localScale = Vector3.zero;
		if (scaler == null || scalerObj == null)
		{
			scalerObj = commonScreenObject.findChild(base.gameObject, "ItemGet_scaler").gameObject;
			scaler = scalerObj.GetComponent<TweenScale>();
		}
		scaler.enabled = false;
		scalerObj.transform.localScale = Vector3.zero;
	}
}
