using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

	private Slider[] sliders = new Slider[3];

	// Set starting value of settings sliders to the values saved in PlayerPrefs.
    void Start()
    {
    	for (int i = 0; i < 3; i++)
    		sliders[i] = transform.GetChild(i).GetComponent<Slider>();

    	sliders[0].value = Settings.MASTER_VOLUME;
    	sliders[1].value = Settings.MUSIC_VOLUME;
    	sliders[2].value = Settings.SOUND_VOLUME;
    }

    public void UpdateMasterVolume(float volume)
    {
    	Settings.MASTER_VOLUME = volume;
    }

    public void UpdateMusicVolume(float volume)
    {
    	Settings.MUSIC_VOLUME = volume;
    }

    public void UpdateSoundVolume(float volume)
    {
    	Settings.SOUND_VOLUME = volume;
    }
}
