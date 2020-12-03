using System.Collections;
using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    [SerializeField]
    private GameObject snowParticles = null;
    [SerializeField]
    private GameObject glacierTransition = null;

    public void AuroreCallMainScene()
    {
        SoundManager.Instance.PickAuroreCall(GameManager.SceneType.MainScene);
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
        SoundManager.Instance.PickAmbiant(GameManager.SceneType.MainScene);
        SoundManager.Instance.PickAuroreCall(GameManager.SceneType.MainScene);
        SoundManager.Instance.PlayWind(GameManager.SceneType.MainScene);
    }

    public void CascadeEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.PickAmbiant(GameManager.SceneType.CascadeScene);
        SoundManager.Instance.PickAuroreCall(GameManager.SceneType.CascadeScene);
        SoundManager.Instance.CascadeSceneSnapshot();
    }

    public void UnderwaterEnvironment() {
        snowParticles.SetActive(false);
        if (glacierTransition != null) glacierTransition.SetActive(false);
        SoundManager.Instance.PickAmbiant(GameManager.SceneType.UnderwaterScene);
        SoundManager.Instance.PickAuroreCall(GameManager.SceneType.UnderwaterScene);
        SoundManager.Instance.PlayAmbiant();
        SoundManager.Instance.UnderwaterSceneSnapshot();
    }

    public void MountainEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.PickAmbiant(GameManager.SceneType.MountainScene);
        SoundManager.Instance.PickAuroreCall(GameManager.SceneType.MountainScene);
        SoundManager.Instance.PlayWind(GameManager.SceneType.MountainScene);
        SoundManager.Instance.MountainSceneSnapshot();
    }
    
    public void FinalCaveEnvironment() {
        snowParticles.SetActive(false);
        SoundManager.Instance.PlayAmbiantCave();
        SoundManager.Instance.PlayWind(GameManager.SceneType.FinalScene);
        SoundManager.Instance.PickAmbiant(GameManager.SceneType.FinalScene);
        SoundManager.Instance.PickAuroreCall(GameManager.SceneType.FinalScene);
        SoundManager.Instance.FinalSceneCaveSnapshot();
    }
    
    public void FinalEnvironment() {
        SoundManager.Instance.FinalSceneSnapshot();
    }
}