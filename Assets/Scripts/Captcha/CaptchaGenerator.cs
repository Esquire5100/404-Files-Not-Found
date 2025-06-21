using UnityEngine;

//Create an instance of this ScriptableObject via the Unity Editor's Assets > Create menu
[CreateAssetMenu]
public class CaptchaGenerator : ScriptableObject
{
    //A list for Captchas (for diff sprites and a value strings)
    public Captcha[] captchas;

    //A static index used to keep track of which captcha to return next (Being 'static' means it is shared across all instances of this class)
    public static int index = 0;

    //Returns a Captcha from the list, rotating through them
    public Captcha Generate()
    {
        //Use modulo (%) to loop around if index exceeds list length
        return captchas[(index++ % captchas.Length)];
    }

    //Checks if the user input matches the captcha's correct value
    public bool IsCodeValid(string input, Captcha c)
    {
        return (input == c.Value);
    }
}