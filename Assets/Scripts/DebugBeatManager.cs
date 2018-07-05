using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugBeatManager {
    static DebugBeatManager(){
        EventManager.Instance.Register("DebugBeat", DebugBeat);
    }
    public static void DebugBeat(GameObject sender, object arg){
        Debug.Log("beep");
    }
}
