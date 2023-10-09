using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSfxCtrl : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
	{
		audioSource.PlayOneShot(audioClip);
	}
}