using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable<T>
{
	bool AddEntity(T type);
	bool RemoveEntity(T type);
	int GetAmount(T type);
}

public enum ItemType
{
	StrategyCard_Offense = 0,
	StrategyCard_Defense,
	StrategyCard_Flank,
	StrategyCard_Inflitrate,
	StrategyCard_Suppress,
	HealingElixir,
	SmiteCard,

	Total
}

public class Item : IStorable<ItemType>
{
	public ItemType type;
	int stack = 0;
	int maxStack;

	public Item(ItemType type, int maxStack)
	{
		this.type = type;
		stack = 0;
		this.maxStack = maxStack;
	}

	public bool AddEntity(ItemType type)
	{
		if(type == this.type && stack < maxStack)
		{
			stack++;
			return true;
		}

		return false;
	}

	public bool RemoveEntity(ItemType type)
	{
		if(type == this.type && stack > 0)
		{
			stack--;
			return true;
		}

		return false;
	}

	public int GetAmount(ItemType type)
	{
		if(type == this.type)
		{
			return stack;
		}

		return 0;
	}

	public int GetStack()
	{
		return stack;
	}

	public int GetMaxStack()
	{
		return maxStack;
	}

	public string GetName() { return GetName(type); }
	public static string GetName(ItemType type)
	{
		return ItemFlyweightScript.Instance.itemFlyweight[(int)type].name;
	}

	public string GetDesc() { return GetDesc(type); }
	public static string GetDesc(ItemType type)
	{
		return ItemFlyweightScript.Instance.itemFlyweight[(int)type].description;
	}

	public Sprite GetSprite() { return GetSprite(type); }
	public static Sprite GetSprite(ItemType type)
	{
		return ItemFlyweightScript.Instance.itemFlyweight[(int)type].sprite;
	}
}

public class Inventory : IStorable<ItemType>
{
	List<Item> items = new List<Item>();

	int stack = 0;
	int maxStack;

	public Inventory(int maxStack)
	{
		stack = 0;
		this.maxStack = maxStack;
	}

	public bool AddEntity(ItemType type)
	{
		for(int i = 0; i < items.Count; i++)
		{
			if(items[i].AddEntity(type)) return true;
		}

		if(stack < maxStack)
		{
			Item newItem = new Item(type, 1);
			newItem.AddEntity(type);
			items.Add(newItem);

			return true;
		}

		return false;
	}

	public bool RemoveEntity(ItemType type)
	{
		for(int i = 0; i < items.Count; i++)
		{
			if(items[i].RemoveEntity(type))
			{
				if(items[i].GetAmount(type) <= 0) items.Remove(items[i]);
				return true;
			}
		}

		return false;
	}

	public int GetAmount(ItemType type)
	{
		int total = 0;

		for(int i = 0; i < items.Count; i++)
		{
			total += items[i].GetAmount(type);
		}

		return total;
	}

	public List<Item> GetItems()
	{
		return items;
	}
}