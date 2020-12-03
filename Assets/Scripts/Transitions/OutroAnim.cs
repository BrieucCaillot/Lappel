using UnityEngine;

public class OutroAnim : MonoBehaviour
{
    public void PlayOutroSound()
    {
        Debug.Log("PlayOutroSound");
        SoundManager.Instance.PlayOutro();
    }
    
    public void ShowCredits()
    {
        Debug.Log("ShowCredits");
        CameraManager.Instance.StartTimeline("finalOutro");
    }
}
