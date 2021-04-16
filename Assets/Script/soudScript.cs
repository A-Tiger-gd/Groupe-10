using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soudScript : MonoBehaviour
{
    public AudioSource audioForMusic;
    public AudioClip music;
    public AudioClip dash;
    public AudioClip shoot;
    public AudioClip heal;
    public AudioClip sheeld;
    public AudioClip enemy;
    public AudioClip botton;

    // Start is called before the first frame update
    void Start()
    {
        audioForMusic.clip = music;
        audioForMusic.Play();
    }
}
