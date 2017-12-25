using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerScript : MonoBehaviour
{
	public enum BattleState
	{
		PlayerChoose = 0,
		EnemyChoose,
		Fight,

		Total
	}

	public static BattleManagerScript Instance;

	void Awake()
	{
		Instance = this;
	}

	public GameObject ui;

	public PlayerScript playerScript;
	public EnemyScript engagedEnemy;

	public bool hasInitialized;

	public BattleState battleState;
	public bool playerAttack;
	public AttributeType attackingCard;
	public CardSpawnerScript cardPanel;
	public int cardsClicked;

	public float timer;
	public float timerDuration = 1.0f;

	public void RunBattle()
	{
		if(!hasInitialized)
		{
			playerScript = SpawnManagerScript.Instance.playerScript;
			engagedEnemy.attrScript.RandomizeAttributes(SpawnManagerScript.Instance.playerScript.attrScript.level + (int)AttributeType.Total);

			playerScript.attrScript.InitializeBonusAttribute();
			engagedEnemy.attrScript.InitializeBonusAttribute();

			ResetState();
			hasInitialized = true;
		}

		UISubjectScript.Instance.Notify(NotificationType.Player_Health, playerScript.unitScript.health.ToString());
		UISubjectScript.Instance.Notify(NotificationType.Enemy_Health, engagedEnemy.unitScript.health.ToString());

		int startNotification = (int)NotificationType.Player_Offense;

		for(int i = 0; i < (int)AttributeType.Total; i++)
		{
			UISubjectScript.Instance.Notify((NotificationType)(startNotification + i), (playerScript.attrScript.attributeCount[i] + playerScript.attrScript.bonusAttributeCount[i]).ToString());
		}

		startNotification = (int)NotificationType.Enemy_Offense;

		for(int i = 0; i < (int)AttributeType.Total; i++)
		{
			UISubjectScript.Instance.Notify((NotificationType)(startNotification + i), (engagedEnemy.attrScript.attributeCount[i] + engagedEnemy.attrScript.bonusAttributeCount[i]).ToString());
		}

		cardPanel.gameObject.SetActive(battleState == BattleState.PlayerChoose);

		switch(battleState)
		{
			case BattleState.PlayerChoose:
				cardPanel.SpawnCards ();
				if(playerAttack && cardsClicked >= 1)
				{
					battleState = BattleState.EnemyChoose;
				}
				else if(cardsClicked >= 2)
				{
					battleState = BattleState.Fight;
					timer = 0.0f;
				}
				break;
			case BattleState.EnemyChoose:
				if(!playerAttack)
				{
					int rand = Random.Range(0, (int)AttributeType.Total);
					attackingCard = (AttributeType)rand;
					engagedEnemy.attrScript.bonusAttributeCount[rand]++;
					battleState = BattleState.PlayerChoose;
				}
				else
				{
					for(int i = 0; i < 2; i++)
						engagedEnemy.attrScript.bonusAttributeCount[Random.Range(0, (int)AttributeType.Total)]++;
					battleState = BattleState.Fight;
					timer = 0.0f;
				}
				break;
			case BattleState.Fight:

				if(timer == 0.0f)
				{
					//BATTLE
					int attackingInt = (int)attackingCard;
					int defendingInt1 = (int)Counter(attackingCard)[0];
					int defendingInt2 = (int)Counter(attackingCard)[1];

					if(playerAttack)
					{
						int atkDmg = playerScript.attrScript.attributeCount[attackingInt] + playerScript.attrScript.bonusAttributeCount[attackingInt];

						int def1 = engagedEnemy.attrScript.attributeCount[defendingInt1] + engagedEnemy.attrScript.bonusAttributeCount[defendingInt1];
						int def2 = engagedEnemy.attrScript.attributeCount[defendingInt2] + engagedEnemy.attrScript.bonusAttributeCount[defendingInt2];

						if(def1 < atkDmg) engagedEnemy.unitScript.Damage();
						if(def2 < atkDmg) engagedEnemy.unitScript.Damage();
					}
					else
					{
						int atkDmg = engagedEnemy.attrScript.attributeCount[attackingInt] + engagedEnemy.attrScript.bonusAttributeCount[attackingInt];

						int def1 = playerScript.attrScript.attributeCount[defendingInt1] + playerScript.attrScript.bonusAttributeCount[defendingInt1];
						int def2 = playerScript.attrScript.attributeCount[defendingInt2] + playerScript.attrScript.bonusAttributeCount[defendingInt2];

						if(def1 < atkDmg) playerScript.unitScript.Damage();
						if(def2 < atkDmg) playerScript.unitScript.Damage();
					}
				}

				timer += Time.deltaTime;

				if(timer >= timerDuration)
				{
					if(engagedEnemy.unitScript.health <= 0)
					{
						playerScript.LevelUp();
						SpawnManagerScript.Instance.enemyList.Remove(engagedEnemy);
						Destroy(engagedEnemy.gameObject);
					}
					else if(playerScript.unitScript.health <= 0 || GameManagerScript.Instance.inventory.GetItems().Count <= 0)
					{
						GameManagerScript.Instance.curState = GameState.GameOver;
					}

					playerAttack = !playerAttack;
					ResetState();
				}
				break;
		}
	}

	AttributeType[] Counter(AttributeType type)
	{
		AttributeType[] result = new AttributeType[2];

		if(type == AttributeType.Offense)
		{
			result[0] = AttributeType.Defense;
			result[1] = AttributeType.Infiltrate;
		}
		else if(type == AttributeType.Defense)
		{
			result[0] = AttributeType.Flank;
			result[1] = AttributeType.Suppress;
		}
		else if(type == AttributeType.Flank)
		{
			result[0] = AttributeType.Offense;
			result[1] = AttributeType.Infiltrate;
		}
		else if(type == AttributeType.Infiltrate)
		{
			result[0] = AttributeType.Defense;
			result[1] = AttributeType.Suppress;
		}
		else if(type == AttributeType.Suppress)
		{
			result[0] = AttributeType.Flank;
			result[1] = AttributeType.Offense;
		}

		return result;
	}

	void ResetState()
	{
		cardsClicked = 0;

		if(playerAttack) battleState = BattleState.PlayerChoose;
		else battleState = BattleState.EnemyChoose;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
