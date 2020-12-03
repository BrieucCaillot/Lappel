using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishTransitionAnim : MonoBehaviour {
    
    private AsyncOperation asyncLoadMountainScene = null;
    
    private void Start()
    {
        StartCoroutine(LoadMountainScene());
    }
    
    public void OnMiddleFishAnimation() {
        asyncLoadMountainScene.allowSceneActivation = true;
    }
    
    IEnumerator LoadMountainScene()
    {
        yield return new WaitForSeconds(3f);
        
        asyncLoadMountainScene = SceneManager.LoadSceneAsync("Mountain Scene");
        asyncLoadMountainScene.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoadMountainScene.isDone)
        {
            yield return null;
        }
    }
}