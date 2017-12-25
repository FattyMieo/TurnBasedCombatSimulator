using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType
{
	Offense = 0,
	Defense,
	Flank,
	Infiltrate,
	Suppress,

	Total
}

public class AttributeScript : MonoBehaviour
{
	public int level = 0;
	public int[] attributeCount = new int[(int)AttributeType.Total];
	public int[] bonusAttributeCount = new int[(int)AttributeType.Total];

	// Use this for initialization
	void Start ()
	{
		
	}

	public void RandomizeAttributes(int points)
	{
		attributeCount = new int[(int)AttributeType.Total];

		for(int i = 0; i < points; i++)
		{
			int rand = Random.Range(0, (int)AttributeType.Total);

			attributeCount[rand]++;
		}
	}

	public void InitializeBonusAttribute()
	{
		bonusAttributeCount = new int[(int)AttributeType.Total];

		for(int i = 0; i < bonusAttributeCount.Length; i++)
		{
			bonusAttributeCount[i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
