using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameButton : MonoBehaviour
{
    Button restartButton;
    GameManager gameManager;

    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        gameManager.RestartGame();

        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
            button.gameObject.SetActive(false);
    }
}
