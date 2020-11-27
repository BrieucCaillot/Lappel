using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : Singleton<MainSceneManager>
{
    [SerializeField]
    private Transform glacierTransition = null;
    
    private void Start()
    {
        EnvironmentManager.Instance.SnowEnvironment();
    }
    
    public void Play()
    {
        Debug.Log("MAIN SCENE PLAY");
        UIManager.Instance.HideInteraction();
        PlayerManager.Instance.RotateIntro();
        CameraManager.Instance.StartTimeline("mainSceneIntroToDefault");
        SoundManager.Instance.MoveAuroreCall(new Vector3(0, 0, -200));
    }

    public void OnTriggerEnterColliderTimeline()
    {
        PlayerManager.Instance.autoMoveIntro = false;
        PlayerManager.Instance.canMove = true;
        UIManager.Instance.ShowCommands();
    }

    public void NextSene()
    {
        StartCoroutine(LoadCascadeScene());
        glacierTransition.transform.position = new Vector3(170, -8.5f, 37);
    }
    
    IEnumerator LoadCascadeScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Cascade Scene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}