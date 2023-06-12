using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Log : MonoBehaviour
{
    private void Update()
    {
      
    }

    public void MovePlayer(float time,Vector3 targetPoint)
    {

        if (transform.position != targetPoint)
        {
            transform.DOLocalMove(new Vector3(0,0,0), time).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (this != null)
                {
                    time -= 0.075f;
                    MovePlayer(time,targetPoint);
                }

            });

        }
        else
        {
            Debug.Log(targetPoint);
        }

       
   


    }



}
