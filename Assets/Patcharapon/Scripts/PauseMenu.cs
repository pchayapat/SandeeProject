using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingMenuUI;
    public GameObject tutorialCanvas; // Reference to the Tutorial Canvas
    public Text keyGuideText; // Reference to the Text UI for key guide
    public GameObject objectToDeactivate;
    private bool isPaused = false;

    void Start()
    {
        // Initialize key guide text
        UpdateKeyGuide();
    }

    void UpdateKeyGuide()
    {
        keyGuideText.text = "Press Esc to toggle pause\nPress E to interact\nPress Y for tutorial";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Toggle tutorial canvas with Y key
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ToggleTutorialCanvas();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        tutorialCanvas.SetActive(false); // Make sure tutorial canvas is deactivated when resuming
        Time.timeScale = 1f;
        isPaused = false;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        settingMenuUI.SetActive(false); // Make sure setting menu is deactivated when pausing
        Time.timeScale = 0f;
        isPaused = true;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ToggleTutorialCanvas()
    {
        if (tutorialCanvas != null)
        {
            tutorialCanvas.SetActive(!tutorialCanvas.activeSelf);

            // Pause the game when showing tutorial
            if (tutorialCanvas.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void OnResumeButton()
    {
        ResumeGame();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
