using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObserverScript : MonoBehaviour, INotifiable
{
	public NotificationType observerType;
	public UnitScript observerUnitScript;

	// Use this for initialization
	void Start ()
	{
		observerUnitScript = GetComponent<UnitScript>();
		UISubjectScript.Instance.Subscribe(this);
	}

	public void Notify (NotificationType nType, string value)
	{
		int _value;
		if(!int.TryParse(value, out _value)) return;

		if(observerType == nType)
		{
			observerUnitScript.SetHealth(_value);
		}
	}
}
