using UnityEngine;

public class DestroyChildCollider : MonoBehaviour
{
    void Update()
    {
        // Iterate through all child objects of this GameObject
        foreach (Transform child in transform)
        {
            // Check if the child has a BoxCollider component
            BoxCollider boxCollider = child.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                // Destroy the BoxCollider component
                Destroy(boxCollider);
            }
        }
    }
}
