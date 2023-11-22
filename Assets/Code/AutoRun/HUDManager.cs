using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public HeroController hc;
    public Text score_text;
    private int score_shown;
    void Start()
    {
        score_shown = hc.score;
    }

    void Update()
    {
        score_shown = hc.score;
        score_text.text = string.Format("x{0:00}", score_shown);
    }
}
