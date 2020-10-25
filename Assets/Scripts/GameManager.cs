using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive { get; set; }

    [SerializeField] GameObject blackSamurai;
    [SerializeField] Button restartButton;

    CharacterController characterController;
    SoundManager soundManager;
    ParticleSystem spawnBkackSamuraiEffect;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        characterController = GameObject.Find("Player").GetComponent<CharacterController>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        spawnBkackSamuraiEffect = GameObject.Find("BlackSamuraiEffect").GetComponent<ParticleSystem>();
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        if(Enemy.deathCount >= 3)
        {
            Enemy.deathCount = 0;
            soundManager.PlaySpawnBlackSamurai();
            StartCoroutine(PlaySpeakBlackSamurai());


            StartCoroutine(ShowRestartButton());
        }
    }

    public void StartGame()
    {
        IsGameActive = true;
        soundManager.StartMainLoop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator PlaySpeakBlackSamurai()
    {
        // Play spawn Effect
        spawnBkackSamuraiEffect.Play();
        blackSamurai.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        soundManager.PlayBlackSamurai();
        yield return new WaitForSecondsRealtime(5f);
        soundManager.PlayAbulityUnlock();
        characterController.PlayAbuilityEffect();
        characterController.PlayStartEffect();
        characterController.PlayDanceAnimation();
        characterController.CanAttack = false;
    }

    private IEnumerator ShowRestartButton()
    {
        yield return new WaitForSecondsRealtime(20f);
        restartButton.gameObject.SetActive(true);
    }
}
