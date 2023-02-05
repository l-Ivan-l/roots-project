using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPool : MonoBehaviour
{
    public static SFXPool instance;
    [SerializeField] private AudioSource source;
    [Header("Respawn Sound")]
    [SerializeField] [Range(0,1)] private float respawnVolume = 1;
    [SerializeField] private List<AudioClip> respawnSounds = new List<AudioClip>();

    [Header("Impact Sound")]
    [SerializeField] [Range(0,1)] private float impactVolume = 1;
    [SerializeField] private List<AudioClip> impactounds = new List<AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayRespawnSound()
    {
        int ran = Random.Range(0,respawnSounds.Count -1);
        source.PlayOneShot(respawnSounds[ran], respawnVolume);
    }

    public void PlayImpactSound()
    {
        int ran = Random.Range(0,impactounds.Count -1);
        source.PlayOneShot(impactounds[ran], impactVolume);
    }
}

