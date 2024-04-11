using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UISkinShop : UICanvas
{
    public enum ShopType { Hat, Pant, Accessory, Skin, Weapon }
    [SerializeField] private ShopData data;
    [SerializeField] private ShopItem prefab;
    [SerializeField] private Transform content;
    [SerializeField] private ShopBar[] shopBars;
    [SerializeField] private TextMeshProUGUI playerCoinText;
    [SerializeField] private ButtonState buttonState;
    [SerializeField] private Text coinText;
    [SerializeField] private Text adsText;

    [SerializeField] private ShopItem prevItem;
    [SerializeField] private ShopItem currentItem;
    MiniPool<ShopItem> miniPool = new MiniPool<ShopItem>();
    private ShopItem itemEquiped;
    private ShopBar currentBar;
    public ShopType shopType => currentBar.Type;

    private void Awake()
    {
        miniPool.OnInit(prefab, 10, content);

        for (int i = 0; i < shopBars.Length; i++)
        {
            shopBars[i].SetShop(this);
        }
    }

    public override void Open()
    {
        base.Open();
        SelectBar(shopBars[0]);
        CameraFollow.Ins.ChangeState(CameraFollow.State.Shop);
        playerCoinText.SetText(UserData.Ins.GetDataState(UserData.Key_Coin).ToString());
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
        UIManager.Ins.OpenUI<UIMainMenu>();

        LevelManager.Ins.player.TakeOffClothes();
        LevelManager.Ins.player.OnTakeClothsData();
        LevelManager.Ins.player.WearClothes();
    }

    internal void SelectBar(ShopBar shopBar)
    {
        if (currentBar != null)
        {
            currentBar.ToggleActive(false);
        }

        currentBar = shopBar;
        currentBar.ToggleActive(true);

        miniPool.Collect();
        itemEquiped = null;

        switch (currentBar.Type)
        {
            case ShopType.Hat:
                InitShipItems(data.hats.Ts, ref itemEquiped);
                break;
            case ShopType.Pant:
                InitShipItems(data.pants.Ts, ref itemEquiped);
                break;
            case ShopType.Accessory:
                InitShipItems(data.accessories.Ts, ref itemEquiped);
                break;
            case ShopType.Skin:
                InitShipItems(data.skins.Ts, ref itemEquiped);
                break;
            default:
                break;
        }

    }

    private void InitShipItems<T>(List<ShopItemData<T>> items, ref ShopItem itemEquiped)  where T : Enum
    {
        for (int i = 0; i < items.Count; i++)
        {
            ShopItem.State state = UserData.Ins.GetEnumData(items[i].type.ToString(), ShopItem.State.Buy);
            ShopItem item = miniPool.Spawn();
            item.SetData(i, items[i], this);
            item.SetState(state);
            
            if (state == ShopItem.State.Equipped)
            {
                SelectItem(item);
                itemEquiped = item;
            }

        }
    }

    public ShopItem.State GetState(Enum t) => UserData.Ins.GetEnumData(t.ToString(), ShopItem.State.Buy);

    internal void SelectItem(ShopItem item)
    {
        if (currentItem != null)
        {
            currentItem.SetState(GetState(currentItem.Type));
        }
        prevItem = currentItem;
        currentItem = item;

        switch (currentItem.state)
        {
            case ShopItem.State.Buy:
                buttonState.SetState(ButtonState.State.Buy);
                break;
            case ShopItem.State.Bought:
                buttonState.SetState(ButtonState.State.Equip);
                break;
            case ShopItem.State.Equipped:
                buttonState.SetState(ButtonState.State.Equiped);
                break;
            case ShopItem.State.Selecting:
                break;
            default:
                break;
        }

        LevelManager.Ins.player.TryCloth(shopType, currentItem.Type);
        currentItem.SetState(ShopItem.State.Selecting);

        //check data
        coinText.text = item.data.cost.ToString();
        adsText.text = item.data.ads.ToString();
    }

    public void BuyButton()
    {
        //TODO: check xem du tien hay k
        if(UserData.Ins.GetDataState(UserData.Key_Coin) >= currentItem.data.cost){
            UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Bought);
            UserData.Ins.SetDataState(UserData.Key_Coin, UserData.Ins.GetDataState(UserData.Key_Coin) - currentItem.data.cost);
            playerCoinText.SetText(UserData.Ins.GetDataState(UserData.Key_Coin).ToString());
            buttonState.SetState(ButtonState.State.Equip);
        }
    }

    public void AdsButton()
    {

    }

    public void EquipButton()
    {
        if (currentItem != null)
        {
            if(prevItem != null){
                if(prevItem.state == ShopItem.State.Equipped){
                    prevItem.SetState(ShopItem.State.Bought);
                }
            }
            UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Equipped);

            switch (shopType)
            {
                case ShopType.Hat:
                    UserData.Ins.SetEnumData(UserData.Ins.playerHat.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Hat, ref UserData.Ins.playerHat, (HatType)currentItem.Type);
                    break;
                case ShopType.Pant:
                    UserData.Ins.SetEnumData(UserData.Ins.playerPant.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Pant, ref UserData.Ins.playerPant, (PantType)currentItem.Type);
                    break;
                case ShopType.Accessory:
                    UserData.Ins.SetEnumData(UserData.Ins.playerAccessory.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Accessory, ref UserData.Ins.playerAccessory, (AccessoryType)currentItem.Type);
                    break;
                case ShopType.Skin:
                    UserData.Ins.SetEnumData(UserData.Ins.playerSkin.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Skin, ref UserData.Ins.playerSkin, (SkinType)currentItem.Type);
                    break;
                case ShopType.Weapon:
                    break;
                default:
                    break;
            }
  
        }

        if (itemEquiped != null)
        {
            itemEquiped.SetState(ShopItem.State.Bought);
        }

        currentItem.SetState(ShopItem.State.Equipped);
        SelectItem(currentItem);
    }

}
