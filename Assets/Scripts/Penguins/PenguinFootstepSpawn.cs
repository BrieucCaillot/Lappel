using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PenguinFootstepSpawn : MonoBehaviour
{
    public GameObject rightFootprint;
    public GameObject leftFootprint;

    public Transform leftFootLocation;
    public Transform rightFootLocation;
    
    public float footPrintOffset = 0.05f;
    
    private AudioSource[] audioSources;
    private float footprintFadeValue = 7f;

    private void Start()
    {
        audioSources = transform.GetComponents<AudioSource>();
    }

    public void LeftFootstep()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out hit))
        {
            if (audioSources.Length > 0) audioSources[Random.Range(0, audioSources.Length - 1)].Play();
            var left = Instantiate(leftFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, leftFootLocation.up));
            left.GetComponent<SpriteRenderer>().DOFade(0, footprintFadeValue)
                .OnComplete(() => Destroy(left, 0));
        }
    }
    public void RightFootstep()
    {
        RaycastHit hit;
        if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out hit))
        {
            if (audioSources.Length > 0) audioSources[Random.Range(0, audioSources.Length - 1)].Play();
            var right = Instantiate(rightFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, rightFootLocation.up));

            right.GetComponent<SpriteRenderer>().DOFade(0, footprintFadeValue)
                .OnComplete(() => Destroy(right, 0));
        }
    }
}
