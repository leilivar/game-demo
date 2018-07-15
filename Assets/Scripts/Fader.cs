using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    private Image image;
    private Image FadeImage { 
        get {
            if (image == null)
                image = GetComponent<Image>();
            return image;
        } }

	// Use this for initialization
	void Start () {
        EventManager.Instance.Register("SongOver", MusicStop);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MusicStop(GameObject sender,object obj){
        FadeOut(1.5f, () => {
            SceneManager.LoadScene("StartScene");
        });
    }

    public void FadeIn(float time,Action fadeInAction){
        Func<bool> condition = () => {
            return FadeImage.color.a > 0; 
        };
        float timer = 0;
        Action action = () => {
            float a = Mathf.Lerp(1, 0, timer / time);
            timer += Time.deltaTime;
            FadeImage.color = new Color(0, 0, 0, a);
        };
        CoroutineManager.Instance.Loop(condition,action,fadeInAction);
    }

    public void FadeOut(float time,Action fadeOutAction){
        Func<bool> condition = () => {
            return FadeImage.color.a < 1;
        };
        float timer = 0;
        Action action = () => {
            float a = Mathf.Lerp(0, 1, timer / time);
            timer += Time.deltaTime;
            FadeImage.color = new Color(0, 0, 0, a);
        };
        CoroutineManager.Instance.Loop(condition, action, fadeOutAction);
    }
}
