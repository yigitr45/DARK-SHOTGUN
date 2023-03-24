using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject throwObject;

    private SpriteRenderer objRenderer;

    public AudioSource ChestAudioSource;
    public AudioClip ChestClip;

    private Rigidbody2D throwRigid;

    private Vector2 throwVector;

    private void Awake()
    {
        throwRigid = throwObject.GetComponent<Rigidbody2D>();
        objRenderer = throwObject.GetComponent<SpriteRenderer>();

        ChestAudioSource = GameObject.Find("Sound Effect").GetComponent<AudioSource>();

        throwObject.SetActive(false);

        throwVector = new Vector2(Random.Range(-5, 5), Random.Range(30, 35));
    }

    public void Throw ()
    {
        throwObject.SetActive(true);

        throwRigid.velocity = throwVector;

        StartCoroutine(Trigger());
    }

    IEnumerator Trigger ()
    {
        objRenderer.sortingOrder = 10;
        throwObject.layer = LayerMask.NameToLayer("NoContact");
        yield return new WaitForSeconds(0.5f);
        throwObject.layer = LayerMask.NameToLayer("Default");
    }

    public void ChestSound()
    {
        ChestAudioSource.PlayOneShot(ChestClip);
    }
}
