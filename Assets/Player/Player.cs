using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime*10, jump);
        //Debug.Log(horizontalMove * Time.deltaTime+"; "+ crouch+"; "+ jump);
        jump = false;
    }
}