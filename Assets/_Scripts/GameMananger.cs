using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    public static GameMananger Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField]private int wallSummitHeight;
    [SerializeField]private GameObject winMenuUI;
    [SerializeField]private GameObject deathMenuUI;
    [SerializeField]private GameObject pauseMenuUI;
    private PlayerStatus player;
    private GameState currentState = GameState.Climbing; // The current game state
    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
    }
    private void Update()
    {
        if(player.CheckHasClimbedWall(wallSummitHeight)&&currentState==GameState.Climbing)
        {
            ChangeGameState(GameState.Win);
        }
    }
    public GameState GetGameState()
    {
        return currentState;
    }
    public void ChangeGameState(GameState newState)
    {
        winMenuUI.SetActive(false);
        deathMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
        currentState = newState;
        switch (currentState)
        {
            case GameState.Win:  
                winMenuUI.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.Death:
                deathMenuUI.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.Climbing:
                break;
        }
    }
}
public enum GameState
{
    Win,
    Death,
    Paused,
    Climbing
}
