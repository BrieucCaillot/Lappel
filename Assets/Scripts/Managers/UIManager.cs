using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Image logo = null;
    [SerializeField]
    private CanvasGroup interaction = null;
    [SerializeField]
    private CanvasGroup command = null;
    [SerializeField]
    public Image backgroundBlack = null;
    
    [SerializeField]
    private Animator cascadeTransitionAnimator = null;
    
    private static float duration = 2f;

    private void Start()
    {
        logo.DOFade(0, 0);
        interaction.DOFade(0, 0);
        command.DOFade(0, 0);
        backgroundBlack.DOFade(0, 0);
    }

    public void ShowInteraction()
    {
        print("ShowInteraction");
        if (GameManager.Instance.DebugMode) return;
        interaction.DOFade(1, duration).OnComplete(() => GameManager.Instance.introShowed = true);
    }

    public void HideInteraction()
    {
        if (!GameManager.Instance.introShowed) return;
        interaction.DOFade(0, duration);
    }
    
    public void ShowTitle()
    {
        logo.DOFade(1, duration);
    }
    
    public void HideTitle()
    {
        logo.DOFade(0, duration);
    }
    
    public void ShowCommands()
    {
        command.DOFade(1, duration);
    }
    
    public void HideCommands()
    {
        command.DOFade(0, duration);
    }
    
    public void FadeBackgroundBlack(float end)
    {
        backgroundBlack.DOFade(end, 2f);
    }
    
    public void ShowCascadeTransition()
    {
        cascadeTransitionAnimator.SetTrigger("Cascade Anim");
    }
}
