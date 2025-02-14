using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    public GameObject UI;
    public PlayerScript playerScripts;
    private void Start()
    {
        UI.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        UI.SetActive(true);
        playerScripts.finish();
        Destroy(gameObject);
        }
    }
}
