using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static Manager ManagerScript;

    public GameObject SettingsScreen;

    public Slider musicSlider;
    public Slider effectSlider;

    public TMPro.TMP_Dropdown qualitySettings;

    private void Awake()
    {
        ManagerScript = FindObjectOfType<Manager>();

        musicSlider.value = ManagerScript.BackgroundMusic.volume;
        effectSlider.value = ManagerScript.SoundEffect.volume;

        qualitySettings.value = ManagerScript.quality;
    }

    public void MusicVolume()
    {
        ManagerScript.BackgroundMusic.volume = musicSlider.value;
    }

    public void EffectVolume()
    {
        ManagerScript.SoundEffect.volume = effectSlider.value;
    }

    public void StartGame()
    {
        ManagerScript.SoundEffect.PlayOneShot(ManagerScript.Click);

        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        if (SettingsScreen.activeSelf)
        {
            ManagerScript.SoundEffect.PlayOneShot(ManagerScript.Click);

            SettingsScreen.SetActive(false);
        }
        else
        {
            ManagerScript.SoundEffect.PlayOneShot(ManagerScript.Click);

            SettingsScreen.SetActive(true);
        }
    }

    public void Exit()
    {
        ManagerScript.SoundEffect.PlayOneShot(ManagerScript.Click);
        Application.Quit();
    }

    public void QualitySetting(int kalite)
    {
        QualitySettings.SetQualityLevel(kalite);

        ManagerScript.quality = kalite;
    }
}
