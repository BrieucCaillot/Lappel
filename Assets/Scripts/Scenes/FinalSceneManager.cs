using DG.Tweening;
using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private InteractionFinalManager interactionFinalManager = null;
    private bool finalAnimPlayed = false;

    [SerializeField] private GameObject finalSplash = null;
    private ParticleSystem finalSplashParticles = null;

    [SerializeField]
    private GameObject colliders = null;

    private void Start()
    {
        interactionFinalManager.onPlayerInteracted += OnInteractFinal;
    }

    public static void Play()
    {
        Debug.Log("FINAL SCENE PLAY");

        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        EnvironmentManager.Instance.FinalEnvironment();
    }

    private void OnInteractFinal()
    {
        Debug.Log("OnInteractFinal");

        PlayerManager.Instance.transform
            .DORotate(new Vector3(0, 180, 0), 1f)
            .OnComplete(() =>
            {
                colliders.SetActive(false);
                PlayerAnimManager.Instance.StartCrevasseAnim();
                PlayerManager.Instance.transform
                    .DOMove(PlayerManager.Instance.transform.position + Vector3.back * 10, 1f)
                    .SetDelay(2f)
                    .OnComplete(() =>
                    {
                        colliders.SetActive(true);
                    });
            });
    }
}