using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{

    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject musicOff;
    [SerializeField] private GameObject effectOn;
    [SerializeField] private GameObject effectOff;

    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Setting);
        UIManager.Ins.CloseUI<UIGameplay>();
        Time.timeScale = 0.0f;
    }

    public void MusicToggle()
    {
        musicOn.SetActive(!SoundManager.Ins.isMusicOn);
        musicOff.SetActive(SoundManager.Ins.isMusicOn);
        SoundManager.Ins.TurnOnOffMusic();
        SoundManager.Ins.PlaySoundEffect(SoundEffectState.Button);
    }

    public void SoundEffectToggle()
    {
        effectOn.SetActive(!SoundManager.Ins.isEffectOn);
        effectOff.SetActive(SoundManager.Ins.isEffectOn);
        SoundManager.Ins.TurnOnOffSoundEffect();
        SoundManager.Ins.PlaySoundEffect(SoundEffectState.Button);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1.0f;
        GameManager.Ins.ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<UIGameplay>();
        Close(0);
    }

    public void HomeButton()
    {
        Time.timeScale = 1.0f;
        LevelManager.Ins.Home();
    }
}
