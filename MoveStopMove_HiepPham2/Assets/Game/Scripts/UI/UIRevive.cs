using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRevive : UICanvas
{
    [SerializeField] TextMeshProUGUI counterText;
    private float counter;

    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Revive);
        counter = 5;
    }

    private void Update()
    {
        while (counter > 0) {
            counter -= Time.deltaTime;
            counterText.SetText(counter.ToString("F0"));
        }
        if (counter <= 0.01f)
        {
            CloseButton();
        }
    }

    public void ReviveButton()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Close(0);
        LevelManager.Ins.Revive();
        UIManager.Ins.OpenUI<UIGameplay>();
    }

    public void CloseButton()
    {
        Close(0);
        LevelManager.Ins.Fail();
    }
}
