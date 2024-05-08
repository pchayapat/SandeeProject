using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public static int scoreCount;
    public int goalMission1 = 4;
    public int goalMission2 = 8;
    public MinigameCore minigameCore;
    public WaterSpawn waterSpawn;
    void Start()
    {
        scoreCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // scoreText.text = "Score : " + Mathf.Round(scoreCount);
        scoreText.text = "" + Mathf.Round(scoreCount);

        if(scoreCount >= goalMission1 && minigameCore.mission1 == false){
            minigameCore.mission1 = true;
            minigameCore.mission1IsPlaying = false;
            minigameCore.DestroyWallMission1();
        }
        if(scoreCount >= goalMission2 && minigameCore.mission2 == false){
            minigameCore.mission2 = true;
            minigameCore.mission2IsPlaying = false;
            minigameCore.DestroyWallMission2();
        }
        if(scoreCount == 20)
        {
            waterSpawn.spawnInterval =2.5f;
        }
        if(scoreCount == 30)
        {
            waterSpawn.spawnInterval = 2;
        }
        if(scoreCount == 40)
        {
            waterSpawn.spawnInterval = 1.5f;
        }

    }
}
