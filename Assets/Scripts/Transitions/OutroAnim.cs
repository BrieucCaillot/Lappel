using UnityEngine;

public class OutroAnim : MonoBehaviour
{
    public void ShowCredits()
    {
        CameraManager.Instance.StartTimeline("finalOutro");
    }
}
