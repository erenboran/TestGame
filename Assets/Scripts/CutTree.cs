using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CutTree : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    GameObject logPrefab;

    public void Cut()
    {
        transform.DOShakePosition(0.125f, 0.125f);
        transform.DOShakeRotation(0.125f, 0.125f);
        transform.DOShakeScale(0.125f, 0.125f);

        Instantiate(logPrefab,spawnPoint.position,spawnPoint.rotation);

    }
}
