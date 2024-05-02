using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;


    void Start()
    {
        int localeID = PlayerPrefs.GetInt("localeID", 0);
        StartCoroutine(SetLocale(localeID));
    }

    public void ChangeLocale()
    {
        if (active) return;

        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            StartCoroutine(SetLocale(1));
        }
        else
        {
            StartCoroutine(SetLocale(0));
        }
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
        PlayerPrefs.SetInt("localeID", _localeID);
        PlayerPrefs.Save();
    }
}
