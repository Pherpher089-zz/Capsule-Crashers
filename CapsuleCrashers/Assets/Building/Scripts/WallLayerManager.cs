using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallState {Perfect, Damaged, Destroyed}

[RequireComponent(typeof(WallLayerHealthManager))]
/// <summary>
/// This script will manage the state of the wall layer, switching on all the components when built and turning them off whe destroyed
/// </summary>
public class WallLayerManager : MonoBehaviour {
    WallLayerHealthManager wallHealth;
    public int requiredWallLevel;
    public int requiredMaterial;
    public WallType wallType;
    public WallState wallState;

    private void Awake()
    {
        wallHealth = GetComponent<WallLayerHealthManager>();
    }

    private void Update()
    {
       if(wallHealth.health == wallHealth.maxHealth)
        {
            wallState = WallState.Perfect;
        }
       else if(wallHealth.health > 0)
        {
            wallState = WallState.Damaged;
        }
       else
        {
            wallState = WallState.Destroyed;
        }
    }
}
