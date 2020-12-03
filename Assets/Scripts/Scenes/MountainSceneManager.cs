using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainSceneManager : Singleton<MountainSceneManager>
{
    public float maxTimeInCorridor = 4f;
    [NonSerialized] 
    public bool onMountain = false;
    [SerializeField]
    private Transform door = null;
    [SerializeField]
    private AudioSource doorOpeningSound = null;
    [SerializeField]
    private ParticleSystem dustParticlesDoor = null;
    private bool loadedScene = false;
    private float stayTime = 0f;
    private AsyncOperation asyncLoad = null;

    private void Start()
    {
        Debug.Log("MOUNTAIN SCENE START");
        PlayerAnimManager.Instance.StartIdleAnim();
        PlayerAnimManager.Instance.StopWingsBubbles();
        CameraManager.Instance.underwaterToMoutain.Play();
        PlayerManager.Instance.SetPosition(new Vector3(0, 0, 0));
        PlayerManager.Instance.speed = 6;
        EnvironmentManager.Instance.MountainEnvironment();
        StartCoroutine(LoadFinalScene());
    }

    public void OnMountain()
    {
        if (onMountain)
        {
            Debug.Log("ON MOUNTAIN");
            PlayerAnimManager.Instance.StartMountainIdleAnim();
        }
        else
        {
            Debug.Log("OUT MOUNTAIN");
            PlayerAnimManager.Instance.StartIdleAnim();
        }
    }

    public void OpenDoor()
    {
        PlayerAnimManager.Instance.StartIdleAnim();
        PlayerManager.Instance.RotateMoutainCorridor();
        PlayerManager.Instance.canMove = false;

        door.DOShakePosition(0.3f, new Vector3(0.2f, 0, 0.2f), 5, 25, false, false)
            .OnStart((() => dustParticlesDoor.Play()))
            .OnComplete(() =>
            {
                var playedParticles = false;
                var openDoorTween = door.DOMove(new Vector3(0, 24, -1), 8f);
                openDoorTween
                    .SetEase(Ease.InOutQuart)
                    .OnStart(() => doorOpeningSound.Play())
                    .OnUpdate(() =>
                    {
                        if (!playedParticles && openDoorTween.ElapsedPercentage() >= 0.65f)
                        {
                            dustParticlesDoor.Play();
                            playedParticles = true;
                        }
                    })
                    .OnComplete(() => PlayerManager.Instance.autoMove = true);
            });
    }

    public void StayInCorridor()
    {
        SoundManager.Instance.MountainSceneCorridorSnapshot();
        if (loadedScene) return;
        stayTime += Time.deltaTime;
        if (stayTime > maxTimeInCorridor)
        {
            loadedScene = true;
            asyncLoad.allowSceneActivation = true;
        }
    }

    public void InCorridor()
    {
        UIManager.Instance.FadeInBackgroundBlack();
    }

    public void OutCorridor()
    {
        stayTime = 0.0f;
    }

    IEnumerator LoadFinalScene()
    {
        yield return new WaitForSeconds(3f);
        
        asyncLoad = SceneManager.LoadSceneAsync("Final Scene");
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}