using System;
using DG.Tweening;
using UnityEngine;

public class InteractionMountainManager : MonoBehaviour
{
    public event Action onPlayerInInteractionZone;
    public event Action onPlayerOutInteractionZone;
    public event Action onPlayerInteracted;

    private static bool inInteractionZone = false;
    [SerializeField]
    private SpriteRenderer interactionOn = null;
    [SerializeField]
    private SpriteRenderer interactionOff = null;

    void Start()
    {
        interactionOn.DOFade(0, 0);
        onPlayerInInteractionZone += ShowInteractionOn;
        onPlayerInInteractionZone += HideInteractionOff;

        onPlayerOutInteractionZone += HideInteractionOn;
        onPlayerOutInteractionZone += ShowInteractionOff;

        onPlayerInteracted += MountainSceneManager.Instance.OpenDoor;
    }

    private void Update()
    {
        if (inInteractionZone && Input.GetKeyDown(KeyCode.Space)) PlayerInteracted();
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

    public void PlayerInteracted()
    {
        if (onPlayerInteracted != null) onPlayerInteracted();
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
