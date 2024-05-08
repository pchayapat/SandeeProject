
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject targetObject; // Assign your GameObject to this in the inspector

    void Update()
    {
        // Toggle GameObject visibility with T key
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleObject();
        }
    }

    void ToggleObject()
    {
        targetObject.SetActive(!targetObject.activeSelf);
    }
}