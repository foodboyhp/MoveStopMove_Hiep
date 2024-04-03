using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBar : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private UISkinShop.ShopType type;
    private UISkinShop shop;
    public UISkinShop.ShopType Type => type;
    public void SetShop(UISkinShop shop)
    {
        this.shop = shop;
    }

    public void Select()
    {
        shop.SelectBar(this);
    }

    public void ToggleActive(bool active)
    {
        background.SetActive(!active);
    }
}
