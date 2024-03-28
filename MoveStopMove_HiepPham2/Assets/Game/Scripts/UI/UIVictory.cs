using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] RectTransform x3Point;
    [SerializeField] RectTransform mainMenuPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void x3Button()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

        // UIVfx.Ins.AddCoin(9, coin * 3, x3Point.position, UIVfx.Ins.CoinPoint);
        CoinControl.AddCoin(30);
    }

    public void NextAreaButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

        // UIVfx.Ins.AddCoin(3, coin, mainMenuPoint.position, UIVfx.Ins.CoinPoint);
        CoinControl.AddCoin(10);
    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.SetText(coin.ToString());
    }
}
