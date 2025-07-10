using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilesScene : MonoBehaviour
{
    public GameObject Phiona;
    public GameObject Dialer;
    public GameObject Scottie;
    public GameObject Security;

    public GameObject Keycard;
    public GameObject Hacking;
    public GameObject Flashbang;
    public GameObject NightMode;

    // Start is called before the first frame update
    void Start()
    {
        
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
