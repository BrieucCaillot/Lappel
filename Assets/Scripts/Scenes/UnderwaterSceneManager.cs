using UnityEngine;

public class UnderwaterSceneManager : Singleton<UnderwaterSceneManager>
{

    private void Start()
    {
        Debug.Log("UNDERWATER SCENE START");
        CameraManager.Instance.StartTimeline("cascadeSceneRightToUnderwater");
        PlayerManager.Instance.speed = 20;
        PlayerManager.Instance.canMove = true;
        PlayerAnimManager.Instance.StartSwimIdleAnim();
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }
}