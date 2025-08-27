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

    public GameObject Hacking;
    public GameObject Flashbang;

    //Buttons
    public GameObject btnPhiona;
    public GameObject btnDialer;
    public GameObject btnScottie;

    public GameObject btnSecurity;
    public GameObject btnHacking;

    public GameObject btnFlashbang;

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
            btnHacking.SetActive(false);
            btnSecurity.SetActive(false);
        }

        //Unlock based on total file count
        int totalFiles = FileProgressTracker.GetTotalFiles();

        if (totalFiles > 5) btnPhiona.SetActive(true);
        if (totalFiles > 6) btnDialer.SetActive(true);
        if (totalFiles > 7) btnScottie.SetActive(true);
        if (totalFiles > 8) btnSecurity.SetActive(true);
        if (totalFiles > 9) btnHacking.SetActive(true);
        if (totalFiles > 10) btnFlashbang.SetActive(true);
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
        Hacking.SetActive(false);
        Flashbang.SetActive(false);

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

            case "Hacking":
                Hacking.SetActive(true);
                break;
            case "Flashbang":
                Flashbang.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid page name: " + buttonName);
                break;
        }
    }
}