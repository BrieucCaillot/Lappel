using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public int sceneIndex = 0;
    public bool isPlayable = false; 

    void Start()
    {
        // UIManager.Instance.HideIntro();
        // StartCoroutine(SwitchScene());
        // AudioManager.Instance.PlaySound(AudioManager.Sound.Whatever);
    }

    private void Update()
    {
        if (isPlayable) return;
        if (Input.GetKey(KeyCode.Space)) isPlayable = true;
    }

    public void SwitchScene()
    {
        sceneIndex++;
        
        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Main Scene");
                break;
            case 1:
                SceneManager.LoadScene("Banquise Scene");
                break;
            case 2:
                SceneManager.LoadScene("Montagne Scene");
                break;
            case 3:
                SceneManager.LoadScene("Eau Scene");
                break;
            case 4:
                SceneManager.LoadScene("Dinosaure Scene");
                break;
            case 5:
                SceneManager.LoadScene("Aurore Scene");
                break;
            default:
                SceneManager.LoadScene("Main Scene");
                break;
        }
    }

    public void IntroSceneCompleted() {
        Debug.Log("INTRO SCENE COMPLETED");
    }
}