using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour {
    public CinemachineVirtualCamera cam;
    public PlayableDirector timeline;
    public PlayableDirector timeline2;

    private void Start() {

        // StartCoroutine("StartTimeline");
    }

    public void StartTimeline(string Movement) {
        switch (Movement) {
            case "cam1":
                Debug.Log("cam1");
                timeline.Play();
                break;
            case "cam2":
                Debug.Log("cam2");
                timeline2.Play();
                break;

            default:
                break;
        }
    }

    // IEnumerator StartTimeline(){
    //     yield return new WaitForSeconds(3f);
    //     timeline.Play();
    // }

    void Update() {

    }
}