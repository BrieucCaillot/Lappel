﻿using System;
using DG.Tweening;
using UnityEngine;

public class InteractionCascadeManager : MonoBehaviour
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

        onPlayerOutInteractionZone += HideInteractionOn;
        onPlayerOutInteractionZone += ShowInteractionOff;
    }

    private void Update()
    {
        if (inInteractionZone && Input.GetKeyDown(KeyCode.Space)) PlayerCanInteract();
    }

    public void PlayerInInteractionZone()
    {
        inInteractionZone = true;
        CameraManager.Instance.StartTimeline("cascadeSceneDefaultToRight");
        if (onPlayerInInteractionZone != null) onPlayerInInteractionZone();
    }

    public void PlayerOutInteractionZone()
    {
        inInteractionZone = false;
        CameraManager.Instance.StartTimeline("cascadeSceneRightToDefault");
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
