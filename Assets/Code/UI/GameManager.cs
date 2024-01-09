using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
