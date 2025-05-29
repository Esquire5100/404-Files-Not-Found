using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{

    public GameObject mobileUI;
    public bool showMobileUI;

    // Start is called before the first frame update
    void Start()
    {
        mobileUI.SetActive(showMobileUI);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
