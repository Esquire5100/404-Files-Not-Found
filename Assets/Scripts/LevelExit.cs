using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelExit : MonoBehaviour
{
    public SoundEffectManager soundEffectManager;

    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
         soundEffectManager = FindObjectOfType<SoundEffectManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) //If the Player hits the object...
        {
            SoundEffectManager.Instance.StopMusic();
        }

        if (LvlManager.Instance.CanExitLevel())
        {
            SceneManager.LoadScene(sceneToLoad); //...then bring player to the next level
        }
        else
        {
            Debug.Log($"Collect {LvlManager.Instance.filesRequired - LvlManager.Instance.FileCount} more files!");
        }
    }


}
