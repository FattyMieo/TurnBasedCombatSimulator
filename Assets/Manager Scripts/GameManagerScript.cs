using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	PlayerSetAttribute = 0,
	PlayerMove,
	EnemyMove,
	Combat,
	GameOver,

	Total
}

public class GameManagerScript : MonoBehaviour
{
	public static GameManagerScript Instance;

	void Awake()
	{
		Instance = this;
	}

	public GameState curState = GameState.PlayerSetAttribute;

	float moveTimer = 0.0f;
	public float moveDuration = 0.1f;

	public InventoryScript inventory;
	public GameObject gameOverScene;

	// Use this for initialization
	void Start ()
	{
		inventory = GetComponent<InventoryScript>();
		inventory.AddItem(ItemType.StrategyCard_Offense, 1);
		inventory.AddItem(ItemType.StrategyCard_Defense, 1);
		inventory.AddItem(ItemType.StrategyCard_Flank, 1);
		inventory.AddItem(ItemType.StrategyCard_Inflitrate, 1);
		inventory.AddItem(ItemType.StrategyCard_Suppress, 1);
	}

	// Update is called once per frame
	void Update ()
	{
		if(curState == GameState.GameOver)
		{
			Debug.Log("GAME OVER");
			gameOverScene.SetActive(true);
		}

		if(curState == GameState.Combat)
		{
			BattleManagerScript.Instance.ui.SetActive(true);
			BattleManagerScript.Instance.RunBattle();
		}
		else
		{
			BattleManagerScript.Instance.ui.SetActive(false);

			if(curState == GameState.PlayerSetAttribute)
			{
				AttributeManagerScript.Instance.ui.SetActive(true);
				AttributeManagerScript.Instance.RunUpgrade();
			}
			else
			{
				AttributeManagerScript.Instance.ui.SetActive(false);

				if(curState == GameState.EnemyMove)
				{
					if(moveTimer <= 0.0f)
					{
						for(int i = 0; i < SpawnManagerScript.Instance.enemyList.Count; i++)
						{
							SpawnManagerScript.Instance.enemyList[i].Move();
						}
					}

					moveTimer += Time.deltaTime;
					if(moveTimer > moveDuration)
					{
						moveTimer = 0.0f;
						GameManagerScript.Instance.curState = GameState.PlayerMove;
					}
				}
			}
		}
	}
}
