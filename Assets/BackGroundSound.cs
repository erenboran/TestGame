using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSound : MonoBehaviour
{
    public static BackGroundSound istance;

    [HideInInspector]
    public AudioSource audioSource;
    private void Awake()
    {
        istance = this;
        audioSource = GetComponent<AudioSource>();
    }


}
