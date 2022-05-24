using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public AudioClip button;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            audioSource.PlayOneShot(button);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            audioSource.PlayOneShot(button);
        }

    }
}
