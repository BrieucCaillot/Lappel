using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainSceneManager : MonoBehaviour
{

    public static void Play()
    {
        Debug.Log("MOUNTAIN SCENE PLAY");
        SceneManager.LoadSceneAsync("Mountain Scene");
        PlayerManager.Instance.SetPosition(new Vector3(0, 0, 0));
        PlayerManager.Instance.speed = 6;
        PlayerAnimManager.Instance.StartIdleAnim();
    }
}
