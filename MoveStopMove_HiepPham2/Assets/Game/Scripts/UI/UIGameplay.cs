using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    public TextMeshProUGUI characterAmountText;

    public override void Setup()
    {
        base.Setup();
        UpdateTotalCharacter();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        LevelManager.Ins.SetTargetIndicatorAlpha(1);
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
        LevelManager.Ins.SetTargetIndicatorAlpha(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<UISetting>();
    }

    public void UpdateTotalCharacter()
    {
        characterAmountText.text = LevelManager.Ins.TotalCharacter.ToString();
    }
}
