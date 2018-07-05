using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineManager : Singleton <CoroutineManager> {
	
	public void DelayedAction(float time, Action action){
		StartCoroutine (_DelayedAction (time, action));
	}

	private IEnumerator _DelayedAction(float time, Action action){
		yield return new WaitForSeconds (time);
		action ();
	}

	public void LoopAction (Func<bool> condition, Action action){
		StartCoroutine (_LoopAction (condition, action));
	}

	private IEnumerator _LoopAction (Func<bool> condition, Action action){
		while (condition()) {
			action ();
			yield return new WaitForEndOfFrame ();
		}
	}
}
