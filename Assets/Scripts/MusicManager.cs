using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager> {
    public float timeError = 0.15f;

    private SongInfo songInfo;
    private float startTime = 0;
    private BeatInfo lastHitBeat;//最后一次正确击打到的节拍
    private BeatInfo lastPerformedBeat;
    private int nextBeatIndex = 0;
    private int onBeatIndex = 1;
    private AudioSource audio;

    List<BeatInfo> performed = new List<BeatInfo>();

    private bool isPlaying = false;
    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlaying)
            return;

        float currentTime = GetSongPlayedTime();
        int currentBeat = songInfo.GetBeatIndex(currentTime);
        if(currentBeat>=onBeatIndex){
            onBeatIndex++;
            EventManager.Instance.OnEventTrigger("OnBeat", gameObject, null);
        }


        BeatInfo next = songInfo.GetBeatInfo(nextBeatIndex);

        if (next == null)
        {
            //isPlaying = false;
            //EventManager.Instance.OnEventTrigger("SongOver", gameObject, null);
            return;
        }
        if (performed.Contains(next))
        {
            nextBeatIndex++;
            return;
        }

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

        //BeatInfo nearest = songInfo.GetNeareatInputBeat(GetSongPlayedTime());
        /*
        if (nearest == lastHitBeat)
            return;
        float nearestTime = songInfo.GetBeatInfoTime(nearest);
        //MergeParameter mp = new MergeParameter { nearestBeatPosition = nearestTime };

        if (InputManager.HasUserInput(null))
        {
            if (InputManager.IsPlayerInputCorrect(null))
            {
                if (nearest == lastHitBeat)
                {
                    //已经处理过这拍了，错误的输入
                }
                else
                {
                    float error = currentTime - nearestTime;
                    if (Mathf.Abs(error) <= timeError)
                    {
                        //正确的输入
                        Debug.Log("right on");
                        performed.Add(nearest);
                        //mp.catType = nearest.inputs;
                        EventManager.Instance.OnEventTrigger("InputGood", gameObject, null);
                        lastHitBeat = nearest;
                        foreach (string action in nearest.actions)
                        {
                            EventManager.Instance.OnEventTrigger(action, null, null);
                        }
                    }
                    else
                    {
                        //错误的输入
                    }
                }
            }
            else{
                //错误的输入，暂时不考虑惩罚
            }
        }
        else{
            
        }
        */

        /*
        BeatInfo latestBeat = songInfo.GetBeatInfo(nextBeatIndex);
        if(latestBeat == null){
            isPlaying = false;
            EventManager.Instance.OnEventTrigger("SongOver", gameObject, null);
            return;
        }
        if(performed.Contains(latestBeat)){
            nextBeatIndex++;
            return;
        }
        float latestTime = songInfo.GetBeatInfoTime(latestBeat);
        if (currentTime - timeError > latestTime)
        {
            //玩家错过了输入时机
            Debug.Log("miss");
            performed.Add(latestBeat);
            EventManager.Instance.OnEventTrigger("InputMiss", gameObject, null);
            lastHitBeat = nearest;
        }
        */
        
	}

    public void StartSong(SongInfo song){
        if (song.IsBeatInfosEmpty())
            return;
        if(audio==null){
            audio = GetComponent<AudioSource>();
        }
        performed.Clear();
        isPlaying = true;
        audio.time = 0;
        audio.Play();
        songInfo = song;
        startTime = Time.time;
        nextBeatIndex = 0;
        lastHitBeat = null;
        NoteManager.Instance.StartSong(song);
    }

    public void StopSong(){
        //isPlaying = false;
    }

    public float GetSongPlayedTime(){
        //return Time.time - startTime;
        return audio.time;
    }

    private bool IsPlayerPressedButton(){
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
}
