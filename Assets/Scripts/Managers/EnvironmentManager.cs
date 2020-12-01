﻿using System.Collections;
using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    [SerializeField]
    private GameObject snowParticles = null;
    [SerializeField]
    private GameObject glacierTransition = null;

    public void AuroreCallMainScene()
    {
        SoundManager.Instance.PlayAuroreCallMainScene();
        UIManager.Instance.ShowAuroreOverlay();
    }
    
    public void AuroreCall()
    {
        SoundManager.Instance.PlayAuroreCall();
        UIManager.Instance.ShowAuroreOverlay();
        StartCoroutine(DelayBeforeNewAuroreCall());
    }

    IEnumerator DelayBeforeNewAuroreCall()
    {
        yield return new WaitForSeconds(SoundManager.Instance.auroreDuration * 2);
    }
    
    public void MainSceneEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.PlayWind(GameManager.SceneType.MainScene);
    }

    public void CascadeEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.CascadeSceneSnapshot();
    }

    public void UnderwaterEnvironment() {
        snowParticles.SetActive(false);
        if (glacierTransition != null) glacierTransition.SetActive(false);
        SoundManager.Instance.PlayAmbiant2();
        SoundManager.Instance.UnderwaterSceneSnapshot();
    }

    public void MountainEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.PlayWind(GameManager.SceneType.MountainScene);
        SoundManager.Instance.MountainSceneSnapshot();
    }
    
    public void FinalEnvironment() {
        snowParticles.SetActive(false);
        SoundManager.Instance.FinalSceneCaveSnapshot();
    }
}