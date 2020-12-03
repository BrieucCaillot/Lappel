using UnityEngine;

public class OutroAnim : MonoBehaviour
{
    public void PlayFinalSceneOutroSnapshot()
    {
        SoundManager.Instance.FinalSceneOutroSnapshot();
    }
    
    public void PlayFinalSceneCreditsSnapshot()
    {
        SoundManager.Instance.FinalSceneCreditsSnapshot();
    }
    
    public void PlayOutroSound()
    {
        SoundManager.Instance.PlayOutro();
    }
    
    public void ShowCredits()
    {
        Debug.Log("ShowCredits");
        CameraManager.Instance.StartTimeline("finalOutro");
    }
}
