using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    private static AudioAssets _Assetsinstance;

    public static AudioAssets AudioAssetsInstance
    {
        get
        {
            if (_Assetsinstance == null && !FindObjectOfType<AudioAssets>())
            {
                _Assetsinstance = Instantiate(Resources.Load<AudioAssets>("AudioAssets"));
            }
            return _Assetsinstance;
        }
    }

    private void Awake()
    {
        if (AudioAssetsInstance)
        {
            Destroy(gameObject);
        }
        _Assetsinstance = this;
        DontDestroyOnLoad(this);
    }

    public SoundAudioClip[] audioClipArray;
    [System.Serializable]

    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip audioClip;
    }
}
