using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CasquadeSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionZone;

    private void Update()
    {
        CheckDistancePlayerCrevasse();
    }

    public static void Play()
    {
        Debug.Log("CASQUADE SCENE PLAY");
    }

    public void CheckDistancePlayerCrevasse()
    {
        Debug.Log(Vector3.Distance(PlayerManager.GetPosition(), interactionZone.transform.position));
    }
}