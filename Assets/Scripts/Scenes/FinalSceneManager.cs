using System.Collections;
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
        PlayerManager.Instance.autoMove = false;
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
                var offset = new Vector3(0, 0, -12);
                var destination1 = PlayerManager.Instance.transform.position + offset;
                var destination2 = destination1 + new Vector3(0, -15, -5);
                PlayerAnimManager.Instance.StartCascadeAnim();
                StartCoroutine(DelayTransition(destination1));
                PlayerManager.Instance.transform
                    .DOMove(destination1, 1f)
                    .SetDelay(1f);

                PlayerManager.Instance.transform
                    .DOMove(destination2, 1f).SetDelay(1.25f);
            });
    }

    IEnumerator DelayTransition(Vector3 destination1)
    {
        yield return new WaitForSeconds(1.5f);
        finalSplash.transform.DOMove(destination1 + Vector3.down * 3, 0f).OnComplete(() => finalSplashParticles.Play());
        UIManager.Instance.ShowOutro();
        colliders.SetActive(true);
    }
}