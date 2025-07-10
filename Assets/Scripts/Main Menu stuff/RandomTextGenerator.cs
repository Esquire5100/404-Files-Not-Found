using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomTextDisplay : MonoBehaviour
{
    public TMP_Text targetText;
    public TextMeshProUGUI dialogueText; //Assign in Inspector

    private string[] dialogueLines = new string[]
    {
        "I'm disappointed, Scottie. You only had one job…",
        "Not your brightest moment, Scottie.",
        "Next time, try harder. Or just try at all.",
        "*sigh* And you were doing so well too…",
        "I thought I told you not to come back empty-handed…"
    };

    void Start()
    {
        // Select random line
        string selectedLine = dialogueLines[Random.Range(0, dialogueLines.Length)];

        // Set the text field (invisible for now — it will be overwritten by TextCreator)
        targetText.text = selectedLine;

        // Set reference for TextCreator to work on
        TextCreator.viewText = targetText;

        // Trigger the "printing" animation
        TextCreator.runTextPrint = true;
    }
}
