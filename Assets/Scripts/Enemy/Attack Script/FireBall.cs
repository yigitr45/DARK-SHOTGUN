using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Enemy enemy;

    public Rigidbody2D FireballRigidbody { get; set; }

    public float speed;

    void Start()
    {
        enemy = FindObjectOfType<Enemy>();

        FireballRigidbody = GetComponent<Rigidbody2D>();
    }
}
