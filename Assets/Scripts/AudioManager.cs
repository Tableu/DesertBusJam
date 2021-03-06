using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static List<AudioSource> sources = new List<AudioSource>();
    public enum Sound
    {
        uiClick,
        uiHover,
        uiBack,
        crowdCheer01,
        crowdCheer02,
        crowdCheer03,
        joustHit,
        joustBlock,
        victory,
        tug,
    }

    //Clears any nonexistant audio sources
    public static void ClearNonExistantSources()
    {
        List<AudioSource> activeSources = new List<AudioSource>();
        foreach (AudioSource auSrc in sources)
        {
            if (auSrc != null)
            {
                activeSources.Add(auSrc);
            }
        }
        sources = activeSources;
    }

    //Gets next available audio source
    private static AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource auSource in sources)
        {
            if (auSource != null && !auSource.isPlaying)
            {
                return auSource;
            }
        }
        return AddNewAudioSource();
    }

    //Adds new audio source GameObject with an output to the SFX AudioMixer channel
    private static AudioSource AddNewAudioSource()
    {
        GameObject soundContainer = GameObject.Find("AudioSourceContainer");
        if (!soundContainer)
        {
            soundContainer = new GameObject("AudioSourceContainer");
        }
        GameObject soundGameObject = new GameObject($"Sound-{sources.Count}");
        soundGameObject.transform.SetParent(soundContainer.transform);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        AudioMixer audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        AudioMixerGroup[] audioMixerGroup = audioMixer.FindMatchingGroups("SFX");
        audioSource.outputAudioMixerGroup = audioMixerGroup[0];
        sources.Add(audioSource);
        return audioSource;
    }

    //Finds called sound in AudioAssets.cs
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.AudioAssetsInstance.audioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
    private static Dictionary<Sound, float> soundTimerDictionary;

    //Function to call the SoundTimer
    public static void InitializeSoundTimer()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.tug] = 0f;
    }

    //Sets timer for the tug sfx is played
    private static bool SoundTimer(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.tug:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerInputMax = 0.5f;
                    if (lastTimePlayed + playerInputMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    //break;
                }
                else
                {
                    return true;
                }
        }
    }


    //Play sound
    public static void PlaySound(Sound sound)
    {
        if (SoundTimer(sound))
        {
            GetAvailableAudioSource().PlayOneShot(GetAudioClip(sound));
        }
    }

    //Toggle sfx
    public void ToggleSfx()
    {
        foreach (AudioSource sfxSrc in sources)
        {
            if (sfxSrc != null)
            {
                sfxSrc.mute = !sfxSrc.mute;
            }
        }
    }

}
