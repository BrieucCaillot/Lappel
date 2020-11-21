using UnityEngine;

public class PenguinFootstepSpawn : MonoBehaviour
{
    public GameObject rightFootprint;
    public GameObject leftFootprint;

    public Transform leftFootLocation;
    public Transform rightFootLocation;
    
    public float footPrintOffset = 0.05f;
    
    public AudioSource[] audioSources;
    
    public void LeftFootstep()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out hit))
        {
            // audioSources[Random.Range(0, audioSources.Length - 1)].Play();
            var left = Instantiate(leftFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, leftFootLocation.up));
            Destroy(left, 5);
        }
    }
    public void RightFootstep()
    {
        RaycastHit hit;
        if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out hit))
        {
            // audioSources[Random.Range(0, audioSources.Length - 1)].Play();
            var right = Instantiate(rightFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, rightFootLocation.up));
            Destroy(right, 5);
        }
    }
}
