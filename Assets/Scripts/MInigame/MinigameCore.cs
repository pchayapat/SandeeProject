using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCore : MonoBehaviour
{
    public bool mission1;
    public bool mission2;
    public bool mission3;
    public bool mission1IsPlaying;
    public bool mission2IsPlaying;
    public bool mission3IsPlaying;
    
    //In game mode
    public static int scoreCount;
    void Start()
    {
        scoreCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStateNow();
    }

    public void CheckStateNow(){
        if(mission1 == false && mission2 == false && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Go to Mission 1");
        }
        if(mission1 == false && mission2 == false && mission3 == false &&
        mission1IsPlaying == true && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Playing Mission 1");
        }
        if(mission1 == true && mission2 == false && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Mission 1 was successed, Go to Mission 2");
        }
        if(mission1 == true && mission2 == false && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == true && mission3IsPlaying == false){
            Debug.Log("Playing Mission 2");
        }
        if(mission1 == true && mission2 == true && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Mission 2 was successed, Go to Mission 3");
        }
        if(mission1 == true && mission2 == true && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == true){
            Debug.Log("Playing Mission 3");
        }
        if(mission1 == true && mission2 == true && mission3 == true &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Mission 3 was successed");
        }
    }
}
