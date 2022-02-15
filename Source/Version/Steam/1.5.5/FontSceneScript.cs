using UnityEngine;

public class FontSceneScript : MonoBehaviour
{
	private GameObject FontRootObj;

	private UILabel normLabel;

	private UILabel thinLabel;

	private UILabel titleLabel;

	private Font normFont;

	private Font thinFont;

	private Font titleFont;

	private Material[] normFontMat;

	private Material[] thinFontMat;

	private Material[] titleFontMat;

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
		normFontMat = new Material[1];
		thinFontMat = new Material[1];
		titleFontMat = new Material[1];
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
			MeshRenderer[] componentsInChildren2 = FontRootObj.GetComponentsInChildren<MeshRenderer>();
			MeshRenderer[] array2 = componentsInChildren2;
			foreach (MeshRenderer meshRenderer in array2)
			{
				switch (meshRenderer.gameObject.name)
				{
				case "font_normal_mat":
					normFontMat = meshRenderer.materials;
					break;
				case "font_thin_mat":
					thinFontMat = meshRenderer.materials;
					break;
				case "font_title_mat":
					titleFontMat = meshRenderer.materials;
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
		TextMesh[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<TextMesh>();
		switch (Constants.LANGUAGE)
		{
		case LanguageType.kLanguageTypeEnglish:
		case LanguageType.kLanguageTypeGermany:
		case LanguageType.kLanguageTypeRussia:
		case LanguageType.kLanguageTypeFrench:
		case LanguageType.kLanguageTypeItalian:
		case LanguageType.kLanguageTypeSpanish:
		{
			TextMesh[] array4 = componentsInChildren3;
			foreach (TextMesh textMesh2 in array4)
			{
				if (textMesh2.font.name == "rounded-mplus-1p-heavy")
				{
					textMesh2.font = normFont;
				}
				else if (textMesh2.font.name == "rounded-mplus-1p-medium")
				{
					textMesh2.font = thinFont;
				}
				else if (textMesh2.font.name.Contains("rounded"))
				{
					textMesh2.font = titleFont;
				}
			}
			break;
		}
		case LanguageType.kLanguageTypeChinese:
		case LanguageType.kLanguageTypeJap:
		{
			TextMesh[] array3 = componentsInChildren3;
			foreach (TextMesh textMesh in array3)
			{
				if (textMesh.font.name.Contains("Proxima") && textMesh.font.name.Contains("Bold"))
				{
					textMesh.font = normFont;
					textMesh.GetComponent<MeshRenderer>().materials = normFontMat;
				}
				else if (textMesh.font.name.Contains("Proxima") && textMesh.font.name.Contains("Regular"))
				{
					textMesh.font = thinFont;
					textMesh.GetComponent<MeshRenderer>().materials = thinFontMat;
				}
				else if (textMesh.font.name.Contains("cubano"))
				{
					textMesh.font = titleFont;
					textMesh.GetComponent<MeshRenderer>().materials = titleFontMat;
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
			CommonAPI.debug("english");
			FontRootObj = null;
			FontRootObj = GameObject.Find("Font Root Eng");
			if (FontRootObj == null)
			{
				FontRootObj = Object.Instantiate(Resources.Load("Prefab/Font/Font Root Eng")) as GameObject;
				FontRootObj.name = "Font Root Eng";
				FontRootObj.transform.localScale = Vector3.zero;
			}
			break;
		case LanguageType.kLanguageTypeChinese:
		case LanguageType.kLanguageTypeJap:
			CommonAPI.debug("jap");
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
		MeshRenderer[] componentsInChildren2 = FontRootObj.GetComponentsInChildren<MeshRenderer>();
		MeshRenderer[] array2 = componentsInChildren2;
		foreach (MeshRenderer meshRenderer in array2)
		{
			switch (meshRenderer.gameObject.name)
			{
			case "font_normal_mat":
				normFontMat = meshRenderer.materials;
				break;
			case "font_thin_mat":
				thinFontMat = meshRenderer.materials;
				break;
			case "font_title_mat":
				titleFontMat = meshRenderer.materials;
				break;
			}
		}
		normFont = normLabel.trueTypeFont;
		thinFont = thinLabel.trueTypeFont;
		titleFont = titleLabel.trueTypeFont;
	}
}
