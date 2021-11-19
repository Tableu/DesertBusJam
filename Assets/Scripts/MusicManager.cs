using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    //Music source and tracks
    public List<MusicTrack> musicClips;
    public AudioSource musicSrc;
    public AudioSource motiveSrc;

    private bool isPlayingMotive = false;
    public enum Track
    {
        title,
        gas_station,
        minigame1,
        minigame2,
        credits,
    }
    // Don't destroy gameobject
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Plays first index track since technically
    //no level was loaded at start.
    void Start()
    {
        musicSrc = GetComponent<AudioSource>();
        PlayMusic("title");
    }

    void Update()
    {
        if (isPlayingMotive && !motiveSrc.isPlaying)
        {
            musicSrc.UnPause();
            isPlayingMotive = false;
            StartCoroutine(StartFade(musicSrc, 0.3f, 1, delegate { }));
        }
    }
    //Plays music track according to level index
    public void PlayMusic(string songToChangeTo)
    {
        
        int index = (int)Enum.Parse(typeof(Track), songToChangeTo);
        AudioClip levelMusic = musicClips[index].audioClip;
        if (levelMusic)
        {
            if (musicClips[index].loop)
            {
                StartCoroutine(StartFade(musicSrc, 2.0f,0, delegate
                {
                    musicSrc.clip = musicClips[index].audioClip;
                    musicSrc.loop = musicClips[index].loop;
                    musicSrc.Play();
                    StartCoroutine(StartFade(musicSrc, 2.0f, 1, delegate { }));
                }));
            }
            else
            {
                StartCoroutine(StartFade(musicSrc, 0.3f, 0, delegate
                {
                    musicSrc.Pause();
                    motiveSrc.clip = musicClips[index].audioClip;
                    motiveSrc.loop = false;
                    motiveSrc.Play();
                    isPlayingMotive = true;
                    StartCoroutine(StartFade(motiveSrc, 0.3f, 1, delegate { }));
                }));
            }
        }
        Debug.Log("Playing song: " + songToChangeTo);
    }

    public void StopMusic()
    {
        StartCoroutine(StartFade(musicSrc,2.0f,0,delegate {  }));
        StartCoroutine(StartFade(motiveSrc,2.0f,0,delegate {  }));
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, Action callback)
    {
        float currentTime = 2.0f;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        callback();
        yield break;
    }
}

[System.Serializable]
public class MusicTrack
{
    public bool loop;
    public AudioClip audioClip;
    public MusicManager.Track track;
}
