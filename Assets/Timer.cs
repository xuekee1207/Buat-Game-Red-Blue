using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI countdownshadowText;
    public TextMeshProUGUI timerText;
    public float countdownDuration = 5f;
    public float gameTimeDuration = 100f;
    public GameObject timerObject;
    public GameObject ballsObject;
    //public string goToScene;

    private bool isGameStarted = false;
    //private string scoreGame;

    private void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        // Countdown
        float countdownTimer = countdownDuration;
        while (countdownTimer >= 1)
        {
            countdownText.text = countdownTimer.ToString("0");
            countdownshadowText.text = countdownTimer.ToString("0");
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }

        // Display "Start"
        countdownText.text = "Start";
        countdownshadowText.text = "Start";
        isGameStarted = true;
        yield return new WaitForSeconds(1f);
        timerObject.SetActive(false);
        ballsObject.SetActive(true);
        AudioManager.instance.playDesertBGM();

        // Game Time
        float gameTimer = 0f;
        while (gameTimer <= gameTimeDuration)
        {
            if (isGameStarted)
            {
                timerText.text = FormatTime(gameTimer);
                gameTimer++;
            }
            yield return new WaitForSeconds(1f);
        }

        // Game Over
        timerObject.SetActive(true);
        countdownText.text = "Game Over"; 
        countdownshadowText.text = "Game Over";
        isGameStarted = false;
        //SceneManager.LoadScene(goToScene);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    private void Update()
    {

    }

  
}
