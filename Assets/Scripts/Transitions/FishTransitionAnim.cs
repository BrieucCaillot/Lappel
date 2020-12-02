using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishTransitionAnim : MonoBehaviour {
    
    public void OnMiddleFishAnimation() {
        StartCoroutine(LoadMountainScene());
    }
    
    IEnumerator LoadMountainScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Mountain Scene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}