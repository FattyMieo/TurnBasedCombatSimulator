using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public UnitScript unitScript;
	public AttributeScript attrScript;

	public int xPos;
	public int yPos;

	// Use this for initialization
	void Start ()
	{
		unitScript = GetComponent<UnitScript>();
		attrScript = GetComponent<AttributeScript>();

		unitScript.facingDir = (Direction)Random.Range(0, (int)Direction.TOTAL);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Move()
	{
		int newXPos = 0;
		int newYPos = 0;

		bool dirFound = false;

		for(int i = 0; i < (int)Direction.TOTAL; i++)
		{
			switch(unitScript.facingDir)
			{
				case Direction.EAST:
					newXPos = xPos + 1;
					newYPos = yPos;
					break;
				case Direction.WEST:
					newXPos = xPos - 1;
					newYPos = yPos;
					break;
				case Direction.NORTH:
					newXPos = xPos;
					newYPos = yPos + 1;
					break;
				case Direction.SOUTH:
					newXPos = xPos;
					newYPos = yPos - 1;
					break;
			}

			if(SpawnManagerScript.Instance.IsEnemyPresent(newXPos, newYPos))
			{
				int newDir = (int)unitScript.facingDir + 1;
				if(newDir >= (int)Direction.TOTAL) newDir = 0;
				unitScript.SetDirection((Direction)newDir);
			}
			else
			{
				dirFound = true; //A direction is found
				break;
			}
		}

		if(!dirFound) return; //If no direction the unit can go, stop
		
		xPos = newXPos;
		yPos = newYPos;

		if(yPos >= (int)TileEngineScript.Instance.size.y)
		{
			yPos = (int)TileEngineScript.Instance.size.y - 1;
			unitScript.SetDirection(Direction.SOUTH);
		}
		if(xPos >= (int)TileEngineScript.Instance.size.x)
		{
			xPos = (int)TileEngineScript.Instance.size.x - 1;
			unitScript.SetDirection(Direction.WEST);
		}
		if(yPos < 0)
		{
			yPos = 0;
			unitScript.SetDirection(Direction.NORTH);
		}
		if(xPos < 0)
		{
			xPos = 0;
			unitScript.SetDirection(Direction.EAST);
		}

		transform.position = TileEngineScript.Instance.posMap[yPos, xPos];
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			BattleManagerScript.Instance.engagedEnemy = this;
			BattleManagerScript.Instance.hasInitialized = false;
			GameManagerScript.Instance.curState = GameState.Combat;
		}
	}
}
