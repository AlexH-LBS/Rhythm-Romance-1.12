using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endDialog : MonoBehaviour
{
    public static endDialog instance;
    public GameObject black;
    public GameObject prepdia;
    public GameObject popdia;
    public GameObject polydia;
    public GameObject offScreenDeathDialogue;
    public float pulseSpeed = 1f;  // Speed of pulsing effect
    public float minAlpha = 0f;  // Minimum transparency
    public float maxAlpha = 1f;    // Maximum transparency
    public bool active = false;
    private Renderer objectRenderer;
    private Color originalColor;
    private Image spriteRenderer;
    void Start()
    {
        black.SetActive(false);
        popdia.SetActive(false);
        prepdia.SetActive(false);
        polydia.SetActive(false);
        offScreenDeathDialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            Debug.Log("active");
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            if (alpha >= 0.9f)
            {
                Debug.Log("not active");
                alpha = 1;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                active = false;

            }
        }
    }
    public void compare(int pop, int prep)
    {
        Debug.Log("comparing");
        if (pop < 20 && prep < 20)
        {
            ending(offScreenDeathDialogue);
        }
        if (pop > prep)
        {
            ending(popdia);
        }
        if (prep > pop)
        {
            ending(prepdia);
        }
        if (prep == pop)
        {
            ending(polydia);
        }
    }
    void ending(GameObject ending)
    {
        Debug.Log("ending");
        black.SetActive(true);
        ending.SetActive(true);
        spriteRenderer = ending.GetComponent<Image>();
        originalColor = spriteRenderer.color;
        active = true;
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(5);
    }
}
