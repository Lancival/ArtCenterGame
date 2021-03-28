using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShumpController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    int health = 3;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));
    }

    public void Damage()
    {
        health--;

        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
