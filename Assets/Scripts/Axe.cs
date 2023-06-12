using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().TakeHit(0.20f);

        }
    }
}
