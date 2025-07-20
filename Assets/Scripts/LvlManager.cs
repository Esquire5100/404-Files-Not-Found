using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; //script can access the UI element in unity

public class LvlManager : MonoBehaviour
{
    public static LvlManager Instance;  //Singleton reference

    public GameObject mobileUI;
    public bool showMobileUI;

    public int FileCount; //Keep track of no of files player collected

    public TextMeshProUGUI FileCounter; //declare FileCounter as a UI text type

    public int filesRequired = 0;
  

    private void Awake()
    {
        //If no instance exists, keep this one and prevent it from being destroyed
        if (Instance == null)
        {
            Instance = this;

            //Make sure this GameObject is at the root level, otherwise DontDestroyOnLoad will warn
            if (transform.parent == null)
            {
                DontDestroyOnLoad(gameObject); //Prevent it from being destroyed on scene change
            }
            else
            {
                Debug.LogWarning("LvlManager must be on a root GameObject for DontDestroyOnLoad to work.");
            }

            // Listen for scene loads to re-assign UI references
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); //Destroy duplicates
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mobileUI.SetActive(showMobileUI);

        //Check if FileCounter is missing and try to find it in the scene (debugging to prevent the counter from being null)
        if (FileCounter == null)
        {
            GameObject counterObj = GameObject.Find("FileCounter"); // This must match the name of the TMP UI element in your canvas
            if (counterObj != null)
                FileCounter = counterObj.GetComponent<TextMeshProUGUI>();
        }

        /*//Load saved FileCount
        if (PlayerPrefs.HasKey("FileCount"))
        {
            FileCount = PlayerPrefs.GetInt("FileCount");
        }*/

        UpdateUI();
    }

    public void AddFiles(int FilestoAdd)
    {
        FileCount += FilestoAdd; //essentially means "x + 1" in simple math terms

        /*PlayerPrefs.SetInt("FileCount", FileCount); // Auto-save new value
        PlayerPrefs.Save();*/

        UpdateUI();
    }

    // Update is called once per frame
    private void UpdateUI()
    {
        if (FileCounter != null)
        {
            FileCounter.text = "x " + FileCount;
            //GameObject.Find("FileCounter").GetComponent<TextMeshProUGUI>;
        }
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

    // Re-assign FileCounter when the scene is loaded again (e.g., returning from Captcha)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to re-link the FileCounter text component
        GameObject counterObj = GameObject.Find("FileCounter");
        if (counterObj != null)
        {
            FileCounter = counterObj.GetComponent<TextMeshProUGUI>();
            UpdateUI(); // Refresh UI once found
        }
        else
        {
            Debug.LogWarning("FileCounter not found in scene " + scene.name);
        }
    }

    private void OnDestroy()
    {
        //Unsubscribe from event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Open the captcha popup (ontop of current Level scene)
    public void OpenPopup()
    {
        SceneManager.LoadSceneAsync("Captcha", LoadSceneMode.Additive);
    }

    //Close the popup
    public void ClosePopup()
    {
        SceneManager.UnloadSceneAsync("Captcha");

        UpdateUI(); //refresh the FileCounter
    }

    //Call this when the level ends to save how many files were stolen in this run
    public void SaveRunToTotal()
    {
        string levelName = SceneManager.GetActiveScene().name; //Use current scene name as the level identifier
        FileProgressTracker.AddFiles(levelName, FileCount);    //Add this run�s files to the total tracker
    }

    public void MarkLevelComplete(string levelname)
    {
        PlayerPrefs.SetInt(levelname + "_Complete", 1);
        PlayerPrefs.Save();
    }

    public bool CanExitLevel()
    {
        return FileCount >= filesRequired;
    }
}