using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance { get; private set; }//make it refrnced in other scripts without doing fcking script sheit
    //custom input field 
    [SerializeField]
    KeyCode input;
    //true or false
    public bool touching;
    public bool interaction;
    public bool longBlock;
    public bool longBlockScore;
    //score duh
    public int hit;
    public bool ifHit;
    //error time for long
    public float errorTimeForLong = 0.25f;
    //to tell the popularity script hit
    public Popularity Popularity;
    //debug menu shit
    public Return Return;
    public Score Score;
    //hit thingy idk
    public GameObject hitAnim;
    Animator hitAnims;

    private void Start()
    {
        hitMiss.SetActive(false);
        hitAnims = hitAnim.GetComponent<Animator>();
    }

    void Update()
    {
        //if player hit while touching make it do some shit
        if (Input.GetKey(input) && touching)
        {
            if (longBlock)
            {
                interaction = true;
            }
            else
            {
                StartCoroutine(InteractedAtMoment());
            }
        }
        if (Input.GetKeyUp(input)) {
            StartCoroutine(InteractedAtMoment());
        }
        //if player pressed when not touching it say miss
        if (Input.GetKeyDown(input) && !touching)
        {
            print("missed " + gameObject.name);
            HORDisplay(false);
        }
    }
    //sets interaction to true then false after .05 seconds
    IEnumerator InteractedAtMoment()
    {
        interaction = true;
        longBlockScore = true;
        yield return new WaitForSecondsRealtime(0.05f);
        interaction = false;
        longBlockScore = false;

    }
    IEnumerator LongBlockInteraction(Collider2D collision)
    {
        //wait .25 seconds before cheaking if interaction is still not happening(error time)
        yield return new WaitForSecondsRealtime(errorTimeForLong);

        if (!interaction)
        {
            //incase the long block dosent delete it self
            if(collision != null){
                Destroy(collision.gameObject);
            }
            longBlockScore = false;
            Popularity.fame(0,1);
        }
        if (interaction){
            longBlockScore = true;
        }
    }

    //update loop but only when touching block
    public void OnTriggerStay2D(Collider2D collision)
    {
        //the thing that destroys the block and adds scores
        touching = true;
        //cheakcs if its long 
        if (collision.gameObject.tag == "LongMusicBlock")
        {
            longBlock = true;
        }
        if (interaction && longBlock) 
        {

        }
        //if you miss the long block 
        if (!interaction && longBlock) {
                StartCoroutine(LongBlockInteraction(collision));

        }
        //score thingy
        if (interaction && !longBlock) {
            hit += 1;
            
            print("YOU SCORE");
            //I added so that the hitmusic is mulitplied by 1%, probably doesnt work but you get roughly the point. is it possible to make the score and fame/hitmusic the same so we write less code and variables?
            Destroy(collision.gameObject);
            HORDisplay(true);
        }
    }
        //when collsion ends touching ends
    private void OnTriggerExit2D(Collider2D collision)
    {

        //score long block
        if (interaction && longBlockScore && longBlock){
            hit += 1;
            print("YOU SCORE LONG");
            
            longBlockScore = false;
            HORDisplay(true);
        }
        if (interaction && !longBlockScore && longBlock)
        {
            print("you missed long");
            HORDisplay(false);
        }
        if (!interaction && touching && !longBlock)
        {
            print("you missed");
            HORDisplay(false);

        }
        touching = false;
        longBlock = false;
    }   
    //HitOrMiss part of le scropt
    public void finish()
    {
        print("added");
        Score.addints(hit);
    }
    public TextMeshProUGUI hitOrMissDisplay;
    public GameObject hitMiss;
    public Color textColor;
    public void HORDisplay(bool hitOrMiss)
    {
        hitMiss.SetActive(true);
        hitOrMissDisplay.color = textColor;
        if (hitOrMiss)
        {
            hitOrMissDisplay.text = "HIT";
            hitAnims.Play("Pop");
            Popularity.fame(2.5f, 0);
        }
        if (!hitOrMiss)
        {
            hitOrMissDisplay.text = "MISS";
            Popularity.fame(0, 5);
        }
        hitOrMiss = false;
        StartCoroutine(hideWait());
    }
    IEnumerator hideWait()
    {
        
        yield return new WaitForSecondsRealtime(1f);
        hitMiss.SetActive(false);
    }
}
