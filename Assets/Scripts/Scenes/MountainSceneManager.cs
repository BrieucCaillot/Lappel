using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainSceneManager : Singleton<MountainSceneManager>
{
    [SerializeField]
    private Transform door = null;
    [SerializeField]
    private AudioSource doorOpeningSound = null;
    [SerializeField]
    private ParticleSystem dustParticlesDoor = null;

    private void Start()
    {
        PlayerAnimManager.Instance.StartIdleAnim();
        EnvironmentManager.Instance.MountainEnvironment();
    }

    public void Play()
    {
        Debug.Log("MOUNTAIN SCENE PLAY");
        SceneManager.LoadSceneAsync("Mountain Scene");
        PlayerManager.Instance.SetPosition(new Vector3(0, 0, 0));
        PlayerManager.Instance.speed = 6;
        PlayerAnimManager.Instance.StartIdleAnim();
        EnvironmentManager.Instance.MountainEnvironment();
    }

    public void OpenDoor()
    {
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
                });
            });
    }
}
