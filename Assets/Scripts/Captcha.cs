using UnityEngine;
using System;

//Mark the class as Serializable so it can show up in the Unity Inspector
[Serializable]
public class Captcha
{
    //Hold a ref to a sprite for the captcha 
    public Sprite Image;

    //The value that the user must enter to match the captcha image
    public string Value;
}

