using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    private const string ANIM_OPEN = "Open";
    private const string ANIM_CLOSE = "Close";
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Animation anim;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
        coinText.SetText(UserData.Ins.GetDataState(UserData.Key_Coin).ToString());
        anim.Play(ANIM_OPEN);
    }

    public void SkinShopButton()
    {
        UIManager.Ins.OpenUI<UISkinShop>();
        Close(0);
    }


    public void WeaponShopButton()
    {
        UIManager.Ins.OpenUI<UIWeaponShop>();
        Close(0);
    }

    public void PlayButton()
    {
        LevelManager.Ins.Play();
        UIManager.Ins.OpenUI<UIGameplay>();
        CameraFollow.Ins.ChangeState(CameraFollow.State.Gameplay);
        anim.Play(ANIM_CLOSE);
        Close(0.5f);
    }
}

