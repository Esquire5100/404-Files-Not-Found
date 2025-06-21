using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_Pregame : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charPhiona;

    public GameObject textBox;
    public GameObject responses;

    [SerializeField] AudioSource PhionaTalk;

    // Start is called before the first frame update
    void Start()
    {
        //Set everyth but the fade screen to false so its easier to edit the scene in unity
        fadeScreenIn.SetActive(true);
        charPhiona.SetActive(false);
        textBox.SetActive(false);
        responses.SetActive(false);

        StartCoroutine(EventStarter());   
    }

    IEnumerator EventStarter()
    {
        //After 1.5s in the scene, disable the fadescreen and enable phiona gameobj
        yield return new WaitForSeconds(1.5f);
        fadeScreenIn.SetActive(false);
        charPhiona.SetActive(true);

        //Enable text(box) functions
        yield return new WaitForSeconds(1f);
        textBox.SetActive(true);
        PhionaTalk.Play();

        yield return new WaitForSeconds(8f);
        PhionaTalk.Stop();
        responses.SetActive(true);

    }

    public void What()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}