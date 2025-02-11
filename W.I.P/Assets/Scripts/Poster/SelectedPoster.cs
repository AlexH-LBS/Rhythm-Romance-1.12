using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedPoster : MonoBehaviour
{
    //inputs
    [SerializeField] KeyCode up = KeyCode.W;
    [SerializeField] KeyCode down = KeyCode.S;
    [SerializeField] KeyCode back = KeyCode.A;
    [SerializeField] KeyCode forword = KeyCode.D;
    [SerializeField] KeyCode selected = KeyCode.Space;
    //pos
    [SerializeField] private Transform[] points = null;
    [SerializeField] public GameObject poster = null;
    private bool movingToTarget = false;
    private int witchOne;
    public int sceneLoad;
    public bool timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(!timer)
        { 
            if (Input.GetKeyDown(up) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                movingToTarget = true;
                witchOne = 0;
                StartCoroutine(inputcooldown());
            }
            if (Input.GetKeyDown(down) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                movingToTarget = true;
                witchOne = 1;
                StartCoroutine(inputcooldown());
            }
            if (Input.GetKeyDown(back) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movingToTarget = true;
                witchOne = 2;
                StartCoroutine(inputcooldown());
            }
            if (Input.GetKeyDown(forword) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                movingToTarget = true;
                witchOne = 3;
                StartCoroutine(inputcooldown());
            }
        }
        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[witchOne].position, 10 * Time.deltaTime);
        }
        if (Input.GetKeyDown(selected))
        {
            if(!poster.activeSelf)
            {
                poster.SetActive(true);
            }
            else
            {
                poster.SetActive(false);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(NewShit(collision));
    }

    private IEnumerator NewShit(Collider2D collision)
    {
        yield return new WaitForSeconds(.25f);
        points = collision.gameObject.GetComponent<Poster>().points;
        poster = collision.gameObject.GetComponent<Poster>().poster;
        sceneLoad = collision.gameObject.GetComponent<Poster>().loadScene;
        movingToTarget = false;
        
    }
    public IEnumerator inputcooldown()
    {
        timer = true;
        yield return new WaitForSeconds(1.4f);
        timer = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        poster.SetActive(false);
        poster = null;
    }
    public void playgame()
    {
        Debug.Log("pressed");
        SceneManager.LoadScene(sceneLoad);
    }

}
