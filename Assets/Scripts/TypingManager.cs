using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    string[] word;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    TextAsset textFileSource;

    public int targetWord = 10;
    private int wordTyped = 0;

    private bool inGame = true;
    public GameObject gameoverPanel;
    public TextMeshProUGUI typedText;

    private int _answerPtr = 0;
    private int randomIndex;

    void Start()
    {
        ReadTextFile(textFileSource);
        ShowNewText();
        SetScoreText(0, targetWord);
    }

    void Update()
    {
        if (inGame && Input.anyKeyDown)
        {
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
        ShowNewText();
        wordTyped++;
        SetScoreText(wordTyped, targetWord);

        if (wordTyped >= targetWord)
        {
            // Game End
            inGame = false;
            gameoverPanel.SetActive(true);
        }
    }

    void SetScoreText(int current, int target)
    {
        typedText.text = "Typed: " + current.ToString() + "/" + target.ToString();
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
}
