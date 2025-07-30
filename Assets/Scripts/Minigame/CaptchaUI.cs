using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

//This script handles the UI logic for displaying and validating a captcha
public class CaptchaUI : MonoBehaviour
{
    [Header("UI References :")] //Organises the Inspector
    [SerializeField] private UnityEngine.UI.Image uiCodeImage; //Ref to the UI image that shows the captcha sprite
    [SerializeField] private InputField uiCodeInput; //Ref the input area where the player types the captcha
    [SerializeField] private Text uiErrorsText; //Ref to the text shown when u put the wrong code
    [SerializeField] private Text uiSuccessText; //Ref to the text shown when u put the right code
    [SerializeField] private UnityEngine.UI.Button uiRefreshButton; //Button to refresh the captcha
    [SerializeField] private UnityEngine.UI.Button uiSubmitButton; //Button to submit the entered captcha

    [Header("Captcha Generator :")]

    [SerializeField] private CaptchaGenerator captchaGenerator; //Ref to the CaptchaGenerator script
    
    private Captcha currentCaptcha; //Holds the current captcha

    private LvlManager theLvlManager; //make ref to lvlmanager script

    private FileMinigame theFileMinigame;

    public int fileValue = 1;       //attempt to fix x2 bug

    

    //Start is called before the first frame update
    private void Start()
    {
        GenerateCaptcha(); //Show the first captcha when the scene starts

        //Set up buttons
        uiRefreshButton.onClick.AddListener(GenerateCaptcha); //Refresh button calls GenerateCaptcha
      
        theLvlManager = FindAnyObjectByType<LvlManager>();

        theFileMinigame = FindAnyObjectByType<FileMinigame>();
    }

    //Generate a new captcha from the generator and update UI
    private void GenerateCaptcha()
    {
        currentCaptcha = captchaGenerator.Generate();              //Get a new captcha
        uiCodeImage.sprite = currentCaptcha.Image;                 //Set image to match current captcha
        uiErrorsText.gameObject.SetActive(false);                  //Hide error text if visible
        uiSuccessText.gameObject.SetActive(false);
    }

    //Checks if the player's input matches current captcha
    public void Submit()
    {
        string enteredCode = uiCodeInput.text;                     //Get text from input field

        if (captchaGenerator.IsCodeValid(enteredCode, currentCaptcha))
        {
            // Correct captcha: hide the error message
            uiErrorsText.gameObject.SetActive(false);
            uiSuccessText.gameObject.SetActive(true);
            Debug.Log("Submit pressed");
            //Start coroutine to wait, +1 files to the counter, then return to level 1
            StartCoroutine(SuccessSequence());
        }
        else
        {
            //Incorrect captcha: show the error message
            uiErrorsText.gameObject.SetActive(true);
            uiSuccessText.gameObject.SetActive(false);
        }
    }

    //Attempt to fix bug
    public void AddFiles(int count, bool doSave)
    {
        AddFiles(count, true);
    }

    private IEnumerator SuccessSequence()
    {
        //Give player the file
        if (LvlManager.Instance != null)
        {
            LvlManager.Instance.AddFiles(1);
            Debug.Log("File added! Total: " + LvlManager.Instance.FileCount);

            LvlManager.Instance.SaveRunToTotal();
        }
        else
        {
            Debug.LogWarning("LvlManager.Instance is null.");
        }

        yield return new WaitForSeconds(2f); //show success message for 2 seconds

        //Return to Level 1 (close the popup)
        theFileMinigame.CloseEverything();
    }
}