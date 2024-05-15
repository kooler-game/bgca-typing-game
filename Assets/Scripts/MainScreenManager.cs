using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gamemodeMenuPanel;
    public GameObject rankingMenuPanel;
    public TextMeshProUGUI[] rankingItemWpms;

    public static void StartGame(string mode)
    {
        switch (mode)
        {
            case "TIME":
                TypingManager.gamemode = GameMode.TIME;
                break;
            case "COUNT":
                TypingManager.gamemode = GameMode.COUNT;
                break;
            default:
                throw new NotImplementedException("Only accept TIME and COUNT");
        }
        SceneManager.LoadScene("Game");

        SoundEffectManager.Instance.OnClick();
    }

    public void OnClickGameMenu()
    {
        mainMenuPanel.SetActive(false);
        gamemodeMenuPanel.SetActive(true);

        SoundEffectManager.Instance.OnClick();
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

        SoundEffectManager.Instance.OnClick();
    }

    public void OnClickBackToMainMenu()
    {
        rankingMenuPanel.SetActive(false);
        gamemodeMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        SoundEffectManager.Instance.OnClick();
    }
}
