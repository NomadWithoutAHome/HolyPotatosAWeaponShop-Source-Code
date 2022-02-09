using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001AD RID: 429
public class GUIGridController : MonoBehaviour
{
	// Token: 0x06000B4E RID: 2894 RVA: 0x0005F03C File Offset: 0x0005D43C
	private void Awake()
	{
		this.game = GameObject.Find("Game").GetComponent<Game>();
		this.commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		this.grid = GameObject.Find("Grid");
		this.floor = GameObject.Find("Floor").GetComponent<SpriteRenderer>();
		this.wall_left = GameObject.Find("Wall_left").GetComponent<SpriteRenderer>();
		this.wall_right = GameObject.Find("Wall_right").GetComponent<SpriteRenderer>();
		this.sceneBackground = GameObject.Find("SceneBackground");
		this.coordinates = new Vector3[this.widthNoOfSquare, this.heightNoOfSquare];
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x0005F0E9 File Offset: 0x0005D4E9
	public void createWorld(bool refresh = false)
	{
		base.StartCoroutine("CreateWorld", refresh);
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x0005F100 File Offset: 0x0005D500
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

	// Token: 0x06000B51 RID: 2897 RVA: 0x0005F122 File Offset: 0x0005D522
	public int getWidthNoOfSquare()
	{
		return this.widthNoOfSquare;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x0005F12A File Offset: 0x0005D52A
	public int getHeightNoOfSquare()
	{
		return this.heightNoOfSquare;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0005F132 File Offset: 0x0005D532
	public Vector3 getPosition(int x, int z)
	{
		return this.coordinates[x, z];
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x0005F144 File Offset: 0x0005D544
	public Vector2 getCoordinates(Vector3 aPosition)
	{
		aPosition.y = 0f;
		for (int i = 0; i < this.widthNoOfSquare; i++)
		{
			for (int j = 0; j < this.heightNoOfSquare; j++)
			{
				if (Vector3.Distance(aPosition, this.coordinates[i, j]) <= 0.2f)
				{
					return new Vector2((float)i, (float)j);
				}
			}
		}
		return new Vector2(-1f, -1f);
	}

	// Token: 0x04000B7A RID: 2938
	private Game game;

	// Token: 0x04000B7B RID: 2939
	private CommonScreenObject commonScreenObject;

	// Token: 0x04000B7C RID: 2940
	private GameObject grid;

	// Token: 0x04000B7D RID: 2941
	private int widthNoOfSquare = 12;

	// Token: 0x04000B7E RID: 2942
	private int heightNoOfSquare = 14;

	// Token: 0x04000B7F RID: 2943
	private float spawnSpeed;

	// Token: 0x04000B80 RID: 2944
	private SpriteRenderer floor;

	// Token: 0x04000B81 RID: 2945
	private SpriteRenderer wall_left;

	// Token: 0x04000B82 RID: 2946
	private SpriteRenderer wall_right;

	// Token: 0x04000B83 RID: 2947
	private GameObject sceneBackground;

	// Token: 0x04000B84 RID: 2948
	private Vector3[,] coordinates;
}
