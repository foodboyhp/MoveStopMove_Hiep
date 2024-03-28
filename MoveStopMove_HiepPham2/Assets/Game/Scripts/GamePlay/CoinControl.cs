using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinControl : MonoBehaviour {
    
    [SerializeField] public static int coin_amout = 0;
    public void Start(){
        coin_amout = PlayerPrefs.GetInt("coin_amout");
        Debug.Log("Coin: " + coin_amout);
    }
    public static void AddCoin(int amount){
        coin_amout += amount;
        PlayerPrefs.SetInt("coin_amout", coin_amout);
        Debug.Log("Coin: " + coin_amout);
    }

    public static void BuyWithCoin(int amount){
        if(coin_amout < amount){
            Debug.Log("Can't buy with coin");
        } else {
            AddCoin(amount * -1);
        }
        Debug.Log("Coin: " + coin_amout);
    }
}
