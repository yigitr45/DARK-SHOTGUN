using UnityEngine;

public class BulletTrack : MonoBehaviour
{
    public Mermi MermiScript { get; set; }

    void Start()
    {
        MermiScript = FindObjectOfType<Mermi>();

        Vector3 izYon = transform.localScale;

        if (MermiScript.transform.localScale.x < 0)
        {
            izYon.x = -2;
        }
        else
        {
            izYon.x = 2;
        }

        transform.localScale = izYon;
    }

    public void Yok_Ol()
    {
        Destroy(gameObject);
    }
}
