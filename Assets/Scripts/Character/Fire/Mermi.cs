using UnityEngine;

public class Mermi : MonoBehaviour
{
    public Karakter Karakter { get; set; }

    [SerializeField]
    private float BulletSpeed;

    [SerializeField]
    private GameObject BulletTrack;

    private Rigidbody2D BulletRigidbody;

    private Vector3 BulletDirection;

    /*
    [SerializeField]
    private LayerMask BulletLayer;

    [SerializeField]
    private float BulletRange;
    */

    void Start()
    {
        Karakter = FindObjectOfType<Karakter>();

        BulletRigidbody = GetComponent<Rigidbody2D>();

        BulletDirection = transform.localScale;

        if (Karakter.lookingRight)
        {
            BulletDirection.x = 1;
        }
        else
        {
            BulletDirection.x = -1;
        }

        transform.localScale = BulletDirection;

        Destroy (gameObject, 3);
    }

    void FixedUpdate()
    {
        BulletRigidbody.velocity = new Vector2 (BulletSpeed * BulletDirection.x, BulletRigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Duvar") || collision.gameObject.CompareTag("Düþman"))
        {
            Instantiate(BulletTrack, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
