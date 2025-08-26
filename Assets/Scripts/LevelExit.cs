using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public SoundEffectManager soundEffectManager;
    public string sceneToLoad;
    public Animator transition;
    public float transitionTime = 1f;

    public SpriteRenderer targetRenderer;
    public Sprite newSprite;
    public int requiredHacks = 3;

    void Start()
    {
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }

    void Update()
    {
        if (targetRenderer != null && newSprite != null &&
            LvlManager.Instance.FileCount >= requiredHacks &&
            targetRenderer.sprite != newSprite)
        {
            targetRenderer.sprite = newSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundEffectManager.Instance.StopMusic();

            if (transition == null)
            {
                GameObject fadeObj = GameObject.Find("Fading Image");
                if (fadeObj != null)
                {
                    transition = fadeObj.GetComponent<Animator>();
                }
                else
                {
                    Debug.LogWarning("Fading Image not found!");
                }
            }

            if (transition != null)
            {
                transition.SetTrigger("Start");
            }

            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                StartCoroutine(levelLoad());
            }
            else
            {
                Debug.LogError("Scene to load is not set in LevelExit!");
            }
        }
    }

    IEnumerator levelLoad()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
