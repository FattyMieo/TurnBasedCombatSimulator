using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
	[ContextMenuItem("Update", "UpdateSortingLayer")]
	public string sortingLayer;
	[ContextMenuItem("Update", "UpdateSortingOrder")]
	public int sortingOrderIso;
	[ContextMenuItem("Update", "UpdateColor")]
	public Color color;
	public SpriteRenderer[] rends;
	private float lightFactor = 50.0f / 255.0f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void UpdateSortingLayer()
	{
		SetSortingLayer(sortingLayer);
	}

	public void SetSortingLayer(string _sortingLayer)
	{
		sortingLayer = _sortingLayer;

		for(int i = 0; i < rends.Length; i++)
		{
			rends[i].sortingLayerName = sortingLayer;
		}
	}

	public void UpdateSortingOrder()
	{
		SetSortingOrder(sortingOrderIso);
	}

	public void SetSortingOrder(int _sortingOrder)
	{
		sortingOrderIso = _sortingOrder;

		for(int i = 0; i < rends.Length; i++)
		{
			rends[i].sortingOrder = (sortingOrderIso * rends.Length) + i;
		}
	}

	public void UpdateColor()
	{
		SetColor(color);
	}

	public void SetColor(Color newColor)
	{
		color = newColor;

		for(int i = 0; i < rends.Length; i++)
		{
			float r = color.r - (lightFactor * i);
			float g = color.g - (lightFactor * i);
			float b = color.b - (lightFactor * i);

			rends[i].color = new Color(r, g, b);
		}
	}
}
