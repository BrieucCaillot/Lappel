using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishTransitionAnim : MonoBehaviour {
    
    private AsyncOperation asyncLoadMountainScene = null;
    
    // private void Start()
    // {
    //     if (SceneManager.GetActiveScene().name == "Underwater Scene");
    // }
    
    public void OnMiddleFishAnimation() {
        StartCoroutine(LoadMountainScene());
    }
    
    IEnumerator LoadMountainScene()
    {
        asyncLoadMountainScene = SceneManager.LoadSceneAsync("Mountain Scene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoadMountainScene.isDone)
        {
            yield return null;
        }
    }
}