using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _backgroundSource;
    [SerializeField]
    private AudioClip _music;
    [SerializeField]
    private float _volume;

    // Start is called before the first frame update
    void Start()
    {
        LoopSound(_music, _volume);
    }

    private void LoopSound(AudioClip clip, float volume)
    {
        _backgroundSource.clip = clip;
        _backgroundSource.volume = volume;
        _backgroundSource.loop = true;
        _backgroundSource.Play();

        _music = clip;
        _volume = volume;

        Debug.Log("Sound Played");
    }
}
