using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager> {
    public float timeError = 0.1f;

    private SongInfo songInfo;
    private float startTime = 0;
    private BeatInfo lastHitBeat;//最后一次正确击打到的节拍
    private BeatInfo lastPerformedBeat;
    private int nextBeatIndex = 0;

    private bool isPlaying = false;
    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlaying)
            return;
        BeatInfo next = songInfo.GetBeatInfo(nextBeatIndex);
        float currentTime = GetSongPlayedTime();
        if (next != null)
        {
            float nextBeatTime = songInfo.GetBeatInfoTime(next);
            if (next != lastPerformedBeat)
            {
                if (currentTime > nextBeatTime)
                {
                    //刚刚超过下一拍，执行这个拍子的行为
                    foreach (string anction in next.actions)
                    {
                        EventManager.Instance.OnEventTrigger(anction, null, null);
                    }
                    lastPerformedBeat = next;
                    nextBeatIndex++;
                }
            }
        }

        //应该分成两个队列，一个输入队列，一个行为队，以下处理输入队列
        BeatInfo nearest = songInfo.GetNeareatInputBeat(GetSongPlayedTime());
        float nearestTime = songInfo.GetBeatInfoTime(nearest);
        if(IsPlayerPressedButton()){
            if(nearest == lastHitBeat){
                //已经处理过这拍了，错误的输入
            }else{
                float error = currentTime - nearestTime;
                if(Mathf.Abs(error)<=timeError){
                    //正确的输入
                    lastHitBeat = nearest;
                    foreach(string action in nearest.actions){
                        EventManager.Instance.OnEventTrigger(action, null, null);
                    }
                }else{
                    //错误的输入
                }
            }
        }else{
            if(currentTime+timeError>nearestTime){
                //玩家错过了输入时机

            }
        }
        
	}

    public void StartSong(SongInfo song){
        if (song.IsBeatInfosEmpty())
            return;
        isPlaying = true;
        songInfo = song;
        startTime = Time.time;
        nextBeatIndex = 0;
        lastHitBeat = null;
    }

    public void StopSong(){
        isPlaying = false;
    }

    public float GetSongPlayedTime(){
        return Time.time - startTime;
    }

    private bool IsPlayerPressedButton(){
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
}
