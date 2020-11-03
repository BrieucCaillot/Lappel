using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Parallax : MonoBehaviour {
    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;
    private float xOffset;
    private float yOffset;
    private void Start() {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        xOffset = transposer.m_FollowOffset.x;
        yOffset = transposer.m_FollowOffset.y;
    }
    void Update() {
        var coefX = Mathf.Clamp((Input.mousePosition.x / Screen.width) * 2 - 1, -1.0F, 1.0F) * 0.25f;
        var coefY = Mathf.Clamp((Input.mousePosition.y / Screen.height) * 2 - 1, -1.0F, 1.0F) * 0.25f;
        transposer.m_FollowOffset.x = xOffset + coefX;
        transposer.m_FollowOffset.y = yOffset + coefY;
    }
}