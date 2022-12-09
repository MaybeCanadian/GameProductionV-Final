using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<AudioClip> musicClips;
    public List<AudioClip> FXClips;

    public AudioSource MusicSource;
    public AudioSource FXSource;

    bool shuffling = false;

    //------------------------------------------
    //Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Initialize();
    }
    private void Initialize()
    {
        LoadMusicClips();
        LoadSoundClips();
    }
    private void LoadMusicClips()
    {
        musicClips = new List<AudioClip>();

        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Deceptive Skies"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Desolation"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Empty Lands"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Fly, you fools"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Haunted"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Hollow Hope"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Left Behind"));
        musicClips.Add(Resources.Load<AudioClip>("Audio/Music/Midnight Dreams"));
    }
    private void LoadSoundClips()
    {
        FXClips = new List<AudioClip>();

        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/ArrowHit1"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/ArrowHit2"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/ArrowHit3"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/ArrowHit4"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/ArrowHit5"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/ArrowHitWood"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/BallistaBreak"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitEnviorment"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitSword1"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitSword2"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitSword3"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitSword4"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitSword5"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitWall1"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitWall2"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitWall3"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitWall4"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/HitWood"));
        FXClips.Add(Resources.Load<AudioClip>("Audio/Effects/Mining"));
    }
    //------------------------------------------
    
    //------------------------------------------
    //Music Functions
    public void PlayMusic(SongList track, float relativeVolume, bool loop)
    {
        if(shuffling)
        {
            StopCoroutine(WaitForSongFinish(0.0f));
            shuffling = false;
        }

        MusicSource.clip = musicClips[(int)track];
        MusicSource.volume = relativeVolume;
        MusicSource.loop = loop;
        MusicSource.Play();
    }
    public void PlayMusic(float relativeVolume, bool loop)
    {
        MusicSource.clip = musicClips[Random.Range(0, musicClips.Count)];
        MusicSource.volume = relativeVolume;
        MusicSource.loop = loop;
        MusicSource.Play();
    }
    public void ShuffleAllSongs(float relativeVolume)
    {
        shuffling = true;
        StartCoroutine(WaitForSongFinish(relativeVolume));
    }

    private IEnumerator WaitForSongFinish(float relativeVolume)
    {
        MusicSource.volume = relativeVolume;

        while (shuffling)
        {
            if(!MusicSource.isPlaying)
            {
                MusicSource.clip = musicClips[Random.Range(0, musicClips.Count)];
                MusicSource.Play();
            }

            yield return null;
        }

        yield return null;
    }
    public void PauseMusic()
    {
        if(MusicSource.clip != null && MusicSource.isPlaying)
        {
            MusicSource.Pause();
        }
    }
    public void ResumeMusic()
    {
        if(MusicSource.clip != null && !MusicSource.isPlaying)
        {
            MusicSource.Play();
        }
    }
    public void StopMusic()
    {
        if(MusicSource.isPlaying)
        {
            MusicSource.Stop();
        }
    }
    //------------------------------------------
    //Effect Functions
    public void PlayFX(EffectList effect, float relativeVolume)
    {
        FXSource.PlayOneShot(FXClips[(int)effect], relativeVolume);
    }
    public AudioClip GetFXClip(EffectList effect)
    {
        return FXClips[(int)effect];
    } 
    //------------------------------------------
}

[System.Serializable]
public enum EffectList
{
    Arrow_Hit_1,
    Arrow_Hit_2,
    Arrow_Hit_3,
    Arrow_Hit_4,
    Arrow_Hit_5,
    Arrow_Hit_Wood,
    Balista_Break,
    Hit_Enviorment,
    Hit_Sword_1,
    Hit_Sword_2,
    Hit_Sword_3,
    Hit_Sword_4,
    Hit_Sword_5,
    Hit_Wall_1,
    Hit_Wall_2,
    Hit_Wall_3,
    Hit_Wall_4,
    Hit_Wood,
    Mining
}

[System.Serializable]
public enum SongList
{
    Deceptive_Skies,
    Desolation,
    Empty_Lands,
    Fly_You_Fools,
    Haunted,
    Hollow_Hope,
    Left_Behind,
    Midnight_Dreams
}
