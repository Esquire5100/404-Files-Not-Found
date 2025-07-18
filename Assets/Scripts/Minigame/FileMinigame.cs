using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileMinigame : MonoBehaviour
{
    public GameObject minigame;

    public GameObject tab1;
    public GameObject tab2;
    public GameObject tab3;
    public GameObject tab4;

    public GameObject decoy1;
    public GameObject decoy2;
    public GameObject decoyEmpty;

    public GameObject CaptchaScreen;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenTab1()
    {
        tab1.SetActive(true);
    }

    public void OpenTab2()
    {
        tab2.SetActive(true);
    }

    public void OpenTab3()
    {
        tab3.SetActive(true);
    }
    public void OpenTab4()
    {
        tab4.SetActive(true);
    }

    public void OpenDecoy1()
    {
        decoy1.SetActive(true);
    }

    public void OpenDecoy2()
    {
        decoy2.SetActive(true);
    }

    public void OpenDecoyEmpty()
    {
        decoyEmpty.SetActive(true);
    }

    public void OpenCaptcha()
    {
        CaptchaScreen.SetActive(true);
    }

    public void success()
    {
        minigame.SetActive(false);
    }

    public void CloseTab(string tabName)
    {
        GameObject tabToClose = GameObject.Find(tabName);
        if (tabToClose != null)
        {
            tabToClose.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameObject with name " + tabName + " not found.");
        }
    }
}