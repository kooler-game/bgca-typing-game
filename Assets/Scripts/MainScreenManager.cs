using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
