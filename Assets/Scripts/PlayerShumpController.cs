using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShumpController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    public GameObject Bullet;
    GameObject BulletOrigin;
    int delay = 0;

    public int health = 3;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        BulletOrigin = transform.Find("BulletOrigin").gameObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));

        if(Input.GetKey(KeyCode.Space) && delay > 120)
        {
            Shoot();
        }

        delay++;
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
        delay = 0;
        Instantiate(Bullet, BulletOrigin.transform.position, Quaternion.identity);
    }
}
