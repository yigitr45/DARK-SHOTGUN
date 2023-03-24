using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CodeLink : MonoBehaviour
{
    public Karakter Karakter { get; set; }
    public Manager ManagerScript { get; set; }

    public TextMeshProUGUI infoText;

    public GameObject Info;
    public GameObject PausePanel;

    public GameObject SoundButton;

    public Sprite muteSprite;
    public Sprite unmuteSprite;

    public bool Pause;
    public bool Mute;

    private void Awake()
    {
        Karakter = FindObjectOfType<Karakter>();

        ManagerScript = FindObjectOfType<Manager>();

        infoText = Info.GetComponent<TextMeshProUGUI>();

        Pause = false;
        PausePanel.SetActive(false);

        if (ManagerScript.SoundEffect.mute == true && ManagerScript.BackgroundMusic.mute == true)
        {
            Mute = true;

            SoundButton.GetComponent<Image>().sprite = muteSprite;
        }
        else
        {
            Mute = false;

            SoundButton.GetComponent<Image>().sprite = unmuteSprite;
        }
    }

    private void Update()
    {
        // While döngüsüne çevirilebilir

        if (Karakter.currentBullet == 0)
        {
            Info.SetActive(true);

            infoText.text = "NO CHARGE";
        }
        else
        {
            Info.SetActive(false);

            infoText.text = "";
        }
    }

    public void HealPotionButton()
    {
        while (Karakter.CharacterHealt < 100)
        {
            Karakter.CharacterHealt++;
            Karakter.HealtBar.value = Karakter.CharacterHealt;
        }
    }

    public void StaminaPotionButton()
    {
        Karakter.StaminaActive = false;

        while (Karakter.Stamina < 100)
        {
            Karakter.Stamina++;
            Karakter.StaminaBar.value = Karakter.Stamina;
        }

        Karakter.StaminaActive = true;
    }

    public void SpeedPotionButton()
    {
        StartCoroutine(Speed());
    }

    private IEnumerator Speed()
    {
        Karakter.Speed = 15;
        yield return new WaitForSeconds(5);
        Karakter.Speed = 10;
    }

    public void OnApplicationPause()
    {
        if (!Pause)
        {
            Time.timeScale = 0;

            PausePanel.SetActive(true);

            Pause = true;
        }
        else
        {
            Time.timeScale = 1;

            PausePanel.SetActive(false);

            Pause = false;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        ManagerScript.level = Karakter.KarakterKod.sceneNumber;

        SceneManager.LoadScene(0);
    }

    public void MuteSound()
    {
        if (!Mute)
        {
            ManagerScript.SoundEffect.mute = true;
            ManagerScript.BackgroundMusic.mute = true;

            SoundButton.GetComponent<Image>().sprite = muteSprite;

            Mute = true;
        }
        else
        {
            ManagerScript.SoundEffect.mute = false;
            ManagerScript.BackgroundMusic.mute = false;

            SoundButton.GetComponent<Image>().sprite = unmuteSprite;

            Mute = false;
        }
    }
}
