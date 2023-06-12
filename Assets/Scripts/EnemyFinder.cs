using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public List<GameObject> enemiesInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>()!=null && other.GetComponent<EnemyController>().health >0)
        {

            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
