using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CascadeTransitionAnim : MonoBehaviour {
    public void OnMiddleCascadeAnimation() {
        StartCoroutine(LoadUnderwaterScene());
    }
    
    IEnumerator LoadUnderwaterScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Underwater Scene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}