using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapScript : MonoBehaviour
{
    public Player player;
    public enum TrapType
    {
        Ok,
        Trap
    }
    public TrapType trapType;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            if (trapType == TrapType.Trap)
            {
                player.health -= 0.05f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (trapType == TrapType.Ok)
            {
                player.health -= 3f;
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {
            if (trapType == TrapType.Ok)
            {
                player.health -= 3f;
            }
     
    }
}
