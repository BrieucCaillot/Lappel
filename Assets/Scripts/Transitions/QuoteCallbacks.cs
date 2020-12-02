using UnityEngine;

public class QuoteCallbacks : MonoBehaviour
{
   public void PickAmbiantMainScene()
   {
      SoundManager.Instance.PickAmbiant(GameManager.SceneType.MainScene);
   }

   public void PickAmbiantUnderwaterScene()
   {
      SoundManager.Instance.PickAmbiant(GameManager.SceneType.UnderwaterScene);
   }
   
   public void PickAmbiantMountainScene()
   {
      SoundManager.Instance.PickAmbiant(GameManager.SceneType.MountainScene);
   }
}
