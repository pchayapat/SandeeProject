using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class healthSystem : MonoBehaviour
{
    public int heart = 3;
    
    void Update()
    {
        
        if (heart==0){
            SceneManager.LoadScene("GameOver");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Destroy(other);
            heart -= 1;
            HeartManager.heartCount-=1;
        }
    }
}
