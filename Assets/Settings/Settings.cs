using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
	public static float TRANSITION_DURATION {
		get {return 1f;}
		set {;}
	}

	public static float SOUND_VOLUME {
		get {return PlayerPrefs.GetFloat("Music Volume", 1.0f);}
		set {PlayerPrefs.SetFloat("Music Volume", value);}
	}

	public static float MUSIC_VOLUME {
		get {return PlayerPrefs.GetFloat("Music Volume", 1.0f);}
		set {PlayerPrefs.SetFloat("Music Volume", value);}
	}

	public static float MASTER_VOLUME {
		get {return PlayerPrefs.GetFloat("MasterVolume", 1.0f);}
		set {PlayerPrefs.SetFloat("MasterVolume", value);}
	}
}
