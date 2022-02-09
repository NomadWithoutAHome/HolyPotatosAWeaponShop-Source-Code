using UnityEngine;

public class FontScript : MonoBehaviour
{
	private GameObject FontRootObj;

	private UILabel normLabel;

	private UILabel thinLabel;

	private UILabel titleLabel;

	private Font normFont;

	private Font thinFont;

	private Font titleFont;

	private void Awake()
	{
		if (Constants.LANGUAGE == LanguageType.kLanguageTypeJap)
		{
			FontRootObj = GameObject.Find("Font Root Jap");
			if (FontRootObj == null)
			{
				FontRootObj = Object.Instantiate(Resources.Load("Prefab/Font/Font Root Jap")) as GameObject;
				FontRootObj.name = "Font Root Jap";
				FontRootObj.transform.localScale = Vector3.zero;
			}
			replaceFonts();
		}
	}

	public void replaceFonts()
	{
		createFontRoot();
		if (normLabel == null || titleLabel == null)
		{
			UILabel[] componentsInChildren = FontRootObj.GetComponentsInChildren<UILabel>();
			UILabel[] array = componentsInChildren;
			foreach (UILabel uILabel in array)
			{
				switch (uILabel.gameObject.name)
				{
				case "font_normal":
					normLabel = uILabel;
					break;
				case "font_thin":
					thinLabel = uILabel;
					break;
				case "font_title":
					titleLabel = uILabel;
					break;
				}
			}
		}
		if (normFont == null)
		{
			normFont = normLabel.trueTypeFont;
		}
		if (thinFont == null)
		{
			thinFont = thinLabel.trueTypeFont;
		}
		if (titleFont == null)
		{
			titleFont = titleLabel.trueTypeFont;
		}
		UILabel[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<UILabel>();
		switch (Constants.LANGUAGE)
		{
		case LanguageType.kLanguageTypeEnglish:
		case LanguageType.kLanguageTypeGermany:
		case LanguageType.kLanguageTypeRussia:
		case LanguageType.kLanguageTypeFrench:
		case LanguageType.kLanguageTypeItalian:
		case LanguageType.kLanguageTypeSpanish:
		{
			UILabel[] array3 = componentsInChildren2;
			foreach (UILabel uILabel3 in array3)
			{
				if (uILabel3.trueTypeFont != null)
				{
					if (uILabel3.trueTypeFont.name == "rounded-mplus-1p-heavy")
					{
						uILabel3.trueTypeFont = normFont;
					}
					else if (uILabel3.trueTypeFont.name == "rounded-mplus-1p-medium")
					{
						uILabel3.trueTypeFont = thinFont;
					}
					else if (uILabel3.trueTypeFont.name.Contains("rounded"))
					{
						uILabel3.trueTypeFont = titleFont;
					}
				}
			}
			break;
		}
		case LanguageType.kLanguageTypeChinese:
		case LanguageType.kLanguageTypeJap:
		{
			UILabel[] array2 = componentsInChildren2;
			foreach (UILabel uILabel2 in array2)
			{
				if (uILabel2.trueTypeFont != null)
				{
					if (uILabel2.trueTypeFont.name.Contains("Proxima") && uILabel2.trueTypeFont.name.Contains("Bold"))
					{
						uILabel2.trueTypeFont = normFont;
					}
					else if (uILabel2.trueTypeFont.name.Contains("Proxima") && uILabel2.trueTypeFont.name.Contains("Regular"))
					{
						uILabel2.trueTypeFont = thinFont;
					}
					else if (uILabel2.trueTypeFont.name.Contains("cubano"))
					{
						uILabel2.trueTypeFont = titleFont;
					}
				}
			}
			break;
		}
		case LanguageType.kLanguageTypeArabic:
		case LanguageType.kLanguageTypeBahasa:
		case LanguageType.kLanguageTypeThai:
			break;
		}
	}

	public void createFontRoot()
	{
		switch (Constants.LANGUAGE)
		{
		case LanguageType.kLanguageTypeEnglish:
		case LanguageType.kLanguageTypeGermany:
		case LanguageType.kLanguageTypeRussia:
		case LanguageType.kLanguageTypeFrench:
		case LanguageType.kLanguageTypeItalian:
		case LanguageType.kLanguageTypeSpanish:
			FontRootObj = null;
			FontRootObj = GameObject.Find("Font Root Eng");
			if (FontRootObj == null)
			{
				CommonAPI.debug("creating font root eng");
				FontRootObj = Object.Instantiate(Resources.Load("Prefab/Font/Font Root Eng")) as GameObject;
				FontRootObj.name = "Font Root Eng";
				FontRootObj.transform.localScale = Vector3.zero;
			}
			break;
		case LanguageType.kLanguageTypeChinese:
		case LanguageType.kLanguageTypeJap:
			FontRootObj = null;
			FontRootObj = GameObject.Find("Font Root Jap");
			if (FontRootObj == null)
			{
				FontRootObj = Object.Instantiate(Resources.Load("Prefab/Font/Font Root Jap")) as GameObject;
				FontRootObj.name = "Font Root Jap";
				FontRootObj.transform.localScale = Vector3.zero;
			}
			break;
		}
		UILabel[] componentsInChildren = FontRootObj.GetComponentsInChildren<UILabel>();
		UILabel[] array = componentsInChildren;
		foreach (UILabel uILabel in array)
		{
			switch (uILabel.gameObject.name)
			{
			case "font_normal":
				normLabel = uILabel;
				break;
			case "font_thin":
				thinLabel = uILabel;
				break;
			case "font_title":
				titleLabel = uILabel;
				break;
			}
		}
		normFont = normLabel.trueTypeFont;
		thinFont = thinLabel.trueTypeFont;
		titleFont = titleLabel.trueTypeFont;
	}
}
