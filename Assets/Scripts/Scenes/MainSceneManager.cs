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
        glacierTransition.transform.position = new Vector3(170, -8.5f, 37);
        SceneManager.LoadSceneAsync("Cascade Scene");
        CascadeSceneManager.Play();
    }
}