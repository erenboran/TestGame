using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSaw : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0, -3f, 0);
    }
}
