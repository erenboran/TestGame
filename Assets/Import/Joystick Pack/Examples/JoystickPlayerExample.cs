using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    public Vector3 direction;
    [SerializeField]
    FireManager fireManager;

    public float prevSpeed = 250;


    private void Start()
    {
        
    }
    public void FixedUpdate()
    {
        direction = (Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal).normalized;

        rb.velocity = (direction * speed * Time.fixedDeltaTime);

        if (direction != Vector3.zero)
        {
            if (!fireManager.isActiceFire)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
            }


        }

        if (fireManager.isRageSkill)
        {
            speed = 100;
        }
        else
        {
            speed = prevSpeed;
        }

        if (fireManager.isForceSkill)
        {
            speed = 0;
        }
        else
        {
            speed = prevSpeed;

        }
    }
}