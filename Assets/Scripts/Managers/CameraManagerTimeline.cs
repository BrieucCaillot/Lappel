using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManagerTimeline : Singleton<CameraManagerTimeline> {
    public PlayableDirector introToDefaultTimeline;
    // public PlayableDirector timeline2;

    // private void Start() {
    //     StartCoroutine("StartTimeline");
    // }

    //     IEnumerator StartTimeline(){
    //     yield return new WaitForSeconds(3f);
    //     timeline.Play();
    // }

    public void StartTimeline(string Movement) {
        switch (Movement) {
            case "introToDefault":
                Debug.Log("cam1");
                introToDefaultTimeline.Play();
                break;
            case "cam2":
                Debug.Log("cam2");
                // timeline2.Play();
                break;

            default:
                break;
        }
    }



}