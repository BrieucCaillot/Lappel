using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimManager : Singleton<PlayerAnimManager>
{
   [Header("Anims Sounds")]
   [SerializeField]
   private AudioSource jumpAudioSource = null;
   [SerializeField]
   private AudioSource diveAudioSource = null;
   [SerializeField]
   private AudioSource slideAudioSource = null;
   
   [Header("Bubbles Particles")]
   [SerializeField]
   private ParticleSystem bubblesWingLeft = null;
   [SerializeField]
   private ParticleSystem bubblesWingRight = null;
   
   [SerializeField]
   private Animator anim = null;

   public void StartIdleAnim()
   {
      if (SceneManager.GetActiveScene().name != "Underwater Scene" ||
          SceneManager.GetActiveScene().name != "Mountain Scene")
      {
         Debug.Log("START IDLE ANIM");
         StopWingsBubbles();
         anim.SetFloat("vertical", 0);
         anim.SetTrigger("StartIdle");
      }
   }

   public void StartSlideIdleAnim()
   {
      if (SceneManager.GetActiveScene().name != "Underwater Scene" ||
          SceneManager.GetActiveScene().name != "Mountain Scene")
      {
         anim.SetTrigger("StartSlideIdle");
      }
   }
   
   public void StartSwimIdleAnim()
   {
      anim.SetTrigger("StartSwimIdle");
   }
   
   public void StartMountainIdleAnim()
   {
      anim.SetTrigger("StartMountainIdle");
   }
   
   public void StartBeakAnim()
   {
      anim.SetTrigger("StartBeak");
   }

   public void StartCrevasseAnim()
   {
      anim.SetTrigger("StartCrevasse");
   }

   public void StartCascadeAnim()
   {
      anim.SetTrigger("StartCascade");
   }
   
   public void OnSlideAnimSound()
   {
      slideAudioSource.Play();
   }
   
   public void OnAnimCrevasseSound()
   {
      jumpAudioSource.Play();
   }

   public void OnAnimCrevasseStart()
   {
      PlayerManager.Instance.canMove = false;
   }
  
   public void OnAnimCrevasseEnd()
   {
      PlayerManager.Instance.canMove = true;
   }
   
   public void OnAnimCascadeSound()
   {
      diveAudioSource.Play();
   }
   
   public void OnAnimCascadeStart()
   {
      PlayerManager.Instance.canMove = false;
   }

   public void OnAnimCascadeEnd()
   {
      PlayerManager.Instance.canMove = true;
   }
   
   

   public void PlayWingsBubbles()
   {
      bubblesWingLeft.Play();
      bubblesWingRight.Play();
   }
   
   public void StopWingsBubbles()
   {
      bubblesWingLeft.Stop();
      bubblesWingRight.Stop();
      // bubblesWingLeft.Clear();
      // bubblesWingRight.Clear();
   }
}
