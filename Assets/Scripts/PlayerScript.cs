using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public UnitScript unitScript;
	public AttributeScript attrScript;

	public int xPos;
	public int yPos;

	bool hasMoved = false;
	float moveTimer = 0.0f;

	// Use this for initialization
	void Start ()
	{
		unitScript = GetComponent<UnitScript>();
		attrScript = GetComponent<AttributeScript>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(GameManagerScript.Instance.curState == GameState.PlayerMove)
		{
			if(!hasMoved)
			{
				if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
				{
					yPos++;
					unitScript.SetDirection(Direction.NORTH);
				}
				else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
				{
					yPos--;
					unitScript.SetDirection(Direction.SOUTH);
				}
				else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
				{
					xPos--;
					unitScript.SetDirection(Direction.WEST);
				}
				else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
				{
					xPos++;
					unitScript.SetDirection(Direction.EAST);
				}
				else
				{
					return;
				}

				Move();

				hasMoved = true;
			}
			else
			{
				moveTimer += Time.deltaTime;
				if(moveTimer > 0.1f)
				{
					moveTimer = 0.0f;
					GameManagerScript.Instance.curState = GameState.EnemyMove;
					hasMoved = false;
				}
			}
		}
	}

	void Move()
	{
		if(yPos >= (int)TileEngineScript.Instance.size.y)
		{
			yPos = (int)TileEngineScript.Instance.size.y - 1;
		}
		if(xPos >= (int)TileEngineScript.Instance.size.x)
		{
			xPos = (int)TileEngineScript.Instance.size.x - 1;
		}
		if(yPos < 0)
		{
			yPos = 0;
		}
		if(xPos < 0)
		{
			xPos = 0;
		}

		transform.position = TileEngineScript.Instance.posMap[yPos, xPos];
	}

	public void LevelUp()
	{
		attrScript.level++;
		AttributeManagerScript.Instance.pointsRemaining++;
		GameManagerScript.Instance.curState = GameState.PlayerSetAttribute;
	}
}
