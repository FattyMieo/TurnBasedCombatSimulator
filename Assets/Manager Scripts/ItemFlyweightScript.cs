using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemFlyweightElement
{
	public string name;
	public string description;
	public Sprite sprite;
}

public class ItemFlyweightScript : MonoBehaviour
{
	public static ItemFlyweightScript Instance;

	void Awake()
	{
		Instance = this;
	}

	public ItemFlyweightElement[] itemFlyweight;

	[ContextMenu("Initialize")]
	public void InitializeFlyweight()
	{
		itemFlyweight = new ItemFlyweightElement[(int)ItemType.Total];

		for(int i = 0; i < (int)ItemType.Total; i++)
		{
			itemFlyweight[i] = new ItemFlyweightElement();
			itemFlyweight[i].name = ((ItemType)i).ToString();
		}
	}
}
