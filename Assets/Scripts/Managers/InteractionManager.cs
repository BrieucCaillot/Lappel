using System;
using DG.Tweening;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public event Action onPlayerInInteractionZone;
    public event Action onPlayerOutInteractionZone;
    public event Action onPlayerCanInteract;

    private static bool inInteractionZone = false;
    [SerializeField]
    private SpriteRenderer interactionOn = null;
    [SerializeField]
    private SpriteRenderer interactionOff = null;

    // Start is called before the first frame update
    void Start()
    {
        interactionOn.DOFade(0, 0);
        onPlayerInInteractionZone += ShowInteractionOn;
        onPlayerInInteractionZone += HideInteractionOff;
        // onPlayerInInteractionZone += SoundManager.Instance.SoundInInteractionZone;

        onPlayerOutInteractionZone += HideInteractionOn;
        onPlayerOutInteractionZone += ShowInteractionOff;
        // onPlayerOutInteractionZone += SoundManager.Instance.SoundOutInteractionZone;
    }

    private void Update()
    {
        if (inInteractionZone && Input.GetKeyDown(KeyCode.Space)) PlayerCanInteract();
    }

    public void PlayerInInteractionZone()
    {
        inInteractionZone = true;
        if (onPlayerInInteractionZone != null) onPlayerInInteractionZone();
    }

    public void PlayerOutInteractionZone()
    {
        inInteractionZone = false;
        if (onPlayerOutInteractionZone != null) onPlayerOutInteractionZone();
    }

    public void PlayerCanInteract()
    {
        if (onPlayerCanInteract != null) onPlayerCanInteract();
    }

    private void ShowInteractionOn()
    {
        interactionOn.DOFade(1, 2f);
    }

    private void HideInteractionOn()
    {
        interactionOn.DOFade(0, 2f);
    }

    private void ShowInteractionOff()
    {
        interactionOff.DOFade(1, 2f);
    }

    private void HideInteractionOff()
    {
        interactionOff.DOFade(0, 2f);
    }
}
