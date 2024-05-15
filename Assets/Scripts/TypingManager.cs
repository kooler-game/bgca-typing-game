using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;

public struct GameProgress
{
    public int wordTyped;
    public int usedSeconds;
}

public class TypingManager : MonoBehaviour
{
    string[] word;

    [SerializeField] TextMeshProUGUI text;

    [SerializeField] TextAsset textFileSource;

    [SerializeField] LocalizedString timerLocalizedString;
    [SerializeField] LocalizedString typedCountLocalizedString;

    // private int wordTyped = 0;

    private bool inGame = true;
    public GameObject gameoverPanel;
    public TextMeshProUGUI typedText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI wpmText;
    public TextMeshProUGUI gameoverWPM;

    private int _answerPtr = 0;
    private int randomIndex;

    GameProgress gameProgress = new GameProgress
    {
        wordTyped = 0,
        usedSeconds = 0
    };

    private float wpm => gameProgress.wordTyped / System.Math.Max(1 / 60f, gameProgress.usedSeconds / 60f);

    // Game Mode Specific
    public static GameMode gamemode = GameMode.COUNT;
    public ModeManager modeManager;
    public int targetWord = 10;
    public int targetSecond = 10;

    void Start()
    {
        modeManager = ModeManager.CreateModelManager(gamemode);

        timerLocalizedString.Arguments = new object[] { gameProgress.usedSeconds };
        timerLocalizedString.StringChanged += UpdateTimerText;
        typedCountLocalizedString.Arguments = new object[] { gameProgress.wordTyped };
        typedCountLocalizedString.StringChanged += UpdateTypedCountText;

        ReadTextFile(textFileSource);
        ShowNewText();
        SetScoreText(0, targetWord);
        timerLocalizedString.Arguments[0] = modeManager.GetTimerText(gameProgress.usedSeconds, targetSecond);
        timerLocalizedString.RefreshString();


        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }

    void Update()
    {
        if (inGame && Input.anyKeyDown)
        {
            SoundEffectManager.Instance.OnType();

            if (Input.GetKeyDown(word[randomIndex][_answerPtr].ToString()))
            {
                // Correct
                _answerPtr++;

                // Show correct color response
                text.text = "<color=#C5E90B>" + word[randomIndex].Substring(0, _answerPtr) + "</color>" + word[randomIndex].Substring(_answerPtr);

                if (_answerPtr >= word[randomIndex].Length)
                {
                    Correct();
                }
            }
            else
            {
                // Incorrect
                text.text = "<color=#C5E90B>" + word[randomIndex].Substring(0, _answerPtr) + "</color>"
                            + "<color=#FF0000>" + word[randomIndex].Substring(_answerPtr, 1) + "</color>"
                            + word[randomIndex].Substring(_answerPtr + 1);
            }
        }
    }

    void Correct()
    {
        SoundEffectManager.Instance.OnCorrect();
        ShowNewText();
        gameProgress.wordTyped++;
        SetScoreText(gameProgress.wordTyped, targetWord);

        if (modeManager.IsGameOver(gameProgress, targetWord))
        {
            GameEnd();
        }
    }

    void SetScoreText(int current, int target)
    {
        typedCountLocalizedString.Arguments[0] = modeManager.GetScoreText(gameProgress.wordTyped, targetWord); ;
        typedCountLocalizedString.RefreshString();

        wpmText.text = "WPM: " + wpm.ToString("0.00");
        gameoverWPM.text = "WPM: " + wpm.ToString("0.00");
    }

    void ShowNewText()
    {
        _answerPtr = 0;
        randomIndex = Random.Range(0, word.Length);
        text.text = word[randomIndex];
    }

    void ReadTextFile(TextAsset textFile)
    {
        word = textFile.text.Split('\n');
    }

    void UpdateTimer()
    {
        if (inGame)
        {
            gameProgress.usedSeconds += 1;
            timerLocalizedString.Arguments[0] = modeManager.GetTimerText(gameProgress.usedSeconds, targetSecond);
            timerLocalizedString.RefreshString();
            wpmText.text = "WPM: " + wpm.ToString("0.00");

            if (modeManager.IsGameOver(gameProgress, targetWord))
            {
                GameEnd();
            }
        }
    }

    public static void RestartGame()
    {
        // Temp: Scene Reload
        SoundEffectManager.Instance.OnClick();
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public static void BacktoMainMenu()
    {
        SoundEffectManager.Instance.OnClick();
        SceneManager.LoadScene("MainMenu");
    }

    private void UpdateTimerText(string value)
    {
        timeText.text = value;
    }

    private void UpdateTypedCountText(string value)
    {
        typedText.text = value;
    }

    private void GameEnd()
    {
        ScoreManager.EndGame(wpm);
        inGame = false;
        gameoverPanel.SetActive(true);
    }
}
