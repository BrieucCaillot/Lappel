using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public Image Background;
    public Text Logo;
    private static float duration = 2f;

    public void HideIntro()
    {
        BackgroundFadeOut();
        LogoFadeOut();
    }

    private void BackgroundFadeIn()
    {
        Background.DOFade(1, duration);
    }

    private void BackgroundFadeOut()
    {
        Background.DOFade(0, duration).OnComplete(() => GameManager.Instance.IntroSceneCompleted());
    }

    private void LogoFadeIn()
    {
        Logo.DOFade(1, duration);
    }

    private void LogoFadeOut()
    {
        Logo.DOFade(0, duration);
    }
}
