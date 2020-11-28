using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (DebugMode) OnDebugMode();
        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            onPlayerStart += EnterGame;
        }
        else
        {
            EnterGame();
            PlayerManager.Instance.canMove = true;
        }
    }
    //
    // void OnGUI()
    // {
    //     if (GUI.Button(new Rect(10, 10, 150, 50), "Idle"))
    //     {
    //         PlayerAnimManager.Instance.StartIdleAnim();
    //     }
    //     
    //     if (GUI.Button(new Rect(10, 100, 150, 50), "Cascade"))
    //     {
    //         PlayerAnimManager.Instance.StartCascadeAnim();
    //     }
    //     
    //     if (GUI.Button(new Rect(10, 200, 150, 50), "Underwater"))
    //     {
    //         PlayerAnimManager.Instance.StartUnderwaterAnim();
    //     }
    // }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) UIManager.Instance.HideCommandKeys();
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
        if (SceneManager.GetActiveScene().name == "Main Scene") MainSceneManager.Instance.Play();
    }

    private void OnDebugMode()
    {
        enteredGame = true;
        PlayerManager.Instance.canMove = true;
        PlayerManager.Instance.speed = 20;
        CameraManager.Instance.StartTimeline("mainSceneIntroToDefault");
    }
}