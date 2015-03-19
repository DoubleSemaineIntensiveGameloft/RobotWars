using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioSystem : MonoBehaviour
{

    public static AudioSystem instance;

    public float volume = 1.0f;
    public AudioSource[] audioSources;
    public AudioClip[] audioClips;

    void Start()
    {

        instance = this;

    }

    void Update()
    {

    }

    public void setVolume(float volume)
    {
        this.volume = Mathf.Clamp(volume, 0.0f, 1.0f);
    }

    public void setVolume(Slider slider)
    {
        this.setVolume(slider.value);
    }

    public void PlayAudio(int audioToPlay, float volume, float pitch)
    {
        foreach (AudioSource actAudioSource in audioSources)
        {
            if (!actAudioSource.isPlaying)
            {
                actAudioSource.clip = audioClips[audioToPlay];
                actAudioSource.pitch = pitch;
                actAudioSource.volume = volume;
                actAudioSource.Play();
                break;
            }
        }
    }

    public void PlayAudio(int audioToPlay, float volume)
    {
        this.PlayAudio(audioToPlay, volume, 1);
    }

    public void PlayAudio(int audioToPlay)
    {
        this.PlayAudio(audioToPlay, this.volume);
    }
}
