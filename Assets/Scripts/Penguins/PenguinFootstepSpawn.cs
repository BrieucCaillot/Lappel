using DG.Tweening;
using UnityEngine;

public class PenguinFootstepSpawn : MonoBehaviour
{
    [Header("Footprint")]
    [SerializeField]
    private GameObject rightFootprint = null;
    [SerializeField]
    private GameObject leftFootprint = null;
    
    [SerializeField]
    private Transform leftFootLocation = null;
    [SerializeField]
    private Transform rightFootLocation = null;
    
    [SerializeField]
    private float footPrintOffset = 0.05f;
    private float footprintFadeValue = 7f;

    [SerializeField]
    private PenguinFootstepsSound penguinFootstepsSound = null;

    private void Start()
    {
        penguinFootstepsSound.GetComponent<PenguinFootstepsSound>();
    }

    public void LeftFootstep()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out hit))
        {
            penguinFootstepsSound.PlayFootstepSound();
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
            penguinFootstepsSound.PlayFootstepSound();
            var right = Instantiate(rightFootprint, hit.point + hit.normal * footPrintOffset, Quaternion.LookRotation(hit.normal, rightFootLocation.up));
            right.GetComponent<SpriteRenderer>().DOFade(0, footprintFadeValue)
                .OnComplete(() => Destroy(right, 0));
        }
    }
}
