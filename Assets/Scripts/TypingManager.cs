using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    string[] word = { "hello", "bye", "happy" };

    [SerializeField]
    TextMeshProUGUI text;

    private int _answerPtr = 0;
    private int randomIndex;

    void Start()
    {
        ShowNewText();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(word[randomIndex][_answerPtr].ToString()))
            {
                // Correct
                _answerPtr++;

                if (_answerPtr >= word[randomIndex].Length)
                {
                    ShowNewText();
                }
            }
            else
            {
                // Incorrect
            }
        }
    }

    void ShowNewText()
    {
        _answerPtr = 0;
        randomIndex = Random.Range(0, word.Length);
        text.text = word[randomIndex];
    }
}
