using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    private Camera cam;

    public Player[] players;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
        players = GameObject.FindObjectsOfType<Player>();
    }

    private void Update()
    {
        
    }
}
