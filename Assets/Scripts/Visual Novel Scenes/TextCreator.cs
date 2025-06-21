using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCreator : MonoBehaviour
{
    //Use static variables so other scripts can access them
    public static TMPro.TMP_Text viewText; //Ref to the text itself
    public static bool runTextPrint; //Determins the text being "printed"
    public static int charCount; //No. of characters 

    [SerializeField] string transferText;
    [SerializeField] int internalCount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        internalCount = charCount;
        charCount = GetComponent<TMPro.TMP_Text>().text.Length;      //assign to charCount the number of letters inside text
        
        //Determine if we are going to print the text
        if (runTextPrint == true)
        {
            runTextPrint = false;                                    //Prevent the text from continuously printing
            viewText = GetComponent<TMPro.TMP_Text>();               //Get the TMPro component so we can edit the text inside
            transferText = viewText.text;
            viewText.text = "";                                      //Set the text to nothing so the coroutine can run properly
            StartCoroutine(RollText());
        }
    }

    IEnumerator RollText()
    {
        //For every letter we define, display 1 letter at a time every 0.03s
        foreach(char c in transferText)
        {
            viewText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
