using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLookAt : MonoBehaviour
{
    [SerializeField]
    EnemyFinder enemyFinder;
    private void LateUpdate()
    {
        if (enemyFinder.enemiesInRange.Count > 0)
        {
            Vector3 lTargetDir = enemyFinder.enemiesInRange[0].transform.position - transform.position;
            lTargetDir.y = 0.0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.25f);


        }
            
    }
}
