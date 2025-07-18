using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreLevel3 : MonoBehaviour
{
    public GameObject This;

    public GameObject fadeScreenIn;
    public GameObject fadeScreenOut;

    public GameObject charPhiona;
    public Sprite PhionaNeutral;
    public Sprite PhionaAnnoyed;
    public Sprite PhionaAngry;

    public GameObject charDialer;
    public Sprite DialerNeutral;
    public Sprite DialerAnnoyed;
    public Sprite DialerCocky;

    public GameObject Dialogue;

    [SerializeField] AudioSource PhionaTalk;
    [SerializeField] AudioSource PhionaSqueal;
    [SerializeField] AudioSource PhionaShout;

    [SerializeField] AudioSource DialerTalk;

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

        if (eventPos == 5)
        {
            //Wait for a bit before fading out and loading next scene
            new WaitForSeconds(2f);
            fadeScreenOut.SetActive(true);

            Time.timeScale = 1.0f; //Resume game after the dialogue
            This.SetActive(false);
        }
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
        yield return new WaitForSeconds(1f);
        fadeScreenIn.SetActive(false);
        charPhiona.SetActive(true);

        //Enable and control the text(box) functions
        yield return new WaitForSeconds(1f);
        PhionaSqueal.Play(); //Play the sfx for Phiona talking
        PhionaTalk.Play();

        //Activate the textbox, but deactivate the dialogue text so that it will print properly
        mainTextObject.SetActive(true);

        //Set a small delay between when the text box appears and when the text starts printing
        yield return new WaitForSeconds(1f);
        textToSpeak = "You’re doing great, Scottie! Just one more building and I'll be able to overthrow them.";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(5f);
        PhionaTalk.Stop();
        responses.SetActive(true);

        eventPos = 1;
    }

    //Event 1
    public void Overthrow()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseOne());
        }
    }

    private IEnumerator ResponseOne()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                 //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite

        textToSpeak = "Oh Scottie…you should know by now… Why do you think you're in their building stealing their files right now?";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(6f);
        PhionaTalk.Stop();


        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "This last building will be your hardest challenge yet…";     //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(4f);
        PhionaTalk.Stop();


        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "This is the building his office is in. Calister Dialer, CEO of MGD Corp. He won’t let you off easy, but I really need these last few files to piec3 t0gE$her tHei% 5t$@teG/eS.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(7f);
        PhionaTalk.Stop();
        responses1.SetActive(true);

        eventPos = 2;
    }

    public void MGD()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseTwo());
        }
    }
    private IEnumerator ResponseTwo()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                           //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                              //Change Phiona's sprite

        textToSpeak = "Yes, glad you know what you’re doing.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);                    //Wait until the text has finished 

        yield return new WaitForSeconds(3f);
        PhionaTalk.Stop();


        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "This last building will be your hardest challenge yet…";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(4f);
        PhionaTalk.Stop();


        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "This is the building his office is in. Calister Dialer, CEO of MGD Corp. He won’t let you off easy, but I really need these last few files to piec3 t0gE$her tHei% 5t$@teG/eS.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(7f);
        PhionaTalk.Stop();
        responses1.SetActive(true);

        eventPos = 2;
    }

    //Event 2
    public void MsPhiona()
    {
        if (eventPos == 2)
        {
            StartCoroutine(ResponseMsPhiona());
        }
    }

    private IEnumerator ResponseMsPhiona()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                      //Disable the buttons so the player can't keep clicking them
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                         //Change Phiona's sprite
        PhionaTalk.Play();

        textToSpeak = "$ho0t. S@otTi3-";                                            //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(0.5f);
        PhionaTalk.Stop();
        charPhiona.SetActive(false);
        mainTextObject.SetActive(false);


        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Play();                                                                         //Play the sfx for Phiona talking

        textToSpeak = "YoU h@ve t0 F/ni$h th9s J*b.";                                              //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                                //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);                         //Wait until the text has finished 

        yield return new WaitForSeconds(1f);
        PhionaTalk.Stop();
        mainTextObject.transform.Find("CharaName").gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        charDialer.SetActive(true);
        charDialer.GetComponent<Image>().sprite = DialerNeutral;                                   //Change Dialer's sprite
        DialerTalk.Play();
        mainTextObject.transform.Find("???").gameObject.SetActive(true);                           //"change" the name

        textToSpeak = "Well, well, well.";                                                         //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().color = new Color(115f/255f, 91f/255f, 65f/255f);  //Change the colour of the text
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                                //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);                         //Wait until the text has finished
        yield return new WaitForSeconds(1f);
        DialerTalk.Stop();
        responses2.SetActive(true);

        eventPos = 3;
    }
}