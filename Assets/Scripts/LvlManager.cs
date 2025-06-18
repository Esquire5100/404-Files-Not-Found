using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; //script can access the UI element in unity

public class LvlManager : MonoBehaviour
{

    public GameObject mobileUI;
    public bool showMobileUI;

    public int FileCount; //Keep track of no of files player collected

    public TextMeshProUGUI FileCounter; //declare FileCounter as a UI text type

    // Start is called before the first frame update
    void Start()
    {
        mobileUI.SetActive(showMobileUI);

        FileCounter.text = "x " + FileCount; //as the game continues the number and text will change (from 0 to however many gems); hence why we use FileCounter.text
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

    public void AddFiles(int FilestoAdd)
    {
        FileCount += FilestoAdd; //means 'FileCount = FileCount + FilestoAdd;' -> the count will increase

        FileCounter.text = "x " + FileCount; //whenever file is collected, update and display it in the UI 
    }

    public void OpenPopup()
    {
        SceneManager.LoadSceneAsync("Captcha", LoadSceneMode.Additive);
    }

    public void ClosePopup()
    {
        SceneManager.UnloadSceneAsync("Captcha");
    }

}
