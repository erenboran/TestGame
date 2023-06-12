using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodCutterMachine : MonoBehaviour
{
    [SerializeField]
    Transform endPoint, spawnPoint;

    [SerializeField]
    GameObject fence;

    public Transform startPoint;


    public void LoadMachine(GameObject log)
    {
        log.transform.DOMove(spawnPoint.position, 1.25f).OnComplete(()=> 
        {
            Destroy(log);
            GameObject newObject = Instantiate(fence,spawnPoint.position,spawnPoint.rotation);
            newObject.transform.DOMove(endPoint.position, 1);
        
        });

    }
}
