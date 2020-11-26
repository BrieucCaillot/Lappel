using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class PenguinFootstepsSound : MonoBehaviour
{
    
    public enum DisplayCategory
    {
        Player, ManchotGroup
    }
    
    [Header("Footprint Audios")]
    [SerializeField] 
    private DisplayCategory manchotType = DisplayCategory.Player;
    [SerializeField]
    private AudioSource footstepsAudioSource = null;
    [SerializeField]
    private AudioMixerGroup[] footstepsAudioMixerGroups = null;
    [SerializeField]
    private AudioClip[] footstepsAudioClips = null;
    

    private void Start()
    {
        PickRandomSound();
        if (manchotType == DisplayCategory.Player)
        {
            footstepsAudioSource.outputAudioMixerGroup = footstepsAudioMixerGroups[0];
            footstepsAudioSource.spatialBlend = Single.MinValue;

        } else if (manchotType == DisplayCategory.ManchotGroup)
        {
            footstepsAudioSource.outputAudioMixerGroup = footstepsAudioMixerGroups[1];
            footstepsAudioSource.spatialBlend = Single.MaxValue;
        }
    }

    public void PlayFootstepSound()
    {
        PickRandomSound();
        footstepsAudioSource.Play();
    }
    
    
    private void PickRandomSound()
    {
        footstepsAudioSource.clip = footstepsAudioClips[Random.Range(0, footstepsAudioClips.Length - 1)];
    }
}
