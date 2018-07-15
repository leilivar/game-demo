using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string songStr = Resources.Load("songInfo").ToString();
        Debug.Log(songStr);
        SongInfo song = SongInfo.LoadFromString(songStr);
        MusicManager.Instance.StartSong(song);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
