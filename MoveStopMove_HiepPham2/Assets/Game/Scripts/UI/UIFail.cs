using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Random;

public class UIFail : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI rankText;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
        SetCoin(UnityEngine.Random.Range(10,30));
    }

    public void x3Button()
    {
        UserData.Ins.SetDataState(UserData.Key_Coin, UserData.Ins.GetDataState(UserData.Key_Coin) + this.coin * 3);
        LevelManager.Ins.Home();
    }

    public void MainMenuButton()
    {
        UserData.Ins.SetDataState(UserData.Key_Coin, UserData.Ins.GetDataState(UserData.Key_Coin) + this.coin);
        LevelManager.Ins.Home();
    }


    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinText.SetText(coin.ToString());
    }

    internal void SetRank(int rank){
        rankText.SetText("#" + rank.ToString());
    }
}
