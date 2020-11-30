using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    [SerializeField]
    private GameObject snowParticles = null;
    [SerializeField]
    private GameObject glacierTransition = null;

    public void SnowEnvironment() {
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