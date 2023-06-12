using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Transform turretPoint;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float speed;

    bool isActiveTurret;

    Transform tempPoint;


    public void TurretPosition(bool active)
    {
        if (active)
        {
            tempPoint = transform;
            transform.DOMove(turretPoint.position, 0.2f);
            transform.DORotateQuaternion(turretPoint.rotation, 0.2f);
            isActiveTurret = true;
        }
        else
        {
            transform.DOMove(tempPoint.position, 0.2f);
            transform.DORotateQuaternion(tempPoint.rotation, 0.2f);
            isActiveTurret = false;
        }
    
    }

    private void FixedUpdate()
    {
        if (!isActiveTurret)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);

        }

    }


}
