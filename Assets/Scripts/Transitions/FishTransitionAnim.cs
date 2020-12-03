using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishTransitionAnim : MonoBehaviour {
    
    private AsyncOperation asyncLoad = null;
    
    private void Start()
    {
        StartCoroutine(LoadMountainScene());
    }
    
    public void OnMiddleFishAnimation() {
        asyncLoad.allowSceneActivation = true;
    }
    
    IEnumerator LoadMountainScene()
    {
        yield return new WaitForSeconds(3f);
        
        asyncLoad = SceneManager.LoadSceneAsync("Mountain Scene");
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}