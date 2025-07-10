using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseScreen;
    private Scottie_Controller Scottie;
    private LvlManager LvlManager;

    // Start is called before the first frame update
    void Start()
    {
        Scottie = FindObjectOfType<Scottie_Controller>();
        LvlManager = FindObjectOfType<LvlManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0) //Check if game is paused
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0; //freeze game
        pauseScreen.SetActive(true);
        Scottie.canMove = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f; //Resume back to normal realtime
        pauseScreen.SetActive(false);
        Scottie.canMove = true;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }
}
