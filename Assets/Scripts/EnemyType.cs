using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyType")]
public class EnemyType : ScriptableObject
{
    public string enemyName;

    public float health;

    public float speed;

    public int damage;

    public int award;

    public float spawnInterval;

    public float priority;

    public int startDay;








}
