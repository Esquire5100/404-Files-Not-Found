using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    public Button stunButton;
    public Image cooldownOverlay;
    public float cooldownTime = 20f;

    private bool isCooling = false;

    // Start is called before the first frame update
    void Start()
    {
        cooldownOverlay.type = Image.Type.Filled;
        cooldownOverlay.fillMethod = Image.FillMethod.Radial360;
        cooldownOverlay.fillOrigin = (int)Image.Origin360.Bottom;
        cooldownOverlay.fillAmount = 0f;

        stunButton.onClick.AddListener(OnStunButtonPressed);
    }

    void OnStunButtonPressed()
    {
        if (!isCooling)
        {
            // Execute your stun logic here...

            // Start cooldown
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        isCooling = true;
        stunButton.interactable = false;
        float elapsed = 0f;

        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            cooldownOverlay.fillAmount = elapsed / cooldownTime;
            yield return null;
        }

        cooldownOverlay.fillAmount = 0f;
        stunButton.interactable = true;
        isCooling = false;
    }
}
