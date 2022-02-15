using System.Collections.Generic;
using UnityEngine;

public class TextStampScript : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private string fullText;

	private char[] displayText;

	private float widthTotal;

	private List<float> letterWidthList;

	private List<GameObject> letterObjList;

	private float startSize;

	private float endSize;

	private float localHeight;

	private float spacing;

	private float interval;

	private float duration;

	private Color colorTop;

	private Color colorBottom;

	private int depth;

	private bool doAnim;

	public void setTextStampAnim(string aText, float aStartSize, float aEndSize, float aHeight, float aSpacing, float aInterval, float aDuration, Color aColorTop, Color aColorBottom, int aDepth)
	{
		if (commonScreenObject == null)
		{
			commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		}
		fullText = aText;
		displayText = fullText.ToCharArray();
		widthTotal = 0f;
		letterWidthList = new List<float>();
		letterObjList = new List<GameObject>();
		startSize = aStartSize;
		endSize = aEndSize;
		localHeight = aHeight;
		spacing = aSpacing;
		interval = aInterval;
		duration = aDuration;
		colorTop = aColorTop;
		colorBottom = aColorBottom;
		depth = aDepth;
		showTextStampAnim();
	}

	public void showTextStampAnim()
	{
		int num = 0;
		char[] array = displayText;
		for (int i = 0; i < array.Length; i++)
		{
			char c = array[i];
			if (widthTotal > 0f)
			{
				widthTotal += spacing;
			}
			string text = c.ToString();
			GameObject gameObject = commonScreenObject.createPrefab(base.gameObject, "textAnim_" + num + text, "Prefab/Font/AnimTextObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component = gameObject.GetComponent<UILabel>();
			component.text = text;
			component.gradientTop = colorTop;
			component.gradientBottom = colorBottom;
			component.applyGradient = true;
			component.depth = depth;
			gameObject.transform.localScale = new Vector3(endSize, endSize, 1f);
			widthTotal += gameObject.GetComponent<UILabel>().width;
			letterWidthList.Add((float)gameObject.GetComponent<UILabel>().width * endSize);
			gameObject.transform.localScale = new Vector3(startSize, startSize, 1f);
			letterObjList.Add(gameObject);
			num++;
		}
		float num2 = (0f - widthTotal) / 2f * endSize;
		num = 0;
		foreach (GameObject letterObj in letterObjList)
		{
			letterObj.transform.localPosition = new Vector3(num2 + letterWidthList[num] / 2f, localHeight, 0f);
			letterObj.transform.localScale = new Vector3(startSize, startSize, 1f);
			TweenScale component2 = letterObj.GetComponent<TweenScale>();
			component2.duration = duration;
			component2.delay = interval * (float)num;
			component2.from = new Vector3(startSize, startSize, 1f);
			component2.to = new Vector3(endSize, endSize, 1f);
			TweenAlpha component3 = letterObj.GetComponent<TweenAlpha>();
			component3.duration = duration;
			component3.delay = interval * (float)num;
			component3.ResetToBeginning();
			component3.Play();
			num2 += letterWidthList[num];
			num++;
		}
	}
}
