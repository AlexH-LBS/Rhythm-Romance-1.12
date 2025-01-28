using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    public static Score instance { get; private set; }
    public TextMeshProUGUI scoreDisplay;
    public int totalHits;

    public void addints(int added)
    {
        totalHits += added;
        scoreDisplay.text = totalHits.ToString();

    }
}
