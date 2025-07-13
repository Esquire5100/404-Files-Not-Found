using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilesScene : MonoBehaviour
{
    //Panels
    public GameObject Phiona;
    public GameObject Dialer;
    public GameObject Scottie;
    public GameObject Security;

    public GameObject Keycard;
    public GameObject Hacking;
    public GameObject Flashbang;
    public GameObject NightMode;

    //Buttons
    public GameObject btnPhiona;
    public GameObject btnDialer;
    public GameObject btnScottie;

    public GameObject btnSecurity;
    public GameObject btnKeycard;
    public GameObject btnHacking;

    public GameObject btnFlashbang;
    public GameObject btnNightMode;

    void Start()
    {
        HideLockedButtons();

        /*dialerButton.gameObject.SetActive(totalFiles >= 5);
        phionaButton.gameObject.SetActive(totalFiles >= 6);
        scottieButton.gameObject.SetActive(totalFiles >= 7);*/
    }

    private void HideLockedButtons()
    {
        //Unlock based on level completion
        if (!FileProgressTracker.HasCompletedLevel("Level 1"))
        {
            btnFlashbang.SetActive(false);
            btnKeycard.SetActive(false);
            btnHacking.SetActive(false);
            btnSecurity.SetActive(false);
        }

        if (!FileProgressTracker.HasCompletedLevel("Level 2"))
        {
            btnNightMode.SetActive(false);
        }

        //Unlock based on total file count
        int totalFiles = FileProgressTracker.GetTotalFiles();

        if (totalFiles > 5) btnPhiona.SetActive(false);
        if (totalFiles > 6) btnDialer.SetActive(false);
        if (totalFiles > 7) btnScottie.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectPage(string buttonName)
    {
        // Deactivate all tabs first
        Phiona.SetActive(false);
        Dialer.SetActive(false);
        Scottie.SetActive(false);
        Security.SetActive(false);
        Keycard.SetActive(false);
        Hacking.SetActive(false);
        Flashbang.SetActive(false);
        NightMode.SetActive(false);

        switch (buttonName)
        {
            case "Phiona":
                Phiona.SetActive(true);
                break;
            case "Dialer":
                Dialer.SetActive(true);
                break;
            case "Scottie":
                Scottie.SetActive(true);
                break;
            case "Security":
                Security.SetActive(true);
                break;

            case "Keycard":
                Keycard.SetActive(true);
                break;
            case "Hacking":
                Hacking.SetActive(true);
                break;
            case "Flashbang":
                Flashbang.SetActive(true);
                break;
            case "NightMode":
                NightMode.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid page name: " + buttonName);
                break;
        }
    }
}