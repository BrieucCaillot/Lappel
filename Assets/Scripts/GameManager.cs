using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {    
        UIManager.Instance.HideIntro();
    }

    void Update()
    {
    }

    public void IntroSceneCompleted() {
        AudioManager.Instance.PlaySound("Underwater");
        Debug.Log("INTRO SCENE COMPLETED");
    }
}