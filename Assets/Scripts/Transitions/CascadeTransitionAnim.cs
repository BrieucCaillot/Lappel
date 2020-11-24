using UnityEngine;

public class CascadeTransitionAnim : MonoBehaviour
{
    public void OnMiddleCascadeAnimation()
    {
        UnderwaterSceneManager.Play();
        CameraManager.Instance.StartTimeline("cascadeSceneRightToUnderwater");
    }
}
