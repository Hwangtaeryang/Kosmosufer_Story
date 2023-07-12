using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxPlayer;


    public void PlaySFXSound(string _sfxName)
    {
        sfxPlayer.PlayOneShot(Resources.Load<AudioClip>("sfx/" + _sfxName));
    }

}
