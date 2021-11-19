using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiAudio : MonoBehaviour
{
    public AudioClip click;
    public AudioClip back;
    public AudioClip hover;
    public AudioSource uiSrc;

    public void Start()
    {
        uiSrc = GetComponent<AudioSource>();
    }
    public void uiClick()
    {
        uiSrc.PlayOneShot(click);
    }

    public void uiBack()
    {
        uiSrc.PlayOneShot(back);
    }

    public void uiHover()
    {
        uiSrc.PlayOneShot(hover);
    }

}
