using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageLoader : MonoBehaviour
{
    public void Game()
    {
        Application.Quit();
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void LoadLoginScene()
    {
        AudioManager.instance.playStartEngine();
        this.Wait(5f, () => { SceneManager.LoadScene("Login"); });
    }
}
