using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Snapshots")]
    [SerializeField]
    private AudioMixerSnapshot mainSceneIntroSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot cascadeSceneSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot underwaterSceneSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot mountainSceneSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot mountainSceneCorridorSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot finalSceneCaveSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot finalSceneSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot finalSceneOutroSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot finalSceneCreditsSnapshot = null;

    [Header("Aurore Sounds")]
    [SerializeField]
    private AudioSource Aurore = null;
    [NonSerialized]
    public float auroreDuration = 0.0f;
    [SerializeField]
    private GameObject auroreCall = null;
    [SerializeField]
    private AudioSource auroreCallSource = null;
    [SerializeField]
    private AudioClip auroreCallMainScene = null;
    [SerializeField]
    private AudioClip auroreCallCascadeScene = null;
    [SerializeField]
    private AudioClip auroreCallUnderwaterScene = null;
    [SerializeField]
    private AudioClip auroreCallMountainScene = null;
    [SerializeField]
    private AudioClip auroreCallFinalScene = null;

    [Header("Ambiants Sounds")]
    [SerializeField]
    private AudioSource ambiantMusic = null;
    [SerializeField]
    private AudioSource ambiantCave = null;
    [SerializeField]
    private AudioClip ambiantMainScene = null;
    [SerializeField]
    private AudioClip ambiantUnderwaterScene = null;
    [SerializeField]
    private AudioClip ambiantMountainScene = null;
    [SerializeField]
    private AudioClip ambiantFinalScene = null;

    [Header("Winds Sounds")]
    [SerializeField]
    private AudioSource wind = null;
    [SerializeField]
    private AudioClip windMainScene = null;
    [SerializeField]
    private AudioClip windCascadeScene = null;
    [SerializeField]
    private AudioClip windMountainScene = null;
    [SerializeField]
    private AudioClip windFinalScene = null;

    [Header("Final Scene Sounds")]
    [SerializeField]
    private AudioSource outro = null;

    void Start()
    {
        // auroreDuration = auroreCallSource.clip.length;
        auroreDuration = 4f;
    }

    public void MainSceneIntroSnapshot()
    {
        mainSceneIntroSnapshot.TransitionTo(4f);
    }

    public void CascadeSceneSnapshot()
    {
        cascadeSceneSnapshot.TransitionTo(4f);
    }

    public void UnderwaterSceneSnapshot()
    {
        underwaterSceneSnapshot.TransitionTo(0.3f);
    }

    public void MountainSceneSnapshot()
    {
        mountainSceneSnapshot.TransitionTo(2f);
    }

    public void MountainSceneCorridorSnapshot()
    {
        mountainSceneCorridorSnapshot.TransitionTo(1f);
    }

    public void FinalSceneCaveSnapshot()
    {
        finalSceneCaveSnapshot.TransitionTo(2f);
    }

    public void FinalSceneSnapshot()
    {
        finalSceneSnapshot.TransitionTo(0f);
    }

    public void FinalSceneOutroSnapshot()
    {
        finalSceneOutroSnapshot.TransitionTo(1f);
    }

    public void FinalSceneCreditsSnapshot()
    {
        finalSceneCreditsSnapshot.TransitionTo(5f);
    }

    public void PlayAurore()
    {
        Aurore.Play();
    }

    public void PlayOutro()
    {
        outro.Play();
    }

    // AMBIANT

    public void PlayAmbiant()
    {
        ambiantMusic.Play();
    }

    public void PlayAmbiantCave()
    {
        ambiantCave.Play();
    }

    // PICK AND PLAY AMBIANT SOUND
    public void PickAmbiant(GameManager.SceneType sceneType)
    {
        Debug.Log("PICK AMBIANT CALL");
        switch (sceneType)
        {
            case GameManager.SceneType.MainScene:
                ambiantMusic.clip = ambiantMainScene;
                break;
            case GameManager.SceneType.CascadeScene:
                wind.clip = null;
                auroreCallSource.clip = null;
                break;
            case GameManager.SceneType.UnderwaterScene:
                ambiantMusic.clip = ambiantUnderwaterScene;
                break;
            case GameManager.SceneType.MountainScene:
                ambiantMusic.clip = ambiantMountainScene;
                break;
            case GameManager.SceneType.FinalScene:
                ambiantMusic.clip = ambiantFinalScene;
                break;
            default:
                break;
        }
    }

    // AURORE

    public void PlayAuroreCall()
    {
        auroreCallSource.panStereo = 0f;
        auroreCallSource.spatialBlend = Single.MinValue;
        auroreCallSource.Play();
        CameraManager.Instance.ShakeCameraAurore(auroreDuration, 1f);
    }

    public void PlayAuroreCallMainScene()
    {
        auroreCallSource.panStereo = -0.6f;
        auroreCallSource.spatialBlend = Single.MinValue;
        auroreCallSource.Play();
        CameraManager.Instance.ShakeCameraAurore(auroreDuration, 1f);
    }

    public void MoveAuroreCall(Vector3 position)
    {
        auroreCall.transform.position = position;
    }

    // PICK AURORE CALL SOUND
    public void PickAuroreCall(GameManager.SceneType sceneType)
    {
        Debug.Log("PICK AURORE CALL");
        switch (sceneType)
        {
            case GameManager.SceneType.MainScene:
                auroreCallSource.clip = auroreCallMainScene;
                break;
            case GameManager.SceneType.CascadeScene:
                wind.clip = windCascadeScene;
                auroreCallSource.clip = auroreCallCascadeScene;
                break;
            case GameManager.SceneType.UnderwaterScene:
                auroreCallSource.clip = auroreCallUnderwaterScene;
                break;
            case GameManager.SceneType.MountainScene:
                auroreCallSource.clip = auroreCallMountainScene;
                break;
            case GameManager.SceneType.FinalScene:
                auroreCallSource.clip = auroreCallFinalScene;
                break;
            default:
                break;
        }
    }

    // WIND

    // PICK WIND SOUND AND PLAY
    public void PlayWind(GameManager.SceneType sceneType)
    {
        switch (sceneType)
        {
            case GameManager.SceneType.MainScene:
                wind.clip = windMainScene;
                break;
            case GameManager.SceneType.CascadeScene:
                wind.clip = windCascadeScene;
                break;
            case GameManager.SceneType.MountainScene:
                wind.clip = windMountainScene;
                break;
            case GameManager.SceneType.FinalScene:
                wind.clip = windFinalScene;
                break;
            default:
                break;
        }
        wind.Play();
    }
}