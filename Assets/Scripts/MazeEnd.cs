using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnd : MonoBehaviour
{
    [SerializeField] private CharacterController2D Player1;
    [SerializeField] private CharacterController2D Player2;
    [SerializeField] private SceneLoader sl;

    void Update()
    {
    	if (Player1.center && Player2.center)
    		sl.LoadNextScene();
    }
}
