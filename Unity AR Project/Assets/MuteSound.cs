using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MuteSound : MonoBehaviour
{
    private Toggle muteSoundToggle;
    private bool isMuted;
    
    void Start()
    {
        muteSoundToggle = GetComponent<Toggle>();

        if (AudioListener.volume == 0)
        {
            muteSoundToggle.isOn = false;
        }
    }

    public void AudioToggleValueChange()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }

}
