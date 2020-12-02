using DG.Tweening;
using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private InteractionFinalManager interactionFinalManager = null;

    [SerializeField] private GameObject finalSplash = null;
    private ParticleSystem finalSplashParticles = null;

    [SerializeField]
    private GameObject colliders = null;

    private void Start()
    {
        Debug.Log("FINAL SCENE START");
        PlayerManager.Instance.autoMove = true;
        Debug.Log(PlayerManager.Instance.autoMove);
        PlayerManager.Instance.canMove = true;
        PlayerAnimManager.Instance.StartIdleAnim();
        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        EnvironmentManager.Instance.FinalCaveEnvironment();

        interactionFinalManager.onPlayerInteracted += OnInteractFinal;
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
                var destination = PlayerManager.Instance.transform.position + Vector3.back * 10;
                PlayerManager.Instance.transform
                    .DOMove(PlayerManager.Instance.transform.position + Vector3.back * 10, 1f)
                    .SetDelay(2f)
                    .OnComplete(() =>
                    {
                        finalSplash.transform.DOMove(destination + Vector3.down * 3, 0f).OnComplete(() => finalSplashParticles.Play());
                        UIManager.Instance.ShowOutro();
                        colliders.SetActive(true);
                    });
            });
    }
}