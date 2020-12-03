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

    public enum SceneType
    {
        MainScene,
        CascadeScene,
        UnderwaterScene,
        MountainScene,
        FinalScene
    }

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

    // void OnGUI()
    // {
        // if (GUI.Button(new Rect(10, 20, 150, 50), "Idle"))
        // {
        //     PlayerAnimManager.Instance.StartIdleAnim();
        // }
        
        // if (GUI.Button(new Rect(10, 90, 150, 50), "Slide"))
        // {
        //     PlayerAnimManager.Instance.StartSlideIdleAnim();
        // }
        //
        // if (GUI.Button(new Rect(10, 150, 150, 50), "Swim"))
        // {
        //     PlayerAnimManager.Instance.StartSwimIdleAnim();
        // }
        //
        // if (GUI.Button(new Rect(10, 220, 150, 50), "Mountain"))
        // {
        //     PlayerAnimManager.Instance.StartMountainIdleAnim();
        // }
        // if (GUI.Button(new Rect(10, 20, 150, 50), "Crevasse"))
        // {
        //     PlayerAnimManager.Instance.StartCrevasseAnim();
        //
        // }
        // if (GUI.Button(new Rect(10, 80, 150, 50), "Cascade"))
        // {
        //     PlayerAnimManager.Instance.StartCascadeAnim();
        // }
        //
        // if (GUI.Button(new Rect(10, 180, 150, 50), "Slide"))
        // {
        //     PlayerAnimManager.Instance.StartSlideIdleAnim();
        // }
    // }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetKeyDown(KeyCode.LeftShift)) {
            UIManager.Instance.HideCommandKeys();
            UIManager.Instance.HideCommandShift();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) PlayerAnimManager.Instance.StartSlideIdleAnim();
        if (Input.GetKeyUp(KeyCode.LeftShift)) PlayerAnimManager.Instance.StartIdleAnim();
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
        // PlayerManager.Instance.speed = 20;
        CameraManager.Instance.StartTimeline("mainSceneIntroToDefault");
    }
}