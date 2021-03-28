using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShumpController : MonoBehaviour
{
    Rigidbody2D rb;

    public  float xspeed;
    public float yspeed;
    float fireRate;
    float health;

    private void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xspeed, yspeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerShumpController>().Damage();
        }
    }

    void Damage()
    {
        health--; 
           if(health == 0)
        {
            Destroy(gameObject);
        }
    }

}
