using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        // Load the first level of the game
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
