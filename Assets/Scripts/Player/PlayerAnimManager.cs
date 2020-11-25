using UnityEngine;

public class PlayerAnimManager : Singleton<PlayerAnimManager>
{
   private static Animator anim;
   
   private void Start()
   {
      anim = GetComponent<Animator>();
   }
   
   public void StartIdleAnim()
   {
      anim.SetTrigger("StartIdle");
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
   
   public void StartUnderwaterAnim()
   {
      anim.SetTrigger("StartUnderwater");
   }
   
   public void OnAnimStart()
   {
      PlayerManager.Instance.canMove = false;
   }

   public void OnAnimEnd()
   {
      PlayerManager.Instance.canMove = true;
   }
}
