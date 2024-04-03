using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    private static UserData ins;
    public static UserData Ins
    {
        get
        {
            if (ins == null)
            {
                UserData[] datas = Resources.LoadAll<UserData>("");

                if (datas.Length == 1)
                {
                    ins = datas[0];
                }
                else {
                    Debug.Log("there can be only one UserData");
                }
            }

            return ins;
        }
    } 
    public const string Key_Level = "Level";
    public const string Key_Coin = "Coin";
    public const string Key_Player_Weapon = "PlayerWeapon";
    public const string Key_Player_Hat = "PlayerHat";
    public const string Key_Player_Pant = "PlayerPant";
    public const string Key_Player_Accessory = "PlayerAccessory";
    public const string Key_Player_Skin = "PlayerSkin";

    public const string Keys_Weapon_Data = "WeaponDatas";
    public const string Keys_Hat_Data = "HatDatas";
    public const string Keys_Pant_Data = "PantDatas";
    public const string Keys_Accessory_Data = "AccessoryDatas";
    public const string Keys_Skin_Data = "SkinDatas";

    public int level = 0;
    public int coin = 0;
    public WeaponType playerWeapon;
    public HatType playerHat;
    public PantType playerPant;
    public AccessoryType playerAccessory;
    public SkinType playerSkin;

    public void SetDataState(string key, int ID, int state)
    {
        PlayerPrefs.SetInt(key + ID, state);
    }
    public int GetDataState(string key, int ID, int state = 0)
    {
        return PlayerPrefs.GetInt(key + ID, state);
    }

    public void SetDataState(string key, int state)
    {
        PlayerPrefs.SetInt(key, state);
    }

    public int GetDataState(string key, int state = 0)
    {
        return PlayerPrefs.GetInt(key, state);
    }
    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value);
    } 
    
    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(key, value);
    }

    public void SetEnumData<T>(string key, ref T variable, T value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public void SetEnumData<T>(string key,T value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public T GetEnumData<T>(string key, T defaultValue) where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }
    
    public void OnInitData()
    {
        level = PlayerPrefs.GetInt(Key_Level, 0);
        coin = PlayerPrefs.GetInt(Key_Coin, 0);

        playerWeapon = GetEnumData(Key_Player_Weapon, WeaponType.W_Hammer_1);
        playerHat = GetEnumData(Key_Player_Hat, HatType.HAT_Arrow);
        playerPant = GetEnumData(Key_Player_Pant, PantType.Pant_1);
        playerAccessory = GetEnumData(Key_Player_Accessory, AccessoryType.ACC_None);
        playerSkin = GetEnumData(Key_Player_Skin, SkinType.SKIN_Normal);
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }

}