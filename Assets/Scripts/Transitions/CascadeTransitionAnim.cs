using UnityEngine;

public class CascadeTransitionAnim : MonoBehaviour {
    public void OnMiddleCascadeAnimation() {
        UnderwaterSceneManager.Instance.Play();
        CameraManager.Instance.StartTimeline("cascadeSceneRightToUnderwater");
    }
}