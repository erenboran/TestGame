using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents instance;

    public static event Action<int> OnWallTakeHit;
    public static event Action<Vector3> OnRepairWall;

    [SerializeField] Transform[] spawnPoints;

    private void OnEnable()
    {
        instance = this;
    }

    public void HitWall(int damage)
    {
        OnWallTakeHit?.Invoke(damage);
    }
    public void RepairWall(Vector3 spawnPoint)
    {
        OnRepairWall?.Invoke(spawnPoint);

    }
}
