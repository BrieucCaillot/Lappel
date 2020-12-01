﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Image logo = null;
    [SerializeField]
    private Text start = null;
    
    [Header("Commands")]
    [SerializeField]
    private Image commandKeys = null;
    [SerializeField]
    private Image commandSpace = null;

    [Header("Overlays")]
    [SerializeField]
    public Image backgroundBlack = null;
    [SerializeField]
    public Image backgroundWhite = null;
    
    [Header("Animators")]
    [SerializeField]
    private Animator animAnimator = null;
    
    private static float duration = 2f;

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        logo.DOFade(0, 0);
        start.DOFade(0, 0);
        commandKeys.DOFade(0, 0);
        commandSpace.DOFade(0, 0);
        backgroundBlack.DOFade(0, 0);
        backgroundWhite.DOFade(0, 0);
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
        commandKeys.DOFade(1, duration);
    }
    
    public void HideCommandKeys()
    {
        commandKeys.DOFade(0, duration);
    }
    
    public void ShowCommandSpace()
    {
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
    
    public void DefaultAnim()
    {
        animAnimator.SetTrigger("Default");
    }
    
    public void ShowCascadeTransition()
    {
        animAnimator.SetTrigger("Cascade Anim");
    }
    
    public void ShowAuroreOverlay()
    {
        animAnimator.SetTrigger("Aurore Overlay");
        StartCoroutine(HideAuroreOverlay());
    }

    IEnumerator HideAuroreOverlay()
    {
        yield return new WaitForSeconds(SoundManager.Instance.auroreDuration);
        DefaultAnim();
    }
}
