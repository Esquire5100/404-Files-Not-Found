using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
