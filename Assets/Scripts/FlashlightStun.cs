using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightStun : MonoBehaviour
{
    public GameObject flashlight;
    public Button stunButton;
    public Image cooldownOverlay;
    public float stunDuration = 2f;
    public float stunCooldown = 5f;
    private float lastStunTime = -Mathf.Infinity;
    private bool flashlightOn = false;

    void Update()
    {
        flashlightOn = flashlight.activeInHierarchy;

        if (Time.time < lastStunTime + stunCooldown)
        {
            float elapsed = Time.time - lastStunTime;
            if (elapsed < stunCooldown)
            {
                float t = elapsed / stunCooldown;
                cooldownOverlay.fillAmount = 1 - t; // from full→empty
                stunButton.interactable = false;
            }
            else
            {
                cooldownOverlay.fillAmount = 0f;
                stunButton.interactable = true;
            }
        }
    }
    public void TurnOnFlashLight(float duration)
    {
        if (Time.time < lastStunTime + stunCooldown) return;
        {
            flashlight.SetActive(true);
            flashlightOn = true;
            lastStunTime = Time.time;
            Invoke("TurnOffFlashlight", duration);
        }
    }

    private void TurnOffFlashLight()
    {
        flashlight.SetActive(false);
        flashlightOn = false;
    }

    /*private IEnumerator DelayedActivate(float duration)
    {
        flashlightOn = false;
        flashlight.SetActive(true);

        yield return null;

        flashlightOn = true;
        yield return new WaitForSeconds(duration);
        flashlightOn = false;
        flashlight.SetActive(true);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!flashlightOn || Time.time < lastStunTime + stunCooldown) return;
        if (other.CompareTag("Enemy"))
        {
            var guard = other.GetComponent<Guard_Controller>();
            guard?.Stun(stunDuration);
        }
    }
}