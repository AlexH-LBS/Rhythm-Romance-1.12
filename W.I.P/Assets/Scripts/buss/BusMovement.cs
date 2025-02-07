using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D bus;
    public float Speed;
    public bool move;

    [SerializeField] private AudioClip feet;
    private AudioSource audioSource;
    private bool iswalking = false;
    Animator animator;
    public string Direction;
    bool animsbool;
    void Start()
    {
        bus = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (move)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                bus.velocity += new Vector2(0, Speed);
                animator.Play(Direction);
                animsbool = false;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                bus.velocity += new Vector2(0, -Speed);
                animator.Play(Direction);
                animsbool = false;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bus.velocity += new Vector2(-Speed, 0);
                Direction = "Walking-";
                animator.Play(Direction);
                animsbool = false;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                bus.velocity += new Vector2(Speed, 0);
                Direction = "Walking";
                animator.Play(Direction);
                animsbool = false;
            }
            if (Direction == "Walking" && animsbool)
            {
                animator.Play("Idle");
            }
            if(Direction == "Walking-" && animsbool)
            {
                animator.Play("MirroedIdle");
            }
            else
            {
                animsbool = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(5);
            }


            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                bus.velocity = new Vector2(0, 0);
            }
            else
            {
                bus.velocity = bus.velocity.normalized * Speed;

            }
        }


        //footstepsounds :)
        if (bus.velocity.x != 0 || bus.velocity.y != 0)
        {
            iswalking = true;
        }
        if(bus.velocity.x == 0 && bus.velocity.y == 0)
        {
            iswalking = false;
        }
        if (iswalking && !audioSource.isPlaying)
        {

            
            audioSource.Play();
            
        }
        if(!iswalking && audioSource.isPlaying)
        {
            audioSource.Stop();
        }



    }

   
}
