using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager ManagerScript = null;

    public GameObject SettingsScreen;

    public GameObject[] ItemSave;

    public AudioClip Click;

    public AudioSource BackgroundMusic;
    public AudioSource SoundEffect;

    public int level;
    public int quality;

    public int CoinSave;
    public int HealSave;

    public float TotalBulletSave;
    public float CurrentBulletSave;

    private void Awake()
    {
        if (ManagerScript == null)
        {
            ManagerScript = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        ItemSave = new GameObject[5];

        level = 2;
        quality = 0;

        CoinSave = 0;
        HealSave = 50;

        TotalBulletSave = 10;
        CurrentBulletSave = 5;
    }

    /*
    public void MusicVolume()
    {
        BackgroundMusic.volume = musicSlider.value;
    }

    public void EffectVolume()
    {
        SoundEffect.volume = effectSlider.value;
    }

    public void YeniOyun()
    {
        SoundEffect.PlayOneShot(Click);

        level = 2;

        SceneManager.LoadScene(1);
    }

    public void Ayarlar()
    {
        if (SettingsScreen.activeSelf)
        {
            SoundEffect.PlayOneShot(Click);

            SettingsScreen.SetActive(false);
        }
        else
        {
            SoundEffect.PlayOneShot(Click);

            SettingsScreen.SetActive(true);
        }
    }

    public void Exit()
    {
        SoundEffect.PlayOneShot(Click);

        Application.Quit();
    }

    public void KaliteAyar(int kalite)
    {
        QualitySettings.SetQualityLevel(kalite);
    }
    */
}
