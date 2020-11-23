public class CameraManagerTimeline : Singleton<CameraManagerTimeline>
{

    public bool cascadeSceneDefaultToRightStarted = false;
    public bool cascadeSceneDefaultToRightEnded = false;
    public bool cascadeSceneRightToDefaultStarted = false;
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