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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Visual Novel (Pregame)");
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
}
