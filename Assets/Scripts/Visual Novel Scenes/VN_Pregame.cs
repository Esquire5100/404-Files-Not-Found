using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VN_Pregame : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject fadeScreenOut;

    public GameObject charPhiona;
    public Sprite PhionaNeutral;
    public Sprite PhionaAnnoyed;
    public Sprite PhionaAngry;
    public Sprite PhionaTutorial;

    public GameObject Dialogue;

    [SerializeField] AudioSource PhionaTalk;
    [SerializeField] AudioSource PhionaSqueal;
    [SerializeField] AudioSource PhionaShout;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;  //Ref the parent gameobj of the text(box)s

    [SerializeField] GameObject responses;
    [SerializeField] GameObject responses1;
    [SerializeField] GameObject responses2;

    [SerializeField] int eventPos = 0;
    
    // Update is called once per frame
    void Update()
    {
        textLength = TextCreator.charCount; //Ref to the charCount in TextCreator script
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set everyth but the fade screen to false so its easier to edit the scene in unity
        fadeScreenIn.SetActive(true);
        charPhiona.SetActive(false);
        mainTextObject.SetActive(false);

        StartCoroutine(EventStarter());   
    }

    IEnumerator EventStarter()
    {
        //Event 0

        //After 1.5s in the scene, disable the fadescreen and enable phiona gameobj
        yield return new WaitForSeconds(1.5f);
        fadeScreenIn.SetActive(false);
        charPhiona.SetActive(true);

        //Enable and control the text(box) functions
        yield return new WaitForSeconds(1f);
        PhionaTalk.Play(); //Play the sfx for Phiona talking
        
        //Activate the textbox, but deactivate the dialogue text so that it will print properly
        mainTextObject.SetActive(true);

        //Set a small delay between when the text box appears and when the text starts printing
        yield return new WaitForSeconds(1f);
        textToSpeak = "Evening, Scottie! I need you to do something for me.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;    //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);  //Wait until the text has finished 
        
        yield return new WaitForSeconds(3f);
        PhionaTalk.Stop();
        responses.SetActive(true);

        eventPos = 1;
    }

    // Event 1
    public void What()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseOneW());
        }
    }

    private IEnumerator ResponseOneW()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                 //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite

        textToSpeak = "You heard me, I have a job for you.";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(3f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking
     
        textToSpeak = "Now, I need you to break into MGD Corp. and steal their top-tier files. Get as many as you can, but don�t you dare return empty-handed!";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(4f);
        PhionaTalk.Pause();
        PhionaShout.Play();
        charPhiona.GetComponent<Image>().sprite = PhionaAngry;                      //Change Phiona's sprite
        PhionaTalk.Play();

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Stop();
        responses1.SetActive(true);

        eventPos = 2;
    }
    
    public void YesPhiona()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseTwoYP());
        }
    }
    private IEnumerator ResponseTwoYP()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                           //Disable the buttons so the player can't keep clicking them
        
        textToSpeak = "Excellent! No questions asked is what I expect from my top intern!";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        PhionaSqueal.Play();

        yield return new WaitForSeconds(1f);
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking

        yield return new WaitUntil(() => textLength == currentTextLength);                    //Wait until the text has finished 

        yield return new WaitForSeconds(1f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking

        textToSpeak = "Now, I need you to break into MGD Corp. and steal their top-tier files. Get as many as you can, but don�t you dare return empty-handed!";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(4f);
        PhionaTalk.Pause();
        PhionaShout.Play();
        charPhiona.GetComponent<Image>().sprite = PhionaAngry;                                //Change Phiona's sprite

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Play();

        yield return new WaitUntil(() => textLength == currentTextLength);                    //Wait until the text has finished 

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Stop();
        responses1.SetActive(true);

        eventPos = 2;
    }

    // Event 2
    public void But()
    {
        if (eventPos == 2)
        {
            StartCoroutine(ResponseOneB());
        }
    }

    private IEnumerator ResponseOneB()
    {
        yield return new WaitForSeconds(0.03f);
        responses1.SetActive(false);                                                //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite

        textToSpeak = "Are you going to do the job? Or not.";                       //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(2f);
        PhionaTalk.Stop();
        responses2.SetActive(true);

        eventPos = 3;
    }

    public void YesIDMBPhiona()
    {
        if (eventPos == 2)
        {
            StartCoroutine(ResponseTwoYIDMBP());
        }
    }
    private IEnumerator ResponseTwoYIDMBP()
    {
        yield return new WaitForSeconds(0.03f);
        responses1.SetActive(false);                                                          //Disable the buttons so the player can't keep clicking them
        PhionaSqueal.Play();                                                                  //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                              //Change Phiona's sprite

        textToSpeak = "Perfect!";                                                             //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);                    //Wait until the text has finished 

        eventPos = 4;

        if (eventPos == 4)
        {
            SceneManager.LoadScene("Tutorial Dialogue");
        }
    }


    // Event 3
    public void YesPhionaITMB()
    {
        if (eventPos == 3)
        {
            StartCoroutine(ResponseTwoYPITMB());
        }
    }
    private IEnumerator ResponseTwoYPITMB()
    {
        yield return new WaitForSeconds(0.03f);
        responses2.SetActive(false);                                                          //Disable the buttons so the player can't keep clicking them
        PhionaSqueal.Play();                                                                  //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                              //Change Phiona's sprite

        textToSpeak = "Perfect!";                                                             //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);                    //Wait until the text has finished 

        yield return new WaitForSeconds(0.5f);
        
        eventPos = 4;


        if (eventPos == 4)
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Tutorial Dialogue");
        }
    }

}