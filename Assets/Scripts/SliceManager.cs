using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceManager : MonoBehaviour {
    public float minDistance = 50;
    Vector2 startPosition;
    float startTime;
    bool sliceStart = false;
    private Camera camera;
	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            startPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("startPosition:" + startPosition);
            startTime = Time.time;
            sliceStart = true;
        }
        if(Input.GetMouseButtonUp(0)){
            Vector2 endPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("endPosition:" + endPosition);
            sliceStart = false;
            if (Vector2.Distance(startPosition, endPosition) > 2)
            {
                Vector2 dir = endPosition - startPosition;
                RaycastHit2D hit2D = Physics2D.Raycast(startPosition, dir, 50);
                if (!hit2D)
                    return;
                Debug.Log("up");
                Note note = hit2D.collider.GetComponent<Note>();
                if(note!=null){
                    note.OnPlayerInput();
                }
            }
        }
	}
}
