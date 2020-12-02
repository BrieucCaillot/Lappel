using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnderwaterSceneManager : Singleton<UnderwaterSceneManager>
{
    [SerializeField]
    private Transform fishPosition = null;

    private bool transitionStarted = false;

    [SerializeField]
    private float offsetX = 0f;
    [SerializeField]
    private float offsetY = 0f;

    [SerializeField]
    private float offsetZ = -17f;
    
    private void Start()
    {
        Debug.Log("UNDERWATER SCENE START");
        CameraManager.Instance.StartTimeline("cascadeSceneRightToUnderwater");
        PlayerManager.Instance.SetPosition(new Vector3(0, -140, 0));
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        PlayerManager.Instance.speed = 20;
        PlayerAnimManager.Instance.StartUnderwaterAnim();
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }

    private void Update()
    {
        if (fishPosition != null)
        {
            fishPosition.position = new Vector3(CameraManager.Instance.MainCameraPosition().x + offsetX, CameraManager.Instance.MainCameraPosition().y + offsetY, CameraManager.Instance.MainCameraPosition().z + offsetZ);
            if (transitionStarted)
            {

                if (offsetZ >= -5)
                {
                    StartCoroutine(LoadYourAsyncScene());
                }
                else
                {
                    offsetZ = offsetZ + 0.1f;
                }
            }
        }
    }

    public void StartFishTransition()
    {
        transitionStarted = true;
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Mountain Scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}