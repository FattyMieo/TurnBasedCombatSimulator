using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
	public static SpawnManagerScript Instance;

	void Awake()
	{
		Instance = this;
	}

	public PlayerScript playerScript;

	public int enemyTotal = 5;
	public GameObject enemyPrefab;
	public List<EnemyScript> enemyList = new List<EnemyScript>();

	public int itemTotal = 5;
	public GameObject itemPrefab;
	public List<DroppedItemScript> itemList = new List<DroppedItemScript>();

	// Use this for initialization
	void Start ()
	{
		
	}

	public bool IsPlayerPresent(int checkX, int checkY)
	{
		if(playerScript == null) return false;

		return playerScript.xPos == checkX && playerScript.yPos == checkY;
	}

	public bool IsEnemyPresent(int checkX, int checkY)
	{
		for(int i = 0; i < enemyList.Count; i++)
		{
			if(enemyList[i].xPos == checkX && enemyList[i].yPos == checkY)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsItemPresent(int checkX, int checkY)
	{
		for(int i = 0; i < itemList.Count; i++)
		{
			if(itemList[i].xPos == checkX && itemList[i].yPos == checkY)
			{
				return true;
			}
		}
		return false;
	}

	public void InitializePlayerPos()
	{
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

		playerScript.xPos = Random.Range(0, (int)TileEngineScript.Instance.size.x);
		playerScript.yPos = Random.Range(0, (int)TileEngineScript.Instance.size.y);

		playerScript.transform.position = TileEngineScript.Instance.posMap[playerScript.yPos, playerScript.xPos];
	}

	public void SpawnEnemies()
	{
		for(int i = 0; i < enemyTotal; i++)
		{
			EnemyScript enemyScript = Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity).GetComponent<EnemyScript>();

			int newXPos = 0;
			int newYPos = 0;

			bool canSpawn = false;

			// ! Make sure pos doesn't have enemy / player
			for(int j = 0; j < 100; j++)
			{
				newXPos = Random.Range(0, (int)TileEngineScript.Instance.size.x);
				newYPos = Random.Range(0, (int)TileEngineScript.Instance.size.y);

				if(!IsPlayerPresent(newXPos, newYPos) && !IsEnemyPresent(newXPos, newYPos))
				{
					canSpawn = true;
					break;
				}
			}

			if(!canSpawn) continue;

			// ! Set enemy pos
			enemyScript.xPos = newXPos;
			enemyScript.yPos = newYPos;
			enemyScript.transform.position = TileEngineScript.Instance.posMap[enemyScript.yPos, enemyScript.xPos];

			// ! Save enemy in list
			enemyList.Add(enemyScript);
		}
	}

	public void SpawnItems(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			DroppedItemScript droppedItemScript = Instantiate(itemPrefab, Vector2.zero, Quaternion.identity).GetComponent<DroppedItemScript>();

			droppedItemScript.type = (ItemType)Random.Range(0, (int)ItemType.Total);
			droppedItemScript.UpdateSprite();

			int newXPos = 0;
			int newYPos = 0;

			bool canSpawn = false;

			// ! Make sure pos doesn't have enemy / player
			for(int j = 0; j < 100; j++)
			{
				newXPos = Random.Range(0, (int)TileEngineScript.Instance.size.x);
				newYPos = Random.Range(0, (int)TileEngineScript.Instance.size.y);

				if(!IsPlayerPresent(newXPos, newYPos) && !IsItemPresent(newXPos, newYPos))
				{
					canSpawn = true;
					break;
				}
			}

			if(!canSpawn) continue;

			// ! Set enemy pos
			droppedItemScript.xPos = newXPos;
			droppedItemScript.yPos = newYPos;
			droppedItemScript.transform.position = TileEngineScript.Instance.posMap[droppedItemScript.yPos, droppedItemScript.xPos];

			// ! Save enemy in list
			itemList.Add(droppedItemScript);
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
