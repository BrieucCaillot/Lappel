using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public bool isPlayable = false; 

    void Start()
    {
        StartCoroutine("LetsGo");
        // AudioManager.Instance.PlaySound(AudioManager.Sound.Whatever);
    }

    private void Update()
    {
        if (isPlayable) return;
        if (Input.GetKey(KeyCode.Space))
        {
            isPlayable = true;
            MainSceneManager.Play();
        }
    }

    IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Instance.ShowIntro();
        CameraManager.Instance.ChangeCameraView(CameraManager.CamName.Intro);
        
        // yield return new WaitForSeconds(3f);
        // CameraManager.Instance.ChangeCameraView(CameraManager.CamName.CloseRight);
        //
        // yield return new WaitForSeconds(3f);
        // CameraManager.Instance.ChangeCameraView(CameraManager.CamName.Behind);
    }
}