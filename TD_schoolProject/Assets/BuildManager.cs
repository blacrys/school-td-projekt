using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Main;

    [Header("References")]
    [SerializeField] private Turret[] turrets;

    
    private int selectedTurret;

    private void Awake()
    {
        Main = this;
    }
    
    public Turret GetSelectedTurret()
    {
        return turrets[selectedTurret];
    }
    
    public void SetSelectedTurret(int _selectedTurret)
    {
        if (CurrencyManager.Main.SpendCurrency(turrets[_selectedTurret].cost))
        {
            selectedTurret = _selectedTurret;
        }
    }
}
