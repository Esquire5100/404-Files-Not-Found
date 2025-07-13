using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileProgressTracker
{
    private static string saveKey = "FileCounts";                //Save the data in PlayerPrefs
    private static Dictionary<string, int> fileCounts = new();   //Track files stolen per level (e.g., {"Level 1": 7, "Level 2": 3})

    static FileProgressTracker() => LoadData();                  //Static constructor: Load saved data when the script is first used

    //Add files stolen from a level
    public static void AddFiles(string levelName, int count)
    {
        if (!fileCounts.ContainsKey(levelName))
            fileCounts[levelName] = 0;                           //Initialize if first time visiting this level

        fileCounts[levelName] += count;                          //Add to the total
        SaveData();                                              //Save updated data to PlayerPrefs
    }

    //Get total files stolen from a specific level
    public static int GetFiles(string levelName)
    {
        return fileCounts.ContainsKey(levelName) ? fileCounts[levelName] : 0;
    }

    //Return the whole dictionary for displaying in the Files scene
    public static Dictionary<string, int> GetAllFileCounts()
    {
        return new(fileCounts);                                 //Return a copy for safety
    }

    //Save the dictionary to PlayerPrefs as JSON
    private static void SaveData()
    {
        string json = JsonUtility.ToJson(new FileData(fileCounts));
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
    }

    //Load the dictionary from PlayerPrefs (if it exists)
    private static void LoadData()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);
            fileCounts = JsonUtility.FromJson<FileData>(json).ToDictionary();
        }
    }

    //Serializable helper class to allow saving Dictionary as JSON
    [System.Serializable]
    private class FileData
    {
        public List<string> keys = new();
        public List<int> values = new();

        //Convert Dictionary to serializable lists
        public FileData(Dictionary<string, int> dict)
        {
            foreach (var kvp in dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        //Convert serialized lists back to a Dictionary
        public Dictionary<string, int> ToDictionary()
        {
            Dictionary<string, int> result = new();
            for (int i = 0; i < keys.Count; i++)
                result[keys[i]] = values[i];
            return result;
        }
    }

    //Return true if a lvel has at least 1 file collected
    public static bool HasCompletedLevel(string levelName)
    {
        return fileCounts.ContainsKey(levelName) && fileCounts[levelName] > 0;
    }

    //Return total number of files across all levels
    public static int GetTotalFiles()
    {
        int total = 0;
        foreach (var count in  fileCounts.Values)
            total += count;
        return total;
    }
}