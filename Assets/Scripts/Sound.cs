using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    private Game _game;
    [HideInInspector] public bool isMuted = false;
    public AudioMixer mixer;
    public AudioSource bgm;
    public AudioSource win;
    public AudioSource lose;
    public AudioSource right;
    public AudioSource wrong;

    public void Init(Game boot) {
        _game = boot;
    }

    public void OnMuteButtonPress() {
        isMuted = !isMuted;
        float value = isMuted ? 0 : 1;
        float dBValue = Mathf.Log10(value + 0.0001f) * 20;
        mixer.SetFloat("MasterVolume", dBValue);
    }
}
