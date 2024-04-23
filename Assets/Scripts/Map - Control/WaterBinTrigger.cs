using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RedCube") || collision.gameObject.CompareTag("BlueCube") || collision.gameObject.CompareTag("GreenCube") || collision.gameObject.CompareTag("YellowCube"))
        {
            Missing.MissingCount -= 1;
            Destroy(gameObject);
        }
    }
}

