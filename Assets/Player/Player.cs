using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rot = 0;
    
    public CharacterController2D controller;

    public float runspeed = 40f;

    private float horizontalMove = 10f;
    private bool jump = false;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,90+rot*Mathf.Rad2Deg);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime*10, jump,rot);
        //Debug.Log(horizontalMove * Time.deltaTime+"; "+ crouch+"; "+ jump);
        jump = false;
    }
}