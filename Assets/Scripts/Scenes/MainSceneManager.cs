using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private void Start()
    {
        EnvironmentManager.Instance.SnowEnvironment();
    }

    public static void Play()
    {
        Debug.Log("MAIN SCENE PLAY");
        UIManager.Instance.HideIntro();
        PlayerManager.Instance.RotateIntro();
        CameraManagerTimeline.Instance.StartTimeline("mainSceneIntroToDefault");
    }
}