//Lucas de la Pena
// 9/22/2024
// This handles the random direction of the objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed = 1f;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb2d.velocity = randomDirection * speed;
    }


    // Update is called once per frame
    void Update()
    {
        velocity = rb2d.velocity;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        speed = velocity.magnitude;
        Vector2 direction = Vector2.Reflect(velocity.normalized, other.contacts[0].normal);
        rb2d.velocity = direction * speed;
    }
}
