using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCollSfx : MonoBehaviour
{
    AudioSource CollisionAudioSource;
    public AudioClip CollisionAudioClip;
    void Start()
    {
        CollisionAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
	{
		CollisionAudioSource.PlayOneShot(CollisionAudioClip);
	}
}
