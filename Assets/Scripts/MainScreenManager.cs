using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject rankingMenuPanel;
    public TextMeshProUGUI[] rankingItemWpms;

    public static void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickRankingMenu()
    {
        // Set ranking
        float[] wpms = ScoreManager.rankingWPM;

        for (int i = 0; i < wpms.Length; i++)
        {
            rankingItemWpms[i].text = (i + 1).ToString() + ". " + wpms[i].ToString("0.00");
        }

        mainMenuPanel.SetActive(false);
        rankingMenuPanel.SetActive(true);
    }

    public void OnClickBackToMainMenu()
    {
        rankingMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
