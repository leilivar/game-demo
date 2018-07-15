using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
    public float actionTime = 1f;
    public float endTime = 2f;
    public List<Note> noteList = new List<Note>();

    Transform startPoint;
    Transform anctionPoint;
    Transform endPoint;


	// Use this for initialization
	void Start () {
        startPoint = transform.Find("Start");
        anctionPoint = transform.Find("Action");
        endPoint = transform.Find("End");
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = noteList.Count - 1; i >= 0;i--){
            Note note = noteList[i];
            note.transform.position = GetPosition(note.Timer);
            if (note.StartTime + note.Timer > note.ActionTime+MusicManager.Instance.timeError)
            {
                note.Remove();
            }
        }
	}

    public Vector3 GetPosition(float time){
        if(time < actionTime){
            return Vector3.Lerp(startPoint.position, anctionPoint.position, time / actionTime);
        }else{
            time = time - actionTime;
            return Vector3.Lerp(anctionPoint.position, endPoint.position, time / (endTime - actionTime));
        }
    }

    public void AddNote(Note note){
        if (noteList.Contains(note))
            return;
        noteList.Add(note);
    }

    public void RemoveNote(Note note){
        noteList.Remove(note);
    }


    public Vector3 getMergePoint()
    {
        if(anctionPoint == null){
            anctionPoint = transform.Find("Action");
        }
        return anctionPoint.position;
    }
}
