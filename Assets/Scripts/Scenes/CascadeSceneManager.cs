using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField] private InteractionManager interactionManagerCrevasse = null;
    private bool crevasseAnimPlayed = false;
    
    [SerializeField] private InteractionManager interactionManagerCascade = null; 
    [SerializeField] private Animator cascadeAnimator = null; 
    private bool cascadeAnimPlayed = false;
    
    [SerializeField] 
    private GameObject colliders = null;

    private void Start()
    {
        interactionManagerCrevasse.onPlayerCanInteract += OnInteractCrevasse;
    }

    public static void Play()
    {
        Debug.Log("CASCADE SCENE PLAY");
        
        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
    }

    private void OnInteractCrevasse()
    {
        
        Debug.Log("OnInteractCrevasse");
        
        if (crevasseAnimPlayed) return;
        crevasseAnimPlayed = true;
        PlayerManager.Instance.GetTransform()
            .DORotate(new Vector3(0, 180, 0), 1f)
            .OnComplete(() =>
            {
                colliders.SetActive(false);
                PlayerManager.Instance.canMove = false;
                PlayerAnimManager.OnCrevasseAnimStart();
                PlayerManager.Instance.GetTransform()
                    .DOMove(PlayerManager.Instance.GetTransform().position + Vector3.back * 10, 1f)
                    .SetDelay(2f)
                    .OnComplete(() =>
                    {
                        colliders.SetActive(true);
                        interactionManagerCrevasse.onPlayerCanInteract -= OnInteractCrevasse;
                        interactionManagerCascade.onPlayerCanInteract += OnInteractCascade;
                    });
                
            });
    }

    private void OnInteractCascade()
    {
        Debug.Log("OnInteractCascade");
        
        if (cascadeAnimPlayed) return;
        cascadeAnimPlayed = true;
        PlayerManager.Instance.GetTransform()
            .DORotate(new Vector3(0, 180, 0), 2f)
            .OnComplete(() =>
            {
                colliders.SetActive(false);
                PlayerManager.Instance.canMove = false;
                PlayerAnimManager.OnCrevasseAnimStart();
                PlayerManager.Instance.GetTransform()
                    .DOMove(PlayerManager.Instance.GetTransform().position + Vector3.back * 10, 1f)
                    .SetDelay(2f)
                    .OnComplete(() =>
                    {
                        colliders.SetActive(true);
                        cascadeAnimator.SetTrigger("Cascade Anim");
                    });
            });
        
    }
}