using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    private void Start()
    {
        // LoadScenes("Main Scene Environment", true);
        // LoadScenes("Main Scene Core", true);
    }

    public void LoadScenes(string sceneName, bool additive = false)
    {
        SceneManager.LoadSceneAsync(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

    public void UnloadScenes(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
