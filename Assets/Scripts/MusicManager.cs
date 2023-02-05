using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour, EventListener<GameEvents>
{
    [SerializeField] private AudioSource source;
    [SerializeField] 
    [Range(0,1)]private float maxVolume = 0.5f;
    [Header("Music")]
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip loseMusic;

    [Header("Ambient sounds")]
    [SerializeField] private List<AudioClip> ambientSounds = new List<AudioClip>();
    [SerializeField] private float minTimeToPlay = 5;
    [SerializeField] private float maxTimeToPlay = 15;


    void Start()
    {
        StartCoroutine(LerpVolume(5,0,maxVolume));
        PlayAmbientSoundInRandomTime();
    }

    void OnEnable()
    {
        this.EventStartListening<GameEvents>();
    }

    void OnDisable()
    {
        this.EventStopListening<GameEvents>();
    }

    IEnumerator LerpVolume(float lerpDuration, float startValue, float endValue) 
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            source.volume = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        source.volume  = endValue;
    }

    private void PlayAmbientSoundInRandomTime()
    {
        float ran = Random.Range(minTimeToPlay,maxTimeToPlay);

        StartCoroutine(PlayRandomAmbientSound(ran));
    }

    IEnumerator PlayRandomAmbientSound(float delay)
    {
        int ran = Random.Range(0,ambientSounds.Count - 1);
        yield return new WaitForSeconds(delay);

        source.PlayOneShot(ambientSounds[ran],1);

        PlayAmbientSoundInRandomTime();
    }

    IEnumerator CoChangeMusic(AudioClip newMusic)
    {
        StartCoroutine(LerpVolume(1,maxVolume,0));
        yield return new WaitForSeconds(1f);
        StartCoroutine(LerpVolume(1,0,maxVolume));
        source.clip = newMusic;
        source.Play();
    }

    public void OnGEvent(GameEvents e)
    {
        switch(e.eventType)
        {
            case GameEventType.gameOver:
                StartCoroutine(CoChangeMusic(loseMusic));
            break;

            case GameEventType.pause:
                Debug.Log("The game is pause");
            break;

            case GameEventType.resume:
                Debug.Log("The game is resume");
            break;

            case GameEventType.win:
                StartCoroutine(CoChangeMusic(winMusic));
            break;
        }
    }
}
