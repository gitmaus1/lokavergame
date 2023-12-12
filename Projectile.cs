using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        // sendir Projectile í rjéta át
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // ef hittir óvin 
        EnemyController e = other.collider.GetComponent<EnemyController>();

        move player = other.gameObject.GetComponent<move>();
        if (e != null)
        {
            e.Fix();
            Destroy(gameObject);
        }
        

        else if (player != null)
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
