using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CameraManager : Singleton<CameraManager> {
    public PlayableDirector mainSceneIntroToDefault;
    public PlayableDirector mainSceneDefaultToOutro;
    public PlayableDirector mainSceneDefaultToSides;
    public PlayableDirector cascadeSceneDefaultToRight;
    public PlayableDirector cascadeSceneRightToDefault;

    public GameObject glacier;

    public void StartTimeline(string Movement) {
        switch (Movement) {
            // MAIN SCENE
            case "mainSceneIntroToDefault":
                mainSceneIntroToDefault.Play();
                break;
            case "mainSceneDefaultToSides":
                mainSceneDefaultToSides.Play();
                break;
            case "mainSceneDefaultToOutro":
                mainSceneDefaultToOutro.Play();
                StartCoroutine("StartTransitionToEnv2");
                break;
            
            // CASCADE SCENE
            case "cascadeSceneDefaultToRight":
                cascadeSceneRightToDefault.Stop();
                cascadeSceneDefaultToRight.Play();
                break;
            case "cascadeSceneRightToDefault":
                cascadeSceneDefaultToRight.Stop();
                cascadeSceneRightToDefault.Play();
                break;
            default:
                break;
        }
    }

    IEnumerator StartTransitionToEnv2() {
        yield return new WaitForSeconds(5f);
        glacier.transform.position = new Vector3(170, -8.5f, 37);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Cascade Scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        CascadeSceneManager.Play();
    }
}