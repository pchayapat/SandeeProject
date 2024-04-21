using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterBinTrigger : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private int count = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RedCube"))
        {
            count--;
            textMeshPro.text = count.ToString();

            if (count <= 0)
            {
                // Perform any additional actions when count reaches 0
                Debug.Log("Count reached 0");
            }
        }
    }
}
