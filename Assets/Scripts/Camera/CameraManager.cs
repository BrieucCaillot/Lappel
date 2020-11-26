using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CameraManager : Singleton<CameraManager>
{
    public PlayableDirector mainSceneIntroToDefault;
    // public PlayableDirector mainSceneDefaultToOutro;
    public PlayableDirector mainSceneDefaultToSides;
    public PlayableDirector cascadeSceneDefaultToRight;
    public PlayableDirector cascadeSceneRightToDefault;
    public PlayableDirector cascadeSceneRightToUnderwater;
    public PlayableDirector underwaterToMoutain;

    public GameObject glacier;

    public void StartTimeline(string Movement)
    {
        switch (Movement)
        {
            // MAIN SCENE
            case "mainSceneIntroToDefault":
                mainSceneIntroToDefault.Play();
                break;
            case "mainSceneDefaultToSides":
                mainSceneDefaultToSides.Play();
                StartCoroutine("StartCascadeScene");
                break;
                // case "mainSceneDefaultToOutro":
                //     mainSceneDefaultToOutro.Play();

                // break;

                // CASCADE SCENE
            case "cascadeSceneDefaultToRight":
                cascadeSceneRightToDefault.Stop();
                cascadeSceneDefaultToRight.Play();
                break;
            case "cascadeSceneRightToDefault":
                cascadeSceneDefaultToRight.Stop();
                cascadeSceneRightToDefault.Play();
                break;
            case "cascadeSceneRightToUnderwater":
                cascadeSceneRightToUnderwater.Play();
                break;

                //UNDERWATER
            case "underwaterToMoutain":
                //poisson qui passe devant la cam
                UnderwaterSceneManager.Instance.StartFishTransition();

                break;
            default:
                break;
        }
    }

    IEnumerator StartCascadeScene()
    {
        yield return new WaitForSeconds(47f);
        glacier.transform.position = new Vector3(170, -8.5f, 37);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Cascade Scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        CascadeSceneManager.Play();
    }

}