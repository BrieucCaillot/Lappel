using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
   private static Animator anim;
   
   private void Start()
   {
      anim = GetComponent<Animator>();
   }

   public static void OnCrevasseAnimStart()
   {
      anim.SetTrigger("Jump Crevasse");
   }
   
   public void OnCascadeAnimEnd()
   {
      anim.SetBool("Jump Creve", false);
   }
}
