using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.EditorTools;

public class VN_Pregame : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject fadeScreenOut;

    public GameObject ProjectorScreen;
    public GameObject PhionaHand;
    public GameObject PC;
    public GameObject File;
    public GameObject Security;
    public GameObject foreshadowing;

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
    [SerializeField] GameObject responses3;
    [SerializeField] GameObject responses4;

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
            StartCoroutine(Tutorial());
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

        eventPos = 4;


        if (eventPos == 4)
        {
            StartCoroutine(Tutorial());
        }
    }

    private IEnumerator Tutorial()
    {
            yield return new WaitForSeconds(0.03f);
            ProjectorScreen.SetActive(true);
            PhionaHand.SetActive(true);

            PhionaTalk.Play();                                                                  //Play the sfx for Phiona talking
            charPhiona.GetComponent<Image>().sprite = PhionaTutorial;                             //Change Phiona's sprite

            textToSpeak = "First thing's first, these are the computers that you need to target. They�ll hold all the necessary files I need.";
            Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            yield return new WaitUntil(() => textLength == currentTextLength);

            yield return new WaitForSeconds(1f);
            PC.SetActive(true);

            yield return new WaitForSeconds(5f);
            PhionaTalk.Stop();

            yield return new WaitForSeconds(1.5f);
            PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking

            textToSpeak = "Hack into them and find the right file. It�ll look like this.";   //Define the text that needs to be printed
            Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            yield return new WaitUntil(() => textLength == currentTextLength);

            yield return new WaitForSeconds(2f);
            PC.SetActive(false);
            File.SetActive(true);

            yield return new WaitForSeconds(3f);
            PhionaTalk.Stop();

            yield return new WaitForSeconds(1.5f);
            PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking
            File.SetActive(false);
            Security.SetActive(true);

            textToSpeak = "Next, remember to watch out for MGD�s security system. Unfortunately, Dialer has significantly enhanced his security since the last time.";   //Define the text that needs to be printed
            Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            yield return new WaitUntil(() => textLength == currentTextLength);

            yield return new WaitForSeconds(6f);
            PhionaTalk.Stop();
            responses3.SetActive(true);

            eventPos = 5;
    }

    public void LastTime()
    {
        if (eventPos == 5)
        {
            StartCoroutine(ResponseLT());
        }
    }

    private IEnumerator ResponseLT()
    {
        yield return new WaitForSeconds(0.03f);
        responses3.SetActive(false);
        PhionaHand.SetActive(false);
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                             //Change Phiona's sprite
        PhionaShout.Play();                                                                    //Play the sfx for Phiona talking

        textToSpeak = "Problem, Scottie?";                                                    //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(1.5f);
        PhionaHand.SetActive(true);
        charPhiona.GetComponent<Image>().sprite = PhionaTutorial;                             //Change Phiona's sprite
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking

        textToSpeak = "Now then, don�t get caught in their light. The camera will alert all the guards in the vicinity, and if they catch you�";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(6f);
        PhionaTalk.Stop();
        responses4.SetActive(true);

        eventPos = 6;
    }

    public void IfTheyCatch()
    {
        if (eventPos == 6)
        {
            StartCoroutine(ResponseITC());
        }
    }

    private IEnumerator ResponseITC()
    {
        yield return new WaitForSeconds(0.03f);
        responses4.SetActive(false);
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking
        foreshadowing.SetActive(true);


        textToSpeak = "Well, let�s just say you�ll be escorted out of the building.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(4f);
        PhionaTalk.Stop();
        foreshadowing.SetActive(false);

        //Wait for a bit before fading out and loading next scene
        yield return new WaitForSeconds(0.03f);
        fadeScreenOut.SetActive(true);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Tutorial Level");

        eventPos = 7;
    }
}