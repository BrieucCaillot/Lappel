using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private Camera mainCamera = null;
    private CinemachineBrain cinemachineBrain = null;
    [SerializeField]
    private PlayableDirector mainSceneIntroToDefault = null;
    // [SerializeField] private PlayableDirector mainSceneDefaultToOutro;
    [SerializeField]
    private PlayableDirector mainSceneDefaultToSides = null;
    [SerializeField]
    private PlayableDirector cascadeSceneDefaultToRight = null;
    [SerializeField]
    private PlayableDirector cascadeSceneRightToDefault = null;
    [SerializeField]
    private PlayableDirector cascadeSceneRightToUnderwater = null;

    public PlayableDirector underwaterToMoutain = null;

    [SerializeField]
    private PlayableDirector mountainDefaultToFar = null;
    [SerializeField]
    private PlayableDirector mountainFarToClose = null;
    [SerializeField]
    private PlayableDirector finalDefaultToSide = null;
    [SerializeField]
    private PlayableDirector finalSideToUp = null;
    [SerializeField]
    private PlayableDirector finalOutro = null;

    public GameObject glacier;

    void Start()
    {
        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
    }

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

            case "mountainDefaultToFar":
                mountainDefaultToFar.Play();
                break;
            case "mountainFarToClose":
                mountainFarToClose.Play();
                break;
            case "finalDefaultToSide":
                finalDefaultToSide.Play();
                break;
            case "finalSideToUp":
                finalSideToUp.Play();
                break;
            case "finalOutro":
                finalOutro.Play();
                break;
            default:
                break;
        }
    }

    public Vector3 MainCameraPosition()
    {
        return mainCamera.transform.position;
    }

    public void ShakeCameraAurore(float amplitude, float frequency)
    {
        cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CameraShake>()
            .ShakeCameraAurore(amplitude, frequency);
    }
}