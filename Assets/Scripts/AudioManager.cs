using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickbutton;
    public AudioSource startengine;
    public AudioSource desertBGM;
    public AudioSource addpoint;
    public AudioSource minuspoint;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void playClickButton()
    {
        clickbutton.Play();
    }

    public void playStartEngine()
    {
        startengine.Play();
    }

    public void playDesertBGM()
    {
        desertBGM.Play();
    }

    public void playAddPoint()
    {
        addpoint.Play();
    }
    public void playMinusPoint()
    {
        minuspoint.Play();
    }
}
