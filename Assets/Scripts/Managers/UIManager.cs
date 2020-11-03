using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    // public Image Background;
    public Text Logo;
    public Text PressSpace;
    private static float duration = 2f;

    private void Start()
    {
        if (GameManager.Instance.DebugMode) return;
        Logo.DOFade(0, 0);
        PressSpace.DOFade(0, 0);
    }

    public void ShowIntro()
    {
        if (GameManager.Instance.DebugMode) return;
        Logo.DOFade(1, duration);
        PressSpace.DOFade(1, duration).OnComplete(() => GameManager.Instance.introShowed = true);
    }
    
    public void HideIntro()
    {
        if (!GameManager.Instance.introShowed) return;
        Logo.DOFade(0, duration);
        PressSpace.DOFade(0, duration);
    }

    // private void BackgroundFadeIn()
    // {
    //     Background.DOFade(1, duration);
    // }

    // private void BackgroundFadeOut()
    // {
    //     Background.DOFade(0, duration).OnComplete(() => GameManager.Instance.IntroSceneCompleted());
    // }
}
