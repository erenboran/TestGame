using System.Collections;
using System    .Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoShake : MonoBehaviour
{

    public GameObject kalp;

    void Start()
    {
        kalp.transform.DOShakeScale(1, 1, 1, 1).SetLoops(-1,LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
