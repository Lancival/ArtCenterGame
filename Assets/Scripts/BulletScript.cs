using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;

    int dir = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeDirection()
    {
        dir *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, 10 * dir);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (dir == 1)
        {
            if (col.gameObject.tag == "enemy")
            {
                col.gameObject.GetComponent<BossShumpController>().Damage();

                Destroy(gameObject);
            }
        }

        if (dir == -1 && col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerShumpController>().Damage();

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }

}
