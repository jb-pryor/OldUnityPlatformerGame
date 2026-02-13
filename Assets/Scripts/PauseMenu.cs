using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false; // Tracks if the game is paused
    public GameObject pauseMenuPanel; // Assign the Pause Menu Panel in the Inspector
    public GameObject helpMenu; // Assign the Help Menu Panel in the Inspector

    private void Update()
    {
        // Check for the pause button (Escape key or Start button)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume game time
        IsPaused = false;

        // Re-enable player controls
        GameManager.Instance.playerObject.GetComponent<PlayerMovement>().SetCanMove(true);
        //GameManager.Instance.playerObject.GetComponent<PlayerAttack>().SetCanAttack(true);
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Freeze game time
        IsPaused = true;

        // Disable player controls
        GameManager.Instance.playerObject.GetComponent<PlayerMovement>().SetCanMove(false);
        //GameManager.Instance.playerObject.GetComponent<PlayerAttack>().SetCanAttack(false);
    }

    public void ShowHelp()
    {
        helpMenu.SetActive(true); // Show the help menu

        // Pause the game while showing help
        Time.timeScale = 0f; 
        IsPaused = true; // Set the paused state

        // Disable player controls
        GameManager.Instance.playerObject.GetComponent<PlayerMovement>().SetCanMove(false);
        //GameManager.Instance.playerObject.GetComponent<PlayerAttack>().SetCanAttack(false);

        // Hide the pause menu
        pauseMenuPanel.SetActive(false);
    }

    public void HideHelp()
    {
        helpMenu.SetActive(false); // Hide the help menu

        // Show the pause menu again
        pauseMenuPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Ensure time is resumed before switching scenes
        SceneManager.LoadScene("Menu");
    }
}
