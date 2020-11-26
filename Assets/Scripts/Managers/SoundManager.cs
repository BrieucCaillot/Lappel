using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Snapshots")]
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
    private AudioSource Ambiant1 = null;
    [SerializeField]
    private AudioSource Ambiant2 = null;
    
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

    public void PlayAmbiant1()
    {
        Ambiant1.Play();
    }
    
    public void PlayAmbiant2()
    {
        Ambiant2.Play();
    }
}
