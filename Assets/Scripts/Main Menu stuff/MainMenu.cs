using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CreditsMainTab;
    public GameObject CreditsMiniTab;
    public GameObject SettingsTab;

    public GameObject Lvl1;
    public GameObject Lvl2;
    public GameObject Lvl3;
    public GameObject FilesTab;

    public GameObject Confirm1Tab;
    public GameObject Confirm2Tab;
    public GameObject Confirm3Tab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //MM Button functions
    public void Play()
    {
        int eventPos = PlayerPrefs.GetInt("SelectedLevel", 0); //default is 0

        if (eventPos == 0)
        {
            SceneManager.LoadScene("Visual Novel (Pregame)");
        }
        else if (eventPos == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (eventPos == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (eventPos == 3)
        {
            SceneManager.LoadScene("Level 3");
        }

        PlayerPrefs.DeleteKey("SelectedLevel");

    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Files()
    {
        SceneManager.LoadScene("Files");
    }

    //Credits and Settings Buttons
    public void OpenCreditsTabs()
    {
        CreditsMainTab.SetActive(true);
        CreditsMiniTab.SetActive(true);
    }

    public void OpenCreditsMiniTab()
    {
        CreditsMiniTab.SetActive(true);
    }

    public void OpenSettingsTab()
    {
        SettingsTab.SetActive(true);
    }

    //Files Buttons
    //Tab selection

    public void SelectTab(string tabName)
    {
        // Deactivate all tabs first
        Lvl1.SetActive(false);
        Lvl2.SetActive(false);
        Lvl3.SetActive(false);
        FilesTab.SetActive(false);

        switch (tabName)
        {
            case "Tab1":
                Lvl1.SetActive(true);
                break;
            case "Tab2":
                Lvl2.SetActive(true);
                break;
            case "Tab3":
                Lvl3.SetActive(true);
                break;
            case "FileTab":
                FilesTab.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid tab name: " + tabName);
                break;
        }
    }

    //Level Selection
    public void Lvl1Select()
    {
        Confirm1Tab.SetActive(true);
    }
    public void Confirm1()
    {
        PlayerPrefs.SetInt("SelectedLevel", 1);
        SceneManager.LoadScene("Main Menu");
    }

    public void Lvl2Select()
    {
        Confirm2Tab.SetActive(true);
    }
    public void Confirm2()
    {
        PlayerPrefs.SetInt("SelectedLevel", 2);
        SceneManager.LoadScene("Main Menu");
    }
    public void Lvl3Select()
    {
        Confirm3Tab.SetActive(true);
    }
    public void Confirm3()
    {
        PlayerPrefs.SetInt("SelectedLevel", 3);
        SceneManager.LoadScene("Main Menu");
    }
}