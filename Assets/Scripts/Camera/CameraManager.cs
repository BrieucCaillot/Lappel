using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    public Camera mainCamera;
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
                MainSceneManager.Instance.OnTriggerEnterColliderTimeline();
                mainSceneDefaultToSides.Play();
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

    public Vector3 MainCameraPosition()
    {
        return mainCamera.transform.position;
    }
}