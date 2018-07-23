using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeat : MonoBehaviour {
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        EventManager.Instance.Register("OnBeat", OnBeatEvent);
        EventManager.Instance.Register("InputGood", OnInputSuccess);
        EventManager.Instance.Register("InputFail", OnInputFail);
        EventManager.Instance.Register("OnWomanAction", OnWomanAction);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnBeatEvent(GameObject sender, object arg){
        animator.SetTrigger("OnBeat");
    }

    private void OnInputSuccess(GameObject sender,object arg){
        animator.SetTrigger("Success");
    }

    private void OnInputFail(GameObject sender,object arg){
        animator.SetTrigger("Fail");
    }

    private void OnWomanAction(GameObject sender,object arg){
        animator.SetTrigger("Action");
    }
}
