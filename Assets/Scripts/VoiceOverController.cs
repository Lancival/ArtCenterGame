using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverController : MonoBehaviour
{

	private float startTime;
	[SerializeField] private SceneLoader sl;

    void Start()
    {
		startTime = Time.time;
    }

    void Update()
    {
		if (Time.time - startTime > 15.5f)
        {
			sl.LoadNextScene();
            Destroy(this);
        }
    }
}
