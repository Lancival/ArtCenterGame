using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive = false;
    
    public float rot = 0;
    
    public CharacterController2D controller;

    public float runspeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;

    [SerializeField] private Player otherPlayer;

    private void Start()
    {
        //find the other player
        Player[] players = GameObject.FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            if (p != this)
            {
                otherPlayer = p;
                break;
            } 
        }
    }

    private void Update()
    {
        if (isActive)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
        //for some reason angle doesn't like to play nice and do negative values
        //this line makes negative values for us
        float flip = Mathf.Sign(-transform.position.y + otherPlayer.transform.position.y);
        rot = (Vector3.Angle(otherPlayer.transform.position - transform.position, Vector3.right))* Mathf.Deg2Rad * flip;
        //Debug.DrawLine(transform.position,transform.position+new Vector3(Mathf.Cos(rot),Mathf.Sin(rot),0));
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,90+rot*Mathf.Rad2Deg);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime*2, jump,rot);
        //Debug.Log(horizontalMove * Time.deltaTime+"; "+ crouch+"; "+ jump);
        jump = false;
    }
}