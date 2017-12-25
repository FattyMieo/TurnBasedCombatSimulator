using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	NORTH = 0,
	WEST,
	SOUTH,
	EAST,

	TOTAL
}

public class UnitScript : MonoBehaviour
{
	public Animator anim;
	public TileScript[] cubes;
	public SpriteRenderer arrow;
	public float transparency;
	[ContextMenuItem("Update", "UpdateColor")]
	public Color color;

	[Header("Status")]
	[ContextMenuItem("Refresh", "RefreshHealth")]
	public int health;
	[ContextMenuItem("Update", "UpdateDirection")]
	public Direction facingDir;

	[Header("Settings")]
	[ContextMenuItem("Refresh", "RefreshHealth")]
	public int maxHealth;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	public void UpdateColor()
	{
		SetColor(color);
	}

	public void SetColor(Color newColor)
	{
		color = newColor;

		for(int i = 0; i < cubes.Length; i++)
		{
			cubes[i].SetColor(color);
		}

		newColor.a = transparency;
		arrow.color = newColor;
	}

	public void UpdateDirection()
	{
		SetDirection(facingDir);
	}

	public void SetDirection(Direction _dir)
	{
		facingDir = _dir;

		arrow.gameObject.transform.rotation = Quaternion.Euler(-60.0f, 0.0f, 45.0f + ((int)facingDir * 90.0f));
	}

	public void RefreshHealth()
	{
		health = maxHealth;
	}

	[ContextMenu("Damage 1 Hitpoint")]
	public void Damage()
	{
		SetHealth(health - 1);
	}

	[ContextMenu("Heal 1 Hitpoint")]
	public void Heal()
	{
		SetHealth(health + 1);
	}

	public void SetHealth(int _hp)
	{
		health = _hp;
		if(anim.isActiveAndEnabled)
		{
			anim.SetInteger("Health", health);
		}
	}
}
