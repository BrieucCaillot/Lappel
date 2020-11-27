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
    private AudioMixerSnapshot finalSceneSnapshot = null;

    [Header("Ambiants Sounds")]
    [SerializeField]
    private AudioSource Aurore = null;
    [SerializeField]
    private GameObject AuroreCall = null;
    private AudioSource AuroreCallSource = null;
    [SerializeField]
    private AudioSource Ambiant1 = null;
    [SerializeField]
    private AudioSource Ambiant2 = null;

    private void Start()
    {
        AuroreCallSource = AuroreCall.GetComponent<AudioSource>();
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
        underwaterSceneSnapshot.TransitionTo(1f);
    }
    
    public void MountainSceneSnapshot()
    {
        mountainSceneSnapshot.TransitionTo(2f);
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
        AuroreCallSource.spatialBlend = Single.MaxValue;   
        AuroreCallSource.Play();
    }

    public void PlayAuroreCallMainScene()
    {
        AuroreCallSource.spatialBlend = Single.MinValue;   
        AuroreCallSource.Play();
    }

    public void MoveAuroreCall(Vector3 position)
    {
        AuroreCall.transform.position = position;
    }

    public void PlayAmbiant1()
    {
        Ambiant1.Play();
    }
    
    public void PlayAmbiant2()
    {
        Ambiant2.Play();
    }
}
