using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float[] TimeInBetween;

    public float timer;
    public int witchOne = 0;
    public GameObject Spawning;
    public float BPM;
    public float BPS;
    //resets timer at start tho make the code simpler and reusable
    void Start()
    {
        BPS = ((float)TimeInBetween[witchOne]);
        BPS = BPM / 60;
        timer = 1 / BPS;
        StartCoroutine(timeor());
    }
    IEnumerator timeor()
    {
        yield return new WaitForSeconds(timer);
        BPS = BPM / 60;
        if (witchOne > TimeInBetween.Length)
        {
            Destroy(gameObject);
        }
        timer = 1 / BPS * (((float)TimeInBetween[witchOne]));
        Instantiate(Spawning, gameObject.transform.position, Quaternion.identity);
        witchOne++;
        StartCoroutine(timeor());
    }
}
