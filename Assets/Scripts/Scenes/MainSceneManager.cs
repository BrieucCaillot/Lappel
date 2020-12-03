using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : Singleton<MainSceneManager>
{
    [SerializeField]
    private Transform glacierTransition = null;

    private AsyncOperation asyncLoadCascadeScene = null;
    
    private void Start()
    {
        EnvironmentManager.Instance.MainSceneEnvironment();
        StartCoroutine(LoadCascadeScene());
    }
    
    public void Play()
    {
        Debug.Log("MAIN SCENE PLAY");
        UIManager.Instance.HideStartGame();
        SoundManager.Instance.MoveAuroreCall(new Vector3(0, 15, -200));
        EnvironmentManager.Instance.AuroreCallMainScene();
        StartCoroutine(MoveIntro());
    }

    IEnumerator MoveIntro()
    {
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.RotateIntro();
    }

    public void OnTriggerEnterColliderTimeline()
    {
        PlayerManager.Instance.autoMove = false;
        PlayerManager.Instance.canMove = true;
        UIManager.Instance.ShowCommandKeys();
        UIManager.Instance.ShowCommandShift();
    }

    public void NextScene()
    {
        asyncLoadCascadeScene.allowSceneActivation = true;
        glacierTransition.transform.position = new Vector3(170, -8.5f, 37);
        Debug.Log("NEXT SCENE");
    }
    
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 20, 150, 50), "Next Scene"))
        {
            NextScene();
        }
    }
    
    IEnumerator LoadCascadeScene()
    {
        yield return new WaitForSeconds(3f);
        
        asyncLoadCascadeScene = SceneManager.LoadSceneAsync("Cascade Scene");
        asyncLoadCascadeScene.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoadCascadeScene.isDone)
        {
            yield return null;
        }
    }
}