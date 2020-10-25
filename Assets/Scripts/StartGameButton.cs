using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    Button button;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        Debug.Log("Game Strted!");
        gameManager.StartGame();

        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
            button.gameObject.SetActive(false);
    }
}
