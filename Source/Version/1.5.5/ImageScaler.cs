using UnityEngine;

public static class ImageScaler
{
	private static float xpos;

	private static int scaledWidth;

	private static int scaledHeight;

	private static float imageRatio;

	private static int originalWidth;

	private static int originalHeight;

	private static float screenRatio;

	public static void ScaleImage(GUITexture guiTexture)
	{
		originalWidth = guiTexture.texture.width;
		originalHeight = guiTexture.texture.height;
		screenRatio = Screen.width / Screen.height;
		imageRatio = originalWidth / originalHeight;
		if (imageRatio <= screenRatio)
		{
			scaledHeight = Screen.height;
			scaledWidth = (int)((float)scaledHeight * imageRatio);
		}
		else
		{
			scaledWidth = Screen.width;
			scaledHeight = (int)((float)scaledWidth / imageRatio);
		}
		xpos = Screen.width / 2 - scaledWidth / 2;
		guiTexture.pixelInset = new Rect(xpos, Screen.height - scaledHeight, scaledWidth, scaledHeight);
	}
}
