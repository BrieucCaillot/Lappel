using UnityEngine;

public class MainSceneManager : MonoBehaviour {
    
    public static void Play()
    {
        Debug.Log("MAIN SCENE PLAY");
        UIManager.Instance.HideIntro();
        PlayerManager.Instance.RotateIntro();
        CameraManagerTimeline.Instance.StartTimeline("introToDefault");
    }
}