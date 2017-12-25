using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManagerScript : MonoBehaviour
{
	public static AttributeManagerScript Instance;

	void Awake()
	{
		Instance = this;
	}

	public GameObject ui;

	public AttributeScript playerAttr;

	public bool hasInitialized = false;

	public int pointsRemaining;

	public void RunUpgrade()
	{
		if(!hasInitialized)
		{
			playerAttr = SpawnManagerScript.Instance.playerScript.attrScript;
			UISubjectScript.Instance.Notify(NotificationType.Player_Health, SpawnManagerScript.Instance.playerScript.unitScript.health.ToString());
			hasInitialized = true;
		}

		int startNotification = (int)NotificationType.Player_Offense;

		for(int i = 0; i < (int)AttributeType.Total; i++)
		{
			UISubjectScript.Instance.Notify((NotificationType)(startNotification + i), playerAttr.attributeCount[i].ToString());
		}

		UISubjectScript.Instance.Notify(NotificationType.PointsRemaining, pointsRemaining.ToString());
		UISubjectScript.Instance.Notify(NotificationType.Level, playerAttr.level.ToString());

		if(pointsRemaining <= 0)
		{
			GameManagerScript.Instance.curState = GameState.PlayerMove;
		}
	}

	public void AddUpgrade(AttributeType type)
	{
		if(pointsRemaining > 0)
		{
			playerAttr.attributeCount[(int)type]++;
			pointsRemaining--;
		}
	}

	//For buttons

	public void AddOffense()
	{
		AddUpgrade(AttributeType.Offense);
	}
	public void AddDefense()
	{
		AddUpgrade(AttributeType.Defense);
	}
	public void AddFlank()
	{
		AddUpgrade(AttributeType.Flank);
	}
	public void AddInfiltrate()
	{
		AddUpgrade(AttributeType.Infiltrate);
	}
	public void AddSuppress()
	{
		AddUpgrade(AttributeType.Suppress);
	}
}
