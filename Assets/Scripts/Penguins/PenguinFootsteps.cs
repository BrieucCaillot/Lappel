using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinFootsteps : MonoBehaviour
{
    public GameObject rightFootprint;
    public GameObject leftFootprint;

    public Transform leftFootLocation;
    public Transform rightFootLocation;
    
    public float footPrintOffset = 0.05f;
    
    public AudioSource leftFootAudioSource;
    public AudioSource rightFootAudioSource;
    
    void LeftFootstep()
    {
        leftFootAudioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out hit))
        {
            Instantiate(leftFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, leftFootLocation.up));
        }
    }
    void RightFootstep()
    {
        rightFootAudioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out hit))
        {
            Instantiate(rightFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, rightFootLocation.up));
        }
    }
}
