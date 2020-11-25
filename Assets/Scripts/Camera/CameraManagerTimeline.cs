using System;

public class CameraManagerTimeline : Singleton<CameraManagerTimeline>
{
    [NonSerialized]
    public bool cascadeSceneDefaultToRightStarted = false;
    [NonSerialized]
    public bool cascadeSceneDefaultToRightEnded = false;
    [NonSerialized]
    public bool cascadeSceneRightToDefaultStarted = false;
    [NonSerialized]
    public bool cascadeSceneRightToDefaultEnded = true;

    // private void FixedUpdate()
    // {
    //     print("DefaultToRightEnded " + cascadeSceneDefaultToRightEnded);
    //     print("RightToStartEnded " + cascadeSceneRightToDefaultEnded);
    // }

    public void OnCascadeSceneDefaultToRightStart()
    {
        cascadeSceneDefaultToRightStarted = true;
        cascadeSceneDefaultToRightEnded = false;
        cascadeSceneRightToDefaultEnded = true;
    }
    public void OnCascadeSceneDefaultToRightEnd()
    {
        cascadeSceneDefaultToRightStarted = false;
        cascadeSceneDefaultToRightEnded = true;
        cascadeSceneRightToDefaultEnded = false;
    }
    
    public void OnCascadeSceneRightToDefaultStart()
    {
        cascadeSceneRightToDefaultStarted = true;
        cascadeSceneRightToDefaultEnded = false;
        cascadeSceneDefaultToRightEnded = false;
    }
    public void OnCascadeSceneRightToDefaultEnd()
    {
        cascadeSceneRightToDefaultStarted = false;
        cascadeSceneRightToDefaultEnded = true;
        cascadeSceneDefaultToRightEnded = false;
    }
}