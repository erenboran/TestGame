using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmeltMachine : MonoBehaviour
{
    [SerializeField]
    GameObject tank;


    private void Start()
    {
        StartCoroutine(RotateTank());
    }

    IEnumerator RotateTank()
    {
        tank.transform.DOLocalRotate(new Vector3(-32.787f, 0, 0), 0.3f);

        yield return new WaitForSeconds(1.3f);


        tank.transform.DOLocalRotate(new Vector3(270, 0, 0), 0.3f); 

        
        tank.transform.DOLocalRotate(new Vector3(240, 0, 0), 0.9f).OnComplete(()=> tank.transform.DOLocalRotate(new Vector3(270, 0, 0), 0.9f));

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(RotateTank());
    }
  
}
