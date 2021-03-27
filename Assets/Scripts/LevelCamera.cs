using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    private Camera cam;
    public float camStartSize = 10;
    public float camStartTime = 1;
    [SerializeField] private bool reachedStartSize = false;
    private float camVelocity = 0;
    
    [SerializeField] private Player activeplayer;
    public Player[] players;

    private Vector3 targetPoint;
    private Vector3 moveVelocity = Vector3.zero;
    private float rotatevelocity = 0;

    private void Start()
    {
        cam = GetComponent<Camera>();
        players = GameObject.FindObjectsOfType<Player>();
        cam.orthographicSize = 2;
        activeplayer = players[0];
        activeplayer.isActive = true;
    }

    private void Update()
    {
        if (!reachedStartSize)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, camStartSize+.2f, ref camVelocity, camStartTime,500,Time.deltaTime);
            if (Mathf.Abs(cam.orthographicSize-camStartSize)<.01f)
            {
                cam.orthographicSize = camStartSize;
                reachedStartSize = true;
                camVelocity = 0;
            }

            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            foreach (Player p in players)
            {
                if (p != activeplayer)
                {
                    activeplayer.isActive = false;
                    activeplayer=p;
                    activeplayer.isActive = true;
                    break;
                }
            }
        }
        
        //calculate target position
        targetPoint = (players[0].transform.position + players[1].transform.position) / 2f;
        
        //smooth damp both rotation and position
        transform.position = Vector3.SmoothDamp(transform.position, targetPoint, ref moveVelocity, .5f,500,Time.deltaTime);
        //smooth damp rotation
        float tempRot = transform.eulerAngles.z;
        tempRot = Mathf.SmoothDamp(tempRot,activeplayer.transform.eulerAngles.z,ref rotatevelocity,.5f,500,Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, tempRot);
        
        //smooth damp size
        float newCamSize = Vector3.Magnitude(players[0].transform.position - players[1].transform.position) * .74f;
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, newCamSize, ref camVelocity, .5f,500,Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
