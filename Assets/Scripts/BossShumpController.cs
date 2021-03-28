using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShumpController : MonoBehaviour
{
    Rigidbody2D rb;

    public  float xspeed;
    public float yspeed;
    float fireRate;
    public float health;

    public GameObject BossBullet;

    private void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        fireRate = fireRate + (Random.Range(fireRate / -2, fireRate / 2));
        InvokeRepeating("Shoot", fireRate, fireRate);
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

    public void Damage()
    {
        health--; 
           if(health == 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        GameObject temp = (GameObject)Instantiate(BossBullet, transform.position, Quaternion.identity);
        temp.GetComponent<BulletScript>().ChangeDirection();
    }
}
