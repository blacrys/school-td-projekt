using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMennuOpen = true;

    public void ToggleMenu ()
    {
        isMennuOpen = !isMennuOpen;
        anim.SetBool("MenuOpen", isMennuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = CurrencyManager.Main.currency.ToString();
        waveUI.text = "Wave " + EnemySpawner.Main.currentWave.ToString();
    }
}