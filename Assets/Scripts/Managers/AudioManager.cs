using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum Sound
    {
        Intro,
        Outro,
        Whatever
    }

    public SoundAudioClip[] soundAudioClips;
    
    [System.Serializable] 
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    public void PlaySound(Sound sound)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (var soundAudioClip in soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}
