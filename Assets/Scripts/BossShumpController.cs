using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShumpController : MonoBehaviour
{
    Rigidbody2D rb;

    public  float xspeed;
    public float yspeed;
    public float fireRate;
    public float health;
    public int delay = 0;

    public GameObject BossBullet;
    GameObject BulletOrigin;

    [SerializeField] private SceneLoader sl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        BulletOrigin = transform.Find("BulletOrigin").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        fireRate = fireRate + (Random.Range(fireRate / -2, fireRate / 2));
        InvokeRepeating("Shoot", fireRate, fireRate);
        rb.velocity = new Vector2(xspeed * Random.Range(-10, 10), yspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 140)
        {
            rb.velocity = new Vector2(xspeed * Random.Range(-3, 3), yspeed);
            delay = 0;
        }
        delay++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerShumpController>().Damage();
        }

        else if(collision.gameObject.tag == "Boundary")
        {
            xspeed *= -1;
            Debug.Log("flipping");
            delay = 150; 
        }
    }

    public void Damage()
    {
        health--; 
           if(health == 0)
        {
            sl.LoadNextScene();
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        GameObject temp = (GameObject)Instantiate(BossBullet, transform.position, Quaternion.identity);
        temp.GetComponent<BulletScript>().ChangeDirection();
    }
}
