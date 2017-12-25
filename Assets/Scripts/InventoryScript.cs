using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
	Inventory inventory = new Inventory(100);

	public int AddItem(ItemType type, int amount)
	{
		for(int i = amount; i > 0; i--)
		{
			if(!inventory.AddEntity(type)) return i;
		}
		return 0;
	}

	public int RemoveItem(ItemType type, int amount)
	{
		for(int i = amount; i > 0; i--)
		{
			if(!inventory.RemoveEntity(type)) return i;
		}
		return 0;
	}

	public List<Item> GetItems()
	{
		return inventory.GetItems();
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
