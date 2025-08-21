using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelExit : MonoBehaviour
{
    public SoundEffectManager soundEffectManager;
    public string sceneToLoad;
    //for crossfade transitions
    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
         soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) //If the Player hits the object...
        {
            SoundEffectManager.Instance.StopMusic();
            transition = GameObject.Find("Fading Image").GetComponent<Animator>();
            transition.SetTrigger("Start");            //transition = GameObject.Find("FadingImage").GetComponent<Animator>();
            StartCoroutine(levelLoad());
        }

        /*if (LvlManager.Instance.CanExitLevel())
        {
            SceneManager.LoadScene(sceneToLoad); //...then bring player to the next level
            startCoroutine(levelLoad)();
        }
        else
        {
            Debug.Log($"Collect {LvlManager.Instance.filesRequired - LvlManager.Instance.FileCount} more files!");
        }*/
    }

    IEnumerator levelLoad()
        {
            //transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneToLoad);
        }
}
