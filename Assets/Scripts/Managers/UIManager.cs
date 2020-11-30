using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Image logo = null;
    [SerializeField]
    private Text start = null;
    [SerializeField]
    private Image commandKeys = null;
    [SerializeField]
    private Image commandSpace = null;
    [SerializeField]
    public Image backgroundBlack = null;
    
    [SerializeField]
    private Animator cascadeTransitionAnimator = null;
    
    private static float duration = 2f;

    private void Start()
    {
        logo.DOFade(0, 0);
        start.DOFade(0, 0);
        commandKeys.DOFade(0, 0);
        commandSpace.DOFade(0, 0);
        backgroundBlack.DOFade(0, 0);
    }

    public void ShowStartGame()
    {
        if (GameManager.Instance.DebugMode) return;
        start.DOFade(1, duration).OnComplete(() => GameManager.Instance.introShowed = true);
        commandSpace.DOFade(1, duration);
    }

    public void HideStartGame()
    {
        if (!GameManager.Instance.introShowed) return;
        start.DOFade(0, duration);
        commandSpace.DOFade(0, duration);
    }
    
    public void ShowTitle()
    {
        logo.DOFade(1, duration);
    }
    
    public void HideTitle()
    {
        logo.DOFade(0, duration);
    }
    
    public void ShowCommandKeys()
    {
        print("ShowCommandKeys");
        commandKeys.DOFade(1, duration);
    }
    
    public void HideCommandKeys()
    {
        commandKeys.DOFade(0, duration);
    }
    
    public void ShowCommandSpace()
    {
        print("ShowCommandSpace");
        commandSpace.DOFade(1, duration);
    }
    
    public void HideCommandSpace()
    {
        commandSpace.DOFade(0, duration);
    }
    
    public void FadeBackgroundBlack(float end)
    {
        backgroundBlack.DOFade(end, MountainSceneManager.Instance.maxTimeInCorridor);
    }
    
    public void ShowCascadeTransition()
    {
        cascadeTransitionAnimator.SetTrigger("Cascade Anim");
    }
}
