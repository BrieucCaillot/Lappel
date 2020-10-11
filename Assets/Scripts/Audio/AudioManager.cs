using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager Instance;
    private static AudioSource audioSource;
    private static AudioClip audioClip;
    private string path;
    private string audioName;

    /// <summary>
    /// Create Audio component on Awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            path = "file://" + Application.streamingAssetsPath + "/Audio/Sounds/";
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Play sound with coroutine
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name)
    {
        PauseSound();
        if (!IsPlaying())
        {
            StartCoroutine(GetAudioClip(name));
        }
    }

    /// <summary>
    /// Pause sound
    /// </summary>
    public static void PauseSound()
    {
        audioSource.Pause();
    }
    
    /// <summary>
    /// Unpause sound
    /// </summary>
    public static void UnPauseSound()
    {
        audioSource.UnPause();
    }

    IEnumerator GetAudioClip(string name)
    {
        
        audioName = name + ".wav";

        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("http://a6.radioheart.ru:8012/live", AudioType.WAV))
        {
            yield return request.SendWebRequest();
            Debug.Log("WTF" + request + audioName);

            Debug.Log(request);
            Debug.Log("WHATS GOING ON");

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("COME ON");
                audioClip = DownloadHandlerAudioClip.GetContent(request);
                if (audioClip)
                {
                    audioClip.name = audioName;
                    audioSource.loop = true;
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }
            }
        }
    }

    /// <summary>
    /// Returns if audio is playing
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
