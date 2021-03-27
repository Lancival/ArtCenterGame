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

    private void Start()
    {
        cam = GetComponent<Camera>();
        players = GameObject.FindObjectsOfType<Player>();
        cam.orthographicSize = 2;
        activeplayer = players[0];
    }

    private void Update()
    {
        if (!reachedStartSize)
        {
            //Debug.Log(cam.orthographicSize);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, camStartSize+.2f, ref camVelocity, camStartTime,5,Time.deltaTime);
            if (Mathf.Abs(cam.orthographicSize-camStartSize)<.01f)
            {
                cam.orthographicSize = camStartSize;
                reachedStartSize = true;
            }

            return;
        }
        
        //calculate target position and rotation
        targetPoint = (players[0].transform.position + players[1].transform.position) / 2f;
        
        //smooth damp both rotation and position
        transform.position = Vector3.SmoothDamp(transform.position, targetPoint, ref moveVelocity, 1,5,Time.deltaTime);

    }
}
