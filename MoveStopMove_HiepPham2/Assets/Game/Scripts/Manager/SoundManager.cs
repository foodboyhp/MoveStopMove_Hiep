using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicState {MainMenu, GamePlay}
public enum SoundEffectState {
    Normal,
    Coin,
    Shoot
}
public class SoundManager : Singleton<SoundManager>
{

    public AudioSource musicSource;
    public AudioSource soundEffectSource;
    public AudioClip[] musicClips;
    public AudioClip[] soundEffectClips;
    private MusicState musicState;
    private SoundEffectState soundEffectState;

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
    }
    public void TurnOnOffMusic(){
        if(this.musicSource.volume > 0.0f){
            this.musicSource.volume = 0.0f;
        } else if(this.musicSource.volume < 0.1f) {
            this.musicSource.volume = 1.0f;
        }
    }
    public void TurnOnOffSoundEffect(){
        if(this.soundEffectSource.volume > 0.0f){
            this.soundEffectSource.volume = 0.0f;
        } else if(this.soundEffectSource.volume < 0.1f) {
            this.soundEffectSource.volume = 1.0f;
        }
    }
}
