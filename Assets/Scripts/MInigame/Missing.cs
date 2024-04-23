using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missing : MonoBehaviour
{
    public Text MissingText;
    public static int MissingCount;
    //Start is called before the first frame update
    public GameObject gameOver;
    public GameObject Mission3Challenge;
    public MinigameCore minigameCore;

    void Start()
    {
        MissingCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        MissingText.text = "" + Mathf.Round(MissingCount);

        if(MissingCount == 0){
            gameOver.SetActive(true);
            Mission3Challenge.SetActive(false);
            //WaterfallSpawner.SetActive(false);
            MinigameCore.mission3IsPlaying = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Game Over!");
        }
    }
}
