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

    void Start()
    {
        var counts = FileProgressTracker.GetAllFileCounts();                          //Get all saved file counts from FileProgressTracker

        if (level1Text != null) // If the Level 1 UI text exists...
            level1Text.text = FileProgressTracker.GetFiles("Level 1").ToString();     //Show just the number

        if (level2Text != null)
            level2Text.text = FileProgressTracker.GetFiles("Level 2").ToString();

        if (level3Text != null)
            level3Text.text = FileProgressTracker.GetFiles("Level 3").ToString();

        //You can add more checks here for additional levels if you have more
    }
}