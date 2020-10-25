using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip thereYouAre;

    private AudioSource audioSource;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBlackSamurai()
    {
        audioSource.PlayOneShot(thereYouAre);
    }
}
