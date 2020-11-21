using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Text logo = null;
    [SerializeField]
    private CanvasGroup interaction = null;
    
    [SerializeField]
    private Animator cascadeTransitionAnimator = null;
    
    private static float duration = 2f;

    private void Start()
    {
        logo.DOFade(0, 0);
        interaction.DOFade(0, 0);
    }

    public void ShowIntro()
    {
        if (GameManager.Instance.DebugMode) return;
        logo.DOFade(1, duration);
        interaction.DOFade(1, duration).OnComplete(() => GameManager.Instance.introShowed = true);
    }

    public void HideIntro()
    {
        if (!GameManager.Instance.introShowed) return;
        logo.DOFade(0, duration);
        interaction.DOFade(0, duration);
    }

    public void ShowCascadeTransition()
    {
        cascadeTransitionAnimator.SetTrigger("Cascade Anim");
    }
}
