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

    [Header("Aurore Sounds")]
    [SerializeField]
    private AudioSource Aurore = null;
    [SerializeField]
    private GameObject AuroreCall = null;
    [NonSerialized]
    public float auroreDuration = 0.0f;
    private AudioSource AuroreCallSource = null;
    
    [Header("Ambiants Sounds")]
    [SerializeField]
    private AudioSource ambiantMusic = null;
    [SerializeField]
    private AudioClip ambiant1 = null;
    [SerializeField]
    private AudioClip ambiant2 = null;
    [SerializeField]
    private AudioClip ambiant3 = null;

    [Header("Winds Sounds")]
    [SerializeField]
    private AudioSource wind = null;
    [SerializeField]
    private AudioClip windMainScene = null;
    [SerializeField]
    private AudioClip windCascadeScene = null;

    void Start()
    {
        AuroreCallSource = AuroreCall.GetComponent<AudioSource>();
        // auroreDuration = AuroreCallSource.clip.length;
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
        finalSceneSnapshot.TransitionTo(2f);
    }

    public void PlayAurore()
    { 
        Aurore.Play();   
    }
    
    public void PlayAuroreCall()
    {
        AuroreCallSource.panStereo = 0f;   
        AuroreCallSource.spatialBlend = Single.MinValue;   
        AuroreCallSource.Play();
        CameraManager.Instance.ShakeCameraAurore(auroreDuration, 1f);
    }

    public void PlayAuroreCallMainScene()
    {
        AuroreCallSource.panStereo = -0.6f;   
        AuroreCallSource.spatialBlend = Single.MinValue;   
        AuroreCallSource.Play();
        CameraManager.Instance.ShakeCameraAurore(auroreDuration, 1f);
    }

    public void MoveAuroreCall(Vector3 position)
    {
        AuroreCall.transform.position = position;
    }

    public void PlayAmbiant1()
    {
        ambiantMusic.clip = ambiant1;
        ambiantMusic.Play();
    }
    
    public void PlayAmbiant2()
    {
        ambiantMusic.clip = ambiant2;
        ambiantMusic.Play();
    }
    
    public void PlayAmbiant3()
    {
        ambiantMusic.clip = ambiant3;
        ambiantMusic.Play();
    }

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
            default:
                break;
        }   
        wind.Play();
    }
}
