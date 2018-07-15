using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : Singleton<NoteManager> {
    public Note note;

    private List<BeatInfo> beatInfos = new List<BeatInfo>();
    private List<BeatInfo> addedBeats = new List<BeatInfo>();
    private SongInfo song;
    private int index = 0;
    private Line line;
	// Use this for initialization
	void Start () {
        //DontDestroyOnLoad(gameObject);
        addedBeats.Clear();
        line = GameObject.Find("Line").GetComponent<Line>();
	}
	
	// Update is called once per frame
	void Update () {
        if (beatInfos.Count < 0)
            return;
        float songPlayedTime = MusicManager.Instance.GetSongPlayedTime();
        if (song == null)
            return;
        BeatInfo beat = song.GetBeatInfo(index);
        if (beat == null)
            return;
        if (addedBeats.Contains(beat))
            return;
        float beatTime = song.GetBeatInfoTime(beat);
        float beatShowTime = beatTime - line.actionTime;
        if (beatShowTime > songPlayedTime)
            return;
        Note note = CreateNote(null);
        note.SetBeatInfo(beat, beatTime);
        line.AddNote(note);
        index++;
        addedBeats.Add(beat);

	}

    public void StartSong(SongInfo song){
        this.song = song;
    }

    public Note CreateNote(string type){
        Note n = Instantiate(note.gameObject).GetComponent<Note>();
        n.line = line;
        return n;
    }
}