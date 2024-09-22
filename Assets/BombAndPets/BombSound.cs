using UnityEngine;

public class BombSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void PlayBombSound()
    {
        SoundManager.instance.PlayActionClip();

    }

    public void PlayGiftSound()
    {
        SoundManager.instance.PlayMiscClip();

    }
}
