using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TestScriptGigLocation : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isPlayerNearby;

    public GameObject Prompt;

    public int loadScene;
    [SerializeField] public KeyCode input = KeyCode.Space;

    void Start()
    {
        Prompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby == true)
        {

            Prompt.SetActive(true);
            if (Input.GetKeyDown(input))
            {
                //This happens if youre in range :>>>
                loadSceehee();
            }
        }
        else
        {
            Prompt.SetActive(false);
        }
    }
    public void loadSceehee()
    {
        SceneManager.LoadScene(loadScene);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Cowboy")
        {
            isPlayerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerNearby = false;
    }
}
