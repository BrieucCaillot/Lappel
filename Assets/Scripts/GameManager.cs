using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {    
        /*UIManager.Instance.HideIntro();*/
        /*StartCoroutine(SwitchScene());*/
        AudioManager.Instance.PlaySound(AudioManager.Sound.Whatever);
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Aurore Scene");
    }

    public void IntroSceneCompleted() {
        Debug.Log("INTRO SCENE COMPLETED");
    }
}