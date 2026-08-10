using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    const float FADE_TIME_SECONDS = 5;
    public static AudioManager instance;
    private AudioSource musicChannel;
    private AudioSource SFXChannel;
    [System.Serializable]public struct SoundClip
    {
        public string soundName;
        public AudioClip soundClip;
    }
    public List <SoundClip> SoundTrack = new List <SoundClip>();
    public List <SoundClip> SoundEffects = new List <SoundClip>();
    
    private bool playActive = true;
    private bool SFXActive = true;

    [SerializeField] private Slider btnVolume;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SFXChannel = transform.GetChild(0).GetComponent<AudioSource>();
        musicChannel = transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void PlayMusic(string musicName)
    {
        bool HasFoundMusic = false;
        int musicIndex = -1;
        for (int i=0; i< SoundTrack.Count; i++)
        {
            if (SoundTrack[i].soundName == musicName)
            {
                HasFoundMusic = true;
                musicIndex = i;
            }
        }
        if (HasFoundMusic == true)
        {
            musicChannel.clip = SoundTrack[musicIndex].soundClip;
            if(playActive == true){
                musicChannel.loop = true;
                musicChannel.Play();
            }           
        }
        else
        {
            Debug.Log("Audio not found");
        }
    }
    public void PlaySFX(string musicName)
    {
        bool HasFoundMusic = false;
        int musicIndex = -1;
        for (int i=0; i< SoundEffects.Count; i++)
        {
            if (SoundEffects[i].soundName == musicName)
            {
                HasFoundMusic = true;
                musicIndex = i;
            }
        }
        if (HasFoundMusic == true)
        {
            SFXChannel.PlayOneShot(SoundEffects[musicIndex].soundClip);
        }
        else
        {
            Debug.Log("Audio not found");
        }
    }

    
    public void ButtonMusicPressed()
    {        
        if(musicChannel.isPlaying)
        {
            musicChannel.Pause();
            playActive = false;            
        }
        else
        {
            musicChannel.Play();
            playActive = true;            
        }
    }

    
     public void ButtonSFXPressed()
    {        
        if(SFXActive == true)
        {            
            SFXActive = false;            
        }
        else
        {
            SFXActive = true;            
        }
    }

    
    public void VolumeChange(float newVolume)
    {              
        
        musicChannel.volume = newVolume;
        SFXChannel.volume = newVolume;
    }      
}
