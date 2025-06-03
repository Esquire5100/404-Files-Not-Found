using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{

    public GameObject mobileUI;
    public bool showMobileUI;

    
    // Start is called before the first frame update
    void Start()
    {
        mobileUI.SetActive(showMobileUI);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnEnable()
    {
        SceneManager.sceneloaded += OnSceneLoaded();
    }

    private void onDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OriginalScene")
        {
            //Reset or initialize necessary states for the original scene
        }
    }*/
}
