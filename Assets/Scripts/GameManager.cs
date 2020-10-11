using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {    
        UIManager.Instance.HideIntro();
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
   
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Aurore Scene");
    }

    void Update()
    {
        
    }

    public void IntroSceneCompleted() {
        AudioManager.Instance.PlaySound("Underwater");
        
        Debug.Log("INTRO SCENE COMPLETED");
    }
}