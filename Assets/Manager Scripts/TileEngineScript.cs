using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEngineScript : MonoBehaviour
{
	public static TileEngineScript Instance;

	void Awake()
	{
		Instance = this;
	}

	public TileScript[,] tiles;
	public Vector2[,] posMap;
//	public GameObject[,] objects;

	[Header("Prefab")]
	public GameObject tilePrefab;

	public Vector2 offsetX;
	public Vector2 offsetY;

	public Vector2 size;

	// Use this for initialization
	void Start ()
	{
		Vector2 curOffsetX = Vector2.zero;
		Vector2 curOffsetY = Vector2.zero;

		tiles = new TileScript[(int)size.y, (int)size.x];
		posMap = new Vector2[(int)size.y, (int)size.x];

		for(int y = 0; y < size.y; y++)
		{
			for(int x = 0; x < size.x; x++)
			{
				posMap[y, x] = (Vector2)transform.position + (curOffsetX + curOffsetY);

				TileScript newTile = Instantiate(tilePrefab, posMap[y, x], Quaternion.identity).GetComponent<TileScript>();

				// (size.x * size.y) - ((size.x * y) + x) 
				// ab - (ay + x) = ab - ay - x = a (b - y) - x
				int newSortingOrder = (int)(size.x * (size.y - y) - x);
				newTile.SetSortingOrder(newSortingOrder);

				if((x + y) % 2 != 0) newTile.SetColor(Color.gray);

				tiles[y, x] = newTile;

				curOffsetX += offsetX;
			}
			curOffsetX = Vector2.zero;

			curOffsetY += offsetY;
		}

		SpawnManagerScript.Instance.InitializePlayerPos();
		SpawnManagerScript.Instance.SpawnEnemies();
		SpawnManagerScript.Instance.SpawnItems(SpawnManagerScript.Instance.itemTotal);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
