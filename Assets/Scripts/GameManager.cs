using System;
using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public bool DebugMode = false;
    [NonSerialized]
    public bool enteredGame = false;  
    [NonSerialized]
    public bool introShowed = false; 
    
    public event Action onPlayerStart;

    void Start()
    {
        if (DebugMode) return;
        onPlayerStart += EnterGame;
        StartCoroutine("LetsGo");
        // AudioManager.Instance.PlaySound(AudioManager.Sound.Whatever);
    }

    private void Update()
    {
        if (DebugMode) enteredGame = true;
        if (enteredGame || !introShowed) return;
        if (Input.GetKey(KeyCode.Space)) PlayerEnterGame();
    }
    
    private void PlayerEnterGame()
    {
        if (onPlayerStart != null) onPlayerStart();
    }

    private void EnterGame()
    {
        enteredGame = true;
        MainSceneManager.Play();
    }

    IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(7f);
        UIManager.Instance.ShowIntro();
    }
}