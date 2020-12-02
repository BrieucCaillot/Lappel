using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroAnim : MonoBehaviour
{
    public void ShowCredits()
    {
        CameraManager.Instance.StartTimeline("finalOutro");
    }
}
