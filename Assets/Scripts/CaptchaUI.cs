using UnityEngine;
using UnityEngine.UI;

//This script handles the UI logic for displaying and validating a captcha
public class CaptchaUI : MonoBehaviour
{
    [Header("UI References :")] //Organises the Inspector
    [SerializeField] private Image uiCodeImage; //Ref to the UI image that shows the captcha sprite
    [SerializeField] private InputField uiCodeInput; //Ref the input area where the player types the captcha
    [SerializeField] private Text uiErrorsText; //Ref to the text used for showing errors
    [SerializeField] private Button uiRefreshButton; //Button to refresh the captcha
    [SerializeField] private Button uiSubmitButton; //Button to submit the entered captcha

    [Header("Captcha Generator :")]

    [SerializeField] private CaptchaGenerator captchaGenerator; //Ref to the CaptchaGenerator script
    
    private Captcha currentCaptcha; //Holds the current captcha

    //Start is called before the first frame update
    private void Start()
    {
        GenerateCaptcha(); //Show the first captcha when the scene starts

        //Set up buttons
        uiRefreshButton.onClick.AddListener(GenerateCaptcha); //Refresh button calls GenerateCaptcha
        uiSubmitButton.onClick.AddListener(Submit);           //Submit button calls Submit
    }

    //Generate a new captcha from the generator and update UI
    private void GenerateCaptcha()
    {
        currentCaptcha = captchaGenerator.Generate();              //Get a new captcha
        uiCodeImage.sprite = currentCaptcha.Image;                 //Set image to match current captcha
        uiErrorsText.gameObject.SetActive(false);                  //Hide error text if visible
    }

    //Checks if the player's input matches current captcha
    private void Submit()
    {
        string enteredCode = uiCodeInput.text;                     //Get text from input field

        if (captchaGenerator.IsCodeValid(enteredCode, currentCaptcha))
        {
            // Correct captcha: hide the error message
            uiErrorsText.gameObject.SetActive(false);
        }
        else
        {
            // Incorrect captcha: show the error message
            uiErrorsText.gameObject.SetActive(true);
        }
    }
}