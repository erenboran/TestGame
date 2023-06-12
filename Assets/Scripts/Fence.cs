using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fence : MonoBehaviour
{
    public void MovePlayer(float time, Vector3 targetPoint)
    {

        transform.DOLocalMove(new Vector3(0, 0, 0), time).SetEase(Ease.Linear).OnComplete(() => 
        {
            ResourceManager.instance.fenceCount++;
            Destroy(gameObject);
        });
        
            
        





    }
}
