using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public EffectList runSoundEffect;
    public EffectList jumpSoundEffect;
    public EffectList hitSoundEffect;

    public AudioSource playerAudio;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    public void PlayHitSound()
    {
        playerAudio.PlayOneShot(SoundManager.instance.GetFXClip(hitSoundEffect));
    }

    public void PlayRunSound()
    {
        playerAudio.PlayOneShot(SoundManager.instance.GetFXClip(runSoundEffect));
    }

    public void PlayJumpSound()
    {
        playerAudio.PlayOneShot(SoundManager.instance.GetFXClip(jumpSoundEffect));
    }
}
