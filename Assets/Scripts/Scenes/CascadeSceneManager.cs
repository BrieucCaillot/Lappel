using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField] private InteractionManager interactionManagerCrevasse; 
    [SerializeField] private InteractionManager interactionManagerCascade; 
    
    [SerializeField] 
    private GameObject interactionJump = null;
    [SerializeField] 
    private GameObject colliders = null;

    private void Start()
    {
        interactionManagerCrevasse.onPlayerCanInteract += OnInteractCrevasse;
        interactionManagerCascade.onPlayerCanInteract += OnInteractCascade;
    }

    public static void Play()
    {
        Debug.Log("CASCADE SCENE PLAY");
        
        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, 180, 0));
    }

    private void OnInteractCrevasse()
    {
        PlayerManager.Instance.GetRigidbody()
            .DOMove(interactionJump.transform.position, 1f)
            .OnComplete(() =>
            {
                colliders.SetActive(true);
                interactionManagerCrevasse.onPlayerCanInteract -= OnInteractCrevasse;
            });
    }

    private void OnInteractCascade()
    {
        Debug.Log("OnInteractCascade");
    }
}