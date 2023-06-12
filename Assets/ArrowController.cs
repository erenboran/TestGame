using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ArrowController : MonoBehaviour
{
    public Transform ArrowPoint;
    public GameObject biz;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(ArrowPoint.position, 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
