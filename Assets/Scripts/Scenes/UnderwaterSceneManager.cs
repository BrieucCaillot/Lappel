using UnityEngine;
using UnityEngine.SceneManagement;

public class UnderwaterSceneManager : Singleton<UnderwaterSceneManager>
{

    public Transform fishPosition;

    private bool transitionStarted = false;

    [SerializeField]
    private float offsetX = 0f;
    [SerializeField]
    private float offsetY = 0f;

    [SerializeField]
    private float offsetZ = -17f;
    private void Start()
    {
        // @TODO Remove when project is done 
        // cameraPosition = Camera.main.
        PlayerManager.Instance.SetPosition(new Vector3(0, -140, 0));
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        PlayerAnimManager.Instance.StartUnderwaterAnim();
        PlayerManager.Instance.speed = 12;
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }
    
    private void Update()
    {
        print(fishPosition);
        fishPosition.position = new Vector3(CameraManager.Instance.MainCameraPosition().x + offsetX, CameraManager.Instance.MainCameraPosition().y + offsetY, CameraManager.Instance.MainCameraPosition().z + offsetZ);
        if (transitionStarted)
        {
            offsetZ = offsetZ + 0.1f;

            if (offsetZ >= -5)
            {
                CameraManager.Instance.underwaterToMoutain.Play();
                NextScene();
            }
        }
    }

    public void Play()
    {
        Debug.Log("UNDERWATER SCENE PLAY");
        SceneManager.LoadSceneAsync("Underwater Scene");
        PlayerManager.Instance.SetPosition(new Vector3(0, -140, 0));
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        PlayerManager.Instance.speed = 12;
        PlayerAnimManager.Instance.StartUnderwaterAnim();
    }

    public void StartFishTransition()
    {
        transitionStarted = true;
        // PlayerManager.Instance.canMove = false;
        // fishPosition.DOMove(fishDestination.position, 15f);
    }

    public void NextScene()
    {
        MountainSceneManager.Instance.Play();
    }
}