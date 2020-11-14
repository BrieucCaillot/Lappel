using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject interactionJump = null;
    [SerializeField] 
    private GameObject colliders = null;

    private bool canInteract = false;
    // private bool playedAnim = false;

    public static void Play()
    {
        Debug.Log("CASCADE SCENE PLAY");
        PlayerManager.Instance.ResetPosition();
        PlayerManager.Instance.SetRotation(new Vector3(0, -170, 0));
    }

    private void Update()
    {
        // if (playedAnim) return;    
        if (InteractionManager.inInteractionZone)
        {
            if (Input.GetKeyDown(KeyCode.Space)) canInteract = true;

            if (!canInteract) return;
            colliders.SetActive(false);
            
            // Vector3 playerPos = Vector3.Lerp(PlayerManager.Instance.GetPosition(), interactionJump.transform.position, 0.6f * Time.deltaTime);
            PlayerManager.Instance.SetPosition(interactionJump.transform.position);
        }
    }
}