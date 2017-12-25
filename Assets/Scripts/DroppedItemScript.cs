using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemScript : MonoBehaviour
{
	public SpriteRenderer rend;
	public ItemType type;

	public int xPos;
	public int yPos;

	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
	}

	public void UpdateSprite()
	{
		rend.sprite = Item.GetSprite(type);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			GameManagerScript.Instance.inventory.AddItem(type, 1);
			SpawnManagerScript.Instance.SpawnItems(1);
			Destroy(gameObject);
		}
	}
}
