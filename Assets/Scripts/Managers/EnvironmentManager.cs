using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    [SerializeField]
    private GameObject snowParticles = null;
    [SerializeField]
    private GameObject glacierTransition = null;
    [SerializeField]
    private GameObject groundBubbles = null;

    public void SnowEnvironment() {
        groundBubbles.SetActive(false);
        snowParticles.SetActive(true);
    }

    public void CascadeEnvironment() {
        groundBubbles.SetActive(false);
        snowParticles.SetActive(true);
        SoundManager.Instance.CascadeSceneSnapshot();
    }

    public void UnderwaterEnvironment() {
        groundBubbles.SetActive(true);
        snowParticles.SetActive(false);
        // glacierTransition.SetActive(false);
        SoundManager.Instance.UnderwaterSceneSnapshot();
    }

    public void MountainEnvironment() {
        groundBubbles.SetActive(false);
        snowParticles.SetActive(true);
        SoundManager.Instance.MountainSceneSnapshot();
    }
}