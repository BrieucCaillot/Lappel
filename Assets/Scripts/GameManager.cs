﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public bool pressedSpace = false; 
    public bool isPlayable = false; 
    public bool introShowed = false; 

    void Start()
    {
        StartCoroutine("LetsGo");
        // AudioManager.Instance.PlaySound(AudioManager.Sound.Whatever);
    }

    private void Update()
    {
        if (pressedSpace || !introShowed) return;
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("PRESSED SPACE");
            pressedSpace = true;
            MainSceneManager.Play();
        }
    }

    IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Instance.ShowIntro();
    }
}