using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CascadeSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionZone;

    private void Start()
    {
        GameEvents.current.onPlayerCanInteract += onPlayerCanInteract;
    }

    private void Update()
    {
       // CheckDistancePlayerCrevasse();
    }

    public static void Play()
    {
        Debug.Log("CASQUADE SCENE PLAY");
    }

    private void onPlayerCanInteract()
    {
        Debug.Log("ON PLAYER CAN INTERACT");
        interactionZone.transform.position = Vector3.Lerp(interactionZone.transform.position, Vector3.up * 5, 0.5f);
    }

    public void CheckDistancePlayerCrevasse()
    {
        Debug.Log(Vector3.Distance(PlayerManager.GetPosition(), interactionZone.transform.position));
    }
}