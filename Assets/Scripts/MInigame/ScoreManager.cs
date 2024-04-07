using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public static int scoreCount;
    public static int levelPlayer;
    public GameObject trashLevel1;
    public GameObject trashLevel2;
    public GameObject trashLevel3;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    void Start()
    {
        levelPlayer = 1;
        scoreCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + Mathf.Round(scoreCount);
        // if(Level1.gameObject.activeSelf == true && scoreCount>=10){
        //     scoreCount = 0;
        //     levelPlayer = 2;
        //     TimeLevel.currentTime = 60f;
        //     trashLevel1.gameObject.SetActive(false);
        //     Level1.gameObject.SetActive(false);
        //     trashLevel2.gameObject.SetActive(true);
        //     Level2.gameObject.SetActive(true);
        // }
        // if(Level2.gameObject.activeSelf == true && scoreCount>=15){
        //     scoreCount = 0;
        //     levelPlayer = 3;
        //     TimeLevel.currentTime = 60f;
        //     trashLevel2.gameObject.SetActive(false);
        //     Level2.gameObject.SetActive(false);
        //     trashLevel3.gameObject.SetActive(true);
        //     Level3.gameObject.SetActive(true);

        // }
    }
}
