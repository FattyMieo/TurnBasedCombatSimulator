using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerScript : MonoBehaviour
{
	public StrategyCardScript[] cardPrefabs;

	public void SpawnCards ()
	{
		List<Item> items = GameManagerScript.Instance.inventory.GetItems();

		for(int i = 0; i < cardPrefabs.Length; i++)
		{
			if(i < items.Count)
			{
				cardPrefabs[i].gameObject.SetActive(true);
				cardPrefabs[i].SetCardDetails(items[i].type);
			}
			else
			{
				cardPrefabs[i].gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
