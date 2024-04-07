using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCheck : MonoBehaviour
{
    public static bool Mission1;
    public static bool Mission2;
    public static bool Mission3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        if(Mission1==true){
            Debug.Log("Congrats!");
        }
    }
}
