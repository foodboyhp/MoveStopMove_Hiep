using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinText;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
        SetCoin(UnityEngine.Random.Range(30,50));

    }

    public void x3Button()
    {
        UserData.Ins.SetDataState(UserData.Key_Coin, UserData.Ins.GetDataState(UserData.Key_Coin) + this.coin * 3);
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

    }

    public void NextAreaButton()
    {
        UserData.Ins.SetDataState(UserData.Key_Coin, UserData.Ins.GetDataState(UserData.Key_Coin) + this.coin);
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();
    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinText.SetText(coin.ToString());
    }
}
