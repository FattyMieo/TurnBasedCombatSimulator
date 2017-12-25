using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubjectScript : MonoBehaviour, INotifiable
{
	public static UISubjectScript Instance;

	void Awake()
	{
		Instance = this;
	}

	private List<INotifiable> observers = new List<INotifiable>();

	public void Notify(NotificationType nType, string value)
	{
		for(int i = 0; i < observers.Count; i++)
		{
			observers[i].Notify(nType, value);
		}
	}

	public void Subscribe(INotifiable observerScript)
	{
		observers.Add(observerScript);
	}

	public void Unsubscribe(INotifiable observerScript)
	{
		observers.Remove(observerScript);
	}
}
