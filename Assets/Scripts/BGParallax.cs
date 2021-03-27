using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallax : MonoBehaviour
{
    public float pSpeed = 1f;

    public Transform cam;
    
    private Vector2 offset = Vector2.zero;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetTextureOffset("_MainTex", new Vector2(cam.position.x,cam.position.y)*pSpeed*-1);
        //Vector3 temp = cam.position;
        //temp[2] = 10;
        //transform.position = temp;
    }
}
