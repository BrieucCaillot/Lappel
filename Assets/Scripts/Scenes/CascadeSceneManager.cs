using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject interactionJump = null;
    [SerializeField] 
    private GameObject colliders = null;

    private void Start()
    {
        InteractionManager.onPlayerCanInteract += CanInteract;
    }

    public static void Play()
    {
        Debug.Log("CASCADE SCENE PLAY");
        
        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, -170, 0));
    }

    private void CanInteract()
    {
        Debug.Log("FROM CASCADE SCENE");
        
        PlayerManager.Instance.GetRigidbody()
            .DOMove(interactionJump.transform.position, 1f)
            .OnComplete(() => colliders.SetActive(true));
    }
}