using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameEventHandler(GameObject sender, object arg);
public class EventManager {
	private static EventManager manager;
	public static EventManager Instance{
		get{
			if (manager == null) {
				manager = new EventManager ();
			}
			return manager;
		}
	}

	private Dictionary<string,GameEventHandler> eventDic = new Dictionary<string, GameEventHandler> ();

	public void Register (string eventType, GameEventHandler handler) {
		if (eventDic.ContainsKey (eventType)) {
			handler = System.Delegate.Combine (eventDic [eventType], handler) as GameEventHandler;
			eventDic.Remove (eventType);
		}
		eventDic.Add (eventType, handler);
	}

	public void Remove (string eventType, GameEventHandler handler) {
		if (!eventDic.ContainsKey (eventType)) {
			return;
		}
		eventDic [eventType] = System.Delegate.Remove (eventDic [eventType], handler) as GameEventHandler;
	}

	public void OnEventTrigger (string eventType, GameObject sender, object arg) {
		if (!eventDic.ContainsKey (eventType)) {
			Debug.Log ("no event as " + eventType);
			return;
		}
		eventDic [eventType] (sender, arg);
	}
}
