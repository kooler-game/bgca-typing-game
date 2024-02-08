using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    string[] word = { "hello", "bye", "happy new year" };

    [SerializeField]
    TextMeshProUGUI text;

    void Start()
    {
        ShowNewText();
    }

    void ShowNewText() {
        int randomIndex = Random.Range(0, word.Length);
        text.text = word[randomIndex];
    }
}
