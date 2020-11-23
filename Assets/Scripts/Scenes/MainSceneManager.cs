using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private void Start()
    {
        EnvironmentManager.Instance.SnowEnvironment();
    }
    
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
        {
            SceneManager.LoadSceneAsync("Cascade Scene");
        }
    }

    public static void Play()
    {
        Debug.Log("MAIN SCENE PLAY");
        UIManager.Instance.HideIntro();
        PlayerManager.Instance.RotateIntro();
        CameraManagerTimeline.Instance.StartTimeline("introToDefault");
    }
}