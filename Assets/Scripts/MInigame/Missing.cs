using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missing : MonoBehaviour
{
    public Text MissingText;
    public static int MissingCount;
    // Start is called before the first frame update
    void Start()
    {
        MissingCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MissingText.text = "Heart : " + Mathf.Round(MissingCount);
    }
}
