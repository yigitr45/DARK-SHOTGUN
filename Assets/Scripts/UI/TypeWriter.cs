using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TypeWriter : MonoBehaviour
{
    private int index;

    public float delay;

    [Multiline]
    public string[] lines;

    public GameObject DialogueBox;

    public AudioClip writerSound;

    private AudioSource AudioSRC;

    private TextMeshProUGUI thisText;

    private void Awake()
    {
        AudioSRC = GetComponent<AudioSource>();

        thisText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        thisText.text = string.Empty;

        StartDialogue();
    }

    private void StartDialogue()
    {
        index = 0;

        StartCoroutine(TypeWrite());
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;

            thisText.text = string.Empty;

            StartCoroutine(TypeWrite());
        }
        else
        {
            DialogueBox.SetActive(false);
        }
    }

    public void NextLineButton()
    {
        if (thisText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();

            thisText.text = lines[index];
        }
    }

    IEnumerator TypeWrite()
    {
        foreach (char i in lines[index].ToCharArray())
        {
            thisText.text += i.ToString();

            AudioSRC.pitch = Random.Range(0.8f, 1.2f);

            AudioSRC.PlayOneShot(writerSound);

            if (i.ToString() == ".")
            {
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
