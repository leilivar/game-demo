using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager{
    private static Dictionary<string, bool> inputDic = new Dictionary<string, bool>();
    private static Dictionary<string, Coroutine> coroutineDic = new Dictionary<string, Coroutine>();
    private static Dictionary<string, float> timer = new Dictionary<string, float>();
    private static List<string> inputNames = new List<string> { "input1", "input2", "input3" };
    private static readonly float TIME_DELAY = 0.05f;
    static InputManager(){
        inputDic = new Dictionary<string, bool>{
            {"input1",false},
            {"input2",false},
            {"input3",false}
        };
        timer = new Dictionary<string, float>{
            {"input1",0},
            {"input2",0},
            {"input3",0}
        };
    }

    public static bool HasUserInput(List<string> input){
        bool userInput = false;
        foreach(string s in input){
            userInput = userInput || Input.GetButtonDown(s);
        }

        foreach(string s in inputNames){
            timer[s] -= Time.deltaTime;
            if (timer[s] <= 0)
                inputDic[s] = false;
        }

        return userInput;
    }

    public static bool IsPlayerInputCorrect(List<string> input){
        int finger = 0;
        foreach (string s in inputNames)
        {
            if (!input.Contains(s) && inputDic[s])
            {
                return false;
            }

            if (Input.GetButtonUp(s))
                inputDic[s] = false;
            if (Input.GetButton(s))
                finger++;
        }
        if (finger > input.Count)
            return false;
        foreach(string inputName in input){
            if(Input.GetButtonDown(inputName)){
                inputDic[inputName] = true;
                timer[inputName] = TIME_DELAY;
            }
        }
        bool correct = true;
        foreach(string i in input){
            correct = correct && inputDic[i];
        }
        if(correct){
            foreach (string i in input)
            {
                inputDic[i] = false;
            }
            return true;
        }
        return false;
    }

}
