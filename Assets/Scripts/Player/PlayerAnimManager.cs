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
      anim.SetTrigger("StartIdleAnim");
   }

   public void StartCrevasseAnim()
   {
      anim.SetTrigger("StartCrevasseAnim");
   }

   public void StartCascadeAnim()
   {
      anim.SetTrigger("StartCascadeAnim");
   }
   
   public void StartUnderwaterAnim()
   {
      anim.SetTrigger("StartUnderwaterAnim");
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
