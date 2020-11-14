using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
   private Animator anim;
   
   private void Start()
   {
      anim = GetComponent<Animator>();
   }

   public void onCascadeAnimDone()
   {
      anim.SetBool("Jump", false);
   }
}
