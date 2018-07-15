using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineManager : Singleton <CoroutineManager> {
	
    public Coroutine Delay(float time, Action action){
		return StartCoroutine (_DelayedAction (time, action));
	}

	private IEnumerator _DelayedAction(float time, Action action){
		yield return new WaitForSeconds (time);
		action ();
	}

    public Coroutine Loop (Func<bool> condition, Action action,Action endAction){
        return StartCoroutine(_LoopAction(condition, action, endAction));
	}

    private IEnumerator _LoopAction (Func<bool> condition, Action action,Action endAction){
		while (condition()) {
			action ();
			yield return new WaitForEndOfFrame ();
		}
        if (endAction != null)
            endAction();
	}
}
