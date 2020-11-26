using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class PenguinCrySound : MonoBehaviour
{
    
    public enum DisplayCategory
    {
        Player, ManchotGroup
    }
    
    [Header("Cry Audios")]
    [SerializeField] 
    private DisplayCategory manchotType = DisplayCategory.Player;
    [SerializeField]
    private AudioSource cryAudioSource = null;
    [SerializeField]
    private AudioMixerGroup[] cryAudioMixerGroups = null;
    [SerializeField]
    private AudioClip[] cryAudioClips = null;

    private void Start()
    {
        PickRandomSound();
        if (manchotType == DisplayCategory.Player)
        {
            cryAudioSource.outputAudioMixerGroup = cryAudioMixerGroups[0];
            cryAudioSource.spatialBlend = Single.MinValue;

        } else if (manchotType == DisplayCategory.ManchotGroup)
        {
            cryAudioSource.outputAudioMixerGroup = cryAudioMixerGroups[1];
            cryAudioSource.spatialBlend = Single.MaxValue;
        }
    }
    
    public void PlayFootstepSound()
    {
        PickRandomSound();
        cryAudioSource.Play();
    }
    
    private void PickRandomSound()
    {
        cryAudioSource.clip = cryAudioClips[Random.Range(0, cryAudioClips.Length - 1)];
    }
}
