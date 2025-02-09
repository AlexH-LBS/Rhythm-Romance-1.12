using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popularity : MonoBehaviour
{
    public float fameInt;
    public float crowdSpeed = 0.0f;
    public float increase = 1.01f;

    public GameObject crowdLayer1;
    public GameObject crowdLayer2;
    public GameObject crowdLayer3;
    public GameObject crowdLayer4;


    // Start is called before the first frame update
    void Start()
    {
        if (fameInt == 0)
        {
            crowdSpeed = 0;
        }
        crowdLayer1.SetActive(false);
        crowdLayer2.SetActive(false);
        crowdLayer3.SetActive(false);
        crowdLayer4.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (fameInt > 10)
        {
            crowdLayer1.SetActive(true);

            if (fameInt > 30)
            {
                crowdLayer2.SetActive(true);

                if (fameInt > 50)
                {
                    crowdLayer3.SetActive(true);

                    if (fameInt > 90)
                    {
                        crowdLayer4.SetActive(true);
                    }
                }
            }
        }



    }

    public void fame(float HitMusic, float miss)
    {
        crowdSpeed = fameInt;
        fameInt += HitMusic;
        fameInt *= 1.01f;
        fameInt -= miss;

        if (fameInt < 0)
        {
            fameInt = 0;
            print("lower than 0");
        }
        if (fameInt >= 0.0f && fameInt <= 1000.0f)
        {
            crowdSpeed *= increase;
            print(crowdSpeed);
        }

        if(fameInt > 100)
        {
            fameInt = 99;
            crowdSpeed = 100;
        }

    }
}