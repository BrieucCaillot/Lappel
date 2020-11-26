using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnderwaterSceneManager : Singleton<UnderwaterSceneManager>
{

    public Transform fishPosition;

    public Transform fishDestination;

    public Transform cameraPosition;

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
        PlayerManager.Instance.SetPosition(new Vector3(0, -139, 0));
        PlayerAnimManager.Instance.StartUnderwaterAnim();
        PlayerManager.Instance.speed = 12;
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }

    public void Play()
    {
        Debug.Log("UNDERWATER SCENE PLAY");
        SceneManager.LoadSceneAsync("Underwater Scene");
        PlayerManager.Instance.SetPosition(new Vector3(0, -139, 0));
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

    private void Update()
    {
        fishPosition.position = new Vector3(cameraPosition.position.x + offsetX, cameraPosition.position.y + offsetY, cameraPosition.position.z + offsetZ);
        if (transitionStarted)
        {
            offsetZ = offsetZ + 0.1f;

            if (offsetZ >= -5)
            {
                Debug.Log("CUT");
                CameraManager.Instance.underwaterToMoutain.Play();
                NextScene();
            }
        }
    }
}