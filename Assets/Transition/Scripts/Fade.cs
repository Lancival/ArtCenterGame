using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *  This script can be attached to any game object with a CanvasGroup component to control fading in and out behaviour.
 *  Attached game object (and its children) will have their alpha component set to 0 while faded out and will set to be
 *  NOT ACTIVE while at full invsibility.
 */

public class Fade : MonoBehaviour {

	private CanvasGroup uiElement;			// Canvas group on the GameObject this script is attached to
    private IEnumerator coroutine = null;	// Current fading coroutine being run, if any

	// Intialize variables, even when inactive at start. Will stop script if no canvas group found.
	private void Awake()
	{
		uiElement = gameObject.GetComponent<CanvasGroup>();
		if (uiElement == null)
		{
			Debug.Log("Error: Fade script attached to a game object without a CanvasGroup component.");
			Destroy(this);
		}
	}

	/*  Changes the alpha value of the canvas group over time, along with activating/deactivating the group
	 *  CanvasGroup cg:	CanvasGroup to be faded.
	 *  float start:	Starting value of alpha to be faded from. Must be between 0 and 1, inclusive.
	 *  float end:		Ending value of alpha to be faded to. Must be between 0 and 1, inclusive.
	 *	float duration:	Length of fade transition, in seconds. Must be a positive number.
	 *	bool active:	Whether the game object attached to cg should end as active or not.
	 */
    private static IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration, bool active) {
    	float startTime = Time.time;
    	float percent = 0;

    	// Lerp between start and end values of alpha
    	while (true) {
    		percent = (Time.time - startTime) / duration;
    		if (percent < 1)
    			cg.alpha = Mathf.Lerp(start, end, percent);
    		else
    		{
    			cg.alpha = end;
    			break;
    		}
    		yield return new WaitForEndOfFrame();
    	}
    	cg.transform.gameObject.SetActive(active);
    }

    // Reactivates the uiElement and fades it to full visibilty, over a time period of duration seconds.
    public void FadeIn(float fadeDuration)
    {
    	gameObject.SetActive(true);
        Stop();

        if (fadeDuration > 0)
        {
    		coroutine = FadeCanvasGroup(uiElement, uiElement.alpha, 1, fadeDuration, true);
        	StartCoroutine(coroutine);
        }
        else
        	uiElement.alpha = 1;
    }

    // Fades the uiElement to full invisibility and deactivates it, over a time period of duration seconds.
    public void FadeOut(float fadeDuration)
    {
        Stop();
        if (fadeDuration > 0)
        {
    		coroutine = FadeCanvasGroup(uiElement, uiElement.alpha, 0, fadeDuration, false);
        	StartCoroutine(coroutine);
        }
        else
        {
        	uiElement.alpha = 0;
        	gameObject.SetActive(false);
        }
    }

    // Immediately end any fade couroutine currently being run.
    private void Stop()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
