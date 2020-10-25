using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive { get; set; }

    [SerializeField] GameObject blackSamurai;

    CharacterController characterController;
    SoundManager soundManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        characterController = GameObject.Find("Player").GetComponent<CharacterController>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void StartGame()
    {
        IsGameActive = true;
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        Debug.Log(Enemy.deathCount);
        if(Enemy.deathCount >= 3)
        {
            blackSamurai.SetActive(true);
            soundManager.PlayBlackSamurai();
            Enemy.deathCount = 0;
        }
    }
}
