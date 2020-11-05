using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManagerTimeline : Singleton<CameraManagerTimeline> {
    public PlayableDirector introToDefaultTimeline;
    public PlayableDirector defaultToOutroTimeline;
    public PlayableDirector defaultToSides;

    public void StartTimeline(string Movement) {
        switch (Movement) {
            case "introToDefault":
                introToDefaultTimeline.Play();
                break;
            case "defaultToSides":
                defaultToSides.Play();
                break;
            case "defaultToOutro":
                defaultToOutroTimeline.Play();
                StartCoroutine("startTransitionToEnv2");
                break;

            default:
                break;
        }
    }

    IEnumerator startTransitionToEnv2() {
        yield return new WaitForSeconds(3f);

    }

    private void Update() { }

}