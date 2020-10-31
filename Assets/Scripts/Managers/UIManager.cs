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
        Logo.DOFade(0, 0);
        PressSpace.DOFade(0, 0);
    }

    public void ShowIntro()
    {
        Logo.DOFade(1, duration);
        PressSpace.DOFade(1, duration);
    }
    
    public void HideIntro()
    {
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
