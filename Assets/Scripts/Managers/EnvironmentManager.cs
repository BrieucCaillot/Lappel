using System.Collections;
using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    [SerializeField]
    private GameObject snowParticles = null;
    [SerializeField]
    private GameObject glacierTransition = null;

    private bool auroreCallPlayed = false;

    public void AuroreCallMainScene()
    {
        SoundManager.Instance.PlayAuroreCallMainScene();
        UIManager.Instance.ShowAuroreOverlay();
    }
    
    public void AuroreCall()
    {
        if (auroreCallPlayed) return;
        SoundManager.Instance.PlayAuroreCall();
        UIManager.Instance.ShowAuroreOverlay();
        auroreCallPlayed = true;
        StartCoroutine(DelayBeforeNewAuroreCall());
    }

    IEnumerator DelayBeforeNewAuroreCall()
    {
        yield return new WaitForSeconds(SoundManager.Instance.auroreDuration * 2);
        auroreCallPlayed = false;
    }
    
    public void MainSceneEnvironment() {
        snowParticles.SetActive(true);
    }

    public void CascadeEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.CascadeSceneSnapshot();
    }

    public void UnderwaterEnvironment() {
        snowParticles.SetActive(false);
        if (glacierTransition != null) glacierTransition.SetActive(false);
        SoundManager.Instance.UnderwaterSceneSnapshot();
    }

    public void MountainEnvironment() {
        snowParticles.SetActive(true);
        SoundManager.Instance.MountainSceneSnapshot();
    }
    
    public void FinalEnvironment() {
        snowParticles.SetActive(false);
        SoundManager.Instance.FinalSceneCaveSnapshot();
    }
}