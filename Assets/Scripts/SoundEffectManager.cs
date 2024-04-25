using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    private AudioSource audioSource;

    public AudioClip onType;
    public AudioClip onHighlight;
    public AudioClip onClick;
    public AudioClip onCorrect;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(transform.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnType()
    {
        audioSource.PlayOneShot(onType, 1f);
    }
    public void OnHighlight()
    {
        audioSource.PlayOneShot(onHighlight, 1f);
    }
    public void OnClick()
    {
        audioSource.PlayOneShot(onClick, 1f);
    }
    public void OnCorrect()
    {
        audioSource.PlayOneShot(onCorrect, 1f);
    }
}
