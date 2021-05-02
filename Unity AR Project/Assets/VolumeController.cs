using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    private AudioSource audioSource;
    private float musicVolume = 1f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void AdjustVolume(float vol)
    {
        musicVolume = vol;
    }
}
