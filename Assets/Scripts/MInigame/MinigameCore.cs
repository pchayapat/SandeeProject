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
    public static bool mission3IsPlaying;
    [Header("UI Score")]
    public GameObject score;
    public GameObject star;
    [Header("Mission Window Mission 1")]
    public GameObject mission1Describe;
    public GameObject pass1;
    public GameObject warning1;
    [Header("Mission Window Mission 2")]
    public GameObject mission2Describe;
    public GameObject pass2;
    public GameObject lock2;
    public GameObject warning2;
    [Header("Mission Window Mission 3")]
    public GameObject mission3Describe;
    public GameObject pass3;
    public GameObject lock3;
    public GameObject warning3;
    public GameObject Mission3Challenge;
    public GameObject WaterfallSpawner;
    public GameObject gameOver;

    [Header("Mission Trigger")]
    public GameObject Mission1EnterTrigger;
    public GameObject Mission2EnterTrigger;
    public GameObject Mission3EnterTrigger;
    [Header("Mission Wall")]
    public GameObject Mission1Wall;
    public GameObject Mission2Wall;
    
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
            //Debug.Log("Go to Mission 1");
        }
        if(mission1 == false && mission2 == false && mission3 == false &&
        mission1IsPlaying == true && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Playing Mission 1");
            score.gameObject.SetActive(true);
            star.gameObject.SetActive(true);
        }
        if(mission1 == true && mission2 == false && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Mission 1 was successed, Go to Mission 2");
            pass1.SetActive(true);
            warning1.SetActive(false);
            warning2.SetActive(true);
            lock2.SetActive(false);
        }
        if(mission1 == true && mission2 == false && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == true && mission3IsPlaying == false){
            Debug.Log("Playing Mission 2"); 
        }
        if(mission1 == true && mission2 == true && mission3 == false &&
        mission1IsPlaying == false && mission2IsPlaying == false && mission3IsPlaying == false){
            Debug.Log("Mission 2 was successed, Go to Mission 3");
            pass2.SetActive(true);
            warning2.SetActive(false);
            warning3.SetActive(true);
            lock3.SetActive(false);
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

    public void Mission1Start()
    {
        Debug.Log("Mission 1 start");
        Mission1EnterTrigger.gameObject.SetActive(false);
        mission1Describe.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mission1IsPlaying = true;
    }
    public void Mission2Start()
    {
        Debug.Log("Mission 2 start");
        Mission2EnterTrigger.gameObject.SetActive(false);
        mission2Describe.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mission2IsPlaying = true;
    }
    public void Mission3Start()
    {
        Debug.Log("Mission 3 start");
        Mission3EnterTrigger.gameObject.SetActive(false);
        mission3IsPlaying = true;
        ScoreManager.scoreCount = 0;
        Missing.MissingCount = 5;
        TimeLevel.currentTime = 300;
        Mission3Challenge.gameObject.SetActive(true);
        WaterfallSpawner.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameOver.SetActive(false);
    }
    public void DestroyWallMission1()
    {
        Mission1Wall.SetActive(false);
    }
    public void DestroyWallMission2()
    {
        Mission2Wall.SetActive(false);
    }
    public void CloseDescribeUI()
    {
        mission1Describe.SetActive(false);
        mission2Describe.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
