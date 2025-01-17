using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource soundsAudioSource;

    public void ChangeMusic(AudioClip musicClip)
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        soundsAudioSource.PlayOneShot(sound);
    }

    public IEnumerator PlayMusicAfterSound(AudioClip clip, AudioClip music)
    {
        musicAudioSource.Pause();
        PlaySound(clip);
        yield return new WaitForSeconds(clip.length);
        ChangeMusic(music);
    }

    public void ToggleSoundMute()
    {
        if (soundsAudioSource.volume == 0)
        {
            soundsAudioSource.volume = 1;
        }
        else
        {
            soundsAudioSource.volume = 0;
        }
    }

    public void ToggleMusicMute()
    {
        if (musicAudioSource.volume == 0)
        {
            musicAudioSource.volume = 0.5f;
        }
        else
        {
            musicAudioSource.volume = 0;
        }
    }
}
