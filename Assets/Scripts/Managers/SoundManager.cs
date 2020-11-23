using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioMixerSnapshot cascadeSceneSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot underwaterSceneSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot mountainSceneSnapshot = null;
    
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

    public void SoundInInteractionZone()
    {
        print("INNNN");
    }
    
    public void SoundOutInteractionZone()
    {
        print("OUTTTTT");
    }
}
