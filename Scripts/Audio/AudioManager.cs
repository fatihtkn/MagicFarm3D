using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource source;

    AudioClip currentClip;

    public void PlaySound(AudioTypes desiredAudio)
    {
        GetAudio(desiredAudio);
    }


    void GetAudio(AudioTypes desiredAudio)
    {
        switch (desiredAudio)
        {
            case AudioTypes.Star:
                currentClip = clips[0];
                source.PlayOneShot(currentClip);
                break;
            case AudioTypes.Orb:
                currentClip = clips[1];
                source.PlayOneShot(currentClip);
                break;
            case AudioTypes.Tree:
                currentClip = clips[2];
                source.PlayOneShot(currentClip);
                break;

            default:
                break;
        }
    }

}
public enum AudioTypes
{
    Star,
    Orb,
    Tree
}


