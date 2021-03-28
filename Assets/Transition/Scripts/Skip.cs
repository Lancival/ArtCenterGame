using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{

	private SceneLoader sl;

    // Start is called before the first frame update
    void Start()
    {
        sl = transform.GetChild(0).GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            sl.LoadNextScene();
        }
    }
}
