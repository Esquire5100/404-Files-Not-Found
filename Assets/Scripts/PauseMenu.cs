using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }

        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze time to pause the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  //Unfreeze time to continue game
        isPaused = false;
    }
    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Ensure time is running again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
}
