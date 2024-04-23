using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public static float currentTime = 0f;
    //public static float startingTime = 60f;
    public static float startingTime = 300f;
    [SerializeField] Text countdownText;
    public MinigameCore minigameCore;
    public GameObject gameOver;
    
    public GameObject Mission3Challenge;
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1*Time.deltaTime;
        countdownText.text = "" + Mathf.Round(currentTime);

        if(currentTime <= 0)
        {
            currentTime = 0;
            if(ScoreManager.scoreCount < 20)
            {
                //SceneManager.LoadScene("GameOver");
                gameOver.SetActive(true);
                Mission3Challenge.SetActive(false);
                //WaterfallSpawner.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                MinigameCore.mission3IsPlaying = false;
                Debug.Log("Time Out");
            }
        }
    }
}
