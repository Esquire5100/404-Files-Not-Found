using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static string LastSceneName;
    public static int LastSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        SceneTracker.LastSceneName = SceneManager.GetActiveScene().name;
        SceneTracker.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
