using UnityEngine;

public class ComboBoxTest : MonoBehaviour
{
	private GUIContent[] comboBoxList;

	private ComboBox comboBoxControl;

	private GUIStyle listStyle = new GUIStyle();

	private void Start()
	{
		comboBoxList = new GUIContent[5];
		comboBoxList[0] = new GUIContent("Thing 1");
		comboBoxList[1] = new GUIContent("Thing 2");
		comboBoxList[2] = new GUIContent("Thing 3");
		comboBoxList[3] = new GUIContent("Thing 4");
		comboBoxList[4] = new GUIContent("Thing 5");
		listStyle.normal.textColor = Color.white;
		GUIStyleState onHover = listStyle.onHover;
		Texture2D background = new Texture2D(2, 2);
		listStyle.hover.background = background;
		onHover.background = background;
		RectOffset padding = listStyle.padding;
		int num = 4;
		listStyle.padding.bottom = num;
		num = num;
		listStyle.padding.top = num;
		num = num;
		listStyle.padding.right = num;
		padding.left = num;
		comboBoxControl = new ComboBox(new Rect(50f, 100f, 100f, 20f), comboBoxList[0], comboBoxList, "button", "box", listStyle);
	}

	private void OnGUI()
	{
		int num = comboBoxControl.Show();
		GUI.Label(new Rect(50f, 70f, 400f, 21f), "dfdsfYou picked " + comboBoxList[num].text + "!");
	}
}
