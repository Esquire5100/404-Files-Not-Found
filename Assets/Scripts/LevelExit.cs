using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) //If the Player hits the object...

        if (LvlManager.Instance.CanExitLevel())
        {
            SceneManager.LoadScene("Main Menu"); //...then bring player to the next level
        }
        else
        {
            Debug.Log($"Collect {LvlManager.Instance.filesRequired - LvlManager.Instance.FileCount} more files!");
        }
    }
}
