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

    [Header("Mandrake Die Sound")]
    [SerializeField] [Range(0,1)] private float mandrakeDieVolume = 1;
    [SerializeField] private List<AudioClip> mandrakeDieSounds = new List<AudioClip>();
    [Header("Gnome Die Sound")]
    [SerializeField] [Range(0,1)] private float gnomeDieVolume = 1;
    [SerializeField] private List<AudioClip> gnomeDieSounds = new List<AudioClip>();
    [Header("Gnome Win Sound")]
    [SerializeField] [Range(0,1)] private float gnomeWinVolume = 1;
    [SerializeField] private AudioClip gnomeWinSound;
    [Header("Mandrake Win Sound")]
    [SerializeField] [Range(0,1)] private float mandrakeWinVolume = 1;
    [SerializeField] private AudioClip mandrakeWinSound;

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

    public void PlayMandrakeDieSound()
    {
        int ran = Random.Range(0,mandrakeDieSounds.Count -1);
        source.PlayOneShot(mandrakeDieSounds[ran], mandrakeDieVolume);
    }

    public void PlayGnomeDieSound()
    {
        int ran = Random.Range(0,gnomeDieSounds.Count -1);
        source.PlayOneShot(gnomeDieSounds[ran], gnomeDieVolume);
    }

    public void PlayGnomeWinSound()
    {

        source.PlayOneShot(gnomeWinSound, gnomeWinVolume);
    }

    public void PlayMandrakeWinSound()
    {

        source.PlayOneShot(mandrakeWinSound, mandrakeWinVolume);
    }
}

