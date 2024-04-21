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
    public static float startingTime = 60f;
    [SerializeField] Text countdownText;
    public MinigameCore minigameCore;
    public GameObject Level1;
    public GameObject Level2;
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1*Time.deltaTime;
        countdownText.text = "Time : " + Mathf.Round(currentTime);

        if(currentTime <= 0)
        {
            currentTime = 0;
            if(ScoreManager.scoreCount < 20)
            {
                //SceneManager.LoadScene("GameOver");
                Debug.Log("GameOver");
            }
        }
    }
}
