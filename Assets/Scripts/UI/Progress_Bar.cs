using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Progress_Bar : MonoBehaviour
{
    private Slider slider;

    private ParticleSystem ParticleSys;

    public Manager ManagerScript { get; set; }

    /*
    private void Start()
    {
        IncrementProgress(1);
    }

    private void Update()
    {
        if (slider.value < targetProgress)
        {
            speed = Random.Range(0.1f, 0.3f);

            slider.value += speed * Time.deltaTime;

            if (!particleSys.isPlaying)
            {
                particleSys.Play();
            }
        }
        else
        {
            particleSys.Stop();
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
    */

    private void Awake()
    {
        ManagerScript = FindObjectOfType<Manager>();

        slider = gameObject.GetComponent<Slider>();

        ParticleSys = GameObject.Find("Progress Bar Particles").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(StartLoading(ManagerScript.level));
    }

    private IEnumerator StartLoading(int level)
    {
        yield return new WaitForSeconds(1);
        
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {
            if (!ParticleSys.isPlaying)
            {
                ParticleSys.Play();
            }
            slider.value = async.progress;
            yield return null;
        }
        ParticleSys.Stop();
    }
}
