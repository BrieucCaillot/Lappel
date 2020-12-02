using UnityEngine;

public class UnderwaterSceneManager : Singleton<UnderwaterSceneManager>
{

    private void Start()
    {
        Debug.Log("UNDERWATER SCENE START");
        CameraManager.Instance.StartTimeline("cascadeSceneRightToUnderwater");
        PlayerManager.Instance.SetPosition(new Vector3(0, -140, 0));
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        PlayerManager.Instance.speed = 20;
        PlayerManager.Instance.canMove = true;
        PlayerAnimManager.Instance.StartSwimIdleAnim();
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }
}