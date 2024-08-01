using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    // Game states
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState currentState;

    // UI panels go here
    

    private void Awake()
    {
        // Ensure only one instance of GameControl exists :)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the game
        ChangeState(GameState.Playing);
    }

    private void Update()
    {
        //input based on the current game state
        switch (currentState)
        {
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Paused:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResumeGame();
                }
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;


        // Perform additional actions based on the state
        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                Time.timeScale = 1;
                break;
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
        SceneManager.LoadScene("GameScene"); 
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void ReturnToMainMenu()
    {
        ChangeState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu"); // Ensure you have a scene named "MainMenu"
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}