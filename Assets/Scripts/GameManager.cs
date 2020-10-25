using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive { get; set; }

    CharacterController characterController;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        characterController = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    public void StartGame()
    {
        IsGameActive = true;
    }
}
