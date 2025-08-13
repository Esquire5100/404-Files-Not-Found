using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    public GameObject This;
    public GameObject SceneController;

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
}
