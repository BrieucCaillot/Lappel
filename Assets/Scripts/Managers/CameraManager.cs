using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.UIElements;
using UnityEngine;
using DG.Tweening;

public class CameraManager : Singleton<CameraManager>
{

    public CinemachineMixingCamera introCam;
    public CinemachineMixingCamera closeRightCam;
    public CinemachineMixingCamera backCam;
    public const int MaxCameras = 8;
    
    public enum CamName
    {
        Intro,
        CloseRight,
        Behind
    }

    public void ChangeCameraView(CamName camName)
    {
        switch (camName)
        {
            case CamName.Intro:
                introCam.enabled = true;
                closeRightCam.enabled = false;
                break;
            case CamName.CloseRight:
                introCam.enabled = false;
                closeRightCam.enabled = true;
                break;
            case CamName.Behind:
                closeRightCam.enabled = false;
                backCam.enabled = true;
                break;
            default:
                Debug.Log("DEFAULT CAMERA");
                break;
        }
    }
}
