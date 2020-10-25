using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip thereYouAre;
    [SerializeField] private AudioClip firstSecret;
    [SerializeField] private AudioClip LegPunch;
    [SerializeField] private AudioClip spawnBlackSamurai;
    [SerializeField] private AudioClip abulityUnlock;

    [SerializeField] private AudioSource backgroundLoop;

    private AudioSource audioSource;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBlackSamurai()
    {
        StartCoroutine(PlayBlackSamuraiSequence());
    }

    public void StartMainLoop()
    {
        backgroundLoop.Play();
        backgroundLoop.volume = 0.05f;
    }

    public void PlayKickSound()
    {
        audioSource.PlayOneShot(LegPunch, 0.06f);
    }

    public void PlaySpawnBlackSamurai()
    {
        audioSource.PlayOneShot(spawnBlackSamurai, 0.5f);
    }

    public void PlayAbulityUnlock()
    {
        audioSource.PlayOneShot(abulityUnlock, 0.5f);
    }

    private IEnumerator PlayBlackSamuraiSequence()
    {
        audioSource.PlayOneShot(thereYouAre, 1f);
        yield return new WaitForSecondsRealtime(thereYouAre.length);
        audioSource.PlayOneShot(firstSecret, 1f);
    }
}
