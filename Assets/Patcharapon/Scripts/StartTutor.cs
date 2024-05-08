using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    public GameObject xxxCanvas; // Drag your xxx canvas GameObject here in the Inspector
    
    private void Start()
    {
        // Activate the xxx canvas when the game starts
        xxxCanvas.SetActive(true);

        // Pause the game and freeze the camera
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseCanvas()
    {
        // Deactivate the xxx canvas
        xxxCanvas.SetActive(false);

        // Resume the game and unfreeze the camera
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Disable this script
        this.enabled = false;
    }
}
