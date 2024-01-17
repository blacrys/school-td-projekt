using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;
using UnityEngine;


public class BuldingManager: MonoBehaviour
{
    public Tile TurretTile;
    public Tile PlatformTile;
    public Tilemap Map;
    public Camera Camera;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Turret turretToBuild = BuildManager.Main.GetSelectedTurret();
            
            var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            var gridPosition = Map.WorldToCell(mousePosition);
            var tile = Map.GetTile(gridPosition);
            if (tile == PlatformTile)
            {
                if(turretToBuild.cost > CurrencyManager.Main.currency)
                {
                    Debug.Log("You do not have enough money!");
                    return;
                }
                CurrencyManager.Main.SpendCurrency(turretToBuild.cost);
                var tilePosition = Map.GetCellCenterWorld(gridPosition);
                var turretPosition = tilePosition - new Vector3(0.125f, 0.125f, 0.1f);
                Instantiate(turretToBuild.prefab, turretPosition, Quaternion.identity);
                Map.SetTile(gridPosition, TurretTile);
            }
            else if (tile == TurretTile)
            {
                //get area of TurretTile, find GameObject in that area and destroy it
                var tilePosition = Map.GetCellCenterWorld(gridPosition);
                var turretPosition = tilePosition - new Vector3(0.125f, 0.125f, 0.1f);
                var turret = GameObject.FindGameObjectsWithTag("Turret");
                foreach (var t in turret)
                {
                    if (t.transform.position == turretPosition)
                    {
                        Destroy(t);
                    }
                }
                Map.SetTile(gridPosition, PlatformTile);
            }
        }
    }
}