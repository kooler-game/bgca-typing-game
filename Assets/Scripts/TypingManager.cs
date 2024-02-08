using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    string[] word;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    TextAsset textFileSource;

    private int _answerPtr = 0;
    private int randomIndex;

    void Start()
    {
        ReadTextFile(textFileSource);
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

                // Show correct color response
                text.text =  "<color=#C5E90B>" + word[randomIndex].Substring(0, _answerPtr) + "</color>" + word[randomIndex].Substring(_answerPtr);

                if (_answerPtr >= word[randomIndex].Length)
                {
                    ShowNewText();
                }
            }
            else
            {
                // Incorrect
                text.text =  "<color=#C5E90B>" + word[randomIndex].Substring(0, _answerPtr) + "</color>" 
                            + "<color=#FF0000>" + word[randomIndex].Substring(_answerPtr, 1) + "</color>"  
                            + word[randomIndex].Substring(_answerPtr + 1);
            }
        }
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
