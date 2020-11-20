using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    // public Image Background;
    public Text Logo = null;
    public Text PressSpace = null;
    public Sprite CascadeMotion = null;
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

    public void ShowMotionCascade()
    {
        Debug.Log("SHOW MOTION CASCADE");
    }
    
    public void HideMotionCascade()
    {
        Debug.Log("SHOW MOTION CASCADE");
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
