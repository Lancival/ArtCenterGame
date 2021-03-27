using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Loop : MonoBehaviour
{
	[SerializeField] private AudioMixerGroup output;	// AudioMixerGroup that generated AudioSources should output to
	[SerializeField] private AudioClip intro;			// AudioClip that should be played once, at the start
	[SerializeField] private AudioClip loop;			// AudioClip that will be played on repeat, after intro has finished

	private AudioSource[] sources;						// Array of generated AudioSources
	private bool playing = true;						// Whether the first AudioSource in sources is currently playing
	private double startTime;							// Time the next AudioClip will start playing

	private static AudioSource CreateSource(GameObject go, AudioMixerGroup mg)
	{
		AudioSource source = go.AddComponent<AudioSource>() as AudioSource;
		source.outputAudioMixerGroup = mg;
		return source;
	}

	private static double Duration(AudioClip clip)
	{
		return (double) clip.samples / clip.frequency;
	}

    void Start()
    {
    	// Create AudioSources to play clips
    	sources = new AudioSource[2];
    	sources[0] = CreateSource(gameObject, output);
    	sources[1] = CreateSource(gameObject, output);

    	// Start playing the intro audio clip
    	startTime = AudioSettings.dspTime + 0.1;
    	sources[0].clip = intro;
    	sources[0].PlayScheduled(startTime);
    	startTime += Duration(intro);
    }

    void Update()
    {
    	// Wait until the next clip will start playing in the next second
    	if (AudioSettings.dspTime > startTime - 1)
    	{
    		AudioSource nextSource = sources[(playing ? 1 : 0)];
    		nextSource.clip = loop;
    		nextSource.PlayScheduled(startTime);
    		startTime += Duration(loop);
    		playing = !playing;
    	}
    }
}
