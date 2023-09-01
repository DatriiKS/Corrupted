using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimplePosNegSfxPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip _positiveClip;
    [SerializeField]
    private AudioClip _negativeClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayPositiveClip()
    {
        _audioSource.clip = _positiveClip;
        _audioSource.Play();
    }
    public void PlayNegatveClip()
    {
        _audioSource.clip = _negativeClip;
        _audioSource.Play();
    }
}
