using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CurrencyManager))]
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Main;
    
    public int currency;

    private void Start()
    {
        Main = GetComponent<CurrencyManager>();
    }

    public void EarnCurrency(int amount)
    {
        currency += amount;
        Debug.Log("You have " + currency + " money!");
    }
    
    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            // buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough money!");
        }
        return false;
    }
}
