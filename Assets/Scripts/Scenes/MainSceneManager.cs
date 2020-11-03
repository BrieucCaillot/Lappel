using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour {

    public GameObject loopObj;
    void Start() {
        //ScenesManager.Instance.LoadScenes("Main Scene Environment", true);
        //ScenesManager.Instance.LoadScenes("Main Scene Core", true);
    }


    public static void Play()
    {
        Debug.Log("MAIN SCENE PLAY");
        GameManager.Instance.isPlayable = true;
        UIManager.Instance.HideIntro();
        CameraManagerTimeline.Instance.StartTimeline("introToDefault");
    }
}