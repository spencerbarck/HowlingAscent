using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{
    public static bool GameIsPaused = false;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameMananger.Instance.GetGameState()==GameState.Paused) {
                Resume();
            } else 
            if (GameMananger.Instance.GetGameState()==GameState.Climbing) {
                Pause();
            }
        }
    }

    private void Resume() 
    {
        GameMananger.Instance.ChangeGameState(GameState.Climbing);
    }

    private void Pause() 
    {
        GameMananger.Instance.ChangeGameState(GameState.Paused);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
