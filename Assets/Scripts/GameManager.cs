using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public bool DebugMode = false;
    public bool pressedSpace = false; 
    public bool canMove = false; 
    public bool canRotateIntro = false; 
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
            pressedSpace = true;
            MainSceneManager.Play();
        }
    }

    IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(7f);
        UIManager.Instance.ShowIntro();
    }
}