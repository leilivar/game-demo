using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongInfo {
    public static readonly string FILE_DIC = Application.dataPath + "/SongInfos/";
    private float offset = 0;
    public float oneBeatTime { get;private set; }
    public List<BeatInfo> beatInfos = new List<BeatInfo>();

    public static SongInfo Load(string fileName){
        SongInfo newSong = new SongInfo();
        string filePath = FILE_DIC + fileName;
        using (StreamReader reader = new StreamReader(filePath)){
            string line = reader.ReadLine();
            newSong.SetCommonInfo(line);

            line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line)){
                newSong.AddBeatInfo(line);
                line = reader.ReadLine();
            }
        }
        return newSong;
    }

    public static SongInfo LoadFromString(string info){
        SongInfo newSong = new SongInfo();
        string[] line = info.Split('\n');
        if (line.Length <= 0)
            return newSong;
        newSong.SetCommonInfo(line[0]);
        for (int i = 1; i < line.Length;i++){
            newSong.AddBeatInfo(line[i]);
        }

        return newSong;
    }

    private void SetCommonInfo(string firstLine){
        string[] info = firstLine.Split(',');
        offset = float.Parse(info[0]);
        float speed = float.Parse(info[1]);
        oneBeatTime = 60f / speed;
    }

    private void AddBeatInfo(string line){
        string[] info = line.Split(';');
        float beatPosition = float.Parse(info[0]);
        BeatInfo beatInfo = new BeatInfo();
        beatInfo.beatPosition = beatPosition;
        /*
        for (int i = 1; i < info.Length;i++){
            string[] one = info[i].Split(',');
            beatInfo.inputs.Add(one[0]);
            beatInfo.lines.Add(one[1]);
        }
        */
        beatInfos.Add(beatInfo);
    }

    public bool IsBeatInfosEmpty(){
        return beatInfos.Count <= 0;
    }

    public static SongInfo LoadDebugBeat(float oneBeatTime, string beatInfoStr){
        SongInfo newSong = new SongInfo();
        if (string.IsNullOrEmpty(beatInfoStr))
            return newSong;
        newSong.oneBeatTime = oneBeatTime;
        string[] beats = beatInfoStr.Split(',');
        float flag = 0;
        foreach(string beat in beats){
            if (string.IsNullOrEmpty(beat))
                continue;
            BeatInfo info = new BeatInfo();
            string b = beat;
            if(beat[0] == '-'){
                b = beat.Substring(1);
                info.beatLength= float.Parse(b);
            }else{
                info.beatLength = float.Parse(b);
                info.actions.Add("DebugBeat");
            }
            info.beatPosition = flag + info.beatLength;
            flag += info.beatLength;
            newSong.beatInfos.Add(info);
        }
        return newSong;
    }

    public float GetTimeLength(){
        float time = offset;
        foreach (BeatInfo bi in beatInfos)
        {
            time += bi.beatLength * oneBeatTime;
        }
        return time;
    }

    public BeatInfo GetBeatInfo(int index)
    {
        if (index < 0 || beatInfos.Count <= index)
            return null;
        return beatInfos[index];
    }

    /// <summary>
    /// 得到此节拍在音乐中的时间
    /// </summary>
    /// <returns>The beat info time.</returns>
    /// <param name="info">Info.</param>
    public float GetBeatInfoTime(BeatInfo info){
        return info.beatPosition * oneBeatTime + offset;
    }

    public BeatInfo GetNeareatInputBeat(float time){
        if (beatInfos.Count <= 0)
            return null;
        BeatInfo nearest = beatInfos[0];
        float timeError = time - nearest.beatPosition * oneBeatTime - offset;
        for (int i = 1; i < beatInfos.Count;i++){
            float e = time - beatInfos[i].beatPosition * oneBeatTime - offset;
            if(Mathf.Abs(e)<Mathf.Abs(timeError)){
                nearest = beatInfos[i];
                timeError = e;
            }
        }
        return nearest;
    }

    public float GetTimeError(BeatInfo beat,float currentTime){
        return currentTime - (beat.beatPosition * oneBeatTime + offset);
    }

}

public class BeatInfo{
    public float beatLength = 1;
    public float beatPosition;//在整个音乐里是第几拍
    public string actionType = string.Empty;
    public List<string> actions = new List<string>();
}
