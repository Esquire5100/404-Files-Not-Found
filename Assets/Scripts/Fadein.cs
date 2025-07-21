using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Fadein : MonoBehaviour
{
    public float fadeTime;

    private Image Logo;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        /*Logo = GetComponent<Image>();
        text = GetComponent<TMP_Text>();
        Logo.CrossFadeAlpha(0f, fadeTime, false);
        text.CrossFadeAlpha(0f, fadeTime, false);*/

        StartCoroutine(GoToMain());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Logo.color.a == 0)
        {
            gameObject.SetActive(false);
        }
        if (text.color.a == 0)
        {
            gameObject.SetActive(false);
        }*/
    }

    private IEnumerator GoToMain()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main Menu");
    }
}