using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuoteCallbacks : MonoBehaviour
{
   public void PlayAmbiant1()
   {
      SoundManager.Instance.PlayAmbiant1();
   }

   public void PlayAmbiant2()
   {
      SoundManager.Instance.PlayAmbiant2();
   }
   
   public void PlayAmbiant3()
   {
      SoundManager.Instance.PlayAmbiant3();
   }
}
