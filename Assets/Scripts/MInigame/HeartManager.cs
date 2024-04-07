using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text heartText;
    public static int heartCount;
    void Start()
    {
        heartCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        heartText.text = "Heart : " + Mathf.Round(heartCount);
    }
}
