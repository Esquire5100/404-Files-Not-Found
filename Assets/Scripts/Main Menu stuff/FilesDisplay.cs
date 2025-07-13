using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FilesDisplay : MonoBehaviour
{
    [Header("Level File Counters")] 
    public TextMeshProUGUI level1Text;
    public TextMeshProUGUI level2Text;
    public TextMeshProUGUI level3Text;

    [Header("Level Tabs")]
    public GameObject Tab1;
    public GameObject Tab2;
    public GameObject Tab3;

    void Start()
    {
        //Unlock tabs based on level completion
        Tab1.SetActive(PlayerPrefs.GetInt("Level 1_Complete", 0) == 1);
        Tab2.SetActive(PlayerPrefs.GetInt("Level 2_Complete", 0) == 1);
        Tab3.SetActive(PlayerPrefs.GetInt("Level 3_Complete", 0) == 1);

        var counts = FileProgressTracker.GetAllFileCounts();                          //Get all saved file counts from FileProgressTracker

        if (level1Text != null) // If the Level 1 UI text exists...
            level1Text.text = FileProgressTracker.GetFiles("Level 1").ToString();     //Show just the number

        if (level2Text != null)
            level2Text.text = FileProgressTracker.GetFiles("Level 2").ToString();

        if (level3Text != null)
            level3Text.text = FileProgressTracker.GetFiles("Level 3").ToString();

        Debug.Log("Level 1_Complete = " + PlayerPrefs.GetInt("Level 1_Complete", 0));

    }

    public void UnlockLevel1ForTesting()
    {
        Debug.Log("Test: UnlockLevel1ForTesting called");
        PlayerPrefs.SetInt("Level 1_Complete", 1);
        PlayerPrefs.Save();
    }

}