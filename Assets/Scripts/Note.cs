using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float StartTime { private set; get; }
    public float ActionTime { private set; get; }
    public float Timer { get; private set; }
    [HideInInspector]
    public Line line;

    private BeatInfo beat;


	// Use this for initialization
	void Start () {
        Timer += Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        Timer = MusicManager.Instance.GetSongPlayedTime() - StartTime;
        if (StartTime + Timer - MusicManager.Instance.timeError > ActionTime)
        {
            EventManager.Instance.OnEventTrigger("InputFail", gameObject, null);
            Remove();
        }
	}

    public void SetBeatInfo(BeatInfo beatInfo,float actionTime){
        this.beat = beatInfo;
        this.ActionTime = actionTime;
        this.StartTime = StartTime = actionTime - line.actionTime;
    }

    public void SetLayer(int layer){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            return;
        spriteRenderer.sortingOrder = layer;
    }

    public float getStartTime(){
        return StartTime;
    }

    public void Remove(){
        if (line == null)
            return;
        line.RemoveNote(this);
        Destroy(gameObject);
    }

    public void OnPlayerInput(){
        Remove();
        float timeError = MusicManager.Instance.GetSongPlayedTime() - ActionTime;
        if(Mathf.Abs(timeError)<MusicManager.Instance.timeError){
            EventManager.Instance.OnEventTrigger("InputGood", null, null);
        }else{
            EventManager.Instance.OnEventTrigger("InputFail", null, null);
        }
        //EventManager.Instance.OnEventTrigger("OnNotePerformed", gameObject, beat);
    }
}
