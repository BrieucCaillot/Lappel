using DG.Tweening;
using UnityEngine;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField] private InteractionCrevasseManager interactionCrevasseManager = null;
    private bool crevasseAnimPlayed = false;

    [SerializeField] private InteractionCascadeManager interactionCascadeManager = null;
    [SerializeField] private GameObject cascadeSplash = null;
    private ParticleSystem cascadeSplashParticles = null;
    private bool cascadeAnimPlayed = false;

    [SerializeField]
    private GameObject colliders = null;

    private void Start()
    {
        Debug.Log("CASCADE SCENE START");
        interactionCrevasseManager.onPlayerInteracted += OnInteractCrevasse;
        interactionCascadeManager.onPlayerInteracted += OnInteractCascade;
        cascadeSplashParticles = cascadeSplash.GetComponent<ParticleSystem>();
        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
        EnvironmentManager.Instance.CascadeEnvironment();
    }

    private void OnInteractCrevasse()
    {
        Debug.Log("OnInteractCrevasse");

        if (crevasseAnimPlayed) return;
        crevasseAnimPlayed = true;
        if (Mathf.Abs(PlayerManager.Instance.transform.rotation.y) < 0.95)
        {
            PlayerManager.Instance.transform
                .DORotate(new Vector3(0, 180, 0), 1f)
                .OnComplete(Jump);
        }
        else
        {
            Jump();
        }
    }

    private void Jump()
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
    }

    private void OnInteractCascade()
    {
        Debug.Log("OnInteractCascade");

        if (cascadeAnimPlayed) return;
        cascadeAnimPlayed = true;
        PlayerManager.Instance.transform
            .DORotate(new Vector3(0, 200, 0), 2f)
            .OnComplete(() =>
            {
                colliders.SetActive(false);
                PlayerManager.Instance.canMove = false;
                var destination = PlayerManager.Instance.transform.position + Vector3.back * 10;
                PlayerAnimManager.Instance.StartCascadeAnim();
                PlayerManager.Instance.transform
                    .DOMove(destination, 1f)
                    .SetDelay(2f)
                    .OnComplete(() =>
                    {
                        cascadeSplash.transform.DOMove(destination + Vector3.down * 3, 0f).OnComplete(() => cascadeSplashParticles.Play());
                        UIManager.Instance.ShowCascadeTransition();
                        // UnderwaterSceneManager.Instance.Play();
                    });
            });
    }
}