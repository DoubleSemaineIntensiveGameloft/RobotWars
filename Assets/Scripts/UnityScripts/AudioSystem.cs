using UnityEngine;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    public static AudioSystem instance;

    public AudioSource[] audioSources;

    public AudioClip[] audioClips;

	// Use this for initialization
	void Start () {

        instance = this;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAudio(int audioToPlay, float pitch = 1)
    {
        foreach (AudioSource actAudioSource in audioSources)
        {
            if(!actAudioSource.isPlaying)
            {
                actAudioSource.clip = audioClips[audioToPlay];
                actAudioSource.pitch = pitch;
                actAudioSource.Play();
                break;
            }
        }
    }
}
