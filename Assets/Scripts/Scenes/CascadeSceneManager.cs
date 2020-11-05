using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionZone;

    private void Start()
    {
        // GameEvents.current.onPlayerCanInteract += onPlayerCanInteract;
    }

    private void Update()
    {
       // CheckDistancePlayerCrevasse();
    }

    public static void Play()
    {
        Debug.Log("CASCADE SCENE PLAY");
        PlayerManager.Instance.ResetPosition();
    }

    private void onPlayerCanInteract()
    {
        Debug.Log("ON PLAYER CAN INTERACT");
        interactionZone.transform.position = Vector3.Lerp(interactionZone.transform.position, Vector3.up * 5, 0.5f);
    }

    public void CheckDistancePlayerCrevasse()
    {
        Debug.Log(Vector3.Distance(PlayerManager.Instance.GetPosition(), interactionZone.transform.position));
    }
}