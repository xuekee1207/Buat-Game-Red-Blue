using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI ScoreText;
    public int score = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        ScoretextDisplay();
    }

    public void AddScore()
    {
        score += 1;
        AudioManager.instance.playAddPoint();
    }

    public void DeductScore()
    {
        score -= 1;
        AudioManager.instance.playMinusPoint();
    }

    public void ScoretextDisplay()
    {
        ScoreText.text = score.ToString();
    }
}
