using System.Collections;
using UnityEngine;

public class GUIGridController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private GameObject grid;

	private int widthNoOfSquare = 12;

	private int heightNoOfSquare = 14;

	private float spawnSpeed;

	private SpriteRenderer floor;

	private SpriteRenderer wall_left;

	private SpriteRenderer wall_right;

	private GameObject sceneBackground;

	private Vector3[,] coordinates;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		grid = GameObject.Find("Grid");
		floor = GameObject.Find("Floor").GetComponent<SpriteRenderer>();
		wall_left = GameObject.Find("Wall_left").GetComponent<SpriteRenderer>();
		wall_right = GameObject.Find("Wall_right").GetComponent<SpriteRenderer>();
		sceneBackground = GameObject.Find("SceneBackground");
		coordinates = new Vector3[widthNoOfSquare, heightNoOfSquare];
	}

	public void createWorld(bool refresh = false)
	{
		StartCoroutine("CreateWorld", refresh);
	}

	private IEnumerator CreateWorld(bool refresh)
	{
		ShopLevel playerShopLvl = this.game.getPlayer().getShopLevel();
		this.floor.sprite = this.commonScreenObject.loadSprite("Image/Shop/" + playerShopLvl.getFloorImg());
		this.wall_left.sprite = this.commonScreenObject.loadSprite("Image/Shop/" + playerShopLvl.getWallLeftImg());
		this.wall_right.sprite = this.commonScreenObject.loadSprite("Image/Shop/" + playerShopLvl.getWallRightImg());
		this.floor.transform.localPosition = playerShopLvl.getFloorPosition();
		this.wall_left.transform.localPosition = playerShopLvl.getWallLeftPosition();
		this.wall_right.transform.localPosition = playerShopLvl.getWallRightPosition();
		this.sceneBackground.transform.localPosition = playerShopLvl.getBgPosition();
		this.widthNoOfSquare = playerShopLvl.getWidth();
		this.heightNoOfSquare = playerShopLvl.getHeight();
		this.coordinates = new Vector3[this.widthNoOfSquare, this.heightNoOfSquare];
		float worldWidth = (float)this.widthNoOfSquare * 0.73f / 2f;
		float worldHeight = (float)this.heightNoOfSquare * 0.74f / 2f;
		int xArrayNo = 0;
		int zArrayNo = 0;
		for (float num = -worldHeight; num < worldHeight - 0.01f; num += 0.74f)
		{
			for (float num2 = -worldWidth; num2 < worldWidth - 0.01f; num2 += 0.73f)
			{
				if (playerShopLvl.getShopName() == "3")
				{
				}
				this.coordinates[xArrayNo, zArrayNo] = new Vector3(num2, 0f, num);
				xArrayNo++;
			}
			zArrayNo++;
			xArrayNo = 0;
		}
		yield return null;
		GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>().createObstacles();
		GameObject.Find("StationController").GetComponent<StationController>().setStation();
		GameObject.Find("GUIDecorationController").GetComponent<GUIDecorationController>().createDecorations();
		if (refresh)
		{
			GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().refreshCharactersPos();
		}
		yield break;
	}

	public int getWidthNoOfSquare()
	{
		return widthNoOfSquare;
	}

	public int getHeightNoOfSquare()
	{
		return heightNoOfSquare;
	}

	public Vector3 getPosition(int x, int z)
	{
		return coordinates[x, z];
	}

	public Vector2 getCoordinates(Vector3 aPosition)
	{
		aPosition.y = 0f;
		for (int i = 0; i < widthNoOfSquare; i++)
		{
			for (int j = 0; j < heightNoOfSquare; j++)
			{
				if (Vector3.Distance(aPosition, coordinates[i, j]) <= 0.2f)
				{
					return new Vector2(i, j);
				}
			}
		}
		return new Vector2(-1f, -1f);
	}
}
