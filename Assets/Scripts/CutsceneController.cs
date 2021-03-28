using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{

	private Fade[] cutscenes;		// Array of cutscenes
	private float startTime;		// Time at the start of the scene
	private ushort current = 0;		// Index of cutscene currently being displayed

	[SerializeField] SceneLoader transition;	// SceneLoader script

    // Start is called before the first frame update
    void Start()
    {
        cutscenes = new Fade[3];
        for (int i = 0; i < 3; i++)
        	cutscenes[i] = transform.GetChild(i).GetComponent<Fade>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;
        if (current == 0 && elapsedTime > 14.5f)
        {
        	cutscenes[0].FadeOut(0.5f * Settings.TRANSITION_DURATION);
        	cutscenes[1].FadeIn(Settings.TRANSITION_DURATION);
        	current++;
        }
        else if (current == 1 && elapsedTime > 27.0f)
        {
        	cutscenes[1].FadeOut(0.5f * Settings.TRANSITION_DURATION);
        	cutscenes[2].FadeIn(Settings.TRANSITION_DURATION);
        	current++;
        }
        else if (current == 2 && elapsedTime > 43.0f)
        {
        	transition.LoadNextScene();
        }
    }
}
