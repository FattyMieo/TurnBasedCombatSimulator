using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeObserverScript : MonoBehaviour, INotifiable
{
	public NotificationType observerType;
	public GameObject[] icons;
	public Text countText;

	// Use this for initialization
	void Start ()
	{
		UISubjectScript.Instance.Subscribe(this);
	}

	public void Notify (NotificationType nType, string value)
	{
		int _value;
		if(!int.TryParse(value, out _value)) return;

		if(observerType == nType)
		{
			for(int i = 0; i < icons.Length; i++)
			{
				icons[i].SetActive(i < _value);
			}

			countText.text = value;
		}
	}
}
