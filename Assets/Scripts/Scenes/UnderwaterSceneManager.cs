using UnityEngine;
using UnityEngine.SceneManagement;

public class UnderwaterSceneManager : MonoBehaviour
{
    private void Start()
    {
        PlayerManager.Instance.SetPosition(new Vector3(0, 0, 0));
        PlayerAnimManager.Instance.StartUnderwaterAnim();
        PlayerManager.Instance.speed = 12;
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }

    public static void Play()
    {
        Debug.Log("UNDERWATER SCENE PLAY");
        SceneManager.LoadSceneAsync("Underwater Scene");
        PlayerManager.Instance.SetPosition(new Vector3(0, 0, 0));
        PlayerManager.Instance.speed = 12;
        PlayerAnimManager.Instance.StartUnderwaterAnim();
    }

    public static void NextScene()
    {
        MountainSceneManager.Play();
    }
    
}
