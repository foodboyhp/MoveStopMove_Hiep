using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicState {
    MainMenu = 0, 
    GamePlay = 1
}
public enum SoundEffectState {
    Normal,
    Coin,
    Shoot,
    Button,
    Victory,
    Fail
}
public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] soundEffectClips;
    public MusicState musicState;
    public SoundEffectState soundEffectState;
    public bool isMusicOn = true;
    public bool isEffectOn = true;

    public void Start(){
        OnInit();   
    }

    private void OnInit(){
        musicState = MusicState.MainMenu;
        soundEffectState = SoundEffectState.Normal;
        musicSource.clip = musicClips[(int)musicState];
        musicSource.Play();
        soundEffectSource.clip = soundEffectClips[(int)soundEffectState];
    }

    public void PlaySoundEffect(SoundEffectState seState){
        this.soundEffectState = seState;
        this.soundEffectSource.clip = soundEffectClips[(int)soundEffectState];
        soundEffectSource.Play();
    }
    public void ChangeMusic(MusicState musicState){
        this.musicState = musicState;
        this.musicSource.clip = musicClips[(int)musicState];
        musicSource.Play();
    }
    public void TurnOnOffMusic(){
        if(this.musicSource.volume > 0.0f){
            isMusicOn = false;
            this.musicSource.volume = 0.0f;
        } else if(this.musicSource.volume < 0.1f) {
            isMusicOn = true;
            this.musicSource.volume = 1.0f;
        }
    }
    public void TurnOnOffSoundEffect(){
        if(this.soundEffectSource.volume > 0.0f){
            isEffectOn = false;
            this.soundEffectSource.volume = 0.0f;
        } else if(this.soundEffectSource.volume < 0.1f) {
            isEffectOn = true;
            this.soundEffectSource.volume = 1.0f;
        }
    }
}
