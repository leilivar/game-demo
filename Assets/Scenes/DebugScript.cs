using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using UnityEngine.UI;


public class DebugScript : MonoBehaviour {
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Text sliderText;
    public UnityEngine.UI.Text input;
    public UnityEngine.UI.Text errorText;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        slider.value = 1;
        OnSliderChanged();
        //GetComponent<InputField>().text = "";
        EventManager.Instance.Register("DebugBeat", PlayDebugBeat);
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
	}

    public void OnSliderChanged(){
        sliderText.text = "每拍的时间:" + slider.value;
    }

    public void PlayBeats(){
        try
        {
            SongInfo song = SongInfo.LoadDebugBeat(slider.value, input.text);
            MusicManager.Instance.StartSong(song);
        }catch(System.Exception e){
            errorText.text = "输入的节拍好像有点问题:)";
            CoroutineManager.Instance.DelayedAction(2, () =>
            {
                errorText.text = "";
            });
        }
    }

    public void PlayDebugBeat(GameObject sender, object arg){
        Debug.Log("beep");
        audio.Play();
    }
}
*/