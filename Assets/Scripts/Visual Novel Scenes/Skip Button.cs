using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    public GameObject This;
    public GameObject SceneController;
    public GameObject AYS;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Skip(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SkipDialogue()
    {
        This.SetActive(false);
        SceneController.SetActive(false);
    }

    public void SkipTutorial()
    {
        AYS.SetActive(true);
    }

    public void YesSkip()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void NoSkip()
    {
        AYS.SetActive(false);
    }
}
