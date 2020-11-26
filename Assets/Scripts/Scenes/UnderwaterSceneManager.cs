using UnityEngine;
using UnityEngine.SceneManagement;

public class UnderwaterSceneManager : MonoBehaviour {

    [SerializeField]
    private ParticleSystem bubblesRightLeft = null;
    [SerializeField]
    private ParticleSystem bubblesRightWing = null;
    
    private void Start() {
        // @TODO Remove when project is done 
        PlayerManager.Instance.SetPosition(new Vector3(0, -139, 0));
        PlayerAnimManager.Instance.StartUnderwaterAnim();
        PlayerManager.Instance.speed = 12;
        EnvironmentManager.Instance.UnderwaterEnvironment();
    }

    public static void Play() {
        Debug.Log("UNDERWATER SCENE PLAY");
        SceneManager.LoadSceneAsync("Underwater Scene");
        PlayerManager.Instance.SetPosition(new Vector3(0, -139, 0));
        PlayerManager.Instance.speed = 12;
        PlayerAnimManager.Instance.StartUnderwaterAnim();
    }
    
    public void PlayBubbles() {
        
    }

    public static void NextScene() {
        MountainSceneManager.Instance.Play();
    }
}