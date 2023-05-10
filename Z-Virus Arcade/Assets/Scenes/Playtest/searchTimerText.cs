using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class searchTimerText : MonoBehaviour
{
    public Text timerText;
 
    public float timer = 0;

    void Start(){
        timerText.enabled = false;
    }
 
    void Update(){
        
        //timerText.text = "Searching...";
 
    }

    public void setText(string input){
        timerText.text = input;
    }

    public void showTimer(){
        timerText.enabled = true;
    }

    public void endTimer(){
        timerText.enabled = false;
    }
}
