using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static bool inInteractionZone = false;
    private float speed = 4f;

    [SerializeField]
    private SpriteRenderer interactionOn = null;
    [SerializeField]
    private SpriteRenderer interactionOff = null;
    
    // Start is called before the first frame update
    void Start()
    {
        interactionOn.DOFade(0, 0);
        BlinkInteraction();
    }

    public void BlinkInteraction()
    {
        if (inInteractionZone)
        {
            InvokeRepeating("ShowInteractionOn", 0f, speed);
            InvokeRepeating("HideInteractionOn", speed / 2, speed);
            InvokeRepeating("ShowInteractionOff", speed / 2, speed);
            InvokeRepeating("HideInteractionOff", 0f, speed);
        }
    }

    private void ShowInteractionOn()
    {
        interactionOn.DOFade(1, 2f);
    }

    private void HideInteractionOn()
    {
        interactionOn.DOFade(0, 2f);
    }
    
    private void ShowInteractionOff()
    {
        interactionOff.DOFade(1, 2f);
    }

    private void HideInteractionOff()
    {
        interactionOff.DOFade(0, 2f);
    }
}
