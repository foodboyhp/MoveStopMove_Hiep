using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UISkinShop;
using static UnityEditor.LightingExplorerTableColumn;

public class ShopItem : MonoBehaviour
{
    public enum State { Buy, Bought, Equipped, Selecting }

    [SerializeField] private GameObject[] stateObjects;
    [SerializeField] private Color[] colorBg;
    [SerializeField] private Image icon;
    [SerializeField] private Image bgIcon;
    private UISkinShop shop;
    private int id;
    public Enum Type;
    public State state;
    internal ShopItemData data;

    public void SetShop(UISkinShop shop)
    {
        this.shop = shop;
    }

    public void SetData<T>(int id, ShopItemData<T> itemData, UISkinShop shop) where T : Enum
    {
        this.id = id;
        this.Type = itemData.type;
        this.data = itemData;
        this.shop = shop;
        icon.sprite = itemData.icon;
        bgIcon.color = colorBg[(int)shop.shopType];
    }

    public void SelectButton()
    {
        shop.SelectItem(this);
    }

    public void SetState(State state)
    {
        for (int i = 0; i < stateObjects.Length; i++)
        {
            stateObjects[i].SetActive(false);
        }

        stateObjects[(int)state].SetActive(true);

        this.state = state;
    }

}
